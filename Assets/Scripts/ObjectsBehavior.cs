using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsBehavior : MonoBehaviour 
{

    public int pointValue;
    public float speed = 3.0f;
    public GameObject extraLifeIndicator;
    public GameObject loseLifeIndicator;

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

        transform.position = RandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        GoodPowerUp();
        transform.Translate(Vector3.down * Time.deltaTime * 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        //In the future, best to break ObjectBehavior script into seperate object specific scripts and use switch statements if possible
        //Also instead of using tags, try to create variables instead and replace tags for best practice https://forum.unity.com/threads/switch-case-using-comparetag.1313106/
        if (gameManager.isGameActive)
        {
            if (other.CompareTag("Player") && gameObject.CompareTag("Good snack"))
            {
                Destroy(gameObject);
                gameManager.UpdateScore(pointValue);
                GameSound.Instance.GoodSnackSound();
            }

            else if (other.CompareTag("Player") && gameObject.CompareTag("Bad snack") )
            {
                Destroy(gameObject);
                gameManager.UpdateScore(pointValue);
                GameSound.Instance.BadSnackSound();
            }

            else if (other.CompareTag("Player") && gameObject.CompareTag("Bad powerup"))
            {
                Destroy(gameObject);
                GameSound.Instance.BadSnackSound();
            }

            else if (other.CompareTag("Player") && gameObject.CompareTag("Good powerup"))
            {
                Destroy(gameObject);
                GameSound.Instance.GoodSnackSound();
            }

            else if (other.CompareTag("Player") && gameObject.CompareTag("Extra life"))
            {
                gameManager.UpdateLives(-1);
                GameSound.Instance.GoodSnackSound();
                Instantiate(extraLifeIndicator, player.transform.position + (player.transform.up * 4), Quaternion.identity);
                Destroy(gameObject);
            }

            else if (other.CompareTag("Ground") && gameObject.CompareTag("Good snack"))
            {
                gameManager.UpdateLives(1);
                GameSound.Instance.BadSnackSound();
                Instantiate(loseLifeIndicator, player.transform.position + (player.transform.up * 4), Quaternion.identity);
                Destroy(gameObject);
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

Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-spawnRange, spawnRange), spawnPosY, spawnPosZ);
    }
}
