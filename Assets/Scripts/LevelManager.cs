using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Goal blueGoal;
    [SerializeField]
    private Goal twoGoal;
    [SerializeField]
    private Goal thereGoal;

    GameObject player;

    private void Start()
    {
        player = FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.HullController>().gameObject;
    }

    public void CheckpointIsReached(Vector3 checkpointPosition)
    {
        player.SendMessage("CheckpointIsReached", checkpointPosition, SendMessageOptions.RequireReceiver);
    }


    public void GoalIsReached(GameManager.IdentityState goalReached)
    {
        player.SendMessage("GoalIsReached", SendMessageOptions.DontRequireReceiver);
        Debug.Log("Level is Won. Alter " + goalReached + " reached their goal.");
        //Load NextLevel
        //Restart
        //Or Whatever
        //Show Scoreboard
    }

}
