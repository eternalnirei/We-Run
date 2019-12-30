using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identity : MonoBehaviour
{
   
    //string name;
    KeyCode[] baseControls;
    int chance; 
    float identityDominance;
    public float IdentityDominance
    {
        get
        {
            return this.identityDominance;
        }
        set
        {
            this.identityDominance = value;
        }
    }

    //timing the Identity
    double identityTimer;
    public double IdentityTimer
    {
        get
        {
            return this.identityTimer;
        }
        set
        {
            this.identityTimer = value;
        }
    }

    //LightMode lightmode;
    //MusicMode musicmode;
    
    public void Replace()
    {

    }

    public void FadeOut()
    {

    }

    public void OverrideBaseControls()
    {

    }

    public float CalculationOfDominance(double IdentityTimer)
    {
        identityDominance = (float)(GameManager.leveltimer - this.IdentityTimer) / 100;

        return identityDominance;
    }


}
