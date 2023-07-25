using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private float horizontalInput;
    private float invertedAxis;
    private float xRange = 15;
    private GameManager gameManager;
    private Animator playerAnim;

    public float speed;
    public GameObject goodIndicator;
    public GameObject badIndicator;
    public bool hasGoodPowerup = false;
    public bool hasBadPowerup = false;
    public float rotationSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerAnim = GameObject.Find("Dog_BorderCollie_01").GetComponent<Animator>();
   
    }

    // Update is called once per frame
    void Update()
    {
        PlayerBounds();
        PlayerMovement();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Good powerup"))
        {
            hasGoodPowerup = true;
            goodIndicator.gameObject.SetActive(true);
            StartCoroutine(GoodPowerUpCountDown());
        }

        if(other.CompareTag("Bad powerup"))
        {
            hasBadPowerup = true;
            badIndicator.gameObject.SetActive(true);
            StartCoroutine(BadPowerUpCountDown());
        }
    }

    IEnumerator GoodPowerUpCountDown()
    {
        yield return new WaitForSeconds(7);
        hasGoodPowerup = false;
        goodIndicator.gameObject.SetActive(false);
    }


    IEnumerator BadPowerUpCountDown()
    {
        yield return new WaitForSeconds(7);
        hasBadPowerup = false;
        badIndicator.gameObject.SetActive(false);
    }

    private void PlayerBounds()
    {
        //Keeps the player within bounds on the x plane
        goodIndicator.transform.position = transform.position + new Vector3(0, .03f, 0);
        badIndicator.transform.position = transform.position + new Vector3(0, .03f, 0);

        if (transform.position.x > xRange)
        {
            transform.position = OutOfBounds();
        }

        if (transform.position.x < -xRange)
        {
            transform.position = -OutOfBounds();
        }
    }

    Vector3 OutOfBounds()
    {
        return new Vector3(xRange, transform.position.y);
    }

    private void PlayerMovement()
    {
        //Controls player movement: when they press A/LeftArrow or D/RightArrow they will move left and right unless they have a bad powerup which reverses the controls temporarily
        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movementDirection = new Vector3(horizontalInput, 0, 0);

        if (gameManager.isGameActive && !hasBadPowerup)
        {
            transform.Translate(movementDirection * Time.deltaTime * speed, Space.World);

            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
                playerAnim.SetFloat("Speed_f", 1.0f);
            }
            else
            {
                playerAnim.SetFloat("Speed_f", 0f);
            }
        }
        else if (gameManager.isGameActive && hasBadPowerup)
        {
            transform.Translate(-movementDirection * Time.deltaTime * speed, Space.World);
            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(-movementDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
                playerAnim.SetFloat("Speed_f", 1.0f);
            }
            else
            {
                playerAnim.SetFloat("Speed_f", 0f);
            }
        }
        if (gameManager.isGameActive == false)
        {
            playerAnim.SetBool("Sit_b", true);
        }
    }
}
