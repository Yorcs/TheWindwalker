using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearClose : MonoBehaviour
{
    // Start is called before the first frame update

     GameObject mainCamera;
    public float appearDist;

    Renderer mainRenderer;


    void Start()
    {
        mainCamera = FindFirstObjectByType<Camera>().gameObject;
        mainRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector3.Distance(mainCamera.transform.position, transform.position);

        if (dist < appearDist)
        {
            mainRenderer.enabled = true;

        }
        else { mainRenderer.enabled = false; }
        
    }




}
