using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public float spawnTimer;//used to count up to next enemy spawn
    public float spawnRate;//sets the interval of enemy spawns

    public int goblinsKilled;
    public TextMeshProUGUI killText;

    public PlayerController player;

    public GameObject[] enemy;
    public Transform[] spawnPoint;

    public GameObject gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        gameOverMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isDead)
        {
            gameOverMenu.SetActive(true);
        }
        else
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnRate)
            {
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(enemy[i], spawnPoint[i].position, Quaternion.identity);

                }

                spawnTimer = 0;//resets the spawn timer
            }

            UpdateKills();
        }
        
    }

    void UpdateKills()
    {
        killText.text = "GOBLINS KILLED: " + goblinsKilled;
    }
}
