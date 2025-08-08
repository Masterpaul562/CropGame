using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCanBuy : MonoBehaviour
{
   
    void Update()
    {
        if(Game_Manager.Instance != null)
        {
            if(Game_Manager.Instance.boughtWateringcan)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                Destroy(this);
            }
        }
    }
}
