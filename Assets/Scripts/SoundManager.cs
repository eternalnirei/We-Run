using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeID.OnIdentityBegin += MusicFade;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MusicFade(ChangeID.IdentityState state)
    {
        switch (state)
        {
            case ChangeID.IdentityState.Two:
                Debug.Log("State is Two");
                break;
            case ChangeID.IdentityState.Dash:
                Debug.Log("State is: " + state);
                break;
        }
        

        
        //MusicFadeOut w
    }



}
