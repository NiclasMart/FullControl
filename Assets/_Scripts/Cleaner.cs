using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Lamp") {
            Debug.Log("deleteLamp");
            Destroy(collision.transform.parent.gameObject);
        }
    }



}
