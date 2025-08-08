using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWateringCan : MonoBehaviour
{
    private void Update()
    {
        if (Game_Manager.Instance != null)
        {
            if (Game_Manager.Instance.boughtWateringcan)
            {
                Destroy(gameObject);
            }
        }
    }
}
