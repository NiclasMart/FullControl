//the attached moves panel only shows the last three moves
//every other move is shown in an seperat panel
//this class handels the display of this seperat panel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvailableMovesDisplayHandler : MonoBehaviour {

    GameManager gameManager;

    public Transform movePanel;
    public IntegerVariable moveIndex;

    public RectTransform redBox;
    public Image startSign;
   

    bool displayIsVisible = true;

    SpriteDisplayHandler spriteHandler;

    private void Awake() {
        spriteHandler = GameObject.Find("SpriteDisplayHandler").GetComponent<SpriteDisplayHandler>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update() {
        //set redBox to the positionof the active move
        if (gameManager.state == GameState.PLAY && moveIndex.value != -1) {
            
            redBox.anchoredPosition = new Vector2(0f, moveIndex.value * 61 + 103);
        }
        
    }

    //adds the new move to the panel
    public void AddMoveImage(MoveModi type) {
        //creates new Image and adds the according sprite
        GameObject newObject = new GameObject();
        Image img = newObject.AddComponent<Image>();
        img.sprite = spriteHandler.ChooseSprite(type);
        //sets position of sprite
        img.transform.SetParent(movePanel);
        img.transform.SetAsFirstSibling();

        //sets start and end sign
        startSign.enabled = true;
    }

    public void AddMoveImage(List<MoveModi> moveList) {
        foreach(MoveModi modi in moveList) {
            AddMoveImage(modi);
        }
    }

    //is called after press to the Display Button
    //changes the visibility of the display
    public void ChangeDisplayVisibility() {
        displayIsVisible = !displayIsVisible;

        CanvasGroup display = movePanel.parent.gameObject.GetComponent<CanvasGroup>();
        Image redBoxImage = redBox.GetComponent<Image>();

        if (displayIsVisible) {
            display.alpha = 1f;
            redBoxImage.enabled = true;

        }
        else {
            display.alpha = 0;
            redBoxImage.enabled = false;
        }
        
        
    }

    

}
