using UnityEngine;
public class CameraController : MonoBehaviour
{
    public GameObject player; // Der Spieler
    public Vector2 blockSize = new Vector2(22, 14); // Größe eines Blocks in Tiles
    public FadeManager fadeManager; // Referenz zum FadeManager

    private Vector3 targetPosition; // Zielposition für die Kamera

    void Start()
    {
        targetPosition = transform.position;
    }

    public void MoveToBlock(Vector3 newCamPosition, Vector3 newPlayerPosition)
    {

        // Starte den Fade und bewege die Kamera nach dem Fade-Out
        player.GetComponent<PlayerMovement>().movementDisabled = true;
        fadeManager.FadeOutAndIn(() =>
        {

            transform.position = newCamPosition;
            
            player.transform.position = newPlayerPosition;
        });
        player.GetComponent<PlayerMovement>().movementDisabled = false;
    }
}
