using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//manages the QTE sections
public class QTEManager : MonoBehaviour
{
    //variables to detect
    private bool qteStarted = false;
    private bool qteRunning = false;
    public char qteChar;
    private bool failed = false;
    public int count = 0;
    public int max = 10;

    private void Update()
    {
        //if the QTE is running and the player has pressed the right key
        if (qteRunning && (Input.GetKeyDown(KeyCode.E) && qteChar == 'E') || (Input.GetKeyDown(KeyCode.Q) && qteChar == 'Q') || (Input.GetKeyDown(KeyCode.Z) && qteChar == 'Z'))
        {
            //if the player hasn't completed enough rounds, start a new one
            StopAllCoroutines();
            count++;
            if (count < max)
            {
                qteRunning = false;
                startQTE();
            }
            else //else unlock the player and reset UI
            { 
                PlayerController.LockedPlayer = false;
                GameObject.Find("QTETest").GetComponent<Text>().text = "";
                Destroy(gameObject);
                return;
            }
        }
    }

    //used for the 2 second interval for players to press the key, if they fail deduct health
    IEnumerator countdown()
    {
        Debug.Log("coroutine starting");
        yield return new WaitForSeconds(1);
        Debug.Log("coroutine ended");
        FindAnyObjectByType<PlayerController>().damage(35f);
        qteRunning = false;
        startQTE();
    }

    //used to trigger QTEs
    public void startQTE()
    {
        if (!qteRunning && !failed)
        {
            //select a character for the QTE
            StopAllCoroutines();
            Debug.Log("starting new!");
            qteRunning = true;
            int randomChar = Random.Range(1, 4);
            switch (randomChar)
            {
                case 1:
                    qteChar = 'E';
                    GameObject.Find("QTETest").GetComponent<Text>().text = "Hit the Key: E";
                    break;
                case 2:
                    qteChar = 'Q';
                    GameObject.Find("QTETest").GetComponent<Text>().text = "Hit the Key: Q";
                    break;
                case 3:
                    qteChar = 'Z';
                    GameObject.Find("QTETest").GetComponent<Text>().text = "Hit the Key: Z";
                    break;
            }

            //start delay countdown
            StartCoroutine("countdown");
        }
        else if (failed) //if the player has failed, let them know
        {
            StopAllCoroutines();
            GameObject.Find("QTETest").GetComponent<Text>().text = "ya failed";
        }
    }

    //used by object which triggers QTE when the player collides with it
    private void OnTriggerEnter(Collider other)
    {
        if (!qteStarted) {
            PlayerController.LockedPlayer = true;
            qteStarted = true;
            startQTE();
        }
    }
}

