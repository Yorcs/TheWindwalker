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
        }
    }
}
