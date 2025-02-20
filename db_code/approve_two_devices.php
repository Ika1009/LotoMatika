<?php
include('db_conn.php');

ob_start(); // Prevents "headers already sent" issue
header('Content-Type: application/json');

$data = json_decode(file_get_contents('php://input'), true);
$password = $data['password'] ?? '';

if (empty($password)) {
    echo json_encode(['success' => false, 'message' => 'Password is required.']);
    exit;
}

$query = "SELECT UID, SecondDeviceAllowed FROM Users WHERE Password = ?";
$stmt = $conn->prepare($query);

if (!$stmt) {
    echo json_encode(['success' => false, 'message' => 'Database error: ' . $conn->error]);
    exit;
}

$stmt->bind_param('s', $password);
$stmt->execute();

// Fetch result using bind_result()
$stmt->bind_result($userId, $secondDeviceAllowed);
if ($stmt->fetch()) {
    $stmt->close(); // Close statement before updating

    // Check if SecondDeviceAllowed is already 1
    if ($secondDeviceAllowed == 1) {
        echo json_encode(['success' => false, 'message' => 'Korisnik već ima dozvolu za dva uređaja.']);
        exit;
    }

    // Update SecondDeviceAllowed to 1 (enable two devices)
    $updateQuery = "UPDATE Users SET SecondDeviceAllowed = 1 WHERE UID = ?";
    $updateStmt = $conn->prepare($updateQuery);

    if (!$updateStmt) {
        echo json_encode(['success' => false, 'message' => 'Database error: ' . $conn->error]);
        exit;
    }

    $updateStmt->bind_param('i', $userId);
    $updateStmt->execute();

    if ($updateStmt->affected_rows > 0) {
        echo json_encode(['success' => true, 'message' => 'User is now allowed to use two devices.']);
    } else {
        echo json_encode(['success' => false, 'message' => 'Failed to update permission.']);
    }

    $updateStmt->close();
} else {
    echo json_encode(['success' => false, 'message' => 'Invalid password.']);
}

$conn->close();
ob_end_flush(); // End output buffering and send output
?>
