//slot which handels the attachement of moves to the coresponding stick
//children of th AttachedMoveHandler

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropHandler : MonoBehaviour, IDropHandler {

    public MoveModi mode;
    private bool _moveAttached = false;

    GameObject _deleteButton;
    Image moveImage;
    Image lastMovePreview;

    //sets initial state
    private void Awake() {
        //adds Listener to delete Button and sets it to false
        _deleteButton = transform.parent.Find("deleteButton").gameObject;
        _deleteButton.GetComponent<Button>().onClick.AddListener(() => DeleteMove());
        lastMovePreview = transform.parent.Find("lastMovePreview").gameObject.GetComponent<Image>();

        ResetSlot();
    }

    //handles transparency of move image according to position
    void Update() {
        if (moveImage != null) { 
            int index = transform.parent.GetSiblingIndex();
            moveImage.color = new Vector4(255, 255, 255, 1 - index * 0.25f);
        }
    }

    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag != null && !_moveAttached) {
            //Clones the draged move and set it to the move slot
            GameObject newMove = Instantiate(eventData.pointerDrag.gameObject);
            newMove.transform.SetParent(transform);
            newMove.transform.SetAsFirstSibling();
            //decreases move supply and set mode of the current slot
            eventData.pointerDrag.gameObject.GetComponentInParent<MoveSupplySlot>().numOfMoves--;
            mode = eventData.pointerDrag.GetComponent<DragHandler>().supplySlot.type;
            //show delete Button
            _deleteButton.SetActive(true);
            //disable lastMovePreviewImage and border of the slot
            lastMovePreview.enabled = false;
            GetComponent<Image>().enabled = false;
            //saves image component in variable
            moveImage = newMove.GetComponent<Image>();
            //set slot as blocked
            _moveAttached = true;
        }
        
    }

    //is called when delete Button of the slot is clicked
    public void DeleteMove() {
        ResetSlot();

        moveImage = null;

        //changes Amount of supply Slot and deletes the move from the stick
        GameObject move = transform.GetChild(0).gameObject;
        move.GetComponent<DragHandler>().supplySlot.addAmount();
        Destroy(move);
    }

    //sets all initial states
    private void ResetSlot() {
        _moveAttached = false;
        mode = MoveModi.NONE;
        _deleteButton.SetActive(false);
        //show last move and border of slot
        lastMovePreview.enabled = true;
        GetComponent<Image>().enabled = true;
    }

}
