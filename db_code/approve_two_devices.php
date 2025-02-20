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

$query = "SELECT * FROM Users WHERE Password = ?";
$stmt = $conn->prepare($query);

if (!$stmt) {
    echo json_encode(['success' => false, 'message' => 'Database error: ' . $conn->error]);
    exit;
}

$stmt->bind_param('s', $password);
$stmt->execute();
$result = $stmt->get_result();

if ($result && $result->num_rows > 0) {
    $user = $result->fetch_assoc();

    // Update the SecondDeviceAllowed field to 1 (true) for the user
    $updateQuery = "UPDATE Users SET SecondDeviceAllowed = 1 WHERE Password = ?";
    $updateStmt = $conn->prepare($updateQuery);

    if (!$updateStmt) {
        echo json_encode(['success' => false, 'message' => 'Database error: ' . $conn->error]);
        exit;
    }

    $updateStmt->bind_param('s', $password);
    $updateStmt->execute();

    if ($updateStmt->affected_rows > 0) {
        echo json_encode(['success' => true, 'message' => 'User is now allowed to use two devices.']);
    } else {
        echo json_encode(['success' => false, 'message' => 'Korisnik već ima dozvolu za dva uređaja.']);
    }

    $updateStmt->close();
} else {
    echo json_encode(['success' => false, 'message' => 'Invalid password.']);
}

$stmt->close();
$conn->close();
ob_end_flush(); // End output buffering and send output
?>
