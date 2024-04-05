using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

//used by moving danger areas which chase the player
public class MovingDangerArea: MonoBehaviour
{
    public Vector3 startPos;
    public bool start = false;
    public bool reverseDirection = false;
    public Vector3 speed;
    public float maxPos;
    public float minPos;
    public bool xDirection;
    public bool yDirection;
    public bool zDirection;

    public UnityEvent[] resetEvents;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (start)
        {
            if (xDirection)
            {
                if (transform.position.x < maxPos && !reverseDirection)
                {
                    transform.position += speed*Time.deltaTime;
                }
                else if (transform.position.x > minPos)
                {
                    reverseDirection = true;
                    transform.position -= speed * Time.deltaTime;
                }
                else
                {
                    reverseDirection = false;
                }
            }else if (yDirection)
            {
                if (transform.position.y < maxPos && !reverseDirection)
                {
                    transform.position += speed * Time.deltaTime;
                }
                else if (transform.position.y > minPos)
                {
                    reverseDirection = true;
                    transform.position -= speed * Time.deltaTime;
                }
                else
                {
                    reverseDirection = false;
                }
            }else if (zDirection)
            {
                if (transform.position.z < maxPos && !reverseDirection)
                {
                    transform.position += speed * Time.deltaTime;
                }
                else if (transform.position.z > minPos)
                {
                    reverseDirection = true;
                    transform.position -= speed * Time.deltaTime;
                }
                else
                {
                    reverseDirection = false;
                }
            }
            


            /*switch (SceneManager.GetActiveScene().name)
            {
                case "DreamTutorial":
                    if (transform.position.x < 13 && !reverseDirection)
                    {
                        transform.position += new Vector3(3f * Time.deltaTime, 0, 0);
                    }else if(transform.position.x > -9)
                    {
                        reverseDirection = true;
                        transform.position -= new Vector3(3f * Time.deltaTime, 0, 0);
                    }
                    else
                    {
                        reverseDirection = false;
                    }
                    break;
                case "DreamLevelOne":
                    if (transform.position.y < -67)
                    {
                        transform.position += new Vector3(0, 1f*Time.deltaTime, 0);
                    }
                    break;
                case "DreamLevelTwo":
                    if (transform.position.z < 27)
                    {
                        transform.position += new Vector3(0, 0, 1f*Time.deltaTime);
                    }
                    break;

            }*/
        }
        
    }
    
    public void resetPos() 
    {
        Debug.Log("back to start");
        start = false;
        transform.position = startPos;

        foreach(UnityEvent ev in resetEvents)
        {
            ev.Invoke();
        }
    }

    
}
