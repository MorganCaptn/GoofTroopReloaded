using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    public GameObject breakEffectPrefab;  // Optionaler Effekt beim Zerstören
    public float maxThrowDistance = 10f;  // Maximale Distanz, die das Fass fliegen darf

    private Vector2 startPosition;        // Position, an der das Fass geworfen wurde
    private bool isThrown = false;        // Wird true, wenn das Fass geworfen wurde

    void Start()
    {
        // Speichert die Startposition, wenn das Objekt erzeugt wird
        startPosition = transform.position;
    }

    void Update()
    {
        if (isThrown)
        {
            // Überprüfe die zurückgelegte Distanz
            float distanceTraveled = Vector2.Distance(startPosition, transform.position);

            // Wenn die maximale Distanz erreicht ist, zerstöre das Fass
            if (distanceTraveled >= maxThrowDistance)
            {
                Break();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Überprüfe, ob das Fass mit einer Wand kollidiert
        if (collision.gameObject.CompareTag("Wall"))
        {
            Break();
        }
    }

    public void ThrowObject(Vector2 throwDirection, float throwForce)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = false;  // Aktiviert die Physik des Fasses
        rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);  // Wirft das Fass

        // Markiere, dass das Fass geworfen wurde
        isThrown = true;

        // Speichere die Startposition, um die zurückgelegte Distanz zu berechnen
        startPosition = transform.position;
    }

    void Break()
    {
        // Optional: Erzeuge einen Effekt beim Zerstören des Fasses
        if (breakEffectPrefab != null)
        {
            Instantiate(breakEffectPrefab, transform.position, transform.rotation);
        }

        // Fass zerstören
        Destroy(gameObject);
    }
}