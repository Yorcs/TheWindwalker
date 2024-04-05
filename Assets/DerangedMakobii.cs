using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerangedMakobii : MonoBehaviour
{

    public BoxCollider attackZone;
    public SphereCollider hitbox;
    Animator animator;
   public Rigidbody rb;

    public Light indicatorLight;
    float lightMaxIntensity;
    public float lightChangeTime;

    public float attackMoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        lightMaxIntensity = indicatorLight.intensity;
        animator = GetComponent<Animator>();
        resetLight();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            attack();
                
        }
    }


    //https://www.reddit.com/r/Unity2D/comments/327kg1/is_there_a_way_to_get_different_effects_from/
    //auugh
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.name == "Player")
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (contact.thisCollider == attackZone)
                {
                    attack();
                }
                else if (contact.thisCollider == hitbox)
                {
                    hurt();
                }
            }
        }
        //if player, start attack, do animations
    }

    void attack()
    {
        rb.velocity = (transform.forward * attackMoveSpeed);
        animator.SetTrigger("Attack");
        StartCoroutine(lightIncrease());
    }

    IEnumerator lightIncrease()
    {
        for(float f = 0; f<lightMaxIntensity; f += ((Time.deltaTime / lightChangeTime) * lightMaxIntensity))
        {
            indicatorLight.intensity = f;
        }
        yield return null;
    }

    void resetLight()
    {
        indicatorLight.intensity = 0;
    }

    void hurt()
    {
        
    }
}
