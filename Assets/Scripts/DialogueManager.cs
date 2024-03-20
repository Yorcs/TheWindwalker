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
    //cast
    //T = Toyen
    //M = Makobii
    //S = Shopkeeper
    //H = Healer
    //L = stern mother
    //C = children (plural, like its not important which one is saying which cuase it's just noise to Toyen)

    //ACT 1
    public static string[] act1Scene1 = 
    {//this is the short dialogue that would play during the first tutorial sequence
            "*T Hmm... This areas too empty, I need to venture deeper"
            "*T "
    };
    public static string[] act1Scene2 =
    {//This is the dialogue that woud play during the walking through the village scene
        "*C GRR!"
        "*C RUN! THE MAKOBII WILL STEAL YOUR DREAMS! Hahaha!"
        "*L Why would you want to go sleep in the fields outside the village? You know what will come out to get you!"
        "*L I know you think your safe because of the totems, but I remember a time before that safety."
     //Toyen aproaches the shopkeepers stall
        "*S Toyen! Long time, no see. My favorite nomad, ever-roaming, searching for his long lost family! Any closer?"
        "*T I know nothing more than I did 25 years ago. It is always the same."
        "*S No return of more memories?"
        "*T Not one."
        "*S Well, thank you for these."
        "*S Your last delivery saved my child from a terrible flu."
        "*S I cannot repay you for what you do for us."
        "*T There is no need."
        "*T You know, if you ever want to settle here, in the village, there is a spare room upstairs. I trust you remember that."
        "*T Thank you, but that won't be necessary."
        "*S Toyen, not to step on your toes, but you may never find them, you know. It may be time to move on."
        "*T Only I will decide when that is."
        "*S Of course."
        "*S Please let me give you something; you look exhausted. Use this to rest when you really need it."
     //Toyen walks away, while walking he walks past the healers stall
        "*H Toyen, I have something you need!"
        "*T I have all I need for now. Save your trinkets for another."
        "*H You take me for a woman with no knowledge of the soul? Pfah! Some of us can sense stubborn facades..."
        "*H ...And some of us can sense a little more. ...Something will be offered to you, Toyen. And to accept it safely, you accept this first."
        "*T ...Please. I could not possibly take--"
        "*H Do you need to be reminded? I too walked without the protection of grand village totems once. Many of us did. And this is what we managed with."
        "*T ..."
        "*H But it's just a relic now. I wasn't even the one who made it! Let me give it to someone who can still make some use of it."
        "*T ...You have my thanks."
        "*H Any time! Now off you go, grand things are coming your way!"
    }
    public static string[] act1Scene3 =
    {//Toyen meets the makobii
        "*T What do you want with me?"
        "*M So it is true. They do exist."
        "*T Speak sense, makobii creature. I know what you are."
        "*M Toyen. The man who moves with the wind. I have discovered something while walking your dreams. A memory buried so deeply in your soul that it is forever inaccessible."
        "*T What? How do you--"
        "*M A Forgotten memory. Everything since you were a child -- gone from this world. Such memories are a rarity, but you, you carry one. And its burden, as it seems."
        "*M I can helpyou discover it."
        "*T You are a dream-eater. You want off with it. I couldn't possibly want this enough to agree to such a ridiculous offer. Go away."
        "*M Tonight. I will appear to you once more. If you do not agree, then you will never see me again."
    }
    public static string[] Walking1 =
    {//this section should feature a little bit of debate from Toyen whiles hes walking to the campsite
        "*T A Makobii, it must be desperate, getting that close to a village, offering a deal..."
        "*T It surely intends to eat whatever it thinks it can find..."
        "*T I might be just as desperate as it is..."
    }
    public static string[] act1Scene4 =
    {
        "*M Hello, Toyen. Have you made your decision?"
        "*T I have. I will journey with you."
        "*M I see."
        "*M I will leave you alone tonight. We will begin tomorrow."
    }
    public static string[] Walking2 =
    {
        "*M Your energy is dim in our plane. You are almost invisible. Other makobii would barely notice you."
        "*T What is that supposed to mean? And why are you following me?"
        "*M It means you're quite boring."
        "*T Does your kindnot have manners?"
        "*M I have learned them from humans. Thank you."
        "*T No. I would have had to do something for you that you are grateful for. Which I have not."
        "*M That, I do not understand. Gratitude is very strange."
        "*T You are strange. Go away."
    }
    public static string[] act2Scene1 = 
    {
        "*M How often do humans eat?"
        "*T We need a lot of food to stay alive. Many say three meals a day. I of course need a little extra, with how much we had to move today."
        "*M I see. Thank you."
        "*T You don't mean that."
        "*T what are we going to do once we are inside my dream?"
        "*M We will find out once we're there. "
        "*T That's not very reassuring."
        "*M How could you be “reassured,” I have not heard of that?"
        "*T Never mind, let's just begin. "
    }
    public static string[] act2Scene2 = 
    {
        "*M Come. Let us begin."
        "*M Why is it these people hide inside their villages?"
        "*T They aren't hiding from you. The totems were an afterthought."
        "*M Then what is the reason?"
        "*T They live together for connection and stability. The people are where they find happiness. They raise their families together."
        "*M The children's dreams must be rich."
        "*T They are not your food. This is my dream. Let's go."
        "*M There is nowhere to go. This is what you are dreaming of; these humans, eating food on a bench."
        "*M Join them..."
        "*T What? Why?"
        "*M We may learn how to go deeper into your subconscious"
        "*T I would rather not..."
    }
    public static string[] act2Scene2choice1 = 
    {

    }
        

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

        if (GameObject.Find("ContinueButton"))
        {
            GameObject.Find("ContinueButton").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("ContinueButton").GetComponent<CanvasGroup>().interactable = true;
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

    //automaticaly load out dialogue
    public void automaticDialogue(Dialogue dialogue)
    {
        dialogueCanvas.SetActive(true);
        sentences.Clear();

        if (GameObject.Find("Name")) GameObject.Find("Name").GetComponent<Text>().text = dialogue.name;

        foreach (string line in dialogue.sentences)
        {
            sentences.Enqueue(line);
        }

        if (GameObject.Find("ContinueButton"))
        {
            GameObject.Find("ContinueButton").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("ContinueButton").GetComponent<CanvasGroup>().interactable = false;
        }

        autoLoad();
    }

    public void autoLoad()
    {
        if (sentences.Count == 0)
        {
            endDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        if (GameObject.Find("Dialogue")) GameObject.Find("Dialogue").GetComponent<Text>().text = sentence;
        StartCoroutine("showSentence");
    }

    IEnumerator showSentence()
    {
        yield return new WaitForSeconds(2);
        autoLoad();
    }
}
