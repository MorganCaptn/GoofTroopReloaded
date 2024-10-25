using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public int blockID; // Eindeutige ID für den Block
    public GameObject[] enemiesInBlock; // Gegner im Block

    public void ActivateEnemies()
    {
        foreach (GameObject enemy in enemiesInBlock)
        {
            enemy.SetActive(true); // Gegner aktivieren
            enemy.GetComponent<EnemyMovement>().ToggleMovement(false);
        }
    }
    public void FreezeEnemies()
    {
        foreach (GameObject enemy in enemiesInBlock)
        {
            enemy.GetComponent<EnemyMovement>().ToggleMovement(true);
            //enemy.SetActive(false); // Gegner deaktivieren

        }
    }
    public void DeactivateEnemies()
    {
        foreach (GameObject enemy in enemiesInBlock)
        {
            enemy.GetComponent<EnemyMovement>().ResetEnemy();
            enemy.SetActive(false); // Gegner deaktivieren
            
        }
    }
}