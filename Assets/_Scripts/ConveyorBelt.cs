﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] bool invertDirection = false;

    public bool InvertDirection { get => invertDirection; }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (rb != null) {
            Vector2 conveyorSpeed = new Vector2 (speed, 0);
            if (InvertDirection) {
                conveyorSpeed *= -1;
            }
            rb.velocity = conveyorSpeed;
        }
    }
}
