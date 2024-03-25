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
                    SceneManager.LoadScene("OutsideLevelZero");
                    break;
                case "OutsideLevelZero":
                    SceneManager.LoadScene("Act1Scene3");
                    break;
                case "Act1Scene3":
                    SceneManager.LoadScene("OutsideLevel1");
                    break;
                case "OutsideLevel1":
                    SceneManager.LoadScene("Act2Scene2");
                    ChoiceManager.shardsFound++;
                    break;
                case "Act2Scene2":
                    SceneManager.LoadScene("OutsideLevel2");
                    break;
                case "OutsideLevel2":
                    SceneManager.LoadScene("DreamLevelOne");
                    ChoiceManager.shardsFound++;
                    break;
                case "DreamLevelOne":
                    SceneManager.LoadScene("DreamLevelOneEnd");            
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
