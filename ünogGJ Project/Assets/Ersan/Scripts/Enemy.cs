using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
public Transform target;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.0f;
    public float shootingDistance = 10f;

    public LayerMask obstacleLayer;
    private UnityEngine.AI.NavMeshAgent agent;
    private bool isPlayerInArea = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {        
        
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            RaycastHit hit;
            if (Physics.Linecast(transform.position, target.position, out hit, obstacleLayer))
            {
                agent.SetDestination(target.position);
                isPlayerInArea = true;
                /*
                if (hit.transform.CompareTag("Player"))
                {
                    agent.SetDestination(target.position);
                    isPlayerInArea = true;
                    // agent.ResetPath();
                }
                else
                {
                    isPlayerInArea = false;
                    // agent.SetDestination(target.position);
                    agent.ResetPath();
                }
                */
            }
            else
            {
                isPlayerInArea = false;
                // agent.SetDestination(target.position);
                agent.ResetPath();
            }
            /*

            if (isPlayerInArea && distance < shootingDistance)
            {
                MermiAt();
            }

            if (!isPlayerInArea)
            {
               // agent.SetDestination(target.position);
            }
            */
        }
    }

    void MermiAt()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
