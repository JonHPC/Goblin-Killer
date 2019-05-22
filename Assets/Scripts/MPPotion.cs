using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPPotion : MonoBehaviour
{
    public int amount = 1;
    public PlayerController player;

    private AudioSource pickupSound;
    private SpriteRenderer child;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();//finds the player game object and access its script
        pickupSound = GetComponent<AudioSource>();
        child = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.mpPotions += amount; // adds one mp potion to the player inventory
            pickupSound.Play();
            child.enabled = false;//disables the sprite renderer
            StartCoroutine(DelayDestroy());

        }
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
    }
}
