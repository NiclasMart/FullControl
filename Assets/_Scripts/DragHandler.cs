using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

    private CanvasGroup canvasGroup;
    public MoveSupplySlot supplySlot;

    Canvas canvas;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponent<Canvas>();
    }

    private void Start() {
        transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
    }


    public void OnDrag(PointerEventData eventData) {
        //if OnBeginnDrag was successfull, pin move icon to mouse
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5f;
        transform.position = mousePos;
      

    }

    //sets move back to the supply channel if the drag ends
    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("EndDrag");
        canvasGroup.blocksRaycasts = true;
        transform.SetAsFirstSibling();
        
        transform.localPosition = Vector3.zero;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        //make dragging only possible, if the move icon is inside of the supply panel (find MoveSupplySlot) and a move is available
        MoveSupplySlot slot = GetComponentInParent<MoveSupplySlot>();
        if (slot && slot.moveAvailable) {
            canvasGroup.blocksRaycasts = false;
            //change hierarchy order, so that the move is shown in front
            transform.SetAsLastSibling();
            transform.parent.SetAsLastSibling();
            Debug.Log("Drag");
        }
        //else stop the drag action
        else {
            eventData.pointerDrag = null;
        }
    }
}
