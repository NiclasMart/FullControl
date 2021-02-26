//handles the growth of the seperat move panel by the amount of added moves

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeDisplay : MonoBehaviour {

    const int RESIZE_HIGHT = 61;

    Vector2 position;

    RectTransform rt;
    Transform imageContainer;

    private void Awake() {
        rt = GetComponent<RectTransform>();
        imageContainer = transform.Find("GridGroup");
        position = rt.anchoredPosition;
    }

    private void Update() {
        //calculates the hight of the panel dependant on the Count of images
        Vector2 panelSize = rt.sizeDelta;
        int imageCount = imageContainer.childCount;
        if (imageCount > 0) {
            panelSize.y = 140 + RESIZE_HIGHT * imageCount;
        }
        else {
            panelSize.y = 50;
        }
        rt.sizeDelta = panelSize;
        //reset position because the applys also downwards
        rt.anchoredPosition = position;
    }

}
