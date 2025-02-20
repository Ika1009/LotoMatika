<?php
include 'db_conn.php';

ob_start(); // Start output buffering to prevent "headers already sent"
header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $data = json_decode(file_get_contents("php://input"), true);
    $password = $data['password'] ?? '';
    $email = $data['email'] ?? '';

    if (empty($password) || empty($email)) {
        echo json_encode(["success" => false, "message" => "Both password and email are required"]);
        exit;
    }

    $query = "INSERT INTO Users (Password, Email, DeviceID) VALUES (?, ?, NULL)";
    $stmt = $conn->prepare($query);

    if ($stmt) {
        $stmt->bind_param("ss", $password, $email);

        if ($stmt->execute()) {
            echo json_encode(["success" => true, "message" => "User added successfully"]);
        } else {
            echo json_encode(["success" => false, "message" => "Failed to add user"]);
        }

        $stmt->close();
    } else {
        echo json_encode(["success" => false, "message" => "Database error: " . $conn->error]);
    }
}

$conn->close();
ob_end_flush(); // End output buffering and send output
?>
