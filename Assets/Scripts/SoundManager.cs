using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
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
        switch (state)
        {
            case GameManager.IdentityState.Two:
                Debug.Log("State is: " + state);
                break;
            case GameManager.IdentityState.Dash:
                Debug.Log("State is: " + state);
                break;
            case GameManager.IdentityState.See:
                Debug.Log("State is: " + state);
                break;
            case GameManager.IdentityState.Blue:
                Debug.Log("State is: " + state);
                break;
            case GameManager.IdentityState.Make:
                Debug.Log("State is: " + state);
                break;


        }


    }


}
