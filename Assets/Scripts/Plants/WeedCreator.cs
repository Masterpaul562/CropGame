using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedCreator : MonoBehaviour
{

    [SerializeField] private TurnManager turnManager;
    [SerializeField] private GameObject weedPrefab;
    [SerializeField] LayerMask interactable;
    private bool doOnce;

    void Awake()
    {
        doOnce = true;
    }

    
    void Update()
    {
        
        if (turnManager.GetComponent<TurnManager>().state == TurnState.EVILENDSTEP && doOnce)
        {
            doOnce = false;
            int random = Random.Range(0, 3);
            int x = Random.Range(0, 6);
            int y = Random.Range(0, 6);
            
            if (random == 0)
            {
                StartCoroutine(Plant(x, y));
            }
            
        }
    }

    private IEnumerator Plant(int x, int y)
    {
       
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(x, y), Vector3.forward, 10, interactable);
       
        if (hit.collider != null)
        {
            var tileScript = hit.collider.GetComponent<Tile>();
            if (tileScript.occupied == true)
            {
                if (tileScript.transform.GetChild(2).gameObject.tag == "Weed")
                {
                    doOnce = true;
                    StopCoroutine(Plant(x,y));

                   
                }else if (tileScript.transform.GetChild(2).gameObject.tag == "Plantable")
                {
                    tileScript.occupied = false;
                    Destroy(tileScript.transform.GetChild(2).gameObject);
                }
                
            }
            var newWeed = Instantiate(weedPrefab, hit.collider.transform.position, Quaternion.identity);
            newWeed.transform.parent = hit.collider.transform;
            tileScript.occupied = true;           
            var weedScript = newWeed.GetComponent<WeedController>();
            weedScript.justMade = true;
            weedScript.hasGrown = true;
            weedScript.turnManager = turnManager;
            weedScript.state = GrowState.SEED;
            yield return null;
        }
    }
}
