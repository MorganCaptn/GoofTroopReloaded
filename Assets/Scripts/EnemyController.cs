using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // Referenz auf den Spieler
    public float moveSpeed = 3f; // Geschwindigkeit des Gegners
    private Vector3 startPosition; // Die Startposition des Gegners

    void Start()
    {
        // Speichere die Startposition des Gegners
        startPosition = transform.position;
    }

    public void ResetEnemy()
    {
        // Setze den Gegner auf seine Startposition zurück
        transform.position = startPosition;
        // Hier kannst du weitere Eigenschaften zurücksetzen (z.B. Gesundheit, Animationen)
    }
    // Update is called once per frame
    void Update()
    {


        if (player != null)
        {
            // Berechnung der Richtung zum Spieler
            Vector2 direction = (player.position - transform.position).normalized;

            // Berechnung der Bewegung
            Vector2 movement = direction * moveSpeed * Time.deltaTime;

            // Anwendung der Bewegung auf den Gegner im Weltkoordinatensystem
            transform.Translate(movement, Space.World);

            //Disable rotation
            /*
            // Überprüfung der dominanten Bewegungsachse
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                // Gegner nach links oder rechts drehen
                if (direction.x > 0)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90f)); // Rechts
                }
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f)); // Links
                }
            }
            else
            {
                // Gegner nach oben oder unten drehen
                if (direction.y > 0)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0f)); // Oben
                }
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180f)); // Unten
                }
            }
            */
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Throwable"))
        {
            // Trifft der Gegner mit einem geworfenen Objekt zusammen, mache etwas
            Debug.Log("Gegner wurde getroffen!");

            // Füge hier Logik ein, z.B. Schaden erleiden oder zerstören
            Destroy(collision.gameObject); // Zerstöre das Fass nach dem Treffer
        }
    }
}