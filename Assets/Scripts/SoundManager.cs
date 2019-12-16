using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    void Start()
    {
        GameManager.OnTransitionExit += MusicFadeIn;
        GameManager.OnTransitionEnter += MusicFadeOut;
    }

   

    void MusicFadeOut(GameManager.IdentityState state)
    {
       
    }

    void MusicFadeIn(GameManager.IdentityState state)
    {
        switch (state)
        {
            case GameManager.IdentityState.Two:
                Debug.Log("State is Two");
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
