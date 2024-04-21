using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impact : MonoBehaviour
{
    Rigidbody rb;

    private Vector3 moveDir;
    private Vector3 normalOfImpactSurface;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(gameObject.transform.forward * 300f);
        //look forward to spawn trail
    }
    private void Update()
    {
    }
    //Melee attack
    private void newRaycast()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("duvar") || other.transform.CompareTag("Ground"))
        {
            moveDir = rb.velocity;
            RaycastHit[] hits = Physics.RaycastAll(gameObject.transform.position, moveDir, 100f);
            for (int c = 0; c < hits.Length; c++)
            {
                if (hits[c].transform.CompareTag("duvar") || other.transform.CompareTag("Ground"))
                {
                    normalOfImpactSurface = hits[c].normal;
                    break;
                }
            }
            rb.velocity = Vector3.zero;
            Vector3 afterImpactMoveDir = Vector3.Reflect(moveDir, normalOfImpactSurface);
            transform.forward = afterImpactMoveDir;
            rb.AddForce(gameObject.transform.forward * 300f);
        }
        else if (other.transform.CompareTag("Enemy"))
        {
            Debug.Log(other.gameObject.name + " Hit");
        }
    }
}