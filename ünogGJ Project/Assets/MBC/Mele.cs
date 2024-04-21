using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mele : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (transform.parent.GetComponent<Move>().meleePressed)
        {
            if (other.CompareTag("Throwable"))
            {
                Debug.Log("GO");
                other.transform.parent.GetComponent<Rigidbody>().AddForce(15 * transform.parent.forward, ForceMode.Impulse);
                other.GetComponent<Throwable>().

                transform.parent.GetComponent<Move>().meleePressed = false;
            }
            else if (other.CompareTag("Enemy"))
            {
                Debug.Log("GO");
                other.transform.parent.GetComponent<Rigidbody>().AddForce(15 * transform.parent.forward, ForceMode.Impulse);

                transform.parent.GetComponent<Move>().meleePressed = false;
            }
        }
    }
}
