using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//used by objects to trigger dialogue, holds a dialogue object to trigger
//https://www.youtube.com/watch?v=_nRzoTzeyxU&t=6s
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool onCollideActivate;
    public bool activated = false;
    public bool autoLoad = false;
    public bool stopPlayer = false;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stopPlayer) PlayerController.LockedPlayer = true;

        if (!activated && onCollideActivate)
        {
            if (!autoLoad) TriggerDialogue();
            else if (SceneManager.GetActiveScene().name == "OutsideLevel1")
            {
                FindObjectOfType<DialogueManager>().automaticDialogue(DialogueManager.walking1);
            }
            else if (SceneManager.GetActiveScene().name == "OutsideLevel2")
            {
                FindObjectOfType<DialogueManager>().automaticDialogue(DialogueManager.walking2);
            }
            else if (SceneManager.GetActiveScene().name == "OutsideLevel3")
            {
                FindObjectOfType<DialogueManager>().automaticDialogue(DialogueManager.walking3);
            }
            else if (SceneManager.GetActiveScene().name == "DreamLevelOne")
            {
                FindObjectOfType<DialogueManager>().automaticDialogue(DialogueManager.act2Scene5p1);
            }
            else FindObjectOfType<DialogueManager>().automaticDialogue(dialogue);
        }

    }
}
