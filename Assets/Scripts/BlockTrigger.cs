using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BlockTrigger : MonoBehaviour
{
    public int blockID; // Die ID des Blocks, zu dem gewechselt wird
    public GameManager gameManager; // Referenz zum GameManager

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("BlockTrigger passiert.");
        // Überprüfen, ob der Spieler den Trigger betritt
        if (other.CompareTag("Player"))
        {
            // Setze den aktuellen Block im GameManager
            gameManager.SetCurrentBlock(blockID);
        }
    }
}