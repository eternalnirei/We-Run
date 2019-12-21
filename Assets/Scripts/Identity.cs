using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identity : MonoBehaviour
{
    //SETTING UP GETTERS AND SETTERS FOR THE IDENTITIES' FIELDS
    public class IdentityValues
    {
        //THE DEFAULT MOVEMENT IN ALL IDENTITIES IS: FORWARD, LEFT, BACKWARD, RIGHT, JUMP

        //WHAT ARE EACH IDENTITIES' CONTROLS?
        public KeyCode[] BaseControls { get; set; }

        //WHAT IS THE CHANCE OF EACH CHARACTER BE THE ONE THAT IS TRIGGERED?
        int chance;
        public int Chance
        {
            get
            {
                return this.chance;
            }
            set
            {
                this.chance = value;
            }
        }

        //HOW DOMINANT IS THE CHARACTER? 
        int dominance;
        public int Dominance
        {
            get
            {
                return this.dominance;
            }
            set
            {
                this.dominance = value;
            }
        }
        //LightMode lightmode; ENUM?
        //MusicMode musicmode; ENUM?
        //maybe put the jumpforce in here and not in CharacterController
    }

    //SETTING THE VALUES OF THE IDENTITIES OF THE CHARACTERS
    public static IdentityValues Surge = new IdentityValues()
    {
        BaseControls = new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Space },
        Chance = 20,
        Dominance = 20
    };

    public static IdentityValues Blue = new IdentityValues()
    {
        //THE NORMAL CONTROLS
        Chance = 20,
        Dominance = 20,
        BaseControls = new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Space }
    };

    public static IdentityValues Two = new IdentityValues()
    {
        //THE CONTROLS ON THE X AXIS ARE REVERSED
        Chance = 20,
        Dominance = 20,
        BaseControls = new KeyCode[] { KeyCode.W, KeyCode.D, KeyCode.S, KeyCode.A, KeyCode.Space }
    };

    public static IdentityValues There = new IdentityValues()
    {
        //THE CONTROLS ON THE X AXIS ARE REVERSED
        Chance = 20,
        Dominance = 20,
        BaseControls = new KeyCode[] { KeyCode.W, KeyCode.D, KeyCode.S, KeyCode.A, KeyCode.Space }
    };

    public static IdentityValues Make = new IdentityValues()
    {
        //THE CONTROLS ON THE X AXIS ARE REVERSED
        Chance = 20,
        Dominance = 20,
        BaseControls = new KeyCode[] { KeyCode.W, KeyCode.D, KeyCode.S, KeyCode.A, KeyCode.Space }
    };

    public void Replace()
    {

    }

    public void FadeOut()
    {

    }

    public void OverrideBaseControls()
    {

    }




}
