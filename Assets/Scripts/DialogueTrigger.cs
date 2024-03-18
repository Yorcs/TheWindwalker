using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used by objects to trigger dialogue, holds a dialogue object to trigger
//https://www.youtube.com/watch?v=_nRzoTzeyxU&t=6s
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool onCollideActivate;
    public bool activated = false;
    public bool autoLoad = false;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!activated && onCollideActivate)
        {
            if(!autoLoad) TriggerDialogue();
            else FindObjectOfType<DialogueManager>().automaticDialogue(dialogue);
        }

    }
}
