using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using CodeMonkey.Utils;

public class MyGrid<TGridObject>
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private TGridObject[,] gridArray;
    private TextMesh[,] debugTextArray;
    public MyGrid(int width, int height, float cellSize, Vector3 originPosition, Func<MyGrid<TGridObject>, int, int, TGridObject> createGridbject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];
        debugTextArray = new TextMesh[width, height];
        for(int x = 0; x < gridArray.GetLength(0); x++)
        {
            for(int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x,y] = createGridbject(this, x, y);
            }
        }

        for(int x = 0; x < gridArray.GetLength(0); x++)
        {
            for(int y = 0; y < gridArray.GetLength(1); y++)
            {
                // debugTextArray[x,y] = UtilsClass.CreateWorldText(gridArray[x,y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);
                // Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y+1), Color.white, 200f);
                // Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x+1, y), Color.white, 200f);
            }
        }
        // Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 200f);
        // Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 200f);
                
    }
    public float GetCellSize()
    {
        return cellSize;
    }
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x,y) * cellSize + originPosition;
    }
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    public void SetGridObject(int x, int y, TGridObject value)
    {
        if(x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].color = Color.white;
        }
    }

    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }
    public TGridObject GetGridObject(int x, int y)
    {
        if(x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
            return default(TGridObject);
    }
    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }
    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
}
