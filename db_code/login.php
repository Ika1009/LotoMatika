<?php
header('Content-Type: application/json');

// Include database connection
include 'db_conn.php';

$data = json_decode(file_get_contents("php://input"), true);
$password = $data['password'] ?? '';

if (empty($password)) {
    echo json_encode(['success' => false, 'message' => 'Password is required.']);
    exit;
}

$query = $conn->prepare("SELECT UID FROM Users WHERE Password = ?");
$query->bind_param("s", $password);
$query->execute();
$result = $query->get_result();

if ($row = $result->fetch_assoc()) {
    $uid = $row['UID'];
    $isAdmin = ($uid == 1);
    echo json_encode(['success' => true, 'isAdmin' => $isAdmin, 'message' => 'Login successful.']);
} else {
    echo json_encode(['success' => false, 'message' => 'Invalid password.']);
}

$query->close();
$conn->close();
?>
