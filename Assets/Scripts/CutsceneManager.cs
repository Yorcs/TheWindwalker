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
            case "Act1Scene2":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act1Scene2);
                break;
            case "Act1Scene3":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act1Scene3);
                break;
            case "Act1Scene4":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act1Scene4);
                break;
            case "Act2Scene1":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act2Scene1);
                break;
            case "Act2Scene2":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act2Scene2);
                break;
            case "Act2Scene3":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act2Scene3);
                break;
            case "Act2Scene4":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act2Scene4);
                break;
            case "Act2Scene5P1":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act2Scene5p1);  
                break;
            case "Act2Scene5P2":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act2Scene5p2);
                break;
            case "Act2Scene8":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act2scene8);
                break;
            case "Act2Scene9":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act2scene9);
                break;
            case "Act3Scene1":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act3scene1);
                break;
            case "Act3Scene2P1":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act3scene2p1);
                break;
            case "Act3Scene2P2":
                FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act3scene2p2);
                break;
            case "Act3Scene3":
                if(ChoiceManager.goodKarma) FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act3scene3endinggood);
                else FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act3scene3endingbad);
                break;
            case "Act3Scene4":
                if (ChoiceManager.goodKarma) FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act3scene4endinggood);
                else FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act3scene4endingbad);
                break;
        }
    }
}
