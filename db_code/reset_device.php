<?php
include 'db_conn.php';

header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $data = json_decode(file_get_contents("php://input"), true);
    $password = $data['password'] ?? '';

    if (empty($password)) {
        echo json_encode(["success" => false, "message" => "Password is required"]);
        exit;
    }

    $query = "UPDATE Users SET DeviceID = NULL, SecondDeviceID = NULL WHERE Password = ?";
    $stmt = $conn->prepare($query);

    if ($stmt) {
        $stmt->bind_param("s", $password);
        if ($stmt->execute() && $stmt->affected_rows > 0) {
            echo json_encode(["success" => true, "message" => "Devices reset successfully"]);
        } else {
            echo json_encode(["success" => false, "message" => "User not found or no changes made"]);
        }
        $stmt->close();
    } else {
        echo json_encode(["success" => false, "message" => "Database error"]);
    }

    $conn->close();
}
?>
