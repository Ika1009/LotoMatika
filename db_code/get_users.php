<?php
include 'db_conn.php';

header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] === 'GET') {
    $query = "SELECT * FROM Users";
    $result = $conn->query($query);

    if ($result) {
        $users = $result->fetch_all(MYSQLI_ASSOC);

        echo json_encode(["success" => true, "data" => $users]);
    } else {
        echo json_encode(["success" => false, "message" => "Failed to retrieve users"]);
    }

    $conn->close();
} else {
    echo json_encode(["success" => false, "message" => "Invalid request method. Only GET allowed."]);
}
?>
