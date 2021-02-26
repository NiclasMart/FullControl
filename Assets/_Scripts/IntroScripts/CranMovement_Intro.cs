using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CranMovement_Intro : MonoBehaviour {

    Rigidbody2D rb;
    public float speed = 5f;

    void Start() {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime, 0);
    }
}
