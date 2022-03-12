using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{   
    public static CheckPointManager instance;
    public GameObject checkPoint;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("More than 1 CheckPointManager in scene");
        }
    }

    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = checkPoint.transform.position;
    }
}
