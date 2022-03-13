using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int checkPointID;

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SetActiveCheckPoint();
        }
    }

    private void SetActiveCheckPoint()
    {
        CheckPointManager.instance.ActiveCheckPoint(checkPointID);
    }
}
