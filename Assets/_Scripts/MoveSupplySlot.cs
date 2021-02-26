//slot which 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSupplySlot : MonoBehaviour {

    MoveSupplyHandler handler;

    public int numOfMoves;
    public bool moveAvailable => numOfMoves > 0;
    public MoveModi type;

    SpriteDisplayHandler spriteHandler;
    public GameObject moveSlot;
    public Image prevImage;

    //private void Awake() {
    public void Initialize(MoveSupplyHandler handler) {
        spriteHandler = GameObject.Find("SpriteDisplayHandler").GetComponent<SpriteDisplayHandler>();
        this.handler = handler;
        Image moveImage = moveSlot.GetComponent<Image>();
        //sets inital picture according to the choosen slot type
        moveImage.sprite = spriteHandler.ChooseSprite(type);
        prevImage.sprite = spriteHandler.ChooseSprite(type);
        //sets move count to the supply slot
        switch (type) {
            case MoveModi.ANGULAR:
                numOfMoves = handler.angularMovesCount;
                break;
            case MoveModi.HORIZONTAL:
                numOfMoves = handler.horizontalMovesCount;
                break;
            case MoveModi.VERTICAL:
                numOfMoves = handler.verticalMovesCount;
                break;
            case MoveModi.COPY:
                numOfMoves = handler.copyMovesCount;
                break;
        }
        moveImage.enabled = true;
        prevImage.enabled = true;
        moveSlot.GetComponent<DragHandler>().supplySlot = this;
       
    }

    // changes the displayed supply amount number
    private void Update() {
        transform.Find("Amount").GetComponent<Text>().text = numOfMoves.ToString();

    }

    //increases slot supply amount by one
    public void addAmount() {
        numOfMoves++;
    }







}
