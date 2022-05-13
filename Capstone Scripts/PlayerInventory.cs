using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfGems { get; private set; }
    public int gemCount;

    public UnityEvent<PlayerInventory> OnGemCollected;

    public void GemCollected()
    {
        NumberOfGems++;
        gemCount++;
        OnGemCollected.Invoke(this);
    }

}
