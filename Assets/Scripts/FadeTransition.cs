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
                    SceneManager.LoadScene("DreamTutorial"); //switch to cutscene #1
                    break;
                /*case "MainMenu": //cutscene1
                    SceneManager.LoadScene("DreamTutorial"); 
                    break;*/
                case "DreamTutorial":
                    SceneManager.LoadScene("OutsideLevelZero"); //switch to cutscene #2
                    break;
                /*case "DreamTutorial": //cutscene2
                    SceneManager.LoadScene("OutsideLevelZero"); //switch to village
                    break;*/
                case "OutsideLevelZero":
                    SceneManager.LoadScene("Act1Scene3"); //makobii intro cutscene
                    break;
                case "Act1Scene3": //makobii intro cutscene
                    SceneManager.LoadScene("OutsideLevel1"); //walkingone
                    break;
                case "OutsideLevel1": //walking one
                    SceneManager.LoadScene("Act2Scene2");//dream 1
                    ChoiceManager.shardsFound++;
                    break;
                case "Act2Scene2": //family picnic dream
                    SceneManager.LoadScene("OutsideLevel2"); //walking two
                    break;
                case "OutsideLevel2": //walkingtwo
                    SceneManager.LoadScene("DreamLevelOne"); //dream 2 intro cutscene
                    ChoiceManager.shardsFound++;
                    break;
                /*case "OutsideLevel2": //dream 2 intro cutscene
                    SceneManager.LoadScene("DreamLevelOne"); //dream 2
                    ChoiceManager.shardsFound++;
                    break;
                case "OutsideLevel2": //dream 2
                    SceneManager.LoadScene("DreamLevelOne"); //dream 2 end cutscene
                    ChoiceManager.shardsFound++;
                    break;*/
                case "DreamLevelOne": //dream 3 intro cutscene
                    SceneManager.LoadScene("DreamLevelOneEnd"); //dream 3
                    break;
                /*case "DreamLevelOne": //dream 3
                    SceneManager.LoadScene("DreamLevelOneEnd"); //dream 3 end cutscene 
                    break;
                 case "DreamLevelOne": //dream 3 end
                    SceneManager.LoadScene("DreamLevelOneEnd"); dream 4      
                    break;
                case "DreamLevelOne": //dream 4
                    SceneManager.LoadScene("DreamLevelOneEnd"); //dream 4 end cutscene
                    break;
                case "DreamLevelOne": //dream 4 end cutscene
                    SceneManager.LoadScene("DreamLevelOneEnd"); //finale cutscene
                    break; */
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
