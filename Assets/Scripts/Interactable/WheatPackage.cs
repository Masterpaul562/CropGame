using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WheatPackage : InteractableBase
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject wheatPrefab;
    [SerializeField] private Transform cursor;
    [SerializeField] private TextMeshPro countDisplay;
    [SerializeField] private TurnManager turnManager;

    private void Awake()
    {
        
        if (Game_Manager.Instance.hasStarted)
        {
            seedCount = Game_Manager.Instance.wheatSeedCount;
        }
    }
    private void Update()
    {
        countDisplay.text = "Wheat: " + seedCount;
        if (turnManager.state != TurnState.ENDSTEP)
        {
            Game_Manager.Instance.wheatSeedCount = seedCount;
        }
        else
        {
            seedCount = Game_Manager.Instance.wheatSeedCount;
        }
    }
    protected override void Interact()
    {
       var spawnedSeed  = Instantiate(wheatPrefab, cursor.transform.position, Quaternion.identity);
       var seedScript = spawnedSeed.GetComponent<PlantBase>();     
        
        spawnedSeed.transform.parent = cursor.transform;
        
        seedScript.turnManager = turnManager;
        seedScript.state = GrowState.SEED;
              
    }
    private void OnMouseEnter()
    {
        menu.GetComponent<SeedMenu>().shouldPull = true;
    }
    private void OnMouseExit()
    {
        menu.GetComponent<SeedMenu>().shouldPull = false;
    }
}
