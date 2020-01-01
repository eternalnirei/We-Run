using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class TherePlatform : MonoBehaviour
{
    SphereCollider sphereCollider;
    MeshRenderer meshRenderer;

    

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
        Deactivate();

        GameManager.OnTransitionExit += ExitTransition;
        GameManager.OnTransitionEnter += EnterTransition;
    }

    void EnterTransition(GameManager.IdentityState state)
    {
        if(state == GameManager.IdentityState.There)
        {
            //makePlatforms slightly visible
        }

    }

    void ExitTransition(GameManager.IdentityState state)
    {
        if (state == GameManager.IdentityState.There)
        {
            Activate();
        }
        else Deactivate();

    }


    void Activate()
    {
        sphereCollider.enabled = true;
        meshRenderer.enabled = true;
    }

    void Deactivate()
    {
        sphereCollider.enabled = false;
        meshRenderer.enabled = false;
    }


}
