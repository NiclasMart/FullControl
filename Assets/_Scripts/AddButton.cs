using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddButton : MonoBehaviour {

    public int slotCount;
    Text addButtonCount;
    public MoveSupplyHandler moveSupplyHandler;

    public List<GameObject> attachedMoveHandlers = new List<GameObject>();

    private void Awake() {
        moveSupplyHandler.Initialize();
        addButtonCount = transform.GetChild(0).gameObject.GetComponent<Text>();
        
    }

    //adds a new slot to the Attache Move Panels if the current slot count is less than the max slot count
    public void AddNewSlots() {
        if (slotCount - 1 > 0) {
            //changes remaining slots number
            slotCount--;
            addButtonCount.text = (slotCount - 1).ToString();

            //adds it to every panel
            foreach (GameObject handler in attachedMoveHandlers) {
                handler.GetComponent<AttachedMoveHandler>().AddMoveSlotToPanel();
            }
        }
    }

    //calls the add function for every AttachedMoves Panel
    //called after start button is presses
    public void AddRemainingMovesToDisplay() {
        int moveSlotsToAdd = CalculateMoveSlotAmount();
        foreach (GameObject handler in attachedMoveHandlers) {
            handler.GetComponent<AttachedMoveHandler>().AddAllMovesToDisplay(moveSlotsToAdd);
        }
    }

    //after the game has startet, set the correct movements to the sticks
    public void SetMovementsOfSticks() {
        foreach (GameObject list in attachedMoveHandlers) {
            Debug.Log("Add Moves to Stick ");
            Stick stick = list.GetComponent<AttachedMoveHandler>().stick;
            MoveMemory memory = list.GetComponent<AttachedMoveHandler>().memory;
            for (int i = 0; i <= memory.GetSize() - 1; i++) {
                stick.SetModi(memory.GetMove(i));
            }
            stick.SetInitialMovement();

        }
    }

    //if moves exist from before, add them to th display
    public void InitializeExistingMoves() {
        int usedMoves = 0;
        foreach (GameObject gObject in attachedMoveHandlers) {
            AttachedMoveHandler attachedMoveHandler = gObject.GetComponent<AttachedMoveHandler>();
            Stick stick = attachedMoveHandler.stick;
            MoveMemory memory = attachedMoveHandler.memory;
            usedMoves += memory.GetSize();
            //show every move in display and calculate remaining moves and slots
            foreach (MoveModi move in memory.GetMoves()) {
                stick.movesDisplay.AddMoveImage(move);
                moveSupplyHandler.RemoveMove(move);
            }

            attachedMoveHandler.Initialize(memory.GetSize() < slotCount);
 
        }
        //Initializes all MoveSupplySlots with the remaining moves
        moveSupplyHandler.Initialize();
        Debug.Log(slotCount);
        if (usedMoves / attachedMoveHandlers.Count == slotCount) {
            slotCount = 1;
        }
        else {
            slotCount -= usedMoves / attachedMoveHandlers.Count;
        }
        addButtonCount.text = (slotCount - 1).ToString();
    }

    //calculates how many move Slots should be added for both handlers
    private int CalculateMoveSlotAmount() {
        int amount = 0;
        foreach(GameObject handlerGO in attachedMoveHandlers) {
            AttachedMoveHandler handler = handlerGO.GetComponent<AttachedMoveHandler>();
            amount = Mathf.Max(handler.GetAmountMoveSlots(), amount);
        }
        return amount;
    }


}
