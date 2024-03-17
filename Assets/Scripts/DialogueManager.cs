using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//manages the display of cutscene dialogue
//https://www.youtube.com/watch?v=_nRzoTzeyxU&t=6s
public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences = new Queue<string>();
    [SerializeField] private GameObject dialogueCanvas;

    //give a dialogue object to show
    public void StartDialogue(Dialogue dialogue)
    {
        dialogueCanvas.SetActive(true);
        sentences.Clear();

        if (GameObject.Find("Name")) GameObject.Find("Name").GetComponent<Text>().text = dialogue.name;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);    
        }
        DisplayNextSentence();
    }

    //displays the next sentence in the queue
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            endDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        if (GameObject.Find("Dialogue")) GameObject.Find("Dialogue").GetComponent<Text>().text = sentence;
    }

    //turns off the dialogue UI
    public void endDialogue()
    {
        dialogueCanvas.SetActive(false);
    }
}
