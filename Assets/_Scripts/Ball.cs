using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    Animator fadeOutAnim;
    GameManager gameManager;
    Rigidbody2D rb;

    public float rollSpeed;
    Vector3 startPos;
    Vector2 normal = Vector2.up;

    bool rollActive = false;
    public bool suckedIn = false;

    private void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        fadeOutAnim = gameObject.GetComponent<Animator>(); 
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start(){
        rb.isKinematic = true;
        startPos = transform.position;
        ResetPosition();

    }

    //set rb settings depending on the gamemode
    private void Update() {
        if (gameManager.state == GameState.WIN) {
            rb.isKinematic = false;
        } 
    }

    // Update is called once per frame
    void FixedUpdate(){
        //fluff rolls if roll is activated
        if (rollActive) {
            
            //calculate slope with normal and correct speed
            float slope = Vector2.Dot(Vector2.right, normal);
 
            Vector2 speed = rb.velocity;
            speed.x = rollSpeed + 1.15f*rollSpeed*slope;
            rb.velocity = speed;
        }
    }

    public void SetDynamic() {
        rb.isKinematic = false;
    }

    //resets fluff to start conditions
    public void ResetPosition() {
        rollActive = false;
        rb.velocity = Vector2.zero;
        transform.position = startPos;
    }

    //tests if any collision would happen if the stick pushes the ball in a specific direction
    public bool CheckCollision(Vector2 direction) {
        //casts the collider in the movement direction and checks for collisions
        RaycastHit2D[] hits = new RaycastHit2D[10];
        int hitsNum = rb.Cast(direction, hits, 0.05f);

        //no collision detected
        if (hitsNum == 0) {
            return false;
        }
        else {
            return true;
        }
    }

    public void SuckIn(Vacuum vacuum) {
        Debug.Log("Hit vacuum");
        rollActive = false;
        suckedIn = true;
        rb.Sleep();
        rb.isKinematic = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //resets ball if it rolls ofscreen
        if (collision.tag == "Cleaner") {
            ResetPosition();
        }
        //lose level if the ball collides with spikes
        else if (collision.tag == "Spike") {
            gameManager.PauseLevel();
        }
        //win level if ball reaches goal
        else if (collision.tag == "Goal") {
            //play Animation
            fadeOutAnim.SetTrigger("fadeOutTrigger");
            gameManager.WinLevel();
        }
    }

    //if fluff collides with floor, set roll to active
    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.transform.tag == "Floor" && !suckedIn) {
            rollActive = true;
        }
        //saves normal to contact point
        normal = collision.contacts[0].normal;

    }
}
