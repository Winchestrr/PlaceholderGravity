using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool canInteract;

    public virtual void Interact()
    {
        Debug.Log(gameObject.name);
    }
}
