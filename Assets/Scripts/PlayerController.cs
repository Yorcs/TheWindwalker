using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public Animator playerAnimator;


    [SerializeField] float groundCheckDistance = 1f;




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

            if (verticalInput != 0 ||horizontalInput!=0) //turns moving animations on if vertical input is detected
            {
                playerAnimator.SetBool("Moving", true);
                
            }
            else
            {
                playerAnimator.SetBool("Moving", false);
               
            }

            /* old movement stuff
            transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
            //transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);

            transform.Rotate(Vector3.up * horizontalInput * turnSpeed * Time.deltaTime * speed);

            */


            //new movement stuff start

            Quaternion dir = Quaternion.LookRotation((Vector3.forward * verticalInput) + (Vector3.right * horizontalInput));
            Vector3 tempVec = new Vector3(horizontalInput, 0f, verticalInput);

            if (tempVec.magnitude > 0)
            {
                transform.rotation = dir;
                transform.Translate(Vector3.forward  *tempVec.magnitude * Time.deltaTime * speed);
            }
            
            
            //new movement stuff end



            //if space is pressed, jump
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                playerAnimator.SetTrigger("Jump");
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                playerAnimator.SetBool("Grounded", false);


            }

            //check for shift to sprint
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerAnimator.SetBool("Running", true);
                speed = 8;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                playerAnimator.SetBool("Running", false);
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
                if (GameObject.Find("DangerArea")) GameObject.Find("DangerArea").GetComponent<MovingDangerArea>().resetPos();
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
            string[] response = { "*T Found a safe area." };
            FindObjectOfType<DialogueManager>().automaticDialogue(response);
            other.GetComponent<ParticleSystem>().Stop();
            Destroy(other.gameObject);
            return;
        }    
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Damage") //if player stays in a damage object, lose health
        {
            loseHealth = true;
            regenHealth = false;
            delayCounter = 0;
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
            if (GameObject.Find("DangerArea")) GameObject.Find("DangerArea").GetComponent<MovingDangerArea>().resetPos();

            return;
        }
    }


    
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "Ground")
        {

                playerAnimator.SetTrigger("Land");
                playerAnimator.SetBool("Grounded", true);

            //onGround = true;
        }
    }
    


    //referenced from https://www.reddit.com/r/Unity3D/comments/3c43ua/best_way_to_check_for_ground/
    private bool isOnGround
    {
        get
        {
            return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundCheckDistance);
        }
    }


    }