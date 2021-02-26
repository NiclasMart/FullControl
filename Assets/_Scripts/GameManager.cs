using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    const int LEVEL_COUNT = 13;

    public GameState state;
    public GameState lastState;
    public UIManager uiManager;
    public AddButton moveHandler;
    public Animator transition;

    public IntegerVariable moveIndex;
    public IntegerVariable lastLevel;

    public Ball fluff;
    public List<MoveMemory> memorys = new List<MoveMemory>();
    
    void Awake(){
        state = GameState.PLANNING;
        uiManager.InitializeUI(); 
    }

    private void Start() {
        //handle start Transition of the current Level
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        AnimationHandler.PlayStartTransition(transition, lastLevel.value, currentLevel);
        //handle moves from the previouse round
        moveHandler.InitializeExistingMoves();
        moveIndex.value = -1;
        
    }

    private void OnApplicationQuit() {
        Debug.Log("DeleteMemory");
        foreach (MoveMemory memory in memorys) {
            memory.DeleteCompleteMemory();
        }
        //SaveSystem.DeleteSaveGame();
    }

    //starts round after planning phase through click on the start button
    public void StartRound() {
        //handles Moves
        moveHandler.AddRemainingMovesToDisplay();
        moveHandler.SetMovementsOfSticks();
        
        //sets state and UI
        state = GameState.PLAY;
        uiManager.InitializePlayModeUI();
        Time.timeScale = 1f;
    }

    //is called if the goal is reached ... starts the next level after 3 seconds
    public void WinLevel() {
        state = GameState.WIN;
        //delete moves in memory
        foreach (MoveMemory memory in memorys) {
            memory.DeleteCompleteMemory();
        }
        //relod next level if the current level is not the last one
        int level = SceneManager.GetActiveScene().buildIndex;
        if (level != LEVEL_COUNT) {
            level++;
        }
        else {
            level = 0;
        }
        StartCoroutine(LoadLevel(level, 2f));
    }

    //function is calles after fluff collides with spikes or player clicks on the reload button
    public void PauseLevel() {
        Time.timeScale = 0f;
        uiManager.DisplayRelodingScreen();
        lastState = state;
        state = GameState.PAUSE;
    }

    //is called from the pause screen if the "X" - Button is pressed
    public void UnpauseLevel() {
        Time.timeScale = 1f;
        uiManager.CloseReloadingScreen();
        state = lastState;
    }

    //is called from restart Screen if Moves should not be saved
    public void RestartLevel() {
        foreach(MoveMemory memory in memorys) {
            Debug.Log("Delete Memory");
            memory.CopyMemory();
            memory.DeleteMoveMemory();
        }
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex, 0f));
    }

    //is called from restart Screen if Moves shold be saved
    public void RestartLevelWithMoves() {
        foreach (MoveMemory memory in memorys) {
            memory.DeleteLastMoveMemory();
        }
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex, 0f));
    }

    //Loads next level
    IEnumerator LoadLevel(int levelIndex, float waitTime) {
        //Teleporter Animation
        yield return new WaitForSeconds(waitTime);

        //handle display of end Transition
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        AnimationHandler.PlayEndTransition(transition, currentLevel, levelIndex);
     
        //save actual level 
        lastLevel.value = currentLevel;

        Debug.Log("Load Level" + levelIndex);
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(levelIndex);

    }

    public void GoToMainMenu() {
        foreach (MoveMemory memory in memorys) {
            memory.DeleteCompleteMemory();
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
