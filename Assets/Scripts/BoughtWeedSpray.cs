using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoughtWeedSpray : MonoBehaviour
{
    void Update()
    {
        if (Game_Manager.Instance != null)
        {
            if (Game_Manager.Instance.boughtWeedspray)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                Destroy(this);
            }
        }
    }
}
