using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracking : MonoBehaviour
{
    [SerializeField] private Camera cam;
   
    
  private void Start()
    {
        Cursor.visible = false;
    }
    
    void Update()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition); ;
        mousePosition.z = 0f;
        transform.position = mousePosition;
       
           // var currentPos = transform.position;
           // transform.position = new Vector2(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y));
        
    }
}
