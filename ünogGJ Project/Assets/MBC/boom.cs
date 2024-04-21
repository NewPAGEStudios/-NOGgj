using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //dmgPLayer
        }
        else if (other.CompareTag("Enemy"))
        {
            //dmgEnemy other
        }
    }
}
