using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttachedMoveHandler : MonoBehaviour {

    private const int MAX_VISIBLE_SLOTS = 3;

    public Stick stick;
    public GameObject slotPrefab;
    public AvailableMovesDisplayHandler display;

    public MoveMemory memory;

    SpriteDisplayHandler spriteHandler;

    private int nextMoveSlotNumber = 0;
    private int visibleSlots = 1;

    public void Initialize(bool slotRemaining) {
        nextMoveSlotNumber = 0;
        spriteHandler = GameObject.Find("SpriteDisplayHandler").GetComponent<SpriteDisplayHandler>();
        if (!slotRemaining) {
            return;
        }

        addSlot();
    }

    //adds a further slot to the panel
    public void AddMoveSlotToPanel() {
        //if there are more slots than the max count, make the last slot invisible
        visibleSlots++;
        if (visibleSlots > MAX_VISIBLE_SLOTS) {
            Transform childSlot = transform.GetChild(MAX_VISIBLE_SLOTS - 1);
            childSlot.localScale = Vector3.zero;
            visibleSlots--;

            //adds the move to the memory and shows image at the seperat display
            memory.AddMoveToMemory(childSlot.Find("AttacheSlot").GetComponent<DropHandler>().mode);
            display.AddMoveImage(memory.GetLastMove());
        }
        
        addSlot();
    }

    //adds new Slot to display
    void addSlot() {
        //creates new slot with the move from the last round slightly transparent shown
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.Find("lastMovePreview").gameObject.GetComponent<Image>().sprite = spriteHandler.ChooseSprite(memory.GetMoveFromLastRound(nextMoveSlotNumber));
        nextMoveSlotNumber++;
        newSlot.transform.SetParent(this.transform);
        newSlot.transform.SetAsFirstSibling();
    }

    //if start button is pressed add all remaining moves to the display panel
    public void AddAllMovesToDisplay(int amount) {
        Debug.Log("Add");
        int oldestChild = Mathf.Min(transform.childCount, 3) - 1;
       
        int i = 0;
        while (i < amount) {
            Transform childSlot = transform.GetChild(oldestChild - i);
            memory.AddMoveToMemory(childSlot.Find("AttacheSlot").GetComponent<DropHandler>().mode);
            display.AddMoveImage(memory.GetLastMove());
            i++;
        }
    }

    // returns amount of move slots which should be added
    //dont count useles empty slots
    public int GetAmountMoveSlots() {
        int counter = 0;
        int oldestChild = Mathf.Min(transform.childCount, 3);
        for (int i = 1; i <= oldestChild; i++) {
            MoveModi mode = transform.GetChild(oldestChild - i).Find("AttacheSlot").GetComponent<DropHandler>().mode;
            if (mode != MoveModi.NONE) {
                counter = i;
            }
        }
        return counter;
    }
}
