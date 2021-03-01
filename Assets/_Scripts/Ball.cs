using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    Animator fadeOutAnim;
    GameManager gameManager;
    Rigidbody2D rb;

    public float maxRollSpeed;
    Vector3 startPos;

    bool rollActive = false;
    public bool suckedIn = false;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        fadeOutAnim = gameObject.GetComponent<Animator>(); 
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start(){
        startPos = transform.position;
        rb.isKinematic = true;
        ResetBall();

    }

    //set rb settings depending on the gamemode
    private void Update() {
        if (gameManager.state == GameState.WIN) {
            rb.isKinematic = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rollActive) {
            MoveBall();
        }
    }

    private void MoveBall()
    {
        if (Mathf.Abs(rb.angularVelocity) < maxRollSpeed) {
            rb.AddTorque(-1);
        }
    }

    public void SetDynamic() {
        rb.isKinematic = false;
    }

    //resets fluff to start conditions
    public void ResetBall() {
        rollActive = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
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
            ResetBall();
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
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        rollActive = false;
    }
}
