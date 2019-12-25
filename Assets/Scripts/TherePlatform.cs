﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(MeshRenderer))]
public class TherePlatform : MonoBehaviour
{

    Collider collider;
    MeshRenderer meshRenderer;

    

    void Start()
    {
        collider = GetComponent<Collider>();
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
        collider.enabled = true;
        meshRenderer.enabled = true;
    }

    void Deactivate()
    {
        collider.enabled = false;
        meshRenderer.enabled = false;
    }


}
