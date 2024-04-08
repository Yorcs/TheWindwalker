using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

//manages the display of cutscene dialogue
//referenced from https://www.youtube.com/watch?v=_nRzoTzeyxU&t=6s
public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences = new Queue<string>();
    [SerializeField] private GameObject dialogueCanvas;
    private string currSentence;
    private bool loadingText = false;
    private int imageCounter = 0;

    private bool autoLoading = false;
    private bool hotkeyEnabled = true;

    public AudioClip dreamLoop;
    public AudioClip forestLoop;
    public AudioClip fragment;
    public AudioClip makobiiGrowl;
    public AudioClip makobiiLaugh;
    public AudioClip makobiiStalk;
    public AudioClip makobiiWindchimes;
    public AudioClip smallMakobiiRoar;

    //cast
    //T = Toyen
    //M = Makobii
    //S = Shopkeeper
    //H = Healer
    //L = stern mother
    //C = children (plural, like its not important which one is saying which cuase it's just noise to Toyen)
    //... = empty, used for image only

    //NEW AS OF 4/2
    public static string[] introCut =
    {
        "*T The last five have been empty, even though this area should provide a closer connection...",
        "*T But I’m getting closer, my family is so close I can feel it...",
        "...",
        "*T Finally another Fragment...",
        "..."
    };

    public static string[] tutorialIntro =//ingame
    {
        "*T There must be a fragment of my memories here...",
        "*T ...but this looks just as empty as the last.",
        "*T Maybe if I go deeper into the dream..."
    };

    public static string[] tutorialEnd =
    {
        "*T You! Show yourself!",
        "...",
        "*T Well, time to get into town."
       
    };

    public static string[] villageIntro = //ingame
    {
        "*L Why would you want to go sleep in the fields outside the village?!",
        "*L You know what will come out to get you!",
        "*L I know you think you're safe because of the totems, but I remember a time before that safety."
    };

    public static string[] shopkeeperScene =//one frame 
    {
        "*S Toyen! Long time, no see. The nomad, ever-roaming, searching for his long lost family! Any closer?",
        "*T Maybe, I have a feeling, more than usual, that I’m going to find something soon.",
        "*S But, no return of any memories?",
        "*T No. Not yet. But, do you mind if I take a little loan?",
        "*S Hmm... You haven't paid me back yet for the last one.",
        "*T I know, there just haven't been many people needing deliveries that I can do while in the area.",
        "*S You know, if you’re wanting to pay me back, why not stay here a while, you can get a job in the fields, I have a spare room upstairs, you can stay there.",
        "*T You know I can’t, I’m close this time.",
        "*S Uh huh...",
        "*T I mean it, this isn’t like before, in my gut, I know something is going to happen soon.",
        "*S Toyen, you may never find them, you know. It may be time to move on.",
        "*T Only I will decide when that is.",
        "*S All right. I’ll give you a loan. Whether or not you pay me back.",
        "*S It’ll last you a week, but I got one little gift for you.",
        "*S It's sleeping agent.",
        "*S You look exhausted. Use this to rest when you really need it."
    };

    public static string[] makobiiIntro =//cutscene
    {
        "*T What? A Makobii!\nWhy do you show yourself here?\nThis is not a dream",
        "*M Toyen, dream scouring.\nWalking on wind, finds nothing.\nBut Makobii could",
        "*T Speak sense, creature. I know what you are.",
        "*M Heheheh",
        "*M We were not speaking in Haiku? Or did you grow tired of it, it’s not your mission after all.",
        "*T What? How do you--",
        "*M A Forgotten memory... How rare...",
        "*M Everything since you were a child… Gone from this world.",
        "*M None are as skilled as I, when it comes to dreaming I'll help you find it",
        "*T You are a dream-eater. You want off with it. I couldn’t possibly want this enough to agree to such a ridiculous offer.",
        "*T Besides, I’m close, I don’t need your help.",
        "*M Close! He says... Hahaha!",
        "*M Are you certain you are close to the memory, or maybe you were close to someone who could help.",
        "*M There is little use lying to a spirit...",
        "*M I know from your dream last night how long you’ve journeyed for this sole purpose.",
        "*T So it was you that was following me last night!",
        "*M Hehe, you noticed huh? Don’t worry, I was just checking to see if you were what I thought you were.",
        "*M So, are you sure you’re not desperate enough?",
        "*T Fine. I’ll work with you, but don’t think you can trick me, I’ll be watching...",
        "*M Heheh, I wouldn’t dream of it."
    };

    public static string[] walkingOne =//ingame
    {
        "*T So, what happens if the next fragment is empty like the last couple have been?",
        "*M It’s hard to say, half the problem is you, you don’t have spiritual energy...,",
        "*T Oh? How so?",
        "*M Well, humans tend to have very rich lives...",
        "*M ...it gives them lots to dream about.",
        "*M Not so much for you...",
        "*T What? I don’t lead a boring life!",
        "*T I go on these journeys, I face challenges and overcome them!",
        "*T I should have plenty to dream about.",
        "*M Hmm... maybe the only thing is you didn’t care about them...",
        "*M ...so your soul didn’t hold on to them. ",
        "*M Say, do you come across the same challenges often?",
        "*T Well, maybe… I get stuck on a lot of the same terrain... ",
        "*T But that's because they're challenging for humans!",
        "*M Or maybe you never adapted to them… maybe you’re stuck on them. ",
        "*M No matter, I think we are close to a fragment."
    };

    public static string[] dreamOne =
    {
        "*M See, you went to the village, so now you dream of it. Dreams act as a way for us to understand oneself.",
        "*T So do I get these empty dreamscapes because I already understand myself?",
        "*M HA! No... more that you can’t begin to understand yourself, you face no inner conflict when you are out in the wild pursuing your goal, but when you ask for another loan from the shopkeeper, you feel shame.",
        "*T But I need supplies to continue on my journey, I’ll pay him back later, when I’m not so close.",
        "*M When you finally find your family, will you really go back and pay him? Will you even be able too?",
        "...",
        "*T Whatever, let’s keep going, we need to go deeper into the dream.",
        "*M Deeper? This is it, Toyen. It’s like I said, This is what you are dreaming of.",
        "*M People, passing by, not paying attention to you.",
        "*M I feel a sort of empathy for you at this moment, it is not unlike how Makobii are ignored now.",
        "*M I used to get many meals, there would be cults that praised us, others would steer clear of us and make deals where we only ate a little before moving on.",
        "*M But humans and their talismans and Totems. The spiritual energy in them keeps us away. We are just another creature that has been conquered.",
        "*M But at least it’s not my fault that I’m an outcast.",
        "*T It’s not my fault either! If I could find my family I would be part of a community!",
        "*M Then why don’t you join one?",
        "*T If I’m just going to leave again, what’s the point?",
        "*M How masochistic humans can be...",
        "*T Never mind, let’s go."
    };

    public static string[] walkingTwo =
    {
        "*M If this keeps going, we may never find anything, even with my help.",
        "*T You haven't exactly made it clear how you will help...",
        "*T ...how will we find this forgotten memory if my life is so boring?",
        "*M I’m not sure, even the most boring prey–",
        "*T Pardon?",
        "*M Sorry, old habit...",
        "*M Even the most boring humans tend to have some memories scattered around their dreams.",
        "*M Those memory fragments lead deeper into the subconscious...",
        "*M ...but we can’t get to that level if you don’t scrape the surface of your mind.",
        "*T Deeper level of the subconscious, would a drug help?",
        "*M Well, perhaps, but it would take a while...",
        "*M Making a sleeping agent out here in the forest is no easy feat.",
        "*T I have some.",
        "*M Well why didn’t you use it earlier?",
        "*T I didn’t think about it.",
        "*M Bah, anyways, we’re close to one now..."
    };

    public static string[] dreamTwo =
    {
        "*T I’ve never seen a place like this, what is it?",
        "*M How would I know? It’s your dream.",
        "*M But it is interesting that the deeper level of your subconscious looks like this...",
        "*T Well? Where too, guide?",
        "*M There seems to be really only one direction, you need my guidance on where to go?",
        "*T I guess not. ", //After the first challenge in the area.
        "*T I feel a presense, Is something following us?", //activate bird caw sound effect here
        "*M There shouldn’t be...",
        "*M But, if we have entered a deeper level of your subconscious, we may be exerting more spiritual energy...",
        "*T So another Makobii is after us?",
        "*M Maybe, but it likely isn’t as put together as me.",
        "*M Many Makobii are starving now, it may be on a rampage. Do not try to reason with it."
    };

    public static string[] dreamTwoEnd =
    {
        "*T It’s one of my memories?",
        "*M Who else but you?",
        "*T If I touch it what happens?",
        "*M You will remember whatever memory it is, but I doubt it's your lost memory...",
        "*M It's likely something else you’ve forgotten over the years.A...small meal... in Makobii terms... but...",
        "*T A small meal?!",
        "*M A talisman!",
        "*T You just want to eat my memory don’t you! That’s all you can think about!",
        "*M Aghh!",
        "...",
        "...",
        "*T Makobii!",
        "*T  Makobii! Answer me!",
        "...",
        "...",
        "...",
        "...",
        "*M You’ve had a TALISMAN. And did not tell me! Why should I guide you if at any moment you could unleash its power on me!",
        "*T Do you think I’m stupid enough to agree to this insane gambit if I didn’t have a way to stop you from taking it?",
        "*T Besides I was right to, you were just looking for your next meal!",
        "*M So what! I was trying to eat the forgotten memory, at the very least it would have sustained me for a lifetime!",
        "*M It’s better than what you would do with it! Find some people who have fallen right off the map!",
        "*T Don’t you dare! They're out there! They're waiting for me!",
        "*M You’re waiting for them! And if you want to wait longer, go to that cliff!",
        "*M There is a fragment there that leads to the forgotten memory.And if you don’t find it, jump off!",
        "..."
    };

    public static string[] dreamThree =
    {
        "*T Makobii?",
        "*T Makobii, are you there?", 
        "...",//toyen touches the memory shard
        "...",
        "*T Makobii? Is that you?",  //in dreamscape
        "...",
        "...",
        "...",
        "...",
        "*M I can’t hold them for long! Get your memory!",
        "*T But they’ll kill you!",
        "*M The age of Makobii is over, go Toyen, if this memory will bring you peace, then it’s worth a hundred of my deaths.",
        "*T Thank you, I won’t forget this."
    };

    public static string[] dreamThreeEnd =
    {   "...",
        "...",
        "...",
        "...",
        "*T NO! I was so close!",
        "*T Makobii! What’s happening?",
        "*M Ah, it’s a small wound, nothing to worry about...",
        "*T Don’t try to trick me now...",
        "*M Heheh, hmm...",
        "*M I’m going to die Toyen, but, I think–",
        "*M –I have just enough strength to send you back in.",
        "*M But I will lose control of my body, you will have to fend for yourself.",
        "*T Why, why do you care now?",
        "*M I think, maybe, I always did.",
        "*M I’ve lived longer than you could count in all your remaining years...",
        "*M I’ve eaten so many dreams, and those memories, maybe slowly, made me care for humans.",
        "*T Is there anything I can do to help?",
        "*M Just get the memory, I’ll be at peace then...",
        "*M ...Knowing I helped you uncover your past and define yourself.",
        "*T I... don’t think that the memory will help anymore...",
        "*M What?",
        "*T I was so obsessed with the idea of a family that I pushed anyone who could’ve loved me away...",
        "*T I unleashed hell upon my only companion with the talisman. I don’t think it will mend my soul.",
        "*T Let’s get this memory, not because it matters, but because we came here to do so.",
        "*T We will win, the age of the Makobii might be over, humans may have conquered the dream eaters, but you said it yourself,",
        "*T ‘None are as skilled as I, when it comes to dreaming’, now let’s go find it.",
        "..." //toyen touches mask
    };

    public static string[] dreamFourEnd =
    {   "...",
        "...",
        "...",
        "*T I don’t need it, I never did.",
        "...",
        "...", //makobii transforms
        "...",
        "...",
        "...",
        "...",
        "*M Why... why would you do such a thing? You had it, it was right in front of–",
        "*T I tricked you!",
        "*M Huh?",
        "*T I have defined myself, I know who I am.My actions defined me, not the memory I was searching for.",
        "*T When I would beg and borrow, when I would spend weeks out in the wild and when I pulled a talisman on you, those actions hurt people, they hurt you.",
        "*T I have defined myself as a man who doesn’t care about anyone, who searches for something that will never have made my life complete.",
        "*T But today that's going to change.",
        "*M Heheh, I hope so.",
        "*T That’s besides the point, how are you feeling?",
        "*M Better than I have in a long time. I think I won't have to eat for a thousand years.",
        "...", //makobii gives beads
        "*M A gift, to remember me by.",
        "*T What, no more adventures?",
        "*M We both know this is where we say farewell.",
        "*T Hmm. You’re right, I have debts to repay after all."
    };

    public static string[] finalScene =
    {
        "*S Toyen! So were you close?",
        "*T I was, but I gave up.",
        "...",
        "*T I made a friend on the road, and they taught me a lesson about myself.",
        "*S Oh? Are you going to be traveling with them then?",
        "*T They have their own life to get to, and they helped me realize, I do too.",
        "*T I was thinking that I may stay for a while. Is that room you spoke of still available?"
    };

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && hotkeyEnabled && sentences.Count >= 0 && !autoLoading)
        {
            DisplayNextSentence();
        }
    }

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

    //give a string array to show
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

    //used to load out sentences
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
        //if text is loading, show the sentence in full
        if (loadingText)
        {
            StopAllCoroutines();
            if (GameObject.Find("Dialogue")) GameObject.Find("Dialogue").GetComponent<Text>().text = currSentence;
            loadingText = false;
        }
        else //else load the next sentence
        {

            //if there are no more sentences, end the cutscene
            if (sentences.Count == 0)
            {
                hotkeyEnabled = false;
                endDialogue();
                return;
            }

            //check for sound effects/images
            hotkeyEnabled = true;
            //Debug.Log("/ UIAssets / Cutscenes / A1S3 / a1s3_" + imageCounter + ".png");
            Sprite temp;
           
            if (GameObject.Find("CutsceneImage"))
            {
                imageCounter++;
                Debug.Log(imageCounter);
                switch (SceneManager.GetActiveScene().name)
                {
                    case "Cutscene1":
                        if (imageCounter <= introCut.Length)
                        {
                            temp = Resources.Load<Sprite>("Sprites/Cutscene1/Cutscene1_" + imageCounter);
                            GameObject.Find("CutsceneImage").GetComponent<Image>().sprite = temp;
                        }
                        break;
                    case "Cutscene2":
                        if (imageCounter <= tutorialEnd.Length)
                        {
                            temp = Resources.Load<Sprite>("Sprites/Cutscene2/Cutscene2_" + imageCounter);
                            GameObject.Find("CutsceneImage").GetComponent<Image>().sprite = temp;
                        }
                        break;
                    case "Cutscene3":
                        if (imageCounter <= makobiiIntro.Length)
                        {
                            temp = Resources.Load<Sprite>("Sprites/Cutscene3/cutscene3_" + imageCounter);
                            GameObject.Find("CutsceneImage").GetComponent<Image>().sprite = temp;
                        }
                        break;
                    case "Dream1":
                        if (imageCounter <= dreamOne.Length)
                        {
                            temp = Resources.Load<Sprite>("Sprites/Cutscene4/cutscene4_" + imageCounter);
                            GameObject.Find("CutsceneImage").GetComponent<Image>().sprite = temp;
                        }
                        break;
                    case "Cutscene4":
                        if (imageCounter <= dreamOne.Length)
                        {
                            temp = Resources.Load<Sprite>("Sprites/Cutscene4/cutscene3_" + imageCounter);
                            GameObject.Find("CutsceneImage").GetComponent<Image>().sprite = temp;
                        }
                        break;
                    case "Cutscene5":
                        if (imageCounter <= dreamTwoEnd.Length)
                        {
                            temp = Resources.Load<Sprite>("Sprites/Cutscene5/cutscene5_" + imageCounter);
                            GameObject.Find("CutsceneImage").GetComponent<Image>().sprite = temp;
                        }
                        break;
                    case "Cutscene6":
                        if (imageCounter <= dreamThree.Length)
                        {
                            temp = Resources.Load<Sprite>("Sprites/Cutscene6/cutscene6-" + imageCounter);
                            GameObject.Find("CutsceneImage").GetComponent<Image>().sprite = temp;
                        }
                        break;
                    case "Cutscene7":
                        if (imageCounter <= dreamThreeEnd.Length)
                        {
                            temp = Resources.Load<Sprite>("Sprites/Cutscene7/cutscene7-" + imageCounter);
                            GameObject.Find("CutsceneImage").GetComponent<Image>().sprite = temp;
                        }
                        break;
                    case "Cutscene8":
                        if (imageCounter <= dreamFourEnd.Length)
                        {
                            temp = Resources.Load<Sprite>("Sprites/Cutscene8/cutscene8-" + imageCounter);
                            GameObject.Find("CutsceneImage").GetComponent<Image>().sprite = temp;
                        }
                        break;
                    case "Cutscene9":
                        if (imageCounter <= finalScene.Length)
                        {
                            temp = Resources.Load<Sprite>("Sprites/Cutscene9/cutscene9-" + imageCounter);
                            GameObject.Find("CutsceneImage").GetComponent<Image>().sprite = temp;
                        }
                        break;
                }
            }

            
            string sentence = sentences.Dequeue();
            if (sentence.Equals("...")) //if text is ..., hide the dialogue/name boxes
            {
                if (GameObject.Find("NameBox")) GameObject.Find("NameBox").GetComponent<CanvasGroup>().alpha = 0;
                if (GameObject.Find("DialogueBox")) GameObject.Find("DialogueBox").GetComponent<CanvasGroup>().alpha = 0;
            }
            else
            {
                if (GameObject.Find("NameBox")) GameObject.Find("NameBox").GetComponent<CanvasGroup>().alpha = 1;
                if (GameObject.Find("DialogueBox")) GameObject.Find("DialogueBox").GetComponent<CanvasGroup>().alpha = 1;
            }

            //used to control the speaker
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

            //if this cutscene is camera based, swap the camera to face whoever is speaking
            if (FindAnyObjectByType<CameraManager>())
            {
                switch (SceneManager.GetActiveScene().name)
                {
                    case "OutsideLevelZero":
                        if (GameObject.Find("Name"))
                        {
                            if(GameObject.Find("Name").GetComponent<Text>().text.Equals("Shopkeeper"))
                            {
                                FindAnyObjectByType<CameraManager>().cam2.enabled = false;
                                FindAnyObjectByType<CameraManager>().cam1.enabled = true;
                            }
                            else
                            {
                                FindAnyObjectByType<CameraManager>().cam2.enabled = true;
                                FindAnyObjectByType<CameraManager>().cam1.enabled = false;
                            }
                        }
                        break;
                    case "Dream2":
                        if (GameObject.Find("Name"))
                        {
                            if (GameObject.Find("Name").GetComponent<Text>().text.Equals("Toyen"))
                            {
                                FindAnyObjectByType<CameraManager>().cam2.enabled = false;
                                FindAnyObjectByType<CameraManager>().cam1.enabled = true;
                            }
                            else
                            {
                                FindAnyObjectByType<CameraManager>().cam2.enabled = true;
                                FindAnyObjectByType<CameraManager>().cam1.enabled = false;
                            }
                        }
                        break;
                }

            }
            StartCoroutine(ProgressSentence(sentence));
            checkSounds(sentence);
        }     
    }

    //turns off the dialogue UI
    public void endDialogue()
    {
        imageCounter = 0;
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
   
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log(sceneName);
        if (!sceneName.Equals("Dream0") && !sceneName.Equals("OutsideLevelZero") && !sceneName.Equals("Walking1") && !sceneName.Equals("Walking2") && !sceneName.Equals("Dream2") && !sceneName.Equals("Dream3") && !sceneName.Equals("Dream4"))
        {
            Debug.Log("fading");
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

    //auto progresses dialogue 
    public void autoLoad()
    {
        autoLoading = true;
        if (sentences.Count == 0)
        {
            autoLoading = false;
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

    //used for autoloading, shows sentence for 3 seconds
    IEnumerator showSentence()
    {
        yield return new WaitForSeconds(3f);
        autoLoad();
    }

    //checks if the current sentence should have a sound effect play
    private void checkSounds(string sentence)
    {

        if (sentence!=null)
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Cutscene1":
                    if (imageCounter == 4) GetComponent<AudioSource>().PlayOneShot(fragment);
                    break;
                case "Cutscene2":
                    if (imageCounter == 2) GetComponent<AudioSource>().PlayOneShot(makobiiWindchimes);
                    break;
                case "Cutscene3":
                    if (imageCounter == 1) GetComponent<AudioSource>().PlayOneShot(makobiiWindchimes);
                    else if (imageCounter == 4) GetComponent<AudioSource>().PlayOneShot(makobiiLaugh);
                    break;
                case "Cutscene5":
                    if (imageCounter == 9) GetComponent<AudioSource>().PlayOneShot(makobiiGrowl);
                    else if (imageCounter == 11) GetComponent<AudioSource>().PlayOneShot(makobiiStalk);
                    else if (imageCounter == 12 || imageCounter == 14 || imageCounter == 16) GetComponent<AudioSource>().PlayOneShot(makobiiGrowl);
                    else if (imageCounter == 17) GetComponent<AudioSource>().PlayOneShot(fragment);
                    break;
                case "Cutscene6":
                    if (imageCounter == 3) GetComponent<AudioSource>().PlayOneShot(fragment);
                    else if (imageCounter == 6) GetComponent<AudioSource>().PlayOneShot(makobiiStalk);
                    else if (imageCounter == 8 || imageCounter == 9) GetComponent<AudioSource>().PlayOneShot(makobiiGrowl);
                    break;
                case "Cutscene7":
                    if (imageCounter == 26) GetComponent<AudioSource>().PlayOneShot(fragment);
                    break;
                case "Cutscene8":
                    if (imageCounter == 4) GetComponent<AudioSource>().PlayOneShot(makobiiGrowl);
                    else if (imageCounter == 6) GetComponent<AudioSource>().PlayOneShot(fragment);
                    else if (imageCounter == 9) GetComponent<AudioSource>().PlayOneShot(makobiiWindchimes);
                    else if (imageCounter == 21) GetComponent<AudioSource>().PlayOneShot(makobiiLaugh);
                    break;
            }
            
            
            
        }
    }

}
