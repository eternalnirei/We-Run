﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(10f);
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.VelocityChange);
    }

   
}
