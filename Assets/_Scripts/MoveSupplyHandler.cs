using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSupplyHandler : MonoBehaviour {

    public int verticalMovesCount = 0;
    public int horizontalMovesCount = 0;
    public int angularMovesCount = 0;
    public int copyMovesCount = 0;

    public void Initialize() {
        foreach (Transform childSlot in transform) {
            Debug.Log("Initialize Handler");
            childSlot.gameObject.GetComponent<MoveSupplySlot>().Initialize(this);
        }
    }
   

    public void RemoveMove(MoveModi moveModi) {
        switch (moveModi) {
            case MoveModi.ANGULAR:
                angularMovesCount--;
                break;
            case MoveModi.COPY:
                copyMovesCount--;
                break;
            case MoveModi.HORIZONTAL:
                horizontalMovesCount--;
                break;
            case MoveModi.VERTICAL:
                verticalMovesCount--;
                break;
        }
    }
}
