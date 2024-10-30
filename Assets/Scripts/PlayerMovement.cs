using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Geschwindigkeit des Spielers
    private Rigidbody2D rb2D;
    private Animator animator; // Referenz zum Animator
    public GameObject carriedObject; // Aktuell getragenes Objekt
    public bool movementDisabled = false;    
    public float throwForce = 15f; // Wurfeigenschaften anpassen
    private Vector2 movementInput; // Für Bewegung und Wurfrichtung
    public Transform visualChild; // Reference to the visual child object
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // Zugriff auf das Rigidbody2D-Komponente
        animator = visualChild.GetComponent<Animator>(); // Zugriff auf den Animator
    }

    void Update()
    {
 
        // Eingaben erfassen
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Bewegungseingaben speichern
        movementInput = new Vector2(horizontal, vertical).normalized;
        // Bestimme, welche Achse dominiert
        bool horizontalDominates = Mathf.Abs(horizontal) > Mathf.Abs(vertical);

        // Setze Animator-Parameter basierend auf der dominanten Achse
        if (horizontalDominates)
        {
            animator.SetFloat("MoveX", horizontal);
            animator.SetFloat("MoveY", 0);  // Setze Y auf 0, um keine diagonalen Bewegungen darzustellen
        }
        else
        {
            animator.SetFloat("MoveX", 0);  // Setze X auf 0
            animator.SetFloat("MoveY", vertical);
        }
        // Bewegung berechnen (aber noch nicht anwenden)
        Vector2 movement = new Vector2(horizontal, vertical) * moveSpeed * Time.deltaTime;
        // Animator-Parameter setzen

        //animator.SetFloat("MoveX", horizontal);
        //animator.SetFloat("MoveY", vertical);
        animator.SetBool("IsMoving", horizontal != 0 || vertical != 0); // Setzt, ob der Spieler sich bewegt


  
       // Überprüfung, ob der Spieler sich bewegt
       if (horizontal != 0 || vertical != 0)
       {
           // Bestimmen der dominanten Eingabeachse
           if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
           {

               // Spieler nach links oder rechts drehen
               if (horizontal > 0)
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
               // Spieler nach oben oder unten drehen
               if (vertical > 0)
               {
                   transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0f)); // Oben
               }
               else
               {
                   transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180f)); // Unten
               }
               

            }

            //freeze rotation for sprite animations
            if (carriedObject != null)
            {

                carriedObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0f));
            }
           
            visualChild.rotation = Quaternion.Euler(new Vector3(0, 0, 0f));
            
            
        }
             
        // Aufheben von Objekten
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (carriedObject == null)
            {
                TryPickUpObject();
            }
            else
            {
                ThrowObject();
            }
        }

    }

    void FixedUpdate()
    {
        Vector2 movement = movementInput * moveSpeed * Time.fixedDeltaTime;
     
        // Berechnung der neuen Position basierend auf der Eingabe und Geschwindigkeit
        if (movementDisabled)
        {
             movement = new Vector2(0.0f, 0.0f);
        }
     
       

        // Anwendung der Bewegung auf den Spieler über Rigidbody2D
        rb2D.MovePosition(rb2D.position + movement);
    }

    public void ToggleMovement(bool activateMovement)
    {
        movementDisabled = activateMovement;
    }

    void TryPickUpObject()
    {
        // Überprüfe, ob es ein Fass in der Nähe gibt
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f); // Radius anpassen
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Throwable"))
            {
                carriedObject = collider.gameObject;

                // Fass an CarryPoint anhängen
                carriedObject.transform.SetParent(transform.Find("CarryPoint"));
                carriedObject.transform.localPosition = Vector3.zero;
                //carriedObject.transform.localRotation = Quaternion.identity; // Setze Rotation des Fasses auf neutral

                // Fixiere Rotation
                //freeze rotation for sprite animation
                //carriedObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0f));
                //carriedObject.GetComponent<Rigidbody2D>().freezeRotation = true;
                carriedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                carriedObject.GetComponent<CircleCollider2D>().isTrigger = false;

                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), carriedObject.GetComponent<Collider2D>(), true);
                break;
            }
        }
    }

    void ThrowObject()
    {
        if (carriedObject != null)
        {
            carriedObject.transform.SetParent(null); // Trennung vom Spieler
            Rigidbody2D rb = carriedObject.GetComponent<Rigidbody2D>();
            rb.isKinematic = false; // Reaktiviere physikalische Effekte
                                    // Aktiviere Kollision zwischen Spieler und Objekt wieder
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), carriedObject.GetComponent<Collider2D>(), false);

            // Bestimme die Wurfrichtung basierend auf der aktuellen Blickrichtung des Spielers
            Vector2 throwDirection = Vector2.zero;
            
          
            if (transform.rotation == Quaternion.Euler(0, 0, 0))
            {
                throwDirection = Vector2.up;
            }
            else if (transform.rotation == Quaternion.Euler(0, 0, 180f))
            {
                throwDirection = Vector2.down;
            }
            else if (transform.rotation == Quaternion.Euler(0, 0, 90f))
            {
                throwDirection = Vector2.left;
            }
            else if (transform.rotation == Quaternion.Euler(0, 0, -90f))
            {
                throwDirection = Vector2.right;
            }
            // **Fixiere die Rotation des Child-Objekts**
            visualChild.rotation = Quaternion.identity; // Verhindert die Rotation des visualChilds
        


        // Rufe die Methode zum Werfen auf
        ThrowableObject throwable = carriedObject.GetComponent<ThrowableObject>();
            throwable.ThrowObject(throwDirection, throwForce);
        }
    }

}
