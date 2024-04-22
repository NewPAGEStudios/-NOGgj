using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eNEM : MonoBehaviour
{
    public float hitPoint;

    public controller gc;


    private void Start()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<controller>();    
    }

    public void setHitPoint(float dmg)
    {
        hitPoint -= dmg;
        if (hitPoint <= 0)
        {
            gc.needKilltoLevelUP -= 1;
            Destroy(gameObject);
        }
    }
}
