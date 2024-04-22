using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impact : MonoBehaviour
{
    Rigidbody rb;

    private Vector3 moveDir;
    private Vector3 normalOfImpactSurface;

    float dmg=50;

    int countOfiteration;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(gameObject.transform.forward * 300f);
        countOfiteration = 0;
        //look forward to spawn trail
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("duvar") || other.transform.CompareTag("Ground") || other.transform.CompareTag("Enemy"))
        {
            Debug.Log(other.name);
            moveDir = rb.velocity;
            RaycastHit[] hits = Physics.RaycastAll(gameObject.transform.position, moveDir, 100f);
            for (int c = 0; c < hits.Length; c++)
            {
                if (hits[c].transform.CompareTag("Enemy"))
                {
                    if (countOfiteration <= 0)
                    {
                        Destroy(gameObject);
                    }
                    hits[c].transform.GetComponent<eNEM>().setHitPoint(dmg * 2);
                    Debug.Log("lo");
                    Destroy(gameObject);
                }
                else if (hits[c].transform.CompareTag("duvar") || other.transform.CompareTag("Ground"))
                {
                    countOfiteration++;
                    if (countOfiteration >= 5)
                    {
                        Destroy(gameObject);
                    }
                    normalOfImpactSurface = hits[c].normal;
                    break;
                }
            }
            rb.velocity = Vector3.zero;
            Vector3 afterImpactMoveDir = Vector3.Reflect(moveDir, normalOfImpactSurface);
            transform.forward = afterImpactMoveDir;
            rb.AddForce(gameObject.transform.forward * 300f);
        }
    }
}