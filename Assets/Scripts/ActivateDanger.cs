using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDanger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FindAnyObjectByType<MovingDangerArea>().start = true;
    }

}
