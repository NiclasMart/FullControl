using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject fluff;
    float  nextSpawnTime;

    // Start is called before the first frame update
    void Start() {
        nextSpawnTime = Time.time + Random.Range(1f, 5f);
        
    }

    // Update is called once per frame
    void Update(){
        if (Time.time > nextSpawnTime) {
            Instantiate(fluff, transform.position, Quaternion.identity);
            nextSpawnTime = Time.time + Random.Range(1f, 10f);
        }
    }
}
