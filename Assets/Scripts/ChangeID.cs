using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeID : MonoBehaviour
{
    public Material calm;
    public Material excited;
    public Material psychotic;

    public enum IdentityState {Two, Dash, See, Blue, Make};
    IdentityState currentIdentity = IdentityState.Blue;

    float timeToSwitch;
    float transitionDuration;
    int randSwitch;

    //Event Manager
    public delegate void ChangeIdentity(IdentityState nextIdentity);
    public static event ChangeIdentity OnIdentityBegin;


    // Start is called before the first frame update
    void Start()
    {
        timeToSwitch = 10;
        transitionDuration = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            renderingMaterial();
        if (timeToSwitch <= 0)
        {
            renderingMaterial();
        }
        timeToSwitch -= Time.deltaTime;
        //Debug.Log(timeToSwitch);
    }

     
    void renderingMaterial()
    {
        //getiing the renderer component in order to use the materials
        Renderer rend = GetComponent<Renderer>();
        randSwitch = Random.Range(0, 5);
        switch (randSwitch)
        {
            case 0:
                currentIdentity = IdentityState.Two;
                
                break;
            case 1:
                currentIdentity = IdentityState.Dash;
                break;
            case 2:
                currentIdentity = IdentityState.See;
                break;
            case 3:
                currentIdentity = IdentityState.Blue;
                break;
            case 4:
                currentIdentity = IdentityState.Make;
                break;
        }
        OnIdentityBegin(currentIdentity);
        Debug.Log(currentIdentity);
        //resetting timer
        timeToSwitch = Random.Range(3,6);
    }
}
