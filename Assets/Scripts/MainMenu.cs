using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void onStartPress() {
        SceneManager.LoadScene("Prueba nuves dinamicas", LoadSceneMode.Single);
    }


    public void onExitPress() {
        Application.Quit();
    }
}
