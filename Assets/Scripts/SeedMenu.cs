using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedMenu : MonoBehaviour
{
    public bool shouldPull;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform target;
    
   private void OnMouseEnter ()
    {
       shouldPull = true;
    }
    private void OnMouseExit () 
    {
        shouldPull = false;
    }
    private void Update()
    {
        if(shouldPull) {
            transform.position = Vector2.Lerp(transform.position, target.position, Time.deltaTime*2);
        }
        else
        {

            transform.position = Vector2.Lerp(transform.position, startPos.position, Time.deltaTime*2);
        }
    }
}
