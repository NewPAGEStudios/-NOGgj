using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    //    yunus  emre buradaydý :)
    private float boomTime = 3f;
    private bool notActive = true;
    private void Update()
    {
        if (notActive)
        {
            return;
        }
        if (boomTime <= 0)
        {
            gameObject.GetComponentInChildren<Collider>().enabled = true;
        }
        else
        {
            boomTime -= Time.deltaTime;
        }
    }
    public void boom()
    {
        notActive = false;
    }


}
