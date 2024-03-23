using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MakobiiScript : MonoBehaviour
{

    GameObject destination;
    //public UnityEngine.AI.NavMeshAgent navAgent;
    // Start is called before the first frame update

    public Animator makAnimator; //this should ideally be moved somewhere else

    public float moveSpeed;

    public float accelRate, maxAccel;

    float currAccel;

    float currMoveSpeed;

    bool moving = false;

    public float decelStart;
    //maybe just get this from initial distance?  when it starts movin
    //just take movement speed from current distance, scale it by that

    GameObject player;

    GameObject lookTarg;
    
    void Start()
    {
        //player = GameObject.FindFirstObjectByType<PlayerController>().gameObject;
        lookTarg = GameObject.Find("MakLookTarg").gameObject;
        destination = GameObject.Find("MakobiiDest").gameObject;
        //navAgent.SetDestination(destination.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
      //  navAgent.SetDestination(destination.transform.position);
        if (transform.position!=destination.transform.position)
        {
            //wait nvm uh
            makAnimator.SetBool("Moving", true);
            moving = true;
        }
        else
        {
            makAnimator.SetBool("Moving", false);
            moving = false;


            if (currAccel > 0) currAccel -= (accelRate * Time.deltaTime);

        }

        if(moving && currAccel < maxAccel)
        {
            currAccel += (accelRate * Time.deltaTime);
        }

        if (moving && currMoveSpeed < moveSpeed)
        {
            currMoveSpeed += currAccel;
        }

        //uhhh square scale? idk
        //just basically movement speed gets lower as it gets closer; inverse proportional

        //accel = uhh

        //current move speed should still be the final thing
        //you want to be scaling move speed

        //do it on a 1 scale instead

        currMoveSpeed = Vector3.Distance(transform.position, destination.transform.position) * moveSpeed;

        transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, currMoveSpeed*Time.deltaTime);

        float tempY = transform.rotation.y;
        transform.LookAt(lookTarg.transform.position);
        
    }

    [ContextMenu("setDest")]
    void setDest()
    {
       // navAgent.SetDestination(destination.transform.position);
    }
    //ok just do moving stuff manually


}
