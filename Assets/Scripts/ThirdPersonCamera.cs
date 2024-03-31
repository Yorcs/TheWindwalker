using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//referenced from https://www.youtube.com/watch?v=UCwwn2q4Vys
public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    void Start()
    {
        //hides the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //rotates orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        //rotates the player object
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //apply forward direction
        if(inputDir != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}
