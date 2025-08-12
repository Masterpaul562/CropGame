using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseShopInteract : MonoBehaviour
{
    public LayerMask interactable;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 10, interactable);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "WateringCan")
                {
                    if (Game_Manager.Instance != null)
                    {
                        if (Game_Manager.Instance.money >= 5)
                        {
                            Game_Manager.Instance.money -= 5;
                            Game_Manager.Instance.boughtWateringcan = true;
                        }
                    }
                }
                if (hit.collider.gameObject.tag == "Shovel")
                {
                    if (Game_Manager.Instance != null)
                    {
                        if (Game_Manager.Instance.money >= 10)
                        {
                            Game_Manager.Instance.money -= 10;
                            Game_Manager.Instance.boughtShovel = true;
                        }
                    }
                }


                if (hit.collider.gameObject.tag == "SeedPack")
                {

                    Buy(hit.collider.gameObject.name);
                }

            }
        }
    }
    private void Buy(string type)
    {
        if (type == "WheatSack")
        {
            if (Game_Manager.Instance.money >= 1)
            {
                Game_Manager.Instance.money -= 1;
                Game_Manager.Instance.wheatSeedCount += 1;
            }
        }
        if (type == "CornSack")
        {
            if (Game_Manager.Instance.money >= 3)
            {
                Game_Manager.Instance.money -= 3;
                Game_Manager.Instance.cornSeedCount += 1;
            }
        }
    }
}
