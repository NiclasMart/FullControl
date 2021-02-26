using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class AnimationHandler {

    //only play transition to begin of a scene if a completly new scene is loaded
    public static void PlayStartTransition(Animator transition, int lastLevel, int currentLevel) {
        if (lastLevel != currentLevel) {
            //set transition Text
            Text levelText = transition.transform.Find("Text").GetComponent<Text>();
            levelText.text = "Level " + currentLevel.ToString();
            //start transition
            transition.SetTrigger("LevelStartTrigger");

            //saves level to progress
            SaveSystem.SaveData(currentLevel);
        }
        else {
            transition.SetTrigger("LevelStartDontShowTrigger");
        }
    }

    //Transition only if a new scene is loaded
    public static void PlayEndTransition(Animator transition, int currentLevel, int nextLevel) {
        if (nextLevel != currentLevel) {
            //set transition Text
            Text levelText = transition.transform.Find("Text").GetComponent<Text>();
            levelText.text = "Level " + nextLevel.ToString();
            //start transition
            transition.SetTrigger("LevelEndTrigger");
        }
    }
    
}
