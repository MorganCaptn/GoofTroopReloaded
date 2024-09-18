using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTrigger : MonoBehaviour
{
    public int nextBlockID; // Die ID des Blocks, zu dem gewechselt wird
    public GameManager gameManager; // Referenz zum GameManager
    public Direction exitDirection; // In welche Richtung zeigt der Ausgang? Wichtig für den nachfolgenden Spawn der Player
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("BlockTrigger passiert.");
        // Überprüfen, ob der Spieler den Trigger betritt
        if (other.CompareTag("Player"))
        {
        
            var nextSpawnPos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            //Find the correct spawn position for the next block
            if (exitDirection == Direction.Left )
                {
                //keep y value
                nextSpawnPos.x -= 2;
                }
            else if (exitDirection == Direction.Right)
            {
                //keep y value
                nextSpawnPos.x += 2;
            }
            else if (exitDirection == Direction.Up)
            {
                //keep x value
                nextSpawnPos.y += 2;
            }
            else if (exitDirection == Direction.Down)
            {
                //keep x value
                nextSpawnPos.y -= 2;
            }
            gameManager.SetPlayerSpawnPositionForNextBlock(nextSpawnPos);
            gameManager.SetCurrentBlock(nextBlockID);
      

        }
    }
}