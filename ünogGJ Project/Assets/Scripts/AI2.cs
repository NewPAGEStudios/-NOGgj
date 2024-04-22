using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI2 : MonoBehaviour
{
    private float hitPoint = 100;

    public Transform target;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.0f;
    public float shootingDistance = 10f;

    public LayerMask obstacleLayer;
    private NavMeshAgent agent;

    private float mermiAtTime = 3;

    private bool isPlayerInArea = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        agent.SetDestination(target.position);
        hitPoint = 140;
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
                agent.ResetPath();
                if (mermiAtTime <= 0)
                {
                    // agent.SetDestination(target.position);
                    mermiAtTime = 3;
                    MermiAt();
                }
                else
                {
                    mermiAtTime -= Time.deltaTime;
                }
            }
        }
    }
    public void takeDmg(float dmg)
    {
        hitPoint -= dmg;
        if (hitPoint <= 0)
        {
            Destroy(gameObject);
        }
    }
    void MermiAt()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
