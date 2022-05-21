using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {
    public void Play() {
        SceneManager.LoadScene("Game");
    }

    public void GoToOptions() {

    }

    public void ReturnToMenu() {

    }

    public void Quit() {
        Application.Quit();
    }
}
