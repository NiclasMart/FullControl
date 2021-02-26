using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockInPlace : MonoBehaviour {

    Transform cameraPos;
    Vector3 thisStartPosition;

    RectTransform rt;

    private void Awake() {
        rt = GetComponent<RectTransform>();
        cameraPos = GameObject.Find("Main Camera").GetComponent<Transform>();
        thisStartPosition = rt.anchoredPosition;
    }

    //repositions the add panels when camera moves
    private void Update() {
        Vector3 pos = rt.anchoredPosition;
        pos.x = thisStartPosition.x - cameraPos.position.x * 67.5f;
        rt.anchoredPosition = pos;
    }
}
