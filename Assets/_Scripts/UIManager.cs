using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameManager gameManager;

    public GameObject planningScreen;
    public GameObject reloadScreen;

    public void InitializeUI() {
        reloadScreen.SetActive(false);
        planningScreen.SetActive(true);
    }

    public void InitializePlayModeUI() {
        planningScreen.SetActive(false);
    }

    public void DisplayRelodingScreen() {
        reloadScreen.SetActive(true);
    }

    public void CloseReloadingScreen() {
        reloadScreen.SetActive(false);
    }
}
