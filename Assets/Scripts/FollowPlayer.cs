using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach to camera so it follows the player
//https://www.youtube.com/watch?v=a-rogIWEJlY
public class FollowPlayer : MonoBehaviour
{

    public GameObject cube;
    float rotationSpeed = 600;

    // Start is called before the first frame update
    void Start()
    {
        //transform.Rotate(0, 15, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cube.transform.position + new Vector3(/*-1*/ 0, 1.5f, -3);
        if (Input.GetKey(KeyCode.Mouse2)) CamOrbit();
    }

    private void CamOrbit()
    {
        if(Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
        {
            float verticalInput = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.right, verticalInput);
            transform.Rotate(Vector3.up, horizontalInput, Space.World);
        }
    }

    Vector3 GetCenterPoint()
    {
        var bounds = new Bounds(cube.gameObject.transform.position, Vector3.zero);
        bounds.Encapsulate(cube.gameObject.transform.position);

        return bounds.center;
    }
}
