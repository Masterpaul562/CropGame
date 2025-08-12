using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedController : PlantBase
{

    float x;
    float y;
    private bool hasBeenCalled;
    [SerializeField] private bool shouldHarvest;
    [SerializeField] GameObject weed;
    [SerializeField] LayerMask interactable;
    [SerializeField] public bool hasGrown;
    [SerializeField] public bool justMade = false;

    private void Awake()
    {
        hasBeenCalled = true;
        sprite = GetComponent<SpriteRenderer>();
        shouldHarvest = true;
        if (!justMade)
        {
            hasGrown = false;
        }
    }

    private void Update()
    {

        if(turnManager.state == TurnState.EVILENDSTEP)
        {
            if (state == GrowState.GROWN )
            {
                if (shouldHarvest)
                {
                    StartCoroutine(Harvest());
                    shouldHarvest=false;
                }
            }
            else
            {
                Grow();
            }
            if (hasBeenCalled)
            {
                hasBeenCalled=false;
                SaveData();
            }
        }
        if(state == GrowState.SEED)
        {
            sprite.sprite = seed;
        }else if (state == GrowState.GROWING)
        {
            sprite.sprite = growing;
        } else if ( state == GrowState.GROWN)
        {
            sprite.sprite = grown;
        }
    }

    protected override void Grow()
    {
        if(state == GrowState.SEED && !hasGrown)
        {
            state = GrowState.GROWING;
            hasGrown=true;
        }else if(state == GrowState.GROWING && !hasGrown)
        {
            hasGrown = true;
            state = GrowState.GROWN;
        }
    }
    protected override void SaveData()
    {
        var refrence = Game_Manager.Instance;
        refrence.plantLocation[refrence.arrayNum] = this.transform.position;
        refrence.plantType[refrence.arrayNum] = "Weed";
        refrence.growthState[refrence.arrayNum] = state;
        refrence.arrayNum++;
    }
    protected override IEnumerator Harvest()
    {
        Debug.Log("YAY");
        int direction = Random.Range(0, 4);
        int count = 0;
        bool foundTile = false;
        while (foundTile == false)
        {
            bool goodToPlant = true;
            if (direction == 0)
            {
                x = transform.position.x + 1;
                y = transform.position.y;

            }
            else if (direction == 1)
            {
                x = transform.position.x - 1;
                y = transform.position.y;
            }
            else if (direction == 2)
            {
                x = transform.position.x;
                y = transform.position.y + 1;
            }
            else if (direction == 3)
            {
                x = transform.position.x;
                y = transform.position.y - 1;
            }

            RaycastHit2D hit = Physics2D.Raycast(new Vector2(x, y), Vector3.forward, 10, interactable);
            if (hit.collider == null)
            {
                direction = Random.Range(0, 4);
                goodToPlant = false;
            }
            if (hit.collider != null)
            {
                Debug.Log("FOUND");
                if (hit.collider.GetComponent<Tile>().occupied)
                {
                    var plant = hit.collider.gameObject.transform.GetChild(2).gameObject;
                    if (plant.tag == "Plantable")
                    {
                        plant.GetComponent<WheatController>().dontSave = true;
                        Destroy(plant);
                        hit.collider.GetComponent<Tile>().occupied = false;
                    }
                    else if (plant.tag == "Weed")
                    {
                        
                        direction = Random.Range(0, 4);
                        goodToPlant = false;
                    }

                }
                
                
            }
            if(goodToPlant)
            {
                Debug.Log("YOU DID IT");
                var spawnedWeed = Instantiate(weed, hit.collider.transform.position, Quaternion.identity);
                hit.collider.GetComponent<Tile>().occupied = true;
                spawnedWeed.transform.parent = hit.collider.transform;
                spawnedWeed.GetComponent<PlantBase>().state = GrowState.SEED;
                spawnedWeed.GetComponent<WeedController>().justMade = true;
                spawnedWeed.GetComponent<WeedController>().hasGrown = true;
                foundTile = true;
            }else
            {
                count++;
                Debug.Log(count);
                if (count >= 4)
                {
                    foundTile = true;
                }
            }
            Debug.Log(hit.collider);
            yield return null;
        }
        

        
        yield return null;
    }
}
