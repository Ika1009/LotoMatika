<?php
include('db_conn.php');

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
$stmt->bind_param('s', $password);
$stmt->execute();
$result = $stmt->get_result();

if ($result->num_rows > 0) {
    $user = $result->fetch_assoc();

    // Check if the user is allowed to use two devices before proceeding
    if ($user['SecondDeviceAllowed'] == 1) {
        // Update the SecondDevice field to NULL and remove the second device allowance
        $updateQuery = "UPDATE Users SET SecondDeviceAllowed = 0, SecondDeviceID = NULL WHERE Password = ?";
        $updateStmt = $conn->prepare($updateQuery);
        $updateStmt->bind_param('s', $password);
        $updateStmt->execute();

        if ($updateStmt->affected_rows > 0) {
            echo json_encode(['success' => true, 'message' => 'User\'s second device has been removed.']);
        } else {
            echo json_encode(['success' => false, 'message' => 'No changes were made, user might not have a second device assigned.']);
        }
    } else {
        echo json_encode(['success' => false, 'message' => 'User is not allowed to use two devices.']);
    }
} else {
    echo json_encode(['success' => false, 'message' => 'Invalid password.']);
}

$conn->close();
?>
