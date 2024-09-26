using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTrigger : MonoBehaviour
{
    public GameObject spawnBlock;
    private int nextBlockID; // Die ID des Blocks, zu dem gewechselt wird
    public GameManager gameManager; // Referenz zum GameManager
    void Start()
    {
        // Finde die nextBlockID durch Zugriff auf das Parent-Objekt von spawnBlock
        if (spawnBlock.transform.parent != null)
        {
            // Prüfe, ob das Parent-Objekt ein Skript hat, das die Block-ID enthält
            BlockManager nextBlockManager = spawnBlock.transform.parent.GetComponent<BlockManager>();

            if (nextBlockManager != null)
            {
                nextBlockID = nextBlockManager.blockID; // Hier nehmen wir an, dass der Block eine blockID hat
            }
            else
            {
                Debug.LogError("Das Parent-Objekt hat keinen BlockManager.");
            }
        }
        else
        {
            Debug.LogError("spawnBlock hat kein Parent-Objekt.");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("BlockTrigger passiert.");
        // Überprüfen, ob der Spieler den Trigger betritt
        if (other.CompareTag("Player"))
        {
            // Setze die Position des Spielers auf die Position des spawnBlock
            var nextSpawnPos = spawnBlock.transform.position;

            gameManager.SetPlayerSpawnPositionForNextBlock(nextSpawnPos);
            gameManager.SetCurrentBlock(nextBlockID);
      

        }
    }
}