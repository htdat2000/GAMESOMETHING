using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridController : MonoBehaviour
{
    public Pathfinding pathfinding;

    // static public GridController Instance;
    [SerializeField] private Vector3 OriginalPosition;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        pathfinding = new Pathfinding(40,40);
    }

    // Update is called once per frame
    void Update()
    {  
    }
}
