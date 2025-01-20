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

    $query = "INSERT INTO Users (Password, DeviceID) VALUES (?, NULL)";
    $stmt = $conn->prepare($query);

    if ($stmt) {
        $stmt->bind_param("s", $password);
        if ($stmt->execute()) {
            echo json_encode(["success" => true, "message" => "User added successfully"]);
        } else {
            echo json_encode(["success" => false, "message" => "Failed to add user"]);
        }
        $stmt->close();
    } else {
        echo json_encode(["success" => false, "message" => "Database error"]);
    }

    $conn->close();
}
?>
