using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public CanvasGroup canvas;
    public Button resume;
    public Button quit;

    private void Start()
    {
        resume.onClick.AddListener(resumeGame);
        quit.onClick.AddListener(quitGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canvas.alpha == 0)
            {
                Cursor.lockState = CursorLockMode.Confined;
                canvas.alpha = 1;
                canvas.interactable = true;
                canvas.blocksRaycasts = true;
            }
            else
            {
                resumeGame();
            }           
        }
    }

    private void resumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }

    private void quitGame()
    {
            Application.Quit();
    }
}
