using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionDisplay : MonoBehaviour
{ 
    [SerializeField] Transform directionArrow;

    ConveyorBelt conveyorBelt;

    private void Start()
    {
        conveyorBelt = GetComponentInParent<ConveyorBelt>();
        if (conveyorBelt.InvertDirection) {
            RotateArrowTo(180);
        }
    }

    public void UpdateArrowDirection()
    {
        if (conveyorBelt.InvertDirection) {
            RotateArrowTo(180f);
        }
        else {
            RotateArrowTo(0);
        }
    }

    void RotateArrowTo(float angle)
    {
        directionArrow.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
