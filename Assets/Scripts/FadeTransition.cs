using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTransition : MonoBehaviour
{
    public CanvasGroup fadeObject;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    
    IEnumerator fade()
    {
        if (fadeObject.alpha < 1)
        {
            fadeObject.alpha += 0.1f;
            yield return new WaitForSeconds(0.03f);
            StartCoroutine(fade());
        }
        else
        {
            yield return new WaitForSeconds(1);
            switch (SceneManager.GetActiveScene().name)
            {
                case "MainMenu":
                    SceneManager.LoadScene("DreamTutorial");
                    break;
                case "DreamTutorial":
                    SceneManager.LoadScene("Act1Scene2");
                    break;
                case "Act1Scene2":
                    SceneManager.LoadScene("Act1Scene3");
                    break;
                case "Act1Scene3":
                    SceneManager.LoadScene("OutsideLevel1");
                    break;
                case "OutsideLevel1":
                    SceneManager.LoadScene("Act1Scene4");
                    break;
                case "Act1Scene4":
                    SceneManager.LoadScene("OutsideLevel2");
                    break;
            }     
            StartCoroutine(fadeOut());
        }
    }

    IEnumerator fadeOut()
    {
        if (fadeObject.alpha > 0)
        {
            fadeObject.alpha -= 0.1f;
            yield return new WaitForSeconds(0.03f);
            StartCoroutine(fadeOut());
        }
        else
        {
            StopAllCoroutines();
        }
    }


    public void startFade()
    {
            StartCoroutine(fade());
    }
}
