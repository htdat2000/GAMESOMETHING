using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class PathMover : MonoBehaviour
{

    private int currentPathIndex;
    private float speed = 0.5f;
    private List<Vector3> pathVectorList;
    private bool chasing = false;
    
    private GameObject player;
    // Start is called before the first frame update
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void StopMoving()
    {
        pathVectorList = null;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public void Update()
    {
        if(chasing == true)
        {
            SetTargetPosition(player.transform.position);
            HandleMovement();
        }
    }
    private void HandleMovement()
    {
        if(pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex] ; //+ new Vector3(Pathfinding.Instance.GetMyGrid().GetCellSize()/2, Pathfinding.Instance.GetMyGrid().GetCellSize()/2, 0)
            if(Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                Vector3 moveDir = (targetPosition -transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                //animate
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
                // transform.position = Vector3.Cross(transform.position, new Vector3(1f, 1f, 0f));
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
    public void Chase()
    {
        chasing = true;
    }
    public void StopChasing()
    {
        chasing = false;
    }
    public bool GetChasing()
    {
        return chasing;
    }
}
