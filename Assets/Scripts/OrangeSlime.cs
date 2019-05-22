using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeSlime : MonoBehaviour
{
    public Transform player;
    public GameController gameController;
    public float moveSpeed = 4;
    public float smoothTime = 10f;
    public float maxDist = 20f;
    public GameObject bloodSplatter;

    public GameObject[] drops;

    public int hp = 2;

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
        if(hp <= 0)
        {
            StartCoroutine(Death());
        }

        

        //transform.LookAt(player);
        float distance = Vector2.Distance(transform.position, player.position);
        if(distance < maxDist)
        {
            transform.position = Vector2.SmoothDamp(transform.position, player.position, ref smoothVelocity, smoothTime);
        }

        if (smoothVelocity.x > 0)
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

        int randomNumber = UnityEngine.Random.Range(0, 10);
        if (randomNumber == 1)
        {
            Instantiate(drops[0], transform.position, Quaternion.identity);//
        }
        else if (randomNumber == 7)
        {
            Instantiate(drops[1], transform.position, Quaternion.identity);//
        }
        gameController.goblinsKilled += 1;//adds one to the kill score
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Attack"))
        {
            hp -= 1;
            damageSound.Play();
            StartCoroutine(Blood());
        }

        if(other.gameObject.CompareTag("Player"))
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
