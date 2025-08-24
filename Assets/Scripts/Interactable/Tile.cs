using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{



    [SerializeField] private LayerMask tile;
    [SerializeField] RaycastHit2D hit;
    [SerializeField] private Color baseColor, offSetColor;
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private GameObject highLight;
    [SerializeField] public MouseHighlight mouseScript;
    public bool isWatered;
    public bool occupied;

    private void Update()
    {
        if (occupied)
        {
            if (transform.GetChild(2) != null)
            {


                if (isWatered)
                {

                    transform.GetChild(2).gameObject.GetComponent<PlantBase>().isWatered = true;

                }

            }
        }
    }

    public void Init(bool isOffset)
    {
        render.color = isOffset ? offSetColor : baseColor;
    }
 
        
    void OnMouseEnter ()
    {
        if (mouseScript.holdingCan)
        {
            Highlight(true);
        }
        highLight.SetActive (true);
    }
    void OnMouseExit () { 
        if (mouseScript.holdingCan)
        {
            Highlight (false);  
        }
    highLight.SetActive (false);
     }



    private RaycastHit2D RayCast(int x, int y)
    {
        hit = Physics2D.Raycast(new Vector2(transform.position.x - x, transform.position.y - y), Vector3.forward, 10, tile);

        return hit;

    }
    private void Highlight(bool state)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                hit = RayCast(1, 0);
            }
            if (i == 1)
            {
                hit = RayCast(1, 1);
            }
            if (i == 2)
            {
                hit = RayCast(0, 1);
            }
            if (hit.collider != null)
            {
                hit.collider.GetComponent<Tile>().highLight.SetActive(state);

            }
        }
    }
}
