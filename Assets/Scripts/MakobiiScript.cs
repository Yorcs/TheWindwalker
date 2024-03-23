using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MakobiiScript : MonoBehaviour
{

    public GameObject destination;
    public UnityEngine.AI.NavMeshAgent navAgent;
    // Start is called before the first frame update

    public Animator makAnimator; //this should ideally be moved somewhere else
    void Start()
    {
        navAgent.SetDestination(destination.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        navAgent.SetDestination(destination.transform.position);
        if (navAgent.velocity.magnitude > 0)
        {
            makAnimator.SetBool("Moving", true);
        }
        else makAnimator.SetBool("Moving", false);
    }

    [ContextMenu("setDest")]
    void setDest()
    {
        navAgent.SetDestination(destination.transform.position);
    }
}
