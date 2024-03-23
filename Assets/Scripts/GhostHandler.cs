using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public float spawnRate;

    float spawnTimer;

    public float zLowBound, zHighBound;

    public GameObject ghostBase;

    //probably make some box to spawn them from; or actually no just random position
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnRate)
        {
            spawnTimer = 0;
            spawnNewGhost();
        }

    }

    void spawnNewGhost()
    {
        
        GameObject newGhost = Instantiate(ghostBase, transform);
        newGhost.SetActive(true);
        newGhost.transform.position = new Vector3(transform.position.x+Random.Range(zLowBound, zHighBound), newGhost.transform.position.y, newGhost.transform.position.z);
    }
}
