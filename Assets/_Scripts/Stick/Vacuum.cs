using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour {

    float HEIGHT_STICK = 2f;

    Animator suckAnimation;
    CollisionDetector collisionDetector;
    GameManager gameManager;

    Transform stickFluff;
    CircleCollider2D circleCollider;
    public bool fluffIsSticked = false;
    public bool vacuumIsActive = false;

    private bool vacuumColliderIsFree = true;

    private void Awake() {
        suckAnimation = GetComponent<Animator>();
        collisionDetector = transform.parent.gameObject.GetComponent<CollisionDetector>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        //ativates/deactivates vacuum function
        if (Input.GetKeyDown(KeyCode.Space) && gameManager.state == GameState.PLAY) {
            vacuumIsActive = !vacuumIsActive;
            suckAnimation.SetBool("suckActive", vacuumIsActive);
        }
    }

    private void FixedUpdate() {
        //if fluff is attached and vacuum is deactivated, detach fluff and give him some velocity
        if (fluffIsSticked && !vacuumIsActive) {
            Debug.Log("detach Ball");
            Rigidbody2D fluff = stickFluff.gameObject.GetComponent<Rigidbody2D>();
            fluff.isKinematic = false;
            fluff.velocity = (stickFluff.position - transform.position) * 1.5f;
            fluffIsSticked = false;
            collisionDetector.AddCircleCollider(false);

            stickFluff.gameObject.GetComponent<Ball>().suckedIn = false;
        }

        //handle movement and rotation of sticked fluff
        if (fluffIsSticked) {
            
            Vector3 pos = Vector3.zero;
            pos.x = transform.position.x + Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * HEIGHT_STICK;
            pos.y = transform.position.y + Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * -HEIGHT_STICK;
            stickFluff.position = pos;
            stickFluff.localRotation = transform.rotation;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        
        if (vacuumIsActive && collision.tag == "Ball" && vacuumColliderIsFree && !fluffIsSticked && !collision.gameObject.GetComponent<Ball>().suckedIn) {
            Debug.Log("On Trigger Stay:" + collision.tag);
            stickFluff = collision.transform;

            //change position of ball
            Vector3 pos = transform.position;
            pos.y -= HEIGHT_STICK;
            stickFluff.position = pos;

            fluffIsSticked = true;
            collisionDetector.AddCircleCollider(true);
            stickFluff.gameObject.GetComponent<Ball>().SuckIn(this);
            Debug.Log("SaveFluff");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Collision vacuum happen: " + collision.tag);
        if (collision.tag != "Ball") {
            vacuumColliderIsFree = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Debug.Log("Leave Vacuum Collision: " + collision.tag);
        if (collision.tag != "Ball") {
            vacuumColliderIsFree = true;
        }
    }
}
