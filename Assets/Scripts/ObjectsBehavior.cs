using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsBehavior : MonoBehaviour 
{
    [SerializeField]private GameObject extraLifeIndicator, loseLifeIndicator;
    [SerializeField]private int pointValue;

    private float speed = 10.0f;
    private float spawnRange = 15;
    private float spawnPosZ = 0;
    private float spawnPosY = 30;
    private GameManager gameManager;
    private PlayerController playerController;
    private GameObject player;
    private Rigidbody snacksRb;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        player = GameObject.Find("Player");
        snacksRb = GetComponent<Rigidbody>();
        RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        GoodPowerUp();
        ObjectFallSpeed();
    }

    private void OnTriggerEnter(Collider other)
    {
        //In the future, best to break ObjectBehavior script into seperate object specific scripts and use switch statements if possible
        //Also instead of using tags, try to create variables instead and replace tags for best practice https://forum.unity.com/threads/switch-case-using-comparetag.1313106/
        if (gameManager.isGameActive)
        {
            if (other.CompareTag("Player") && gameObject.CompareTag("Good snack"))
            {
                EatGoodSnack();
            }

            else if (other.CompareTag("Player") && gameObject.CompareTag("Bad snack") )
            {
                EatBadSnack();
            }

            else if (other.CompareTag("Player") && gameObject.CompareTag("Bad powerup"))
            {
                GotBadPowerUp();
            }

            else if (other.CompareTag("Player") && gameObject.CompareTag("Good powerup"))
            {
                GotGoodPowerUp();
            }

            else if (other.CompareTag("Player") && gameObject.CompareTag("Extra life"))
            {
                GotExtraLife();
            }

            else if (other.CompareTag("Ground") && gameObject.CompareTag("Good snack"))
            {
                LoseLife();
            }
            else if (other.CompareTag("Ground") && !gameObject.CompareTag("Good snack"))
            {
                Destroy(gameObject);
            }
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void GoodPowerUp()
    {
        if (playerController.HasGoodPowerUp == true && CompareTag("Good snack"))
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            transform.Translate(lookDirection * Time.deltaTime * 10);
        }
    }

    protected void ObjectFallSpeed()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }

    private void RandomSpawnPos()
    {
        transform.position = RandomPosition();
    }

    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-spawnRange, spawnRange), spawnPosY, spawnPosZ);
    }

    private void EatGoodSnack()
    {
        Destroy(gameObject);
        gameManager.UpdateScore(pointValue);
        GameSound.Instance.GoodSnackSound();
    }

    private void EatBadSnack()
    {
        Destroy(gameObject);
        gameManager.UpdateScore(pointValue);
        GameSound.Instance.BadSnackSound();
    }

    private void GotBadPowerUp()
    {
        Destroy(gameObject);
        GameSound.Instance.BadSnackSound();
    }

    private void GotGoodPowerUp()
    {
        Destroy(gameObject);
        GameSound.Instance.GoodSnackSound();
    }

    private void GotExtraLife()
    {
        gameManager.UpdateLives(-1);
        GameSound.Instance.GoodSnackSound();
        SpawnExtraLifeIndicator();
        Destroy(gameObject);
    }

    private void SpawnExtraLifeIndicator()
    {
        Instantiate(extraLifeIndicator, player.transform.position + (player.transform.up * 4), Quaternion.identity);
    }

    private void LoseLife()
    {
        gameManager.UpdateLives(1);
        GameSound.Instance.BadSnackSound();
        SpawnLoseLifeIndicator();
        Destroy(gameObject);
    }

    private void SpawnLoseLifeIndicator()
    {
        Instantiate(loseLifeIndicator, player.transform.position + (player.transform.up * 4), Quaternion.identity);
    }
}
