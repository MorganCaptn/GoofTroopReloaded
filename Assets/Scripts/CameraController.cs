using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Der Spieler
    public float moveSpeed = 2f; // Geschwindigkeit der Kamerabewegung
    public Vector2 blockSize = new Vector2(22, 14); // Größe eines Blocks in Tiles
    public Vector2 tileSize = new Vector2(1, 1); // Größe eines Tiles in Weltkoordinaten

    private Vector3 targetPosition; // Zielposition für die Kamera

    void Start()
    {
        // Initialisiere die Zielposition als Startposition
        targetPosition = transform.position;
    }

    void Update()
    {
        // Bewege die Kamera zur Zielposition
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    // Methode, um die Kamera zu einem Block zu verschieben
    public void MoveToBlock(int blockID)
    {
        // Berechne die Block-Koordinaten in Weltkoordinaten
        int blockX = blockID % (int)(Screen.width / blockSize.x);
        int blockY = blockID / (int)(Screen.width / blockSize.x);

        // Berechne die Weltkoordinate des Blocks
        Vector3 blockPosition = new Vector3(
            (blockX + 0.5f) * blockSize.x,
            (blockY + 0.5f) * blockSize.y,
            transform.position.z
        );

        targetPosition = blockPosition;

        Debug.Log($"Zielposition: {targetPosition}");
    }
}