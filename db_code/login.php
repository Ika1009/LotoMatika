<?php
// Assuming you have a database connection
include('db_connection.php');

// Get the data sent via POST
$data = json_decode(file_get_contents('php://input'), true);

// Extract the variables
$password = $data['password'];
$deviceId = $data['deviceId'];

// Query to get the user info
$query = "SELECT * FROM Users WHERE Password = ?";
$stmt = $conn->prepare($query);
$stmt->bind_param('s', $password);
$stmt->execute();
$result = $stmt->get_result();

if ($result->num_rows > 0) {
    $user = $result->fetch_assoc();
    
    // Check if the deviceId matches or if second device is allowed
    $response = [
        'success' => true,
        'isAdmin' => $user['isAdmin'], // If there's an 'isAdmin' field, include it
        'message' => 'Login successful.',
        'deviceId' => $user['DeviceID'],
        'secondDeviceAllowed' => $user['SecondDeviceAllowed'],
        'secondDeviceId' => $user['SecondDeviceID'],
        'password' => $user['Password'], // you may not want to return password
        'createdAt' => $user['CreatedAt'],
    ];

    if ($user['SecondDeviceAllowed'] == 1) {
        // Check if the provided deviceId matches the second device ID
        if ($user['SecondDeviceID'] && $user['SecondDeviceID'] !== $deviceId) {
            $response['success'] = false;
            $response['message'] = 'Ovom uređaju nije dozvoljen pristup.';
        }
    } elseif ($user['DeviceID'] !== $deviceId) {
        // If second device is not allowed, check the primary device ID
        $response['success'] = false;
        $response['message'] = 'Ovom uređaju nije dozvoljen pristup.';
    }

    echo json_encode($response);
} else {
    echo json_encode(['success' => false, 'message' => 'Incorrect credentials or device ID.']);
}
?>
