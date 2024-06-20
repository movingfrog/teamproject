using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{    
    private bool isInRange = false; 
    private float range;

    Image pressF;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (range <= 1.5f)
        {
            isInRange = true;
            
        }
    }

    private void Barrel()
    {
        bool isBarrel = gameObject.name.Contains("Barrel");

        if (isBarrel)
        {
            if (Input.GetKey(KeyCode.F))
            {

            }
        }
    }


}
