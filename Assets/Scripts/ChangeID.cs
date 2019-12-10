using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeID : MonoBehaviour
{
    public Material calm;
    public Material excited;
    public Material psychotic;
    public enum states {Calm, Excited, Psychotic};
    float timeToSwitch;
    int randSwitch;

    // Start is called before the first frame update
    void Start()
    {
        timeToSwitch = 3;
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
        Debug.Log(timeToSwitch);
    }

     
    void renderingMaterial()
    {
        //getiing the renderer component in order to use the materials
        Renderer rend = GetComponent<Renderer>();
        randSwitch = Random.Range(0, 3);
        switch (randSwitch)
        {
            case 0:
                rend.material = calm;
                break;
            case 1:
                rend.material = excited;
                break;
            case 2:
                rend.material = psychotic;
                break;
        }
        //resetting timer
        timeToSwitch = 3;
    }
}
//Hello Thaleia, this is Sara, if you are reading this, it means it works.