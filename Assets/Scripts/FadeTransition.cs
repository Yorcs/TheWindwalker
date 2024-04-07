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
            SceneManager.sceneLoaded += OnSceneLoaded;

            yield return new WaitForSeconds(1);
            switch (SceneManager.GetActiveScene().name)
            {
                case "MainMenu":
                    SceneManager.LoadScene("Cutscene1");
                    break;
                case "Cutscene1":
                    SceneManager.LoadScene("Dream0"); 
                    break;
                case "Dream0":
                    SceneManager.LoadScene("Cutscene2");
                    break;
                case "Cutscene2":
                    SceneManager.LoadScene("OutsideLevelZero");
                    break;
                case "OutsideLevelZero":
                    SceneManager.LoadScene("Cutscene3"); 
                    break;
                case "Cutscene3":
                    SceneManager.LoadScene("Walking1");
                    break;
                case "Walking1":
                    SceneManager.LoadScene("Dream1");
                    break;
                case "Dream1":
                    SceneManager.LoadScene("Walking2");
                    ChoiceManager.shardsFound++;
                    break;
                case "Walking2":
                    SceneManager.LoadScene("Dream2");
                    break;
                /*case "Cutscene4":
                    SceneManager.LoadScene("Dream2");
                    break;*/
                case "Dream2":
                    SceneManager.LoadScene("Cutscene5");
                    ChoiceManager.shardsFound++;
                    break;
                case "Cutscene5":
                    SceneManager.LoadScene("Cutscene6");
                    break;
                case "Cutscene6":
                    SceneManager.LoadScene("Dream3");
                    break;
                case "Dream3":
                    SceneManager.LoadScene("Cutscene7");
                    ChoiceManager.shardsFound++;
                    break;
                case "Cutscene7":
                    SceneManager.LoadScene("Dream4");
                    break;
                case "Dream4":
                    SceneManager.LoadScene("Cutscene8");
                    break;
                case "Cutscene8":
                    SceneManager.LoadScene("Cutscene9");
                    break;
                case "Credits":
                    SceneManager.LoadScene("MainMenu");
                    break;
            }          
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(fadeOut());
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
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }


    public void startFade()
    {
            StartCoroutine(fade());
    }
}
