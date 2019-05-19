using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    public Transform player;
    public GameController gameController;
    public float moveSpeed = 4;
    public float smoothTime = 10f;
    public float maxDist = 20f;
    public GameObject bloodSplatter;

    public int hp = 3;

    private AudioSource damageSound;
    private Vector2 smoothVelocity;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        damageSound = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform;
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hp<= 0)
        {
            StartCoroutine(Death());
        }

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < maxDist)
        {
            transform.position = Vector2.SmoothDamp(transform.position, player.position, ref smoothVelocity, smoothTime);
        }

        if(smoothVelocity.x > 0)
        {
            sprite.flipX = false;
        }
        else if (smoothVelocity.x < 0)
        {
            sprite.flipX = true;
        }
    }

    IEnumerator Death()
    {
        bloodSplatter.SetActive(true);
        
        yield return new WaitForSeconds(0.25f);
        gameController.goblinsKilled += 1;//adds one to the kill score
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Attack"))
        {
            
            hp -= 1;
            damageSound.Play();
            StartCoroutine(Blood());
            
        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().hp -= 1;
            other.gameObject.GetComponent<AudioSource>().Play();
            other.gameObject.GetComponent<PlayerController>().BloodSplatter();
        }
    }

    IEnumerator Blood()
    {
        bloodSplatter.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        bloodSplatter.SetActive(false);
    }
}
