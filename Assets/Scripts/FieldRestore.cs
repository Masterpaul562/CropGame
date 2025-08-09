using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldRestore : MonoBehaviour
{
    [SerializeField] GameObject wheatPrefab;
    [SerializeField] GameObject cornPrefab;
    [SerializeField] GameObject weedPrefab;
    [SerializeField] TurnManager turnMan;
    [SerializeField] private LayerMask interactable;

   private void Start()
    {       
        var manRef = Game_Manager.Instance;
        if (manRef.hasStarted)
        {
            for (int i = 0; i < manRef.arrayNum; i++)
            {
                if (manRef.plantType[i] == "Wheat")
                {
                    Replant(wheatPrefab,i);
                } else if (manRef.plantType[i] == "Corn")
                {
                    Replant(cornPrefab, i);
                } else if (manRef.plantType[i] == "Weed")
                {
                    Replant(weedPrefab, i);
                }
            }
            CleanArray();
        }
    }
    private void CleanArray()
    {
        for(int i = 0;i < 36; i++)
        {
            var manRef = Game_Manager.Instance;
            manRef.plantType[i] = null;
            manRef.plantLocation[i] = new Vector3(0,0,0);
            manRef.growthState[i] = GrowState.SEED;
            manRef.arrayNum = 0;

        }
    }
    private void Replant(GameObject prefab, int x)
    {
        
        var manRef = Game_Manager.Instance;
        var restoredPlant = Instantiate(prefab, manRef.plantLocation[x], Quaternion.identity);
        var plantScript = restoredPlant.GetComponent<PlantBase>();
        plantScript.state = manRef.growthState[x];
        plantScript.turnManager = turnMan;
        RaycastHit2D hit = Physics2D.Raycast(manRef.plantLocation[x], Vector3.forward, 10, interactable);
        restoredPlant.transform.parent = hit.collider.transform;
        hit.collider.gameObject.GetComponent<Tile>().occupied = true;
    }
}
