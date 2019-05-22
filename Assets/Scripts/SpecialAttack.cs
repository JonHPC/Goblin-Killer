using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public float rotateSpeed = 5f;
    public float radius = 0.1f;
    public PlayerController player;

    //private Vector2 center;
    private float angle;
    
    // Start is called before the first frame update
    void Start()
    {
        //center = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        angle += rotateSpeed * Time.deltaTime;
        
        Vector2 offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
        //transform.position = center + offset;
        transform.position = player.currentPosition + offset;//spins the object around the player
    }
}
