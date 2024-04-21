using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI2 : MonoBehaviour
{
   public Transform target;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.0f;
    public float shootingDistance = 10f;

    public LayerMask obstacleLayer;
    private NavMeshAgent agent;
    private bool isPlayerInArea = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
            }
            else
            {
                isPlayerInArea = false;
                // agent.SetDestination(target.position);
                agent.ResetPath();
            }
        }
    }

    void MermiAt()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
