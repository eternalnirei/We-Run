using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private GameManager.IdentityState currentIdentity;


    void Start()
    {
        GameManager.OnTransitionExit += MyFunction;
        GameManager.OnTransitionEnter += MyFunction2;
    }

   

    void MyFunction2(GameManager.IdentityState state)
    {
       
    }

    void MyFunction(GameManager.IdentityState state)
    {

        currentIdentity = state;
        Debug.Log("State is: " + state);



    }


}
