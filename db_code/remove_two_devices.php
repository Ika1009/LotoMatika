<?php
include('db_conn.php');

ob_start(); // Start output buffering to prevent "headers already sent"
header('Content-Type: application/json');

// Get the data sent via POST
$data = json_decode(file_get_contents('php://input'), true);
$password = $data['password'] ?? '';

if (empty($password)) {
    echo json_encode(['success' => false, 'message' => 'Password is required.']);
    exit;
}

$query = "SELECT UID, SecondDeviceAllowed, SecondDeviceID FROM Users WHERE Password = ?";
$stmt = $conn->prepare($query);
$stmt->bind_param('s', $password);
$stmt->execute();
$stmt->bind_result($UID, $SecondDeviceAllowed, $SecondDeviceID);
$stmt->fetch();
$stmt->close();

if (isset($UID)) {
    if ($SecondDeviceAllowed == 1) {
        // Update the SecondDevice field to NULL and remove the second device allowance
        $updateQuery = "UPDATE Users SET SecondDeviceAllowed = 0, SecondDeviceID = NULL WHERE Password = ?";
        $updateStmt = $conn->prepare($updateQuery);
        $updateStmt->bind_param('s', $password);
        $updateStmt->execute();

        if ($updateStmt->affected_rows > 0) {
            echo json_encode(['success' => true, 'message' => 'Korisniku je uklonjena mogućnost za dva korisnika.']);
        } else {
            echo json_encode(['success' => false, 'message' => 'No changes were made, user might not have a second device assigned.']);
        }
        $updateStmt->close();
    } else {
        echo json_encode(['success' => false, 'message' => 'Korisniku već nije dozvoljeno da koristi 2 uređaja.']);
    }
} else {
    echo json_encode(['success' => false, 'message' => 'Invalid password.']);
}

$conn->close();
ob_end_flush(); // End output buffering and send output
?>
