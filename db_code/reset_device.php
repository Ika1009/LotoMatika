<?php
include 'db_conn.php';

ob_start(); // Start output buffering to prevent "headers already sent"
header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
    echo json_encode(["success" => false, "message" => "Invalid request method. Only POST allowed."]);
    exit;
}

$data = json_decode(file_get_contents("php://input"), true);
$password = $data['password'] ?? '';

if (empty($password)) {
    echo json_encode(["success" => false, "message" => "Password is required."]);
    exit;
}

$query = "UPDATE Users SET DeviceID = NULL, SecondDeviceID = NULL WHERE Password = ?";
$stmt = $conn->prepare($query);

if (!$stmt) {
    echo json_encode(["success" => false, "message" => "Database error: " . $conn->error]);
    exit;
}

$stmt->bind_param("s", $password);
$stmt->execute();

if ($stmt->affected_rows > 0) {
    echo json_encode(["success" => true, "message" => "Devices reset successfully."]);
} else {
    echo json_encode(["success" => false, "message" => "Korisniku je nalog već resetovan ili šifra nije tačna."]);
}

$stmt->close();
$conn->close();
ob_end_flush(); // End output buffering and send output
?>
