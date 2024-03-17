using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interactable object which triggers a cutscene
public class TriggerCutscene : MonoBehaviour, IInteractable
{
    public GameObject cutscene;

    public void Interact()
    {
        cutscene.SetActive(true);
        GameObject.Find("ContinueButton").GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}
