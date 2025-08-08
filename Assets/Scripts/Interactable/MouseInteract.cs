using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteract : MonoBehaviour
{
   
    public LayerMask interactable;
    public bool isHoldingSomething;
   [SerializeField] private GameObject selected;
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private GameObject seedSack;



    void Update()
    {
        if(isHoldingSomething)
        {
            selected = transform.GetChild(0).gameObject;
        } else
        {
            selected = null;
            seedSack = null;
        }
            
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 10, interactable);
            //Debug.Log(hit.collider);    
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "SeedPack" && !isHoldingSomething)
                {
                    SeedSackClick(hit);
                }

                if(hit.collider.gameObject.layer == 8 && !isHoldingSomething)
                {
                   
                    PickUp(hit);
                }

                if (hit.collider.gameObject.tag == "Tile")
                {
                   if (selected != null)
                    {
                        if(selected.tag == "WateringCan" && turnManager.numberOfActions>0)
                        {
                            if (hit.collider.gameObject.GetComponent<Tile>().occupied == true && hit.collider.gameObject.GetComponent<Tile>().isWatered == false)
                            {
                                if (hit.collider.transform.GetChild(2).GetComponent<PlantBase>().state == GrowState.GROWING || hit.collider.transform.GetChild(2).GetComponent<PlantBase>().state == GrowState.MOREGROWING)
                                {
                                    WaterTile(hit);
                                    turnManager.numberOfActions--;
                                }
                            }
                        }


                        if (selected.tag == "Plantable")
                        {
                            if (hit.collider.gameObject.GetComponent<Tile>().occupied == false && turnManager.numberOfActions > 0)
                            {
                                Plant(hit.collider.transform);
                                turnManager.numberOfActions -= 1;
                                hit.collider.gameObject.GetComponent<Tile>().occupied = true;
                               
                            }
                        }
                    }
                }

            } else
            {
                CancelClick();
            }
        }
    }

    private void Plant(Transform tile)
    {
        isHoldingSomething = false;
        selected.transform.position = new Vector2 (tile.position.x, tile.position.y);
        selected.GetComponent<PlantBase>().planted = true;
        selected.transform.parent = tile;

    }
    private void CancelClick()
    {

        if (isHoldingSomething)
        {
            if(selected.tag == "WateringCan")
            {
                selected.transform.position = selected.gameObject.GetComponent<WateringCanController>().ogPos;
                selected.GetComponent<BoxCollider2D>().enabled = true;
                selected.transform.parent = null;
                isHoldingSomething = false;
            }
            if (selected.tag == "Plantable")
            {
                Destroy(selected);
                isHoldingSomething = false;
                seedSack.GetComponent<InteractableBase>().seedCount += 1;
                seedSack = null;
            }
        }
    }
    private void SeedSackClick (RaycastHit2D hit)
    {

       
            if (hit.collider.gameObject.GetComponent<InteractableBase>().seedCount > 0)
            {
                seedSack = hit.collider.gameObject;
                hit.collider.gameObject.GetComponent<InteractableBase>().BaseInteract();
                hit.collider.gameObject.GetComponent<InteractableBase>().seedCount -= 1;
                isHoldingSomething = true;
            }
        }
    private void PickUp(RaycastHit2D hit)
    {
        hit.collider.transform.parent = transform;
        selected = hit.collider.gameObject;
        isHoldingSomething = true;
        hit.collider.enabled = false;
    }
    private void WaterTile(RaycastHit2D hit)
    {
        selected.GetComponent<WateringCanController>().StartWater();
        hit.collider.GetComponent<Tile>().isWatered = true;
    }
}
