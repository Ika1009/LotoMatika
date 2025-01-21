<?php
include('db_conn.php');

header('Content-Type: application/json');

// Get the data sent via POST
$data = json_decode(file_get_contents('php://input'), true);
$password = $data['password'] ?? '';
$deviceId = $data['deviceId'] ?? '';

if (empty($password) || empty($deviceId)) {
    echo json_encode(['success' => false, 'message' => 'Password and Device ID are required.']);
    exit;
}

$query = "SELECT * FROM Users WHERE Password = ?";
$stmt = $conn->prepare($query);
$stmt->bind_param('s', $password);
$stmt->execute();
$result = $stmt->get_result();

if ($result->num_rows > 0) {
    $user = $result->fetch_assoc();
    $response = [
        'success' => true,
        'message' => 'Login successful.',
        'isAdmin' => $user['isAdmin'] ?? false,
        'deviceId' => $user['DeviceID'],
        'secondDeviceAllowed' => $user['SecondDeviceAllowed'],
        'secondDeviceId' => $user['SecondDeviceID']
    ];

    // Check and update DeviceID and SecondDeviceID
    if (is_null($user['DeviceID'])) {
        // Update DeviceID if it's NULL
        $updateQuery = "UPDATE Users SET DeviceID = ? WHERE Password = ?";
        $updateStmt = $conn->prepare($updateQuery);
        $updateStmt->bind_param('ss', $deviceId, $password);
        $updateStmt->execute();
        $response['deviceId'] = $deviceId;
    } elseif ($user['DeviceID'] !== $deviceId) {
        if ($user['SecondDeviceAllowed'] == 1) {
            if (is_null($user['SecondDeviceID'])) {
                // Update SecondDeviceID if allowed and NULL
                $updateQuery = "UPDATE Users SET SecondDeviceID = ? WHERE Password = ?";
                $updateStmt = $conn->prepare($updateQuery);
                $updateStmt->bind_param('ss', $deviceId, $password);
                $updateStmt->execute();
                $response['secondDeviceId'] = $deviceId;
            } elseif ($user['SecondDeviceID'] !== $deviceId) {
                // SecondDeviceID does not match the current device ID
                $response['success'] = false;
                $response['message'] = 'This device is not allowed.';
            }
        } else {
            // SecondDeviceAllowed is not enabled, deny access
            $response['success'] = false;
            $response['message'] = 'This device is not allowed.';
        }
    }

    echo json_encode($response);
} else {
    echo json_encode(['success' => false, 'message' => 'Invalid password or device ID.']);
}

$conn->close();
?>
