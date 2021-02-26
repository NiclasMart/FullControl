using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour {

    public StickNumber id;

    public GameManager gameManager;
    public AvailableMovesDisplayHandler movesDisplay;
    public Stick otherStick;
    public IntegerVariable moveIndexVariable;
    CollisionDetector collisionDetector;
    Rigidbody2D rb;
    
    List<MoveModi> modi = new List<MoveModi>();
    public MoveModi activeMode = MoveModi.NONE;
    int moveIndex = 0;
    

    public float moveSpeed = 2.0f;
    public float rotateSpeed = 10.0f;
    public Vector2 movementDirection;
    Vector3 rotationDirection;
    Vector3 lastRotation;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        collisionDetector = GetComponent<CollisionDetector>();
        lastRotation = Vector3.zero;
    }

    // Update is called once per frame
    void Update(){
        //checks if the Game is active (round is not in planning phase)
        if (gameManager.state == GameState.PLAY) {
            //changes movement mode of the player stick when tab is pressed
            if (Input.GetKeyDown(KeyCode.Tab) && modi.Count != 0) {
                rb.velocity = Vector2.zero;
                if (moveIndex < modi.Count - 1) {
                    activeMode = modi[moveIndex + 1];
                    moveIndex++;
                    moveIndexVariable.value = moveIndex;
                }
            }

            //controlls movement
            //VERTICAL
            if (activeMode == MoveModi.VERTICAL || activeMode == MoveModi.COPY && otherStick.activeMode == MoveModi.VERTICAL) {
                float verticalMovement = Input.GetAxis("Vertical");
                Vector2 direction = new Vector2(0, verticalMovement);

                //only apply move if no collision is detected, else reset movement
                if (!collisionDetector.detectCollision(direction)) {
                    movementDirection = direction;
                }
                else {
                    movementDirection = Vector2.zero;
                }
            }
            //HORIZONTAL
            else if (activeMode == MoveModi.HORIZONTAL || activeMode == MoveModi.COPY && otherStick.activeMode == MoveModi.HORIZONTAL) {
                //Debug.Log("Horizontal");
                float horizontalMovement = Input.GetAxis("Horizontal");
                Vector2 direction = new Vector2(horizontalMovement, 0);

                //only apply move if no collision is detected, else reset movement
                if (!collisionDetector.detectCollision(direction)) {
                    movementDirection = direction;
                }
                else {
                    movementDirection = Vector2.zero;
                }
            }
            //ROTATION
            else if (activeMode == MoveModi.ANGULAR || activeMode == MoveModi.COPY && otherStick.activeMode == MoveModi.ANGULAR) {
                rotationDirection = Vector3.zero;
                if (Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E)) {
                    rotationDirection = Vector3.forward;
                }
                else if (Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q)) {
                    rotationDirection = Vector3.back;
                }
            }
            //if no movement applys
            else {
                movementDirection = Vector2.zero;
            }
        }
        else {
            movementDirection = Vector2.zero;
            movementDirection = Vector2.zero;
        }
    }

    private void FixedUpdate() { 
        //handle movement and rotation
        //ROTATION
        if (activeMode == MoveModi.ANGULAR || activeMode == MoveModi.COPY && otherStick.activeMode == MoveModi.ANGULAR) {
            //rotate stick if no collision happend (collisionis detectet after Fixed Update)
            if (!collisionDetector.hasCollided) {
                transform.Rotate(rotationDirection * rotateSpeed);
            }
            //if the last rotation was a collision rotate only if the last rotational direction is different to the new direction
            else if (lastRotation != rotationDirection) {
                transform.Rotate(rotationDirection * rotateSpeed);
            }
        }
        //MOVEMENT
        else if (activeMode != MoveModi.NONE) {
            rb.velocity = movementDirection * moveSpeed * Time.deltaTime;
        }
    }

   //set movement mode
    public void SetModi(MoveModi mode) {
        modi.Add(mode);
    }
    

    //sets last rotation direction for collision detection
    public void SaveLastMove() {
       lastRotation = rotationDirection;
    }

    //initializes first move after planning phase
    public void SetInitialMovement() {
        if (modi.Count != 0) {
            activeMode = modi[0];
            moveIndexVariable.value = 0;
        }
    }

  
}
