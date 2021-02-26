using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MoveMemory", menuName = "MoveMemory")]
public class MoveMemory : ScriptableObject {

    List<MoveModi> moveList = new List<MoveModi>();
    List<MoveModi> lastMoveList = new List<MoveModi>();


    //functions for deleting memory 
    public void DeleteMoveMemory() => moveList = new List<MoveModi>();

    public void DeleteLastMoveMemory() => lastMoveList = new List<MoveModi>();

    public void DeleteCompleteMemory() {
        DeleteMoveMemory();
        DeleteLastMoveMemory();
    }

    public void AddMoveToMemory(MoveModi move) {
        moveList.Add(move);
    }

    public List<MoveModi> GetMoves() {
        return moveList;
    }

    public MoveModi GetMove(int index) {
        return moveList[index];
    }

    public MoveModi GetLastMove() {
        return GetMove(moveList.Count - 1);
    }

    public MoveModi GetMoveFromLastRound(int index) {
        if (index < lastMoveList.Count)
            return lastMoveList[index];
        else
            return MoveModi.NONE;
    }

    public int GetSize() {
        return moveList.Count;
    }

    public void CopyMemory() {
        lastMoveList = moveList;
    }
}
