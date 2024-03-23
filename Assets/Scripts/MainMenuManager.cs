using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button play;
    public Button quit;

    void Start()
    {
        play.onClick.AddListener(startGame);
        quit.onClick.AddListener(quitGame);
    }

    public void startGame()
    {
        FindObjectOfType<FadeTransition>().startFade();
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
