using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach to camera so it follows the player
//https://www.youtube.com/watch?v=a-rogIWEJlY
public class FollowPlayer : MonoBehaviour
{

    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, 15, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cube.transform.position + new Vector3(-1, 1.5f, -3);       
    }
}
