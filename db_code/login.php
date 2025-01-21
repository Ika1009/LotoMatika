<?php
include('db_conn.php');

header('Content-Type: application/json');

// Get the data sent via POST
$data = json_decode(file_get_contents('php://input'), true);
$password = $data['password'] ?? '';
$deviceId = $data['deviceId'] ?? '';

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
    $response = [
        'success' => true,
        'message' => 'Login successful.',
        // Admin is identified if UID equals 1
        'isAdmin' => isset($user['UID']) && $user['UID'] == 1, 
        'deviceId' => $user['DeviceID'],
        'secondDeviceAllowed' => $user['SecondDeviceAllowed'] == 1, // Convert to boolean
        'secondDeviceId' => $user['SecondDeviceID']
    ];

    // Check and handle DeviceID and SecondDeviceID
    if (is_null($user['DeviceID'])) {
        // Update DeviceID if NULL
        $updateQuery = "UPDATE Users SET DeviceID = ? WHERE Password = ?";
        $updateStmt = $conn->prepare($updateQuery);
        $updateStmt->bind_param('ss', $deviceId, $password);
        $updateStmt->execute();
        $response['deviceId'] = $deviceId;
    } elseif ($user['DeviceID'] !== $deviceId) {
        if ($user['SecondDeviceAllowed'] == 1) {
            if (is_null($user['SecondDeviceID'])) {
                // Update SecondDeviceID if NULL and allowed
                $updateQuery = "UPDATE Users SET SecondDeviceID = ? WHERE Password = ?";
                $updateStmt = $conn->prepare($updateQuery);
                $updateStmt->bind_param('ss', $deviceId, $password);
                $updateStmt->execute();
                $response['secondDeviceId'] = $deviceId;
            } elseif ($user['SecondDeviceID'] !== $deviceId) {
                // Device doesn't match either DeviceID or SecondDeviceID
                $response['success'] = false;
                $response['message'] = 'This device is not allowed.';
            }
        } else {
            // SecondDeviceAllowed is not enabled
            $response['success'] = false;
            $response['message'] = 'This device is not allowed.';
        }
    }

    echo json_encode($response);
} else {
    echo json_encode(['success' => false, 'message' => 'Invalid password.']);
}

$conn->close();
?>
