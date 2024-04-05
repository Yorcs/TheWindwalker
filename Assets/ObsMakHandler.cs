using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsMakHandler : MonoBehaviour
{
    public ObserverMakobii[] makobii;
    public ParticleSystem dissapearParts;
    int currMak;
    // Start is called before the first frame update
    void Start()
    {
        foreach (ObserverMakobii mak in makobii)
        {
            mak.gameObject.SetActive(false);
        }
        makobii[currMak].gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void nextMakobii()
    {
        newParticles();
        makobii[currMak].gameObject.SetActive(false);

        currMak++;
        if (currMak < makobii.Length)
        {
            makobii[currMak].gameObject.SetActive(true);
            newParticles();
        }
        
        
    }


    [ContextMenu("particles")]

    void newParticles()
    {
       
        
        ParticleSystem disParts = Instantiate(dissapearParts, makobii[currMak].transform.position, makobii[currMak].transform.rotation);

        disParts.gameObject.SetActive(true);
    }

}
