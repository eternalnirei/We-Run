using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class GameManager : MonoBehaviour
{
    

    //This is the Game State! This is the most important variable in the entire game
    public enum IdentityState { Blue = 0, Two  = 1, There = 2, Transition = 3}
    [SerializeField] IdentityState startIdentity;
    private IdentityState currentIdentity;
    public IdentityState CurrentIdentity
    {
        get => currentIdentity;
        private set { currentIdentity = value; }
    }
    private IdentityState nextIdentity;

    //This is the Event Manager. It broadcasts possible changes to whoever in the game wants to listen to it.
    public delegate void ChangeIdentity(IdentityState nextIdentity);
    public static event ChangeIdentity OnTransitionEnter;
    public static event ChangeIdentity OnTransitionExit;

    //These control the identity switch itself
    [SerializeField]
    float identityDuration = 30;

    [SerializeField]
    float transitionDuration = 2;

    bool isInTransition;

    //to see if each identity has passed through at least once
    bool firstTimeBlue;
    bool firstTimeTwo;
    bool firstTimeThere;

    //time to switch identities
    float timer;

    //Instances of the Identity class to match up with the IdentityState
    Identity BlueIdentity = new Identity();
    Identity TwoIdentity = new Identity();
    Identity ThereIdentity = new Identity();
    Identity CurrentIdentityCharacteristics = new Identity();

    //timing the level
    public static double leveltimer;

    //GUIElements - Debug only
    GUIStyle guiStyle = new GUIStyle();

    private void Start()
    {
        leveltimer = 0f;
        BlueIdentity.IdentityTimer = 0f;
        TwoIdentity.IdentityTimer = 0f;
        ThereIdentity.IdentityTimer = 0f;
        currentIdentity = startIdentity;
        OnTransitionExit(currentIdentity);

    }

    // Update is called once per frame
    void Update()
    {
        if(!isInTransition)
        {
            //switch to keep track of the time spent in each identity AND the characteristics of the current identity
            switch (currentIdentity)
            {
                case IdentityState.Blue:
                    BlueIdentity.IdentityTimer += Time.deltaTime;
                    CurrentIdentityCharacteristics = BlueIdentity;
                    break;
                case IdentityState.There:
                    ThereIdentity.IdentityTimer += Time.deltaTime;
                    CurrentIdentityCharacteristics = ThereIdentity;
                    break;
                case IdentityState.Two:
                    ThereIdentity.IdentityTimer += Time.deltaTime;
                    CurrentIdentityCharacteristics = TwoIdentity;
                    break;
                default:
                    Debug.LogError("Error in the Switch(CurrentIdentity) of GameManager.Update");
                    break;
            }
            if (timer >= identityDuration)
            {
                //TODO IMPLEMENT THE CALCULATIONS FOR THE SELECTION OF THE NEXT IDENTITY
                nextIdentity = IdentitySelection();
                TransitionBegin();
                
            }
        } else
        {
            if (timer >= transitionDuration)
            {
                TransitionEnd();
               

            }
        }

        timer += Time.deltaTime;
        leveltimer += Time.deltaTime;
    }

    void TransitionBegin()
    {
        isInTransition = true;
        //resetting other timer
        timer = 0;
        //Event OnTransition Enter is broadcasted.
        OnTransitionEnter(CurrentIdentity);
        currentIdentity = IdentityState.Transition;
    }


    void TransitionEnd()
    {
        isInTransition = false;
        //resetting timer
        timer = 0;
        //Parses int to enum
        CurrentIdentity = (IdentityState)nextIdentity;

        //Event On Transition Exit is broadcasted
        OnTransitionExit(CurrentIdentity);
        
        
    }

    public void TriggerIdentity(int identityToTrigger)
    {
        nextIdentity = (IdentityState)identityToTrigger;
        if (!isInTransition)
        {
            TransitionBegin();
        } else
        {
            TransitionEnd();
        }
    }


    private void OnGUI()
    {
        guiStyle.fontSize = 40;
        GUI.Label(new Rect(20, 20, 500, 80), currentIdentity.ToString(), guiStyle);
        GUI.Label(new Rect(550, 20, 500, 80), CurrentIdentityCharacteristics.IdentityTimer.ToString(), guiStyle);
    }

    IdentityState IdentitySelection()
    {
        /*the dominance of an identity depends on the time spent in that identity, as well
        as if the identity has passed again, the stability of the identity, and randomness*/
        //compare the resulting float numbers and declare the corresponding identity the dominant one
        //create or find the instance of each identity (maybe in Identity) and connect it to here.
        IdentityState dominantIdentity = (IdentityState)0;
        float[] dominance = new float[3];
        float maxDominance = 0f;
        int maxIdentity = 0;

        BlueIdentity.IdentityDominance = BlueIdentity.CalculationOfDominance(BlueIdentity.IdentityTimer);
        TwoIdentity.IdentityDominance = TwoIdentity.CalculationOfDominance(TwoIdentity.IdentityTimer);
        ThereIdentity.IdentityDominance = ThereIdentity.CalculationOfDominance(ThereIdentity.IdentityTimer);

        for (int x = 0; x < 3; x++)
        {
            switch (x)
            {
                case 0: 
                    dominance[0] = BlueIdentity.IdentityDominance;
                    break;
                case 1: 
                    dominance[1] = TwoIdentity.IdentityDominance;
                    break;
                case 2: 
                    dominance[2] = ThereIdentity.IdentityDominance;
                    break;
                default:
                    Debug.LogError("Error in the Switch(x) of GameManager.IdentitySelection");
                    break;
            }
            if (maxDominance >= dominance[x])
            { 
                maxDominance = dominance[x];
                maxIdentity = x;
            }
        }
        switch (maxIdentity)
        {
            case 0: 
                dominantIdentity = (IdentityState)0;
                break;
            case 1:
                dominantIdentity = (IdentityState)1;
                break;
            case 2:
                dominantIdentity = (IdentityState)2;
                break;
            default:
                Debug.LogError("Error in the Switch(maxIdentity) of GameManager.IdentitySelection");
                break;
        }
        return dominantIdentity;
    }

}
