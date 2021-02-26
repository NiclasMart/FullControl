using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CameraMove : MonoBehaviour {

    public DestructionHandler destructionHandler;

    public float speed = 1f; 
    // Start is called before the first frame update
    void Start(){
        transform.position = new Vector3(0, 23f, -10f);
    }

    // Update is called once per frame
    void FixedUpdate(){
        Vector3 pos = transform.position;

        if (pos.y > -15.6f) {
            pos.y += -speed * Time.fixedDeltaTime;
            transform.position = pos;
        }
    }

    public IEnumerator TriggerCameraShake() {

        yield return new WaitForSeconds(5f);
        CameraShakeInstance shake = CameraShaker.Instance.StartShake(2.2f, 7.5f, 3f);
        yield return new WaitForSeconds(8f);
        shake.StartFadeOut(4f);

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "IntroStart") {
            StartCoroutine(destructionHandler.BeginDestruction());
            StartCoroutine(TriggerCameraShake());
            Debug.Log("EnterTrigger");
        }
    }
}
