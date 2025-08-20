using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHighlight : MonoBehaviour
{

    [SerializeField] private LayerMask tile;
    public bool holdingCan;
    [SerializeField] RaycastHit2D hit;

    private void Update()
    {
        if (holdingCan)
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
                if (i==2)
                {
                    hit = RayCast(0, 1);
                }
                if (hit.collider != null)
                {
                    hit.collider.GetComponent<Tile>().highLight.SetActive(true);
                    hit.collider.gameObject.GetComponent<Tile>().Start();
                }
            }

        }
    }

    private RaycastHit2D RayCast(int x,int y)
    {
         hit = Physics2D.Raycast(new Vector2(transform.position.x - x, transform.position.y - y), Vector3.forward, 10, tile);

            return hit;

    }
}
