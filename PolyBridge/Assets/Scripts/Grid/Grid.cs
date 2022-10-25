using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject prefab;
    public float size;
    public float space;
    public int width;
    public int height;

    int realWidth;
    int realHeight;

    Vector2 center;
    Vector2 offset;
    List<Vector2> grid;

    Vector2[] top;
    Vector2[] bottom;
    Vector2[] left;
    Vector2[] right;

    GameObject parentGrid;

    private void Awake()
    {
        realWidth = width + 1;
        realHeight = height + 1;
        top = new Vector2[realWidth];
        bottom = new Vector2[realWidth];
        left = new Vector2[realHeight];
        right = new Vector2[realHeight];

        center = getCenter();
        offset = getOffset();

        grid = new List<Vector2>();

        parentGrid = GameObject.Find("Grid");

        createGrid();
    }

    Vector2 getCenter()
    {
        return new Vector2((realWidth / 2), (realHeight / 2)) * (size + space) * -1 + offset;
    }

    Vector2 getOffset()
    {
        return Vector2.one * (size / 2);
    }

    void createGrid()
    {
        int cptTop = 0;
        int cptBottom = 0;
        int cptLeft = 0;
        int cptRight = 0;

        for (int x = 0; x < realWidth; x++)
        {
            for(var y = 0; y < realHeight; y++)
            {
                Vector2 position = getWorldPosition(x, y);
                if (y == 0)
                {
                    bottom[cptBottom] = position;
                    cptBottom++;
                }
                if (y == realHeight - 1)
                {
                    top[cptTop] = position;
                    cptTop++;
                }
                if (x == 0)
                {
                    left[cptLeft] = position;
                    cptLeft++;
                }
                if (x == realWidth - 1)
                {
                    right[cptRight] = position;
                    cptRight++;
                }
                grid.Add(position);
            }
        }
        drawLines();
    }

    void drawLines()
    {
        for(int i = 0; i < realWidth; i++)
        {
            GameObject g = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            g.transform.parent = parentGrid.transform;
            GridLine gl = g.GetComponent<GridLine>();
            gl.draw(top[i], bottom[i]);
        }
        for (int i = 0; i < realHeight; i++)
        {
            GameObject g = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            g.transform.parent = parentGrid.transform;
            GridLine gl = g.GetComponent<GridLine>();
            gl.draw(left[i], right[i]);
        }
    }

    Vector3 getWorldPosition(int x, int y)
    {
        return new Vector2(x, y) * (size + space) + center;
    }

    public Vector2 getRealPosition(Vector3 position)
    {
        int i = Mathf.RoundToInt(position.x / (size + space));
        int j = Mathf.RoundToInt(position.y / (size + space));
        return new Vector2(i, j) * (size + space);
        
    }

    public bool isInGrid(Vector2 position)
    {
        if (grid.Contains(position))
        {
            return true;
        }
        return false;
    }

    public Vector3 getAnchorLeft()
    {
        int i = 0;
        int j = (int)(realHeight / 2);
        return getWorldPosition(-i,j);
    }

    public Vector3 getAnchorRight()
    {
        int i = realWidth-1;
        int j = (int)(realHeight / 2);
        return getWorldPosition(i, j);
    }
}
