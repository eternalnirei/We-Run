using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //This is the Game State! This is the most important variable in the entire game
    public enum IdentityState { Two, Dash, See, Blue, Make }

    private IdentityState currentIdentity;
    public IdentityState CurrentIdentity
    {
        get => currentIdentity;
        private set { currentIdentity = value; }
    }

    //This is the Event Manager. It broadcasts possible changes to whoever in the game wants to listen to it.
    public delegate void ChangeIdentity(IdentityState nextIdentity);
    public static event ChangeIdentity OnTransitionEnter;
    public static event ChangeIdentity OnTransitionExit;

    //These control the identity switch itself
    [SerializeField]
    float timeToSwitch = 10;
    [SerializeField]
    float transitionDuration = 2;
    bool isInTransition;

    
    public Material calm;
    public Material excited;
    public Material psychotic;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!isInTransition)
        {
            if (timeToSwitch <= 0 || Input.GetKeyDown(KeyCode.C))
            {
                TransitionBegin();
                isInTransition = true;
                //resetting other timer
                transitionDuration = 2;
            }
        } else
        {
            if (transitionDuration <= 0)
            {
                TransitionEnd();
                isInTransition = false;
                //resetting timer
                timeToSwitch = Random.Range(3, 6);

            }
        }

        timeToSwitch -= Time.deltaTime;
        transitionDuration -= Time.deltaTime;
    }

    void TransitionBegin()
    {

        //Event OnTransition Enter is broadcasted.
        OnTransitionEnter(CurrentIdentity);
    }


    void TransitionEnd()
    {
        //getting the renderer component in order to use the materials
        Renderer rend = GetComponent<Renderer>();
        int randSwitch = Random.Range(0, 5);
        switch (randSwitch)
        {
            case 0:
                CurrentIdentity = IdentityState.Two; 
                break;
            case 1:
                CurrentIdentity = IdentityState.Dash;
                break;
            case 2:
                CurrentIdentity = IdentityState.See;
                break;
            case 3:
                CurrentIdentity = IdentityState.Blue;
                break;
            case 4:
                CurrentIdentity = IdentityState.Make;
                break;
        }

        //Event On Transition Exit is broadcasted
        OnTransitionExit(CurrentIdentity);
        
        
    }
}
