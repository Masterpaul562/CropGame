using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour
{
    public int seedCount;
    public void BaseInteract()
    {
        Interact();
    }
    protected virtual void Interact() {

    }

}
