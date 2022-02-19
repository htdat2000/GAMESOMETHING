using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Test : MonoBehaviour
{
    private Pathfinding pathfinding;

    [SerializeField] private PathMover slime;

    void Start()
    {
        pathfinding = new Pathfinding(10,10);
    }
    void Update() 
    {
        if(Input.GetKeyDown("space"))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetMyGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetMyGrid().GetXY(slime.gameObject.transform.position, out int t, out int s);
            List<PathNode> path = pathfinding.FindPath(t,s,x,y);
            if(path != null)
            {
                for(int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].y) * 10f + Vector3.one * 5f, Color.green);
                }
            }
            slime.SetTargetPosition(mouseWorldPosition);
        }    
        if(Input.GetMouseButton(1))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetMyGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(false);
        }
    }
}
