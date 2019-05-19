using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed; //floating point variable to store the player's walk speed
    public bool facingRight = true;//determines which way the player is facing
    public bool attackButtonDown = false;
    public bool attacking = false;
    public bool blockButtonDown = false;
    public bool specialButtonDown = false;
    public bool specialOn = false;
    public bool dodgeButtonDown = false;
    public bool isWalking = false;
    public bool isDead = false;
    public GameObject rightAttack;
    public GameObject leftAttack;
    public GameObject deathSound;
    public GameObject bloodSplatter;
    
    public int hp = 10;
    public Slider hpBar;
    public TextMeshProUGUI hpText;

    public int mp = 3;
    public Slider mpBar;
    public TextMeshProUGUI mpText;

    public Vector2 currentPosition;
    public GameObject specialAttack;

    private Rigidbody2D rb;//store a reference to the Rigidbody2D component required to use 2D physics
    private Animator anim;
    private SpriteRenderer sprite;
    private AudioSource hurtSound;

    private void Start()
    {
        facingRight = true;
        isDead = false;
        rb = GetComponent<Rigidbody2D>();//get and store a reference to the Rigidbody2D component so that we can access it
        anim = GetComponent<Animator>();//get and store a reference to the Animator component
        hurtSound = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        deathSound.SetActive(false);
        rightAttack.SetActive(false);
        leftAttack.SetActive(false);
    }

    private void Update()
    {
        if(isDead == false)
        {
            Attack();
            Block();
            Special();
            Dodge();

            if (Input.GetAxisRaw("Attack") == 0)//runs this when the key is released, prevents attack button from being held down
            {
                attackButtonDown = false;

            }
            currentPosition = transform.position;//stores the player's current position
            UpdateHPMP();
            
        }
        

        if(hp <= 0)//if player's hp goes below zero, player dies
        {
            Debug.Log("Player Dead");
            deathSound.SetActive(true);
            bloodSplatter.SetActive(true);
            isDead = true;
            anim.SetBool("isDead", isDead);
        }
    }

    

    void Attack()
    {
        if (Input.GetAxisRaw("Attack") != 0)
        {
            if (attackButtonDown == false)//only runs once per key press
            {
                //Call your event function here

                anim.SetTrigger("Attack");
                attackButtonDown = true;

                if (facingRight == true && attacking == false)
                {
                    
                    StartCoroutine(rightAttackSwing());
                }
                else if (facingRight == false && attacking == false)
                {
                    
                    StartCoroutine(leftAttackSwing());
                }

            }

            
        }
    }

    IEnumerator rightAttackSwing()
    {
        rightAttack.SetActive(true);
        rightAttack.GetComponent<AudioSource>().Play();
        attacking = true;
        yield return new WaitForSeconds(0.25f);
        rightAttack.SetActive(false);
        attacking = false;

        
    }

    IEnumerator leftAttackSwing()
    {
        leftAttack.SetActive(true);
        leftAttack.GetComponent<AudioSource>().Play();
        attacking = true;
        yield return new WaitForSeconds(0.25f);
        leftAttack.SetActive(false);
        attacking = false;

        
    }

    public void BloodSplatter()
    {
        StartCoroutine(Blood());
    }

    IEnumerator Blood()
    {
        bloodSplatter.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        bloodSplatter.SetActive(false);
    }

    void Block()
    {
        if (Input.GetAxisRaw("Block") != 0)
        {
            if (blockButtonDown == false)
            {
                //Call your event function here
                
                blockButtonDown = true;
            }

        }

        if (Input.GetAxisRaw("Block") == 0)//runs this when the key is released
        {
            blockButtonDown = false;
        }
    }

    void Special()
    {
        if (Input.GetAxisRaw("Special") != 0)
        {
            if (specialButtonDown == false)
            {
                //Call your event function here
                if(mp>0 && specialOn == false)
                {
                    
                    StartCoroutine(SpecialAttack());
                }

                Debug.Log("Special");

                specialButtonDown = true;
            }

        }

        if (Input.GetAxisRaw("Special") == 0)//runs this when the key is released
        {
            specialButtonDown = false;
        }
    }

    IEnumerator SpecialAttack()
    {
        specialAttack.SetActive(true);
        mp -= 1;
        specialOn = true;
        anim.SetTrigger("specialOn");
        yield return new WaitForSeconds(3f);
        specialAttack.SetActive(false);
        specialOn = false;
        
    }

    void Dodge()
    {
        if (Input.GetAxisRaw("Dodge") != 0)
        {
            if (dodgeButtonDown == false)
            {
                //Call your event function here
                Debug.Log("Dodge");
                dodgeButtonDown = true;
            }

        }

        if (Input.GetAxisRaw("Dodge") == 0)//runs this when the key is released
        {
            dodgeButtonDown = false;
        }
    }

    void UpdateHPMP()
    {
        hpBar.value = hp;//sets hp bar to match the hp value
        hpText.text = hp + "/10";

        mpBar.value = mp;
        mpText.text = mp + "/3";
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here
    void FixedUpdate()
    {
        if(isDead == false)
        {
            //Store the current horizontal input in the float moveHorizontal
            float moveHorizontal = Input.GetAxis("Horizontal");

            //Store the current vertical input in the float moveVertical
            float moveVertical = Input.GetAxis("Vertical");

            //Use the two stored floats to create a new Vector2 variable movement
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);

            rb.velocity = new Vector2(movement.x * walkSpeed, movement.y * walkSpeed);//updates the player's velocity based on the inputs

            if (movement.x != 0 || movement.y != 0)
            {
                isWalking = true;
                anim.SetBool("isWalking", isWalking);//sets animation to walking

            }
            else if (movement.x == 0 && movement.y == 0)
            {
                isWalking = false;
                anim.SetBool("isWalking", isWalking);//sets animation back to idle

            }

            if (movement.x > 0)//faces sprite right when right arrow pressed
            {
                sprite.flipX = false;
                facingRight = true;
            }
            else if (movement.x < 0)//faces sprite left when left arrow pressed
            {
                sprite.flipX = true;
                facingRight = false;
            }
        }

        if(isDead == true)//if player is dead, stop all movement
        {
            rb.velocity = new Vector2(0, 0);
            
        }
        


    }

}
