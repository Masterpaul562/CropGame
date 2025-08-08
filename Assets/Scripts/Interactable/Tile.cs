using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //public GameObject plant;
    [SerializeField] private Color baseColor, offSetColor;
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private GameObject highLight;
    public bool isWatered;
    public bool occupied;

    private void Update()
    {
        if (occupied)
        {
            if (transform.GetChild(2) != null)
            {
                

                 if (isWatered)
                 {
                   
                        transform.GetChild(2).gameObject.GetComponent<PlantBase>().isWatered = true;
                   
                 }
                
            }
        }
    }
   
    public void Init(bool isOffset)
    {
        render.color = isOffset ? offSetColor : baseColor;
    }
    void OnMouseEnter ()
    {
        highLight.SetActive (true);
    }
    void OnMouseExit () { 
    highLight.SetActive (false);
    }
}
