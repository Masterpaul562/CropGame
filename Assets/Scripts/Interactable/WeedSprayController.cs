using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeedSprayController : ItemBase
{
   
    [SerializeField] private TextMeshPro amountDisplay;

    void Start()
    {
        ogPos = transform.position;
    }
    private void Update()
    {
        amountDisplay.text = "Weed Spray: " + Game_Manager.Instance.amountWeedspray;
           
    }

}
