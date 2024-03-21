using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//manages the display of cutscene dialogue
//https://www.youtube.com/watch?v=_nRzoTzeyxU&t=6s
public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences = new Queue<string>();
    [SerializeField] private GameObject dialogueCanvas;
    private string currSentence;
    private bool loadingText = false;

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
            "*T Hmm... This area's too empty. I need to venture deeper.",
            "*T "
    };
    public static string[] act1Scene2 =
    {//This is the dialogue that woud play during the walking through the village scene
        "*C GRR!",
        "*C RUN! THE MAKOBII WILL STEAL YOUR DREAMS! Hahaha!",
        "*L Why would you want to go sleep in the fields outside the village? You know what will come out to get you!",
        "*L I know you think your safe because of the totems, but I remember a time before that safety.",
     //Toyen aproaches the shopkeepers stall
        "*S Toyen! Long time, no see. My favorite nomad, ever-roaming, searching for his long lost family! Any closer?",
        "*T I know nothing more than I did 25 years ago. It is always the same.",
        "*S No return of more memories?",
        "*T Not one.",
        "*S Well, thank you for these.",
        "*S Your last delivery saved my child from a terrible flu.",
        "*S I cannot repay you for what you do for us.",
        "*T There is no need.",
        "*T You know, if you ever want to settle here, in the village, there is a spare room upstairs. I trust you remember that.",
        "*T Thank you, but that won't be necessary.",
        "*S Toyen, not to step on your toes, but you may never find them, you know. It may be time to move on.",
        "*T Only I will decide when that is.",
        "*S Of course.",
        "*S Please let me give you something; you look exhausted. Use this to rest when you really need it.",
     //Toyen walks away, while walking he walks past the healers stall
        "*H Toyen, I have something you need!",
        "*T I have all I need for now. Save your trinkets for another.",
        "*H You take me for a woman with no knowledge of the soul? Pfah! Some of us can sense stubborn facades...",
        "*H ...And some of us can sense a little more. ...Something will be offered to you, Toyen. And to accept it safely, you accept this first.",
        "*T ...Please. I could not possibly take--",
        "*H Do you need to be reminded? I too walked without the protection of grand village totems once. Many of us did. And this is what we managed with.",
        "*T ...",
        "*H But it's just a relic now. I wasn't even the one who made it! Let me give it to someone who can still make some use of it.",
        "*T ...You have my thanks.",
        "*H Any time! Now off you go, grand things are coming your way!"
    };
    public static string[] act1Scene3 =
    {//Toyen meets the makobii
        "*T What do you want with me?",
        "*M So it is true. They do exist.",
        "*T Speak sense, makobii creature. I know what you are.",
        "*M Toyen. The man who moves with the wind. I have discovered something while walking your dreams. A memory buried so deeply in your soul that it is forever inaccessible.",
        "*T What? How do you--",
        "*M A Forgotten memory. Everything since you were a child -- gone from this world. Such memories are a rarity, but you, you carry one. And its burden, as it seems.",
        "*M I can helpyou discover it.",
        "*T You are a dream-eater. You want off with it. I couldn't possibly want this enough to agree to such a ridiculous offer. Go away.",
        "*M Tonight. I will appear to you once more. If you do not agree, then you will never see me again."
    };
    public static string[] walking1 =
    {//this section should feature a little bit of debate from Toyen whiles hes walking to the campsite
        "*T A Makobii, it must be desperate, getting that close to a village, offering a deal...",
        "*T It surely intends to eat whatever it thinks it can find...",
        "*T I might be just as desperate as it is..."
    };
    public static string[] act1Scene4 =
    {
        "*M Hello, Toyen. Have you made your decision?",
        "*T I have. I will journey with you.",
        "*M I see.",
        "*M I will leave you alone tonight. We will begin tomorrow."
    };
    public static string[] walking2 =
    {
        "*M Your energy is dim in our plane. You are almost invisible. Other makobii would barely notice you.",
        "*T What is that supposed to mean? And why are you following me?",
        "*M It means you're quite boring.",
        "*T Does your kindnot have manners?",
        "*M I have learned them from humans. Thank you.",
        "*T No. I would have had to do something for you that you are grateful for. Which I have not.",
        "*M That, I do not understand. Gratitude is very strange.",
        "*T You are strange. Go away."
    };
    public static string[] act2Scene1 =
    {
        "*M How often do humans eat?",
        "*T We need a lot of food to stay alive. Many say three meals a day. I of course need a little extra, with how much we had to move today.",
        "*M I see. Thank you.",
        "*T You don't mean that.",
        "*T what are we going to do once we are inside my dream?",
        "*M We will find out once we're there. ",
        "*T That's not very reassuring.",
        "*M How could you be 'reassured,' I have not heard of that?",
        "*T Never mind, let's just begin. "
    };
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
    public static string[] act2Scene2choice1 =
    {
        //the makobii is pleased with toyen
        "*T So, what is it that we learn from this?",
        "*M I was wrong, you were so wrapped up in yourself, that your mind doesn't even picture their voices... ",
        "*M You decline these peoples advances. All of them. All the Time. Why?",
        "*T It is for their own good. There is no point of connection if I am to leave again.",
        "*M How masochistic humans can be. "
    };
    public static string[] act2Scene2choice2 =
    {
        "*M You decline their advances. All of them. All the time. Why?",
        "*T It is for their own good. There is no point of connection if I am to leave again.",
        "*M Not even in a dream can you allow yourself to experience these connections and stability that you speak of. How masochistic humans can be. "
    };
    public static string[] act2Scene3 =
    {
        "*M That was fruitless. Your dreams are insufferably boring.",
        "*M I will guide you to places of rest where the energy in my realm is stronger; perhaps that will trigger deeper dreams."
    };

    public static string[] walking3 =
    {
        "*T So how do we find my forgotten memory?",
        "*M Usually, there would be memories scattered around your dreams.  Normal prey-",
        "*T Pardon?",
        "*M Normal... humans... napping in the forest would have had a couple fragments we could use to guide our journey, but you in your boundless boringness have none.",
        "*T Now humans would consider this too blunt, words hurt Makobii",
        "*M Well, no matter the words it would hurt... perhaps words that are more like a sickle would be palatable",
        "*T Haha, perhaps. But my life hasn't been uneventful, I often encounter trials and victories while going from village to village.",
        "*M But you didn't care, it didn't become part of you the way a memory fragment would."
    };
    public static string[] act2Scene4 =
    {
        "*M How is it, that you are always tired and worn from your travels, but when you sleep you are fitful. I am getting nowhere. Have you always slept so terribly?",
        "*T I suppose. The village folk notice this, too.",
        "*T I was given this sleeping agent by a shopkeeper.",
        "*M You should have tried that earlier.",
        "*T Apologies, creature. My memory is troubled, you should know."
    };
    public static string[] act2Scene5p1 =
    {
        "*T I've never seen a place like this... Where is this location?",
        "*M You willed us here. Do not ask me that.",
        "*T You keep saying you do not experience human emotions. What do you feel, then?",
        "*M Hunger, mostly. The strive to continue living. But I am not quite sure why. I do not have anything to live for.",
        "*T Do makobii not care for other makobii?",
        "*M Not especially. We see each other as competition.",
        "*T I do not have family, either. I only barely remember their faces from before I lost my memory. I live on like this anyway.",
        "*M Only to find them. Otherwise, would you not have been village-folk?",
        "*T Perhaps."
    };
    public static string[] act2Scene5p2 =
    {
        "*T We should turnback, makobii.",
        "*M We are so close...",
        "*T To what?",
        "*M A fragment.",
        "*T is it one of my memories?",
        "*M Whose would it be but yours?",
        "*T If I touch it what happens?",
        "*M You will remember what ever memory it is, but I doubt its your lost memory, It's likely something else you've forgotten over the years. ",
        "*M A... small meal... in Makobii terms... but..."
    };
    public static string[] act2Scene5choice2 =
    {
        "*T A small meal?! ",
        "*T You just want to eat my memory, don't you! That's all you can think about!"
    };
    public static string[] act2Scene5Conclusion =
    {
        "*T Makobii!",
        "*T Makobii! Answer me!"
    };
    public static string[] act2scene6choice1 =
    {
        "*M You have had a TALISMAN. And did not tell me! Why should I guide you if at any moment you could unleash its power on me!",
        "*T Nobody would dare to venture with something like you without it. Do you think I'm stupid?",
        "*T Maybe I am, I trusted some dream eater to not eat my dreams and leave me as a husk of the husk I already am!",
        "*M Go to that cliff face, you might find your memories there, if you don't then jump of it!"
    };
    public static string[] act2scene6choice2 =
 {
        "*M You have had a TALISMAN. And did not tell me! Why should I guide you if at any moment you could unleash its power on me!",
        "*T I'm sorry, but makobii aren't exactly harmless, it was for my protection. Besides, if any of us should be angry, it should be me!",
        "*T You tried to eat my memories! While I was being attacked by some monster you were goin to eat my memories! What could I have done instead? ",
        "*M That cliff face, that is likely where you will find the memory."
    };
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
    public static string[] act3scene2karmahigh =
    {
        "*T Thank you for showing me the truth. Even if only for a moment."
    };
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
    public static string[] endinggood =
    {
        "*S Toyen! Welcome! Did you complete your mission?",
        "*T No. I did not.",
        "*S Oh… my apologies. Back on the road soon, then?",
        "*T I was thinking that I may stay for awhile. Is that room you spoke of still available?"
    };
    public static string[] endingbad =
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

    public void StartDialogue(string[] dialogue)
    {
        dialogueCanvas.SetActive(true);
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
            StartCoroutine(ProgressSentence(sentence));
        }     
    }

    //turns off the dialogue UI
    public void endDialogue()
    {
        dialogueCanvas.SetActive(false);
        /*switch (SceneManager.GetActiveScene().name)
        {
            case "Act1Scene2":
                SceneManager.LoadScene("Act1Scene3");
                break;
            case "Act1Scene3":
                SceneManager.LoadScene("OutsideLevel1");
                break;
        }*/
        FindObjectOfType<FadeTransition>().startFade();
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

    //automaticaly load out dialogue
    public void automaticDialogue(string[] dialogue)
    {
        dialogueCanvas.SetActive(true);
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
        yield return new WaitForSeconds(2);
        autoLoad();
    }
}
