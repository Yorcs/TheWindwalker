using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//teleport objects at end of levels to transition to the next
public class Teleport : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        StopAllCoroutines();
        FindObjectOfType<FadeTransition>().startFade();
        /*switch (SceneManager.GetActiveScene().name)
        {
            case "DreamTutorial":
                SceneManager.LoadScene("Act1Scene2");
                break;
            case "OutsideLevel1":
                SceneManager.LoadScene("DreamLevelOne");
                break;
            case "DreamLevelOne":
                SceneManager.LoadScene("DreamLevelTwo");
                break;
            case "DreamLevelTwo":
                SceneManager.LoadScene("DreamLevelThree");
                break;

        }*/
    }
}
