using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//referenced from https://www.youtube.com/watch?v=am3IitICcyA
public class Path : MonoBehaviour
{
    [SerializeField] private Transform[] Points;
    [SerializeField] private float moveSpeed;

    private int pointsIndex;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Points[pointsIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(pointsIndex <= Points.Length-1)
        {
            Debug.Log("moving");
            transform.position = Vector3.MoveTowards(transform.position, Points[pointsIndex].transform.position, moveSpeed*Time.deltaTime);

            if(transform.position == Points[pointsIndex].transform.position)
            {
                pointsIndex += 1;
            }

            if(pointsIndex == Points.Length)
            {
                pointsIndex = 0;
            }
        }
    }

    public void backToStart()
    {
        pointsIndex = 0;
        transform.position = Points[pointsIndex].transform.position;
    }
}
