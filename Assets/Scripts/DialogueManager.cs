using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEditor.Experimental.GraphView;

//manages the display of cutscene dialogue
//https://www.youtube.com/watch?v=_nRzoTzeyxU&t=6s
public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences = new Queue<string>();
    [SerializeField] private GameObject dialogueCanvas;
    private string currSentence;
    private bool loadingText = false;
    private int imageCounter = 0;


    //cast
    //T = Toyen
    //M = Makobii
    //S = Shopkeeper
    //H = Healer
    //L = stern mother
    //C = children (plural, like its not important which one is saying which cuase it's just noise to Toyen)

    //tutorial introduction
    public static string[] act1Scene1 =
    {//this is the short dialogue that would play during the first tutorial sequence
            "*T Hmm... This area's too empty. I need to venture deeper into my memories.",
            "*T "
    };

    //outside level 0 spawn
    public static string[] act1Scene2p1 =
    {//This is the dialogue that woud play during the walking through the village scene
        "*T Let's go into town...",
        "*L Why would you want to go sleep in the fields outside the village? You know what will come out to get you!",
        "*L I know you think your safe because of the totems, but I remember a time before that safety."
    };

    //outside level 0 exit
     public static string[] act1Scene2p2 =
    {
     //Toyen aproaches the shopkeepers stall
        "*S Toyen! Long time, no see. My favorite nomad, ever-roaming, searching for his long lost family! Any closer?",
        "*T I know nothing more than I did 25 years ago. It is always the same.",
        "*S No return of more memories?",
        "*T Not one.",
        "*S Well, thank you for these.",
        "*S Your last delivery saved my child from a terrible flu.",
        "*S I cannot repay you for what you do for us.",
        "*T There is no need.",
        "*S You know, if you ever want to settle here, in the village, there is a spare room upstairs. I trust you remember that.",
        "*T Thank you, but that won't be necessary.",
        "*S Toyen, not to step on your toes, but you may never find them, you know. It may be time to move on.",
        "*T Only I will decide when that is.",
        "*S Of course, please let me give you something; you look exhausted.",
        "*S Sleeping agent. Use this to rest when you really need it."
    };

    //cutscene 1
    public static string[] act1Scene3 =
    {//Toyen meets the makobii
        "*T What do you want with me?",
        "*M So it is true. They do exist.",
        "*T Speak sense, makobii creature. I know what you are.",
        "*M Toyen. The man who moves with the wind. I have discovered something while walking your dreams. A memory buried so deeply in your soul that it is forever inaccessible.",
        "*T What? How do you--",
        "*M A Forgotten memory. Everything since you were a child -- gone from this world. Such memories are a rarity, but you, you carry one. And its burden, as it seems.",
        "*M I can help you discover it.",
        "*T You are a dream-eater. You want off with it. I couldn't possibly want this enough to agree to such a ridiculous offer. Go away.",
        "*M Tonight. I will appear to you once more. If you do not agree, then you will never see me again."
    };

    //outside level 1
    public static string[] walking1 =
    {
        "*M Hello, Toyen. Have you made your decision?",
        "*T I have. I will journey with you.",
        "*M I see.",
        "*M Then, let's begin. Since your energy is dim we Will have to head to a point where the connection between realms is stronger.",
        "*T Dim? What is that supposed to mean?",
        "*M It means you're quite boring.",
        "*T Does your kindnot have manners?",
        "*M I have learned them from humans. Thank you.",
        "*T What are we even looking for once we are in there?",
        "*M We will find out once we’re there.",
        "*T That’s not very reassuring.",
        "*M How could you be “reassured,” I have not heard of that?",
        "*T Never mind, let’s just begin."
    };

    //cutscene2
    public static string[] act2Scene2 =
   {
        "*M Come. Let us begin.",
        "*M Why is it these people hide inside their villages?",
        "*T They aren't hiding from you. The totems were an afterthought.",
        "*M Then what is the reason?",
        "*T They live together for connection and stability. The people are where they find happiness. They raise their families together.",
        "*M The children's dreams must be rich.",
        "*T They are not your food. This is my dream. Let's go.",
        "*M There is nowhere to go. This is what you are dreaming of; these humans, eating food on a bench.",
        "*M Join them...",
        "*T What? Why?",
        "*M We may learn how to go deeper into your subconscious",
        "*T I would rather not..."
    };

    //cutscene 2 + consequences
    public static string[] act2Scene2choice1 =
    {
        //the makobii is pleased with toyen
        "*T So, what is it that we learn from this?",
        "*M I was wrong, you were so wrapped up in yourself, that your mind doesn't even picture their voices... ",
        "*M You decline these peoples advances. All of them. All the Time. Why?",
        "*T It is for their own good. There is no point of connection if I am to leave again.",
        "*M How masochistic humans can be. "
    };

    //cutscene 2 - consequences
    public static string[] act2Scene2choice2 =
    {
        "*T I can't do it.",
        "*M You decline their advances. All of them. All the time. Why?",
        "*T It is for their own good. There is no point of connection if I am to leave again.",
        "*M Not even in a dream can you allow yourself to experience these connections and stability that you speak of.",
        "*M How masochistic humans can be."
    };

    //outside level 2
    public static string[] walking2 =
    {
        "*M That was fruitless. Your dreams are insufferably boring.",
        "*M I will guide you to places of rest where the energy in my realm is stronger; perhaps that will trigger deeper dreams.",
        "*T So how do we find my forgotten memory?",
        "*M Usually, there would be memories scattered around your dreams.  Normal prey-",
        "*T Pardon?",
        "*M Normal... humans... napping in the forest would have had a couple fragments we could use to guide our journey, but you in your boundless boringness have to dive deeper into your mind to find them...",
        "*T Deeper levels, how would we get there?",
        "*M I’m not sure its possible, when you sleep you are fitful.",
        "*T I was given this sleeping agent by a shopkeeper.",
        "*M You should have tried that earlier.",
        "*T Apologies, creature. My memory is troubled, you should know"
    };

    //dream level one start
    public static string[] dreamLevelOneIntro =
    {
        "*T I’ve never seen a place like this... Where is this location?",
        "*M You willed us here. Do not ask me that."
    };

    //dream level one end
    public static string[] dreamLevelOneEnd =
    {
        "*T Is it one of my memories?",
        "*M Whose would it be but yours?",
        "*T ...",
        "*T If I touch it what happens?",
        "*M You will remember what ever memory it is, but I doubt its your lost memory.",
        "*M It’s likely something else you’ve forgotten over the years.",
        "*M A... small meal... in Makobii terms... but..."
    };

    //dream level one  + consequences
    public static string[] dreamLevelOnePos =
    {
        "*T ...",
        "*T Makobii!",
        "*T Makobii! Answer me!",
        "*T ...",
        "*M You have had a TALISMAN. And did not tell me! Why should I guide you if at any moment you could unleash its power on me!",
        "*T I’m sorry, but makobii aren't exactly harmless, it was for my protection.",
        "*T Besides, if any of us should be angry, it should be me! You tried to eat my memories!",
        "*T While I was being attacked by some monster you were goin to eat my memories! What could I have done instead?",
        "*M That cliff face, that is likely where you will find the memory."
    };

    //dream level one  - consequences
    public static string[] dreamLevelOneNeg =
    {
        "*T A small meal?! ",
        "*T You just want to eat my memory, don't you! That's all you can think about!",
        "*T Makobii!",
        "*T Makobii! Answer me!",
        "*T ...",
        "*M You have had a TALISMAN. And did not tell me! Why should I guide you if at any moment you could unleash its power on me!",
        "*T Nobody would dare to venture with something like you without it.",
        "*T Do you think I’m stupid? Maybe I am, I trusted some dream eater to not eat my dreams and leave me as a husk of the husk I already am!",
        "*M Go to that cliff face, you might find your memories there, if you don’t then jump off it!"
    };



    //post draft
    public static string[] walking4 =
    {
        "*T Makobii!",
        "*T Makobii, did you know humans need to cook food? We're different from animals in that regard",
        "*T Why even tell me to go here if you aren't going to be there..."
    };
    public static string[] act2scene8 =
    {
        "*T Makobii? Is that you?",
        "*T Makobii?",
        "*M You can't fight them, they don't want to kill you, they may mutilate you, but you must stay alive if they want your memory.",
        "*T How is there so many??",
        "*M The Forgotten Trail, once found, appears to all makobii. I knew this would happen... I have been watching for them...",
        "*M You have no more sleeping agent, and you have proven you cannot reach this subconscious without it. ",
        "*M Whatever you do, do not wakeup, or there is nothing we can do.",
        "*M You must run. Get the memory. Leave me here."
    };
    public static string[] act2scene8choice1 =
    {
        "*T No... I made it this far because of you, I'll try to distract some of them.",
        "*T Hopefully lighten the load of how many you'll need to fight.",
        "*M TOYEN! RUN! YOU MUST NOT WAKE UP!"

    };
    public static string[] act2scene8choice2 =
    {
        "*T Okay... Thank you..."
    };
    public static string[] act2scene9conseuences1 =
    {
        "*T MAKOBII!",
        "*T How dare you throw me off?! You instructed me to stay asleep.",
        "*T I cannot believe you, you terrible creature. How could I have agreed to this?",
        "*M I'm sorry, I was too weak, I tried to get you too your memory..."
    };
    public static string[] act2scene9conseuences2 =
    {
        "*T NO!",
        "*T I was so close!",
        "*T How dare you stay there to defend me, tell me to run for it on my own?!",
        "*T You know I can't make it in this place on my own.",
        "*T I cannot believe you, you terrible creature. How could I have agreed to this?",
        "*M We, couldn't just make a dash for it together, there were too many of them"
    };
    public static string[] act2scene9 =
    {
        "*T What... what is wrong with your body?",
        "*M This form. I am... losing control of it.",
        "*T Why?",
        "*M I am starving, Toyen.",
        "*T When... was the last time you ate?",
        "*M Long before we met. Most humans reside in villages now.",
        "*M No matter how long I walked your dreams, I could not take from them, lest I slow down finding your Forgotten memory. The temptation was unimaginable.",
        "*M There is one way, Toyen.",
        "*M I can bring you into my realm using the last of my control. There, you can pursue the memory.",
        "*T And what of you?",
        "*M It does not matter. More makobii will come soon. There is no time.",
        "*T I can knock myself out.",
        "*M No.",
        "*T I know of herbs that can induce a coma.",
        "*M Toyen. There is no time..."
    };
    public static string[] act3scene1 =
    {
        "*T Please… do not make me fight you.",
        "*T We have journeyed for so long together.",
        "*T Please. I just want to end all of this."
    };
    public static string[] act3scene2p1 =
    {
        "*M This is it. This is what you have been searching for all this time.",
        "*M Do you feel those strange, human things? Peace? Happiness?",
        "*T Makobii, why are you not devouring it? You are right here.",
        "*M We are not in your mind, we are in a shared dream, here we are equals, and you got to the memory first...",
        "*T What… happens after?",
        "*M When you leave this place, I will likely die."
    };
    public static string[] act3scene2p2 =
    {
        "*T Before we met, I had not journeyed with another thing since I lost my memories.",
        "*T You were my first companion, yet I treated you horribly. I panicked and nearly killed you.",
        "*M I do not blame you. I did not care for you at all.",
        "*M I did everything in my power to get to the Forgotten memory as fast as possible, even if it meant tormenting you.",
        "*M I was terrible to you across our entire journey.",
        "*T I knew you never truly wanted to help me, makobii.",
        "*T Humans know, deep down when they are being deceived, even if they still hope they aren't.",
        "*M I did not at first. But after our time together, I do not regret what I have done."
    };

    public static string act3scene2karmahigh = "*T Thank you for showing me the truth. Even if only for a moment.";

    public static string[] act3scene3endinggood =
    {
        "*T Makobii.",
        "*M Why… why would you do such a thing? You won. Yet--",
        "*T Sacrifice. It is called sacrifice.",
        "*T I… never needed this to begin with.",
        "*T I needed to be reminded of what was in front of me. You gave me that chance.",
        "*M Thank you, Toyen, for saving my life.",
        "*T That is gratitude. You have mastered mimicking it.",
        "*M I did not try to. Perhaps I now understand."
    };

    public static string[] act3scene3endingbad =
    {
        "*T I remember now...",
        "*T You Makobii really are tricksters, leading me all this way in your act of desperation! what a joke--",
        "*T All this for what! your death? All that just for me to continue my search!",
        "*T Nothings changed! Ha! Nothing!"
    };
    public static string[] act3scene4endinggood =
    {
        "*S Toyen! Welcome! Did you complete your mission?",
        "*T No. I did not.",
        "*S Oh… my apologies. Back on the road soon, then?",
        "*T I was thinking that I may stay for awhile. Is that room you spoke of still available?"
    };
    public static string[] act3scene4endingbad =
    {
        "*T They're wrong, I'm so close.",
        "*T After all these years, I can see their faces.",
        "*T I can remember where they were.",
        "*T I'm so close..."
    };
    //give a dialogue object to show
    public void StartDialogue(Dialogue dialogue)
    {
        dialogueCanvas.SetActive(true);
        dialogueCanvas.GetComponent<CanvasGroup>().alpha = 1;
        dialogueCanvas.GetComponent<CanvasGroup>().interactable = true;
        dialogueCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
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

        PlayerController.LockedPlayer = true;

        DisplayNextSentence();
    }

    public void StartDialogue(string[] dialogue)
    {
        dialogueCanvas.SetActive(true);
        dialogueCanvas.GetComponent<CanvasGroup>().alpha = 1;
        dialogueCanvas.GetComponent<CanvasGroup>().interactable = true;
        dialogueCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        sentences.Clear();

        foreach (string sentence in dialogue)
        {
            sentences.Enqueue(sentence);
        }

        if (GameObject.Find("ContinueButton"))
        {
            GameObject.Find("ContinueButton").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("ContinueButton").GetComponent<CanvasGroup>().interactable = true;
        }

        PlayerController.LockedPlayer = true;

        DisplayNextSentence();
    }

    IEnumerator ProgressSentence(string sentence)
    {
        //load out each character in the sentence one by one to be more dynamic
        currSentence = sentence.Substring(3);
        loadingText = true;
        if (GameObject.Find("Dialogue"))
        {
            GameObject.Find("Dialogue").GetComponent<Text>().text = "";
            foreach (char letter in currSentence.ToCharArray())
            {
                GameObject.Find("Dialogue").GetComponent<Text>().text += letter;
                yield return new WaitForSeconds(0.03f);
            }
            //GameObject.Find("Dialogue").GetComponent<Text>().text = sentence.Substring(3);
        }
        loadingText = false;
    }

    //displays the next sentence in the queue
    public void DisplayNextSentence()
    {

        if (loadingText)
        {
            StopAllCoroutines();
            if (GameObject.Find("Dialogue")) GameObject.Find("Dialogue").GetComponent<Text>().text = currSentence;
            loadingText = false;
        }
        else
        {
            if (sentences.Count == 0)
            {
                endDialogue();
                return;
            }

            imageCounter++;
            //Debug.Log("/ UIAssets / Cutscenes / A1S3 / a1s3_" + imageCounter + ".png");
            Sprite temp;

            if (GameObject.Find("CutsceneImage"))
            {
                switch (SceneManager.GetActiveScene().name)
                {
                    case "Act1Scene3":
                        if (imageCounter < act1Scene3.Length)
                        {
                            temp = Resources.Load<Sprite>("Sprites/A1S3/a1s3_" + imageCounter);
                            GameObject.Find("CutsceneImage").GetComponent<Image>().sprite = temp;
                        }
                        break;
                    case "Act2Scene2":
                        if (imageCounter < act2Scene2.Length && !ChoiceManager.scene2Done)
                        {
                            temp = Resources.Load<Sprite>("Sprites/A2S2/a2s2_" + imageCounter);
                            GameObject.Find("CutsceneImage").GetComponent<Image>().sprite = temp;
                        }
                        else
                        {
                            if (imageCounter < act2Scene2.Length + act2Scene2choice1.Length && ChoiceManager.scene2Done)
                            {
                                if(ChoiceManager.scene2) temp = Resources.Load<Sprite>("Sprites/A2S2/a2s2c1_" + imageCounter);
                                else temp = Resources.Load<Sprite>("Sprites/A2S2/a2s2c2_" + imageCounter);
                                GameObject.Find("CutsceneImage").GetComponent<Image>().sprite = temp;
                            }
                        }
                        break;

                }
            }


            string sentence = sentences.Dequeue();
            switch (sentence.Substring(0, 2))
            {
                case "*T":
                    if (GameObject.Find("Name")) GameObject.Find("Name").GetComponent<Text>().text = "Toyen";
                    break;
                case "*M":
                    if (GameObject.Find("Name")) GameObject.Find("Name").GetComponent<Text>().text = "Makobii";
                    break;
                case "*S":
                    if (GameObject.Find("Name")) GameObject.Find("Name").GetComponent<Text>().text = "Shopkeeper";
                    break;
                case "*H":
                    if (GameObject.Find("Name")) GameObject.Find("Name").GetComponent<Text>().text = "Healer";
                    break;
                case "*L":
                    if (GameObject.Find("Name")) GameObject.Find("Name").GetComponent<Text>().text = "Stern Mother";
                    break;
                case "*C":
                    if (GameObject.Find("Name")) GameObject.Find("Name").GetComponent<Text>().text = "Child";
                    break;

            }
            if (FindAnyObjectByType<CameraManager>())
            {
                if (sentences.Count % 2 == 0)
                {
                    FindAnyObjectByType<CameraManager>().cam1.enabled = false;
                    FindAnyObjectByType<CameraManager>().cam2.enabled = true;
                }
                else
                {
                    FindAnyObjectByType<CameraManager>().cam2.enabled = false;
                    FindAnyObjectByType<CameraManager>().cam1.enabled = true;
                }

            }
            StartCoroutine(ProgressSentence(sentence));
        }     
    }

    //turns off the dialogue UI
    public void endDialogue()
    {
        if (FindAnyObjectByType<CameraManager>())
        {
            FindAnyObjectByType<CameraManager>().cam1.enabled = false;
            FindAnyObjectByType<CameraManager>().cam2.enabled = false;
            Camera.main.enabled = true;
        }

        PlayerController.LockedPlayer = false;
        //dialogueCanvas.SetActive(false);
        dialogueCanvas.GetComponent<CanvasGroup>().alpha = 0;
        dialogueCanvas.GetComponent<CanvasGroup>().interactable = false;
        dialogueCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        /*switch (SceneManager.GetActiveScene().name)
        {
            case "Act1Scene2":
                SceneManager.LoadScene("Act1Scene3");
                break;
            case "Act1Scene3":
                SceneManager.LoadScene("OutsideLevel1");
                break;
        }*/
        if ((SceneManager.GetActiveScene().name == "Act2Scene2" && !ChoiceManager.scene2Done) ||
            (SceneManager.GetActiveScene().name == "DreamLevelOneEnd" && !ChoiceManager.level1Done))     
        {
            FindObjectOfType<ChoiceManager>().GetComponent<CanvasGroup>().alpha = 1;
            FindObjectOfType<ChoiceManager>().GetComponent<CanvasGroup>().interactable = true;
        }
        if (SceneManager.GetActiveScene().name == "Act1Scene3" ||
            (SceneManager.GetActiveScene().name == "Act2Scene2" && ChoiceManager.scene2Done) ||
            (SceneManager.GetActiveScene().name == "DreamLevelOneEnd" && ChoiceManager.level1Done))
        {
            FindObjectOfType<FadeTransition>().startFade();
        }
    }

    //automaticaly load out dialogue
    public void automaticDialogue(Dialogue dialogue)
    {
        dialogueCanvas.SetActive(true);
        dialogueCanvas.GetComponent<CanvasGroup>().alpha = 1;
        dialogueCanvas.GetComponent<CanvasGroup>().interactable = true;
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

    //automaticaly load out dialogue
    public void automaticDialogue(string[] dialogue)
    {
        dialogueCanvas.SetActive(true);
        dialogueCanvas.GetComponent<CanvasGroup>().alpha = 1;
        dialogueCanvas.GetComponent<CanvasGroup>().interactable = true;
        sentences.Clear();


        foreach (string line in dialogue)
        {
            sentences.Enqueue(line);
        }

        if (GameObject.Find("ContinueButton"))
        {
            GameObject.Find("ContinueButton").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("ContinueButton").GetComponent<CanvasGroup>().interactable = false;
        }
        Debug.Log("showing ida");
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
        switch (sentence.Substring(0, 2))
        {
            case "*T":
                if (GameObject.Find("Name")) GameObject.Find("Name").GetComponent<Text>().text = "Toyen";
                break;
            case "*M":
                if (GameObject.Find("Name")) GameObject.Find("Name").GetComponent<Text>().text = "Makobii";
                break;
            case "*S":
                if (GameObject.Find("Name")) GameObject.Find("Name").GetComponent<Text>().text = "Shopkeeper";
                break;
            case "*H":
                if (GameObject.Find("Name")) GameObject.Find("Name").GetComponent<Text>().text = "Healer";
                break;
            case "*L":
                if (GameObject.Find("Name")) GameObject.Find("Name").GetComponent<Text>().text = "Stern Mother";
                break;
            case "*C":
                if (GameObject.Find("Name")) GameObject.Find("Name").GetComponent<Text>().text = "Child";
                break;

        }
        if (GameObject.Find("Dialogue")) GameObject.Find("Dialogue").GetComponent<Text>().text = sentence.Substring(3);
        StartCoroutine("showSentence");
    }

    IEnumerator showSentence()
    {
        yield return new WaitForSeconds(2.5f);
        autoLoad();
    }

}
