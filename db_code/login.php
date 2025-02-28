<?php
// Ensure there is absolutely no whitespace before this opening tag
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

$query = "SELECT UID, DeviceID, SecondDeviceAllowed, SecondDeviceID FROM Users WHERE Password = ?";
$stmt = $conn->prepare($query);
$stmt->bind_param('s', $password);
$stmt->execute();
$stmt->store_result(); // Store the result
$stmt->bind_result($uid, $deviceID, $secondDeviceAllowed, $secondDeviceID);

$response = ['success' => false, 'message' => 'Pogrešna šifra.'];

if ($stmt->fetch()) {
    $response = [
        'success' => true,
        'message' => 'Login successful.',
        'isAdmin' => ($uid == 1),
        'deviceId' => $deviceID,
        'secondDeviceAllowed' => ($secondDeviceAllowed == 1),
        'secondDeviceId' => $secondDeviceID
    ];

    // Check and handle DeviceID and SecondDeviceID
    if (is_null($deviceID)) {
        // Update DeviceID if NULL
        $updateQuery = "UPDATE Users SET DeviceID = ? WHERE Password = ?";
        $updateStmt = $conn->prepare($updateQuery);
        $updateStmt->bind_param('ss', $deviceId, $password);
        $updateStmt->execute();
        $response['deviceId'] = $deviceId;
    } elseif ($deviceID !== $deviceId) {
        if ($secondDeviceAllowed == 1) {
            if (is_null($secondDeviceID)) {
                // Update SecondDeviceID if NULL and allowed
                $updateQuery = "UPDATE Users SET SecondDeviceID = ? WHERE Password = ?";
                $updateStmt = $conn->prepare($updateQuery);
                $updateStmt->bind_param('ss', $deviceId, $password);
                $updateStmt->execute();
                $response['secondDeviceId'] = $deviceId;
            } elseif ($secondDeviceID !== $deviceId) {
                // Device doesn't match either DeviceID or SecondDeviceID
                $response['success'] = false;
                $response['message'] = 'Ovom uređaju nije odobren pristup.';
            }
        } else {
            // SecondDeviceAllowed is not enabled
            $response['success'] = false;
            $response['message'] = 'Ovom uređaju nije odobren pristup.';
        }
    }
}

echo json_encode($response);
$conn->close();
?>
