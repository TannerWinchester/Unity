using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public AudioClip coinSound;
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if(playerInventory != null)
        {
            playerInventory.GemCollected();
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
            gameObject.SetActive(false);
        }
    }
}
