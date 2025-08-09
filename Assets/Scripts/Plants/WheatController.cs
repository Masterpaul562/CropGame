using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatController : PlantBase
{  
  
    private bool hasBeenCalled;
    private bool hasGrown;
    public bool dontSave;
    bool x = true;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        hasBeenCalled = true;
        hasGrown = false;
        dontSave = false;
    }
    void Update()
    {
        if (turnManager.state == TurnState.ENDSTEP)
        {
            Grow();
            if (state == GrowState.GROWN)
            {

                dontSave = true;

                if (x)
                {
                    StartCoroutine(Harvest());
                    x = false;
                }
            }
            if (hasBeenCalled && !dontSave)
            {

                hasBeenCalled = false;
                SaveData();
            }


        }
        if (state == GrowState.GROWING)
        {
            sprite.sprite = growing;
        }
        if (state == GrowState.GROWN)
        {
            sprite.sprite = grown;
        }
        if (state == GrowState.SEED)
        {
            sprite.sprite = seed;
        }
        
    }

    protected override void Grow()
    {
        if (state == GrowState.SEED && !hasGrown)
        {
            hasGrown = true;
            state = GrowState.GROWING;
        }
        if (state == GrowState.GROWING && isWatered && !hasGrown)
        {
            hasGrown = true;
            state = GrowState.GROWN;
        }
    }
    protected override void SaveData()
    {
        var refrence = Game_Manager.Instance;
        refrence.plantLocation[refrence.arrayNum] = this.transform.position;
        refrence.plantType[refrence.arrayNum] = "Wheat";
        refrence.growthState[refrence.arrayNum] = state;
        refrence.arrayNum++;
    }
    protected override IEnumerator Harvest()
    {
       
        transform.parent.GetComponent<Tile>().occupied = false;
        Game_Manager.Instance.money += 2;
        yield return new WaitForSeconds(2f);
        // HARVEST ANIMATINO HERE
        Destroy(gameObject);
        yield return null;
    }
}
