using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    const float CAMERA_MOVESPEED = 3f;

    public float startPositionX = 0;
    public int leftBorder;
    public int rightBorder;

    public bool moveAllowed = false;

    Vector3 move = Vector3.zero;

    private void Start() {
        transform.position = new Vector3(startPositionX, 0, -10);
    }

    // Update is called once per frame
    void Update() {
        if (moveAllowed) {
            move = Vector3.zero;
            if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= leftBorder) {
                move.x = -CAMERA_MOVESPEED;
            }
            else if (Input.GetKey(KeyCode.RightArrow) && transform.position.x <= rightBorder) {
                move.x = CAMERA_MOVESPEED;
            }
        }
    }

    private void FixedUpdate() {
        transform.position += move * Time.fixedDeltaTime;
    }
}
