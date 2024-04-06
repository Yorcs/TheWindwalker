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
            else
            {
                switch (SceneManager.GetActiveScene().name){
                    case "Dream0":                     
                        FindObjectOfType<DialogueManager>().automaticDialogue(DialogueManager.tutorialIntro);
                        break;
                    case "OutsideLevelZero":
                        if(tag == "CutsceneOne") FindObjectOfType<DialogueManager>().automaticDialogue(DialogueManager.villageIntro);
                        else FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.shopkeeperScene);
                        break;
                    case "Walking1":
                        FindObjectOfType<DialogueManager>().automaticDialogue(DialogueManager.walkingOne);
                        break;
                    case "Walking2":
                        FindObjectOfType<DialogueManager>().automaticDialogue(DialogueManager.walkingTwo);
                        break;
                    /*case "OutsideLevel2":
                        FindObjectOfType<DialogueManager>().automaticDialogue(DialogueManager.walking2);
                        break;
                    case "OutsideLevel3":
                        //FindObjectOfType<DialogueManager>().automaticDialogue(DialogueManager.walking3);
                        break;
                    case "DreamLevelOne":
                        FindObjectOfType<DialogueManager>().automaticDialogue(DialogueManager.dreamLevelOneIntro);
                        break;*/
                    default:
                        FindObjectOfType<DialogueManager>().automaticDialogue(dialogue);
                        break;
                }
                
            }
            Destroy(gameObject);
            return;
        }
  
    }
}
