using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed;


    public float lifespan;

    float lifeTimer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
        lifeTimer += Time.deltaTime;
        if (lifeTimer > lifespan) Destroy(this.gameObject);
    }

    void move()
    {
        transform.Translate(Vector3.forward* moveSpeed*Time.deltaTime);
    }
}
