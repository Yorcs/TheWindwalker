using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverMakobii : MonoBehaviour
{
    ObsMakHandler makobiiHandler;

    public BoxCollider triggerZone;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        makobiiHandler = FindAnyObjectByType<ObsMakHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if(other.tag == "Player")
        {
            makobiiHandler.nextMakobii();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collider");
        if (collision.gameObject.tag == "Player")
        {
            makobiiHandler.nextMakobii();
        }
    }
}
