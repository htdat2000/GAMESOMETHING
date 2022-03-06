using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private int x, y;
    private void Start()
    {
        GridController.Instance.pathfinding.GetMyGrid().GetXY(this.transform.position - new Vector3(0.1f, 0.1f, 0f), out int x, out int y);
        GridController.Instance.pathfinding.GetNode(x, y)?.SetIsWalkable(false);
        GridController.Instance.pathfinding.GetNode(x+1, y)?.SetIsWalkable(false);
        GridController.Instance.pathfinding.GetNode(x, y+1)?.SetIsWalkable(false);
        GridController.Instance.pathfinding.GetNode(x+1, y+1)?.SetIsWalkable(false);
    }
}
