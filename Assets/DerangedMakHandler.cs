using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerangedMakHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public ParticleSystem dissapearParts;

    DerangedMakobii[] derMakList;
    void Start()
    {
        derMakList = FindObjectsByType<DerangedMakobii>(sortMode:0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dissapearParticles(GameObject mak)
    {
        ParticleSystem disParts = Instantiate(dissapearParts, mak.transform);
        disParts.gameObject.SetActive(true);
    }

    public void resetAllMak()
    {
        foreach(DerangedMakobii mk in derMakList)
        {
            mk.reappear();
        }
    }
}
