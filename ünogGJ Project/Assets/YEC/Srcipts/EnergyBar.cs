using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Animator[] animators;
    private float dashCurrent;
    private bool dashUsed;
    float dashTime=3f;
    // Start is called before the first frame update
    void Start()
    {
        dashCurrent = 3;
        animators = GetComponentsInChildren<Animator>();

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && dashCurrent > 0 ){
            dashUse();
        }
        if(dashTime<=0 && dashCurrent <3){
            dashCurrent+=1;
            EnergyBarUpdate();
            dashTime=3f;
        }
        else{
            dashTime-=Time.deltaTime;
        }
    }
    private void dashUse(){
        dashCurrent--;
        dashTime=3f;
        EnergyBarUpdate();
        
    }
    
    private void EnergyBarUpdate(){
    for (int i = 0; i < animators.Length; i++) {
        animators[i].SetBool("Less", dashCurrent < i + 1);
    }


    }

    
}
