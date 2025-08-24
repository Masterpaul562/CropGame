using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrowState { SEED, GROWING, MOREGROWING, GROWN };
public class PlantBase : MonoBehaviour
{
    public bool planted;
    public bool isWatered;
    public GrowState state;
    public TurnManager turnManager;
    public Sprite seed;
    public Sprite growing;   
    public Sprite grown;
    public SpriteRenderer sprite;


    protected virtual void Grow()
    {

    }
    protected virtual void SaveData()
    {

    }
    protected virtual IEnumerator Harvest()
    {

        yield return null;
    }
}
