using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPOrange : MonoBehaviour
{
    public int hpAmount = 1;
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
            player.hp += hpAmount; //restores the player's hp by the specified amount
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
