using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{

    public Sprite onHover;
    public Sprite offHover;

    private void OnMouseEnter()
    {
        GetComponent<Image>().sprite = onHover;
    }
    private void OnMouseExit()
    {
        GetComponent<Image>().sprite = offHover;
    }
}
