using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class GameManager : MonoBehaviour
{
    

    //This is the Game State! This is the most important variable in the entire game
    public enum IdentityState { Blue = 0, Two  = 1, There = 2, Transition = 3}
    [SerializeField] IdentityState startIdentity = IdentityState.Blue;
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

    float timer;
<<<<<<< HEAD
=======
    float dominanceTimer;
>>>>>>> Finally getting back on track; Fixed logical mistake that didn't differentiate between identities. Implemented the beginnings of identity dominance, imputed also on the UI
    public static float leveltimer;

    //Instantiating the identities
    Identity BlueProperties = new Identity();
    Identity TwoProperties = new Identity();
    Identity ThereProperties = new Identity();
    Identity CurrentIdentityProperties = new Identity();


    //GUIElements - Debug only
    GUIStyle guiStyle = new GUIStyle();

    private void Start()
    {
        currentIdentity = startIdentity;
        OnTransitionExit(currentIdentity);

        UnityStandardAssets.Characters.FirstPerson.HullController.OnDeath += Death;

    }

    // Update is called once per frame
    void Update()
    {
        if(!isInTransition)
        {
            if (timer >= identityDuration)
            {
                nextIdentity = (IdentityState)Random.Range(0, 3);
                TransitionBegin();
                
            }else
            {
                IdentityDominance(currentIdentity);
            }
        } else
        {
            if (timer >= transitionDuration)
            {
                TransitionEnd();
               

            }
        }
        timer += Time.deltaTime;
        dominanceTimer += Time.deltaTime;
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

<<<<<<< HEAD
    void Death(bool isAlive)
    {
        TriggerIdentity((int)startIdentity);
    }
=======
    public void IdentityDominance(IdentityState state)
    {
        switch (state)
        {
            case IdentityState.Blue:
                CurrentIdentityProperties = BlueProperties;
                break;
            case IdentityState.Two:
                CurrentIdentityProperties = TwoProperties;
                break;
            case IdentityState.There:
                CurrentIdentityProperties = ThereProperties;
                break;
        }

        if (dominanceTimer >= 3)
        {
            CurrentIdentityProperties.Dominance += 1;
            if (TriggerWinLevel.levelWon == true)
            {
                CurrentIdentityProperties.Dominance += 50;
                TriggerWinLevel.levelWon = false;
            }
            dominanceTimer = 0;
        }

        switch (state)
        {
            case IdentityState.Blue:
                BlueProperties = CurrentIdentityProperties;
                break;
            case IdentityState.Two:
                TwoProperties = CurrentIdentityProperties;
                break;
            case IdentityState.There:
                ThereProperties = CurrentIdentityProperties;
                break;
        }
    }

>>>>>>> Finally getting back on track; Fixed logical mistake that didn't differentiate between identities. Implemented the beginnings of identity dominance, imputed also on the UI

    private void OnGUI()
    {
        guiStyle.fontSize = 40;
        GUI.Label(new Rect(20, 20, 500, 80), currentIdentity.ToString(), guiStyle);
        GUI.Label(new Rect(250, 20, 500, 80), CurrentIdentityProperties.Dominance.ToString(), guiStyle);
    }

}
