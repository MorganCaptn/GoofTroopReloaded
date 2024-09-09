using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // Methode, die aufgerufen wird, wenn ein Collider mit diesem GameObject kollidiert
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Trigger-Kollision mit Gegner erkannt!");
        }
    }
}