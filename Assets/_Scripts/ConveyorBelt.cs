using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] float maxSpeed = 1f;
    [SerializeField] bool invertDirection = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        

        if (rb != null) {
            Vector2 direction = new Vector2(100f, 0);
            Debug.Log("Beschleunigen");
            rb.AddForce(direction);
        }
    }
}
