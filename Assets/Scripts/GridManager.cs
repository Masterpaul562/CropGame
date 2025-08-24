using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;

    [SerializeField] private Tile tilePrefab;
    [SerializeField] private MouseHighlight mouseScript;
    [SerializeField] private Transform cam;
    [SerializeField] private float gridSpacingX;
    [SerializeField] private float gridSpacingY;


    private void Awake()
    {
            GenerateGrid();     
    }

    void GenerateGrid()
    {
        for (float x = 0; x< width; x ++)
        {
            for (float y = 0; y< height; y++)
            {
                var spawnedTile =  Instantiate(tilePrefab, new Vector3(x*gridSpacingX,y*gridSpacingY), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.GetComponent<Tile>().mouseScript = mouseScript;
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
            }
        }
     cam.transform.position = new Vector3((float)width/2 -0.5f, (float)height/2 + 2.5f, -10);
    }

}
