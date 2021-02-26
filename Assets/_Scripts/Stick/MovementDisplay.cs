using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDisplay : MonoBehaviour {

    Stick stick;
    Transform stickTransform;
    GameManager gameManager;

    public Sprite angularMovementSprite;
    public Sprite verticalsMovementSprite;
    public Sprite horizontalMovementSprite;
    public Sprite copySprite;

    private void Awake() {
        stick = transform.parent.GetComponent<Stick>();
        transform.rotation = Quaternion.Euler(Vector3.zero);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update() {
        if(gameManager.state == GameState.PLAY) {
            switch (stick.activeMode) {
                case MoveModi.ANGULAR:
                    GetComponent<SpriteRenderer>().sprite = angularMovementSprite;
                    break;
                case MoveModi.HORIZONTAL:
                    GetComponent<SpriteRenderer>().sprite = horizontalMovementSprite;
                    break;
                case MoveModi.VERTICAL:
                    GetComponent<SpriteRenderer>().sprite = verticalsMovementSprite;
                    break;
                case MoveModi.COPY:
                    GetComponent<SpriteRenderer>().sprite = copySprite;
                    break;
                case MoveModi.NONE:
                    GetComponent<SpriteRenderer>().sprite = null;
                    break;
            }
        }
    }



}
