using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi : MonoBehaviour
{

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward*100f, ForceMode.Force);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Move>().gameover();
        }
        else if (collision.transform.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
