using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CornPackage : InteractableBase
{
    [SerializeField] private GameObject menu;
    [SerializeField] private TextMeshPro countDisplay;
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private GameObject cornPrefab;
    [SerializeField] private Transform cursor;


    private void Awake()
    {
        if (Game_Manager.Instance != null)
        {
            if (Game_Manager.Instance.hasStarted)
            {
                seedCount = Game_Manager.Instance.cornSeedCount;
            }
        }
    }

    private void Update()
    {
        countDisplay.text = "Corn: " + seedCount;
        if (turnManager.state != TurnState.ENDSTEP)
        {
            Game_Manager.Instance.cornSeedCount = seedCount;
        }
        else
        {
            seedCount = Game_Manager.Instance.cornSeedCount;
        }
    }



    protected override void Interact()
    {
        var spawnedSeed = Instantiate(cornPrefab, cursor.transform.position, Quaternion.identity);
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
