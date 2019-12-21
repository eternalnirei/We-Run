using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identity : MonoBehaviour
{
    //public class IdentityProperties
    //{
        //SETTING UP GETTERS AND SETTERS FOR THE IDENTITIES' FIELDS
        /*string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }*/
        //THE DEFAULT MOVEMENT IN ALL IDENTITIES IS: FORWARD, LEFT, BACKWARD, RIGHT, JUMP
        public KeyCode[] BaseControls { get; set; } //WHAT ARE EACH IDENTITIES' CONTROLS?

        int chance; //WHAT IS THE CHANCE OF EACH CHARACTER BE THE ONE THAT IS TRIGGERED?
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

        int dominance; //HOW DOMINANT IS THE CHARACTER 
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
    //}

    public Identity Blue = new Identity();

    //SETTING THE VALUES OF THE IDENTITIES OF THE CHARACTERS
    public void IdentityProperties()
    {
        //SETTING TWO
        //two.Name = "Two";
        //reversed on the axis of X
        //two.BaseControls = new KeyCode[] { KeyCode.W, KeyCode.D, KeyCode.S, KeyCode.A, KeyCode.Space };
        //two.Chance = 20;
        //two.Dominance = 20;

        //SETTING SURGE
        //SETTING THERE
        //SETTING BLUE
        //SETTING MAKE
    }


    public void IdentyBlue()
    {
        Identity Blue = new Identity();
        //Blue.Name = "Blue";
        Blue.Chance = 20;
        Blue.Dominance = 20;
        Blue.BaseControls = new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Space };
    }

    static public void IdentityTwo()
    {
        Identity Two = new Identity();
        Two.Chance = 20;
        Two.Dominance = 20;
        Two.BaseControls = new KeyCode[] { KeyCode.W, KeyCode.D, KeyCode.S, KeyCode.A, KeyCode.Space };
    }

    public void IdentitySurge()
    {

    }

    public void IdentityThere()
    {

    }

    public void IdentityMake()
    {

    }

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
