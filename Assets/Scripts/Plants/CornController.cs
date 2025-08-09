using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornController : PlantBase
{

    private bool hasBeenCalled;
    private bool hasGrown;
    private bool dontSave;
    bool x = true;
    public Sprite moreGrowing;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        hasBeenCalled = true;
        hasGrown = false;
        dontSave = false;
    }
    void Update()
    {
        if (state == GrowState.GROWING)
        {
            sprite.sprite = growing;
            transform.localScale = new Vector3(0.6f, 0.5f, 0);
        }else
        if(state == GrowState.MOREGROWING)
        {
            sprite.sprite = moreGrowing;
            transform.localScale = new Vector3(0.45f, 0.6f, 0);
            
        }
        else
        if (state == GrowState.GROWN)
        {
            sprite.sprite = grown;
            transform.localScale = new Vector3(0.45f, 0.6f, 0);
        }
        else
        if (state == GrowState.SEED)
        {
            sprite.sprite = seed;
        }


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
    }

    protected override void Grow()
    {
        if (state == GrowState.SEED && !hasGrown)
        {
            hasGrown = true;
            state = GrowState.GROWING;
        }
        if (state == GrowState.GROWING && !hasGrown && isWatered)
        {
            hasGrown = true;
            state = GrowState.MOREGROWING;
            transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
        }
        if (state == GrowState.MOREGROWING && isWatered && !hasGrown)
        {
            hasGrown = true;
            state = GrowState.GROWN;
            transform.position = new Vector3(transform.position.x, transform.position.y + .4f, transform.position.z);
        }
    }
    protected override void SaveData()
    {
        var refrence = Game_Manager.Instance;
        refrence.plantLocation[refrence.arrayNum] = this.transform.position;
        refrence.plantType[refrence.arrayNum] = "Corn";
        refrence.growthState[refrence.arrayNum] = state;
        refrence.arrayNum++;
    }
    protected override IEnumerator Harvest()
    {

        transform.parent.GetComponent<Tile>().occupied = false;
        Game_Manager.Instance.money += 9;
        yield return new WaitForSeconds(2f);
        // HARVEST ANIMATINO HERE
        Destroy(gameObject);
        yield return null;
    }
}
