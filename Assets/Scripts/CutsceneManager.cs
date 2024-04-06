using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    private void Awake()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Cutscene1":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.introCut);
                break;
            case "Cutscene2":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.tutorialEnd);
                break;
            case "Cutscene3":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.makobiiIntro);
                break;
            case "Dream1":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.dreamOne);
                break;
            case "Cutscene4":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.dreamTwo);
                break;
            case "Cutscene5":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.dreamTwoEnd);
                break;
            case "Cutscene6":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.dreamThree);
                break;
            case "Cutscene7":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.dreamThreeEnd);
                break;
            case "Cutscene8":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.dreamFourEnd);
                break;
            case "Cutscene9":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.finalScene);
                break;
        }
    }
}
