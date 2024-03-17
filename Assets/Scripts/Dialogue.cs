using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//holds sentences for dialogue
//https://www.youtube.com/watch?v=_nRzoTzeyxU&t=6s
[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;
}
