using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCanController : ItemBase
{
    
    [SerializeField] Sprite pourCan;
    [SerializeField] Sprite defCan;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        ogPos = transform.position;
    }
    public IEnumerator Water()
    {
        sprite.sprite = pourCan;
        yield return new WaitForSeconds(1f);
        sprite.sprite = defCan; 
    }
    public void StartWater()
    {
        StartCoroutine(Water());

    }
}
