using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{   
    public static CheckPointManager instance;
    public GameObject checkPoint;

    public CheckPointController[] checkPoints;
    private GameObject currentCheckPoint;

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

    public void Revive(GameObject player)
    {
        player.transform.position = currentCheckPoint.transform.position;
    }

    public void ActiveCheckPoint(int i)
    {
        if(checkPoints[i].isActive != true)
        {
            checkPoints[i].isActive = true;
        }
    }

    public void ChooseCheckPoint(int checkPointIndex)
    {
        currentCheckPoint = checkPoints[checkPointIndex].checkPoint;
    }
}
