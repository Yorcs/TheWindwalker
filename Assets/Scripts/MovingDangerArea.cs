using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//used by moving danger areas which chase the player
public class MovingDangerArea: MonoBehaviour
{
    public Vector3 startPos;
    public bool start = false;
    public bool reverseDirection = false;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (start)
        {
            switch (SceneManager.GetActiveScene().name)
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

            }
        }
        
    }
    
    public void resetPos()
    {
        Debug.Log("back to start");
        start = false;
        transform.position = startPos;
    }
}
