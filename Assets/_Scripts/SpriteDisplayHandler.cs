using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDisplayHandler : MonoBehaviour {

    public Sprite angularMovementSprite;
    public Sprite verticalsMovementSprite;
    public Sprite horizontalMovementSprite;
    public Sprite copySprite;
    public Sprite noSprite;

    public Sprite ChooseSprite(MoveModi modi) {
        switch (modi) {
            case MoveModi.ANGULAR:
                return angularMovementSprite;
            case MoveModi.HORIZONTAL:
                return horizontalMovementSprite;
            case MoveModi.VERTICAL:
                return verticalsMovementSprite;
            case MoveModi.COPY:
                return copySprite;
        }
        return noSprite;
    }
}
