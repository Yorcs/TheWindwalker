using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed;


    public float lifespan;

    float lifeTimer;

    public GameObject body;
    public GameObject hair1;
    public GameObject hair2;
    //at start, assign copies of materials to the new things


    //fadespeed, fadeval


    float fadeVal = 0;
    public float fadeSpeed = 0.5f;

    public float maxOp = 0.8f;

    
    //should fade in over fadespeed, fade out over lifespan - fadespeed

    //time
    //should increase by max opacity / fadeTime

    Renderer bodyRender, hair1Render, hair2Render;

    bool makeOpaque = true;
    bool fading = false;

    void Start()
    {
        bodyRender = body.GetComponent<Renderer>();
        hair1Render = hair1.GetComponent<Renderer>();
        hair2Render = hair2.GetComponent<Renderer>();

        bodyRender.material = new Material(bodyRender.material);
        hair1Render.material = new Material(hair1Render.material);
        hair2Render.material = new Material(hair2Render.material);



        matOpacity(bodyRender);
        matOpacity(hair1Render);
        matOpacity(hair2Render);

    }

   
    
    // Update is called once per frame
    void Update()
    {
        move();
        lifeTimer += Time.deltaTime;
        if (lifeTimer > lifespan) Destroy(this.gameObject);

        if (lifeTimer < fadeSpeed && fading==false)
        {
            fadeIn();
        }

        if (lifeTimer > (lifespan - fadeSpeed) && fading==false)
        {
            fadeOut();
        }

        if(fading) changeOpacity();

    }


    void changeOpacity()
    {
        if (makeOpaque == true)
        {
            fadeVal += (maxOp / fadeSpeed) * Time.deltaTime;
            if (fadeVal >= maxOp) fading = false;
        }

        else if (makeOpaque == false)
        {
            fadeVal -= (maxOp / fadeSpeed) * Time.deltaTime;
            if (fadeVal <= 0) fading = false;
        }


        matOpacity(bodyRender);
        matOpacity(hair1Render);
        matOpacity(hair2Render);
    }

    void matOpacity(Renderer rend)
    {
        //rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, fadeVal);
        rend.material.SetFloat("_Transparency_Mult", fadeVal);
    }

    void move()
    {
        transform.Translate(Vector3.forward* moveSpeed*Time.deltaTime);
    }


    void fadeIn()
    {
        fading = true;
        makeOpaque = true;
    }
    void fadeOut()
    {
        fading = true;
        makeOpaque = false;
    }
}
