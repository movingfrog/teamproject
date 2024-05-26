using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private bool isInRange = false;
    private float range;

    private void PickUp()
    {
        if (range <= 1.2f)
        {
            isInRange = true;
            if (Input.GetKey(KeyCode.F))
            {

            }    
        }
    }
}
