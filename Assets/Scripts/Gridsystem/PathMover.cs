using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMover : MonoBehaviour
{

    private int currentPathIndex;
    private float speed = 100f;
    private List<Vector3> pathVectorList;
    // Start is called before the first frame update
    private void StopMoving()
    {
        pathVectorList = null;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    private void HandleMovement()
    {
        if(pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if(Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition -transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                //animate
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            }
            else
            {
                currentPathIndex++;
                if(currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                    //animate
                }
            }
        }
        else
        {
            //animate
        }
    }
    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);

        if(pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
            HandleMovement();
        }
    }
}
