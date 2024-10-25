using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BlockManager[] blocks; // Array aller Block-Manager
    public CameraController cameraController; // Referenz zum CameraController
    
    public GameObject player; // Der Spieler
    private PlayerMovement playerController; // Referenz zum PlayerController
    public FadeManager fadeManager;

    public BlockManager starterBlock;
    private BlockManager currentBlock; // Der aktuell aktive Block
    private BlockManager previousBlock; // Der Block davor

    public float blockSwitchCooldown = 1f; // Cooldown-Zeit in Sekunden
    private bool canSwitchBlock = true; // Flag, die anzeigt, ob ein Blockwechsel möglich ist

    private Vector3 nextPlayerSpawnPos; //


    void Start()
    {
        currentBlock = starterBlock;
        playerController = player.GetComponent<PlayerMovement>();
    }
    public void SetCurrentBlock(int blockID)
    {
        if (!canSwitchBlock) // Blockwechsel nur, wenn Cooldown abgelaufen ist
            return;

        // Starte den Cooldown
        StartCoroutine(BlockSwitchCooldown());

        // Deaktiviere den alten Block

        currentBlock.FreezeEnemies();
   

        // Finde den neuen Block basierend auf der ID
        foreach (BlockManager block in blocks)
        {
            if (block.blockID == blockID)
            {
                previousBlock = currentBlock;
                currentBlock = block;
                Debug.Log("Switch to:");
                Debug.Log(currentBlock.blockID);
                currentBlock.ActivateEnemies();
                break;
            }
        }

        // Bewege die Kamera zum neuen Block
        if (cameraController != null)
        {
            Vector3 newCamPosition = currentBlock.transform.position;

            newCamPosition.z = cameraController.transform.position.z;
            //Debug.Log(nextPlayerSpawnPos);
            //Debug.Log(newCamPosition);
            cameraController.MoveToBlock(newCamPosition, nextPlayerSpawnPos);

            //Clean up all the rest after camera has faded
            if (previousBlock != null)
            {
                previousBlock.DeactivateEnemies();
            }
                

        }


    }
    // Coroutine für den Cooldown
    private IEnumerator BlockSwitchCooldown()
    {
        canSwitchBlock = false; // Blockwechsel deaktivieren
        yield return new WaitForSeconds(blockSwitchCooldown); // Wartezeit
        canSwitchBlock = true; // Blockwechsel wieder aktivieren
    }


    public void SetPlayerSpawnPositionForNextBlock(Vector3 nextSpawnPos)
    {

        nextPlayerSpawnPos = nextSpawnPos;
        
    }
  
}