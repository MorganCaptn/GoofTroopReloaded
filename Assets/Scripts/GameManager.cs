using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BlockManager[] blocks; // Array aller Block-Manager
    public CameraController cameraController; // Referenz zum CameraController
    private BlockManager currentBlock; // Der aktuell aktive Block
    private Vector3 nextPlayerSpawnPos; //
    // Methode, um den aktuellen Block zu setzen
    public void SetCurrentBlock(int blockID)
    {
        // Deaktiviere den alten Block
        if (currentBlock != null)
        {
            currentBlock.DeactivateEnemies();
        }

        // Finde den neuen Block basierend auf der ID
        foreach (BlockManager block in blocks)
        {
            if (block.blockID == blockID)
            {
                currentBlock = block;
                currentBlock.ActivateEnemies();
                break;
            }
        }

        // Bewege die Kamera zum neuen Block
        if (cameraController != null)
        {
            Vector3 newCamPosition = currentBlock.transform.position;

            newCamPosition.z = cameraController.transform.position.z;
            Debug.Log(nextPlayerSpawnPos);
            Debug.Log(newCamPosition);
            cameraController.MoveToBlock(newCamPosition, nextPlayerSpawnPos);
        }
    }

    public void SetPlayerSpawnPositionForNextBlock(Vector3 nextSpawnPos)
    {

        nextPlayerSpawnPos = nextSpawnPos;
        
    }
  
}