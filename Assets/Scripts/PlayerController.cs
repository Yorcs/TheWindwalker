using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//controls the player
//https://www.youtube.com/watch?v=a-rogIWEJlY
public class PlayerController : MonoBehaviour
{
    //holds various movement variables
    public float horizontalInput;
    public float verticalInput;
    public float turnSpeed = 40;
    public int speed = 5;
    public float jumpForce = 15;
    public static bool LockedPlayer = false; //use to lock the player in place by other scripts
    public Vector3 startPos;
    public Quaternion startRot;
    public bool onGround;

    //variables for health
    private float health = 100;
    private bool loseHealth = false;
    public Slider display;
    public float delayRegen = 3;
    public float delayCounter = 0;
    public bool regenHealth = false;


    private void Start()
    {
        //save the starting location/rotation for checkpoints
        startPos = transform.position;
        startRot = transform.rotation;
    }

    private void Update()
    {
        //if the player is not locked, allow for movement
        if (!LockedPlayer) {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
            //transform.Translate(Vector3.right * Time.deltaTime * horizontalInput);
            transform.Rotate(Vector3.up * horizontalInput * turnSpeed * Time.deltaTime * speed);

            //if space is pressed, jump
            if (Input.GetKeyDown(KeyCode.Space) && onGround)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            //check for shift to sprint
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed = 8;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = 5;
            }
        }

        //if the player is in a danger zone
        if (loseHealth)
        {
            //deduct health and display on UI
            health -= 10 * Time.deltaTime;
            display.value = health;

            //if health is less than 0, send the player back to the previous checkpoint
            if (health <= 0)
            {
                transform.position = startPos;
                transform.rotation = startRot;
                health = 100;
                return;
            }
        }

        //if player should regenerate health
        if (regenHealth)
        {
            //have a delay after leaving danger zone before regenerating
            if (delayCounter < delayRegen)
            {
                delayCounter += 1 * Time.deltaTime;
            }
            else //once delay is up, regenerate until healh is full
            {
                health += 12 * Time.deltaTime;
                display.value = health;
                if(health > 100)
                {
                    health = 100;
                    delayCounter = 0;
                    regenHealth = false;
                }
            }
           
        }

        //Debug.Log(health);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Damage") //if player collides with a damage object, lose health
        {
            loseHealth = true;
            regenHealth = false;
            delayCounter = 0;
        }else if(other.tag == "Checkpoint") //if player collides with a checkpoint, set that as the new starting pos/rot
        {
            startPos = other.transform.position;
            startRot = other.transform.rotation;
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        //stop losing health if you leave a damage object
        if(other.tag == "Damage") {
            loseHealth = false;
            regenHealth = true;
        }
    }

    //used by other scripts to damage player by a certain amount
    public void damage(float amount){
        health -= amount;
        display.value = health;
        if (health <= 0)
        {
            transform.position = startPos;
            transform.rotation = startRot;
            health = 100;

            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") onGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") onGround = false;
    }

}