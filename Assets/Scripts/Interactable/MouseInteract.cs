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
    [SerializeField] private MouseHighlight highLight;
    [SerializeField] private LayerMask tile;



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
        if(turnManager.state == TurnState.ENDSTEP)
        {
            CancelClick();
        }
            
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 10, interactable);
            
            //Debug.Log(hit.collider);    
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "SeedPack" && !isHoldingSomething && turnManager.state == TurnState.ACTION)
                {
                    SeedSackClick(hit);
                }

                if(hit.collider.gameObject.layer == 8 && !isHoldingSomething)
                {
                    if (hit.collider.gameObject.tag == "Weedspray")
                    {
                        if (Game_Manager.Instance.amountWeedspray > 0) 
                        {
                            PickUp(hit);
                            highLight.holdingCan = true;
                            Game_Manager.Instance.amountWeedspray--;
                        }
                    }
                    else
                    {
                        PickUp(hit);
                    }
                }

                if (hit.collider.gameObject.tag == "Tile")
                {
                    var tileScript = hit.collider.GetComponent<Tile>();
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
                        if (selected.tag == "Shovel" && turnManager.numberOfActions > 0)
                        {
                            if (tileScript.occupied)
                            {
                                if (hit.collider.transform.GetChild(2).gameObject.tag == "Weed")
                                {
                                    tileScript.occupied = false;
                                    Destroy(hit.collider.transform.GetChild(2).gameObject);
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
                        if(selected.tag == "Weedspray")
                        {
                            Debug.Log("yay");
                            WeedSpraying(hit,0,0);
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
            if(selected.layer == 8)
            {
                if (selected.tag == "Weedspray")
                {
                    highLight.holdingCan = false;
                   Game_Manager.Instance.amountWeedspray++;
                }
                selected.transform.position = selected.gameObject.GetComponent<ItemBase>().ogPos;
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
        if (turnManager.state != TurnState.ENDSTEP)
        {

            if (hit.collider.gameObject.GetComponent<InteractableBase>().seedCount > 0)
            {
                seedSack = hit.collider.gameObject;
                hit.collider.gameObject.GetComponent<InteractableBase>().BaseInteract();
                hit.collider.gameObject.GetComponent<InteractableBase>().seedCount -= 1;
                isHoldingSomething = true;
            }
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
    private void WeedSpraying(RaycastHit2D hit, int x, int y)
    {
       
        
        for (int i = 0; i < 4; i++)
        {
            
            if (i == 0)
            {
                x = 1;
                y = 0; 
            }
            if (i == 1)
            {
                x = 1;
                y= 1;
            }
            if (i == 2)
            {
                x = 0;
                y = 1;
            }
            if (i == 3)
            {
                x = 0;
                y = 0;  
            }
            var hitTile = Physics2D.Raycast(new Vector2(hit.transform.position.x - x, hit.transform.position.y - y), Vector3.forward, 10, tile);
            
            if (hitTile.collider != null)
            {
                Debug.Log(hitTile.collider.gameObject);
                var script =  hitTile.collider.GetComponent<Tile>();
                if (script.occupied)
                {
                    if(hitTile.collider.transform.GetChild(2).gameObject.tag == "Weed")
                    {
                        script.occupied = false;
                        Destroy(hitTile.collider.transform.GetChild(2).gameObject);
                    }
                }
                hitTile.collider.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        Game_Manager.Instance.amountWeedspray--;
        turnManager.numberOfActions--;
        CancelClick();
    }
}
