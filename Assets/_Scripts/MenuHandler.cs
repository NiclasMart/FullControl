using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {

    public Animator levelTransition;
    public Animator menuTransition;

    private void Start() {
        AudioManager.instance.Play("menuMusic");
        levelTransition.SetTrigger("LevelStartTrigger");
    }

    //is calles if the "continue button is pressed
    public void ContinueGame() {
        int level = SaveSystem.LoadData();
        if (level != -1) {
            StartCoroutine(LoadLevel(level));
        }
        else {
            menuTransition.SetTrigger("LoadingErrorTrigger");
            Debug.LogWarning("No existing Game.");
        }
    }

    //is called if "new Game" button is pressed
    public void ShowNewGameDecisionScreen() {
        //if save data exists, show decision screen
        //else load new game immediately
        if (SaveSystem.SaveDataExists()) {
            //Transition
            menuTransition.SetTrigger("NewGamePanelTrigger");
        }
        else {
            StartNewGame();
        }

    }

    //is called if "no" button for new game is pressed
    public void ShowNormalMenuScreen() {
        menuTransition.SetTrigger("NormalMenuTrigger");
    }

    //plays sound if mouse hovers over button
    public void PlayButtonHoverSound() {
        AudioManager.instance.Play("buttonHover");
    }

    //plays sound if mouse clicks on button
    public void PlayButtonClickSound() {
        AudioManager.instance.Play("buttonClick");
    }

    //is called if a new game is startet
    public void StartNewGame() {
        //Delete old save
        StartCoroutine(LoadLevel(1));
    }

    //Loads next level
    IEnumerator LoadLevel(int levelIndex) {
        //Transition
        StartCoroutine(AudioManager.instance.FadeOut("menuMusic", 0.05f));
        AnimationHandler.PlayEndTransition(levelTransition, 0, levelIndex);
        Debug.Log("Load Level" + levelIndex);
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(levelIndex);

    }

    public void EndGame() {
        Application.Quit();
    }
}
