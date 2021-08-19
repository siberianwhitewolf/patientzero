using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractableData
{
    public int wasDestroyed = 0;
    public int contains_item = 0;

    public InteractableData(InteractableObject interactable)
    {

        wasDestroyed = interactable.wasDestroyed;
        contains_item = interactable.contains_item;

    }

}
