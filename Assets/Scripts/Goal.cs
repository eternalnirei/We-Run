using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Goal : MonoBehaviour
{
    [SerializeField]
    GameManager.IdentityState identityGoal;

    private bool isGoal;
    public bool goalIsReached = false;
    public bool checkpointIsReached = false;
    
    

    ParticleSystem.MainModule particleSystems;
    private Color goalColor;

    // Start is called before the first frame update
    private void Awake()
    {
        GameManager.OnTransitionEnter += TransitionEnter;
        GameManager.OnTransitionExit += TransitionExit;
        particleSystems = GetComponent<ParticleSystem>().main;
    }

    void Start()
    {
        GetComponent<SphereCollider>().isTrigger = true;
        

        switch (identityGoal)
        {
            case GameManager.IdentityState.Blue:
                goalColor = Color.blue;
                break;
            case GameManager.IdentityState.There:
                goalColor = Color.white;
                break;
            case GameManager.IdentityState.Two:
                goalColor = Color.yellow;
                break;

        }

    }


    void TransitionEnter(GameManager.IdentityState state)
    {
        //become indifferent
        particleSystems.startColor = Color.black;
    }

    void TransitionExit(GameManager.IdentityState state)
    {
        if (identityGoal == state)
        {
            isGoal = true;
            particleSystems.startColor = Color.green;
        }
        else
        {
            isGoal = false;
            particleSystems.startColor = goalColor;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isGoal)
            {
                goalIsReached = true;
                SendMessageUpwards("GoalIsReached", identityGoal, SendMessageOptions.RequireReceiver);
                
            } else
            {
                checkpointIsReached = true;
                SendMessageUpwards("CheckpointIsReached", transform.position + Vector3.up, SendMessageOptions.RequireReceiver);
                
            }
        }
    }

}
