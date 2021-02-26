using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionHandler : MonoBehaviour {

    public Rigidbody2D metalPlate1;
    public Rigidbody2D metalPlate2;
    public Rigidbody2D metalPlate3;
    public Rigidbody2D lamp;
    public GameObject metalPrefab;

    public IEnumerator BeginDestruction() {

        yield return new WaitForSeconds(8f);
        metalPlate1.isKinematic = false;
        GameObject.Instantiate(metalPrefab, new Vector3(5, -2.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        metalPlate2.isKinematic = false;
        lamp.isKinematic = false;
        GameObject.Instantiate(metalPrefab, new Vector3(-3, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(2f);
        GameObject.Instantiate(metalPrefab, new Vector3(1.7f, -6, 0), Quaternion.identity);
        metalPlate3.isKinematic = false;

        Debug.Log("Destroy");
    }
    
}
