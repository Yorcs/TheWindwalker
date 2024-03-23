using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    // Start is called before the first frame update


    public ParticleSystem partBase;

    public GameObject leftFoot, rightFoot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void leftStep()
    {
        ParticleSystem tempParts = Instantiate(partBase);
        tempParts.transform.position = leftFoot.transform.position;
        tempParts.gameObject.SetActive(true);
        tempParts.Play();
    }
    public void rightStep()
    {
        ParticleSystem tempParts = Instantiate(partBase);
        tempParts.transform.position = rightFoot.transform.position;
        tempParts.gameObject.SetActive(true);
        tempParts.Play();
    }

}
