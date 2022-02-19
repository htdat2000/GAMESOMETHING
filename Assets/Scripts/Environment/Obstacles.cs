using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private int x, y;
    private void Start()
    {
        GridController.Instance.pathfinding.GetMyGrid().GetXY(this.transform.position, out int x, out int y);
        GridController.Instance.pathfinding.GetNode(x, y).SetIsWalkable(false);
    }
}
