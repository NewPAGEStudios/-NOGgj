using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI1 : MonoBehaviour
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
         float distance = Vector3.Distance(transform.position, target.position);
         agent.SetDestination(target.position);

      
        
    }
}
