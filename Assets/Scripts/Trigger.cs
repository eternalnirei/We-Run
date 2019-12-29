using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Trigger : MonoBehaviour
{
    public GameManager.IdentityState identityToTrigger;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<GameManager>() != null)
        {
            if (other.GetComponent<GameManager>().CurrentIdentity != identityToTrigger)
            other.GetComponent<GameManager>().SendMessage("TriggerIdentity", (int)identityToTrigger, SendMessageOptions.RequireReceiver);
        }
    }


}
