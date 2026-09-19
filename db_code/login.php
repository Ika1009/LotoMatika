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
        
$query = "SELECT UID, DeviceID, SecondDeviceAllowed, SecondDeviceID FROM Users WHERE Password = ? LIMIT 1";
$stmt = $conn->prepare($query);
$stmt->bind_param('s', $password);
$stmt->execute();
$stmt->store_result();
$stmt->bind_result($uid, $deviceID, $secondDeviceAllowed, $secondDeviceID);

$response = ['success' => false, 'message' => 'Pogrešna šifra.'];

if ($stmt->fetch()) {
    $uid = (int)$uid;
    $secondDeviceAllowed = (int)$secondDeviceAllowed;

    $isAdmin = ($uid === 1);

    // Base response
    $response = [
        'success' => true,
        'message' => 'Login successful.',
        'isAdmin' => $isAdmin,
        'deviceId' => $deviceID,
        'secondDeviceAllowed' => ($secondDeviceAllowed === 1),
        'secondDeviceId' => $secondDeviceID
    ];

    // ADMIN: bypass device checks entirely
    if ($isAdmin) {
        echo json_encode($response);
        $stmt->close();
        $conn->close();
        exit;
    }

    // NON-ADMIN: validate device access
    if (is_null($deviceID) || $deviceID === '') {
        // Update primary DeviceID if NULL/empty
        $updateQuery = "UPDATE Users SET DeviceID = ? WHERE UID = ?";
        $updateStmt = $conn->prepare($updateQuery);
        $updateStmt->bind_param('si', $deviceId, $uid);
        $updateStmt->execute();
        $updateStmt->close();

        $response['deviceId'] = $deviceId;
        $response['success'] = true;
    } elseif ($deviceID !== $deviceId) {
        if ($secondDeviceAllowed === 1) {
            if (is_null($secondDeviceID) || $secondDeviceID === '') {
                // Update SecondDeviceID if NULL/empty and allowed
                $updateQuery = "UPDATE Users SET SecondDeviceID = ? WHERE UID = ?";
                $updateStmt = $conn->prepare($updateQuery);
                $updateStmt->bind_param('si', $deviceId, $uid);
                $updateStmt->execute();
                $updateStmt->close();

                $response['secondDeviceId'] = $deviceId;
                $response['success'] = true;
            } elseif ($secondDeviceID !== $deviceId) {
                // Device doesn't match either DeviceID or SecondDeviceID
                $response['success'] = false;
                $response['message'] = 'Ovom uređaju nije odobren pristup.';
            } else {
                // matches second device
                $response['success'] = true;
            }
        } else {
            // SecondDeviceAllowed is not enabled
            $response['success'] = false;
            $response['message'] = 'Ovom uređaju nije odobren pristup.';
        }
    } else {
        // matches primary device
        $response['success'] = true;
    }
}

echo json_encode($response);
$stmt->close();
$conn->close();
?>
