using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWinLevel : MonoBehaviour
{
    public static bool levelWon = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GameManager>() != null) levelWon = true;
    }
}
