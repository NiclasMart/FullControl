using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour {

    Stick stick;
    Vacuum vacuum;
    CircleCollider2D circleCol;
    public bool hasCollided = false;

    private void Start() {
        stick = GetComponent<Stick>();
        circleCol = GetComponent<CircleCollider2D>();
        circleCol.enabled = false;
        vacuum = transform.Find("Vacuum").gameObject.GetComponent<Vacuum>();
    }

    //detects if an collision woud happen though VERTICAL or HORIZONTALY movement
    public  bool detectCollision(Vector2 direction) {

        if (direction == Vector2.zero) {
            return false;
        }

        //casts the collider in the movement direction and checks for collisions
        RaycastHit2D[] hits = new RaycastHit2D[10];
        int hitsNum = GetComponent<Rigidbody2D>().Cast(direction, hits, 0.05f);

        //no collision detected
        if (hitsNum == 0) {
            return false;
        }

        //looks if collision happend with sticked ball 
        //if so, dont report collision
        for (int i = 0; i < hitsNum; i++) {
            if (hits[i].transform.tag == "Ball" && vacuum.fluffIsSticked || hits[i].transform.tag == "Lamp" || hits[i].transform.tag == "Vacuum") { 
                continue;
            }
            //tests if the ball would collid with something when its pushed by the stick
            else if (hits[i].transform.tag == "Ball" && !hits[i].transform.gameObject.GetComponent<Ball>().CheckCollision(direction)) {
                continue;
            }
            else {
                Debug.Log("RayCollided: " + hits[i].transform.tag);
                return true;
            }
        }
        return false;
    }

    public void AddCircleCollider(bool ballSticks) {
        if (ballSticks) {
            circleCol.enabled = true;
        }
        else {
            circleCol.enabled = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag != "Ball" && collision.tag != "Lamp" && collision.tag != "Vacuum") {
            Debug.Log("Clone:" + collision.tag);
            stick.SaveLastMove();
            hasCollided = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag != "Ball") {
            hasCollided = false;

        }
    }
}
