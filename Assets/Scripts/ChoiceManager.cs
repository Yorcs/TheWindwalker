using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoiceManager: MonoBehaviour
{
    public static bool scene2 = false;
    public static bool scene5 = false;
    public static bool scene6 = false;

    public static bool goodKarma = false;

    public static string a2s2Pos = "Join Family";
    public static string a2s2Neg = "Depart";

    public static string a2s5Pos = "Trust Makobii";
    public static string a2s5Neg = "Threaten with the Talisman";

    public static string a2s6Pos = "Distract the Enemies";
    public static string a2s6Neg = "Dash for the Memory";

    public Text pos;
    public Text neg;

    private void Awake()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Act2Scene2":
                pos.text = a2s2Pos;
                neg.text = a2s2Neg;
                break;
        }
    }

    public void selected(bool choice)
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Act2Scene2":
                scene2 = choice;
                Debug.Log(scene2);
                if(choice) FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act2Scene2choice1);
                else FindObjectOfType<DialogueManager>().StartDialogue(DialogueManager.act2Scene2choice2);
                break;
        }

        gameObject.SetActive(false);
    }
}
    