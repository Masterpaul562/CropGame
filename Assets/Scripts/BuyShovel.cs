using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyShovel : MonoBehaviour
{
    void Update()
    {
        if (Game_Manager.Instance != null)
        {
            if (Game_Manager.Instance.boughtShovel)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                Destroy(this);
            }
        }
    }
}
