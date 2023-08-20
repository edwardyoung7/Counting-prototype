using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject goodIndicator, badIndicator;
    [SerializeField] private float xRange, rotationSpeed, speed;
    private float horizontalInput;
    private float invertedAxis;
    private GameManager gameManager;
    private Animator playerAnim;
   
    private bool m_hasGoodPowerup = false;
    public bool HasGoodPowerUp
    {
        get { return m_hasGoodPowerup; }
        private set { m_hasGoodPowerup = value; }
    }

    private bool m_hasBadPowerup;
    public bool HasBadPowerUp
    {
        get { return m_hasBadPowerup; }
        private set { m_hasBadPowerup = value; }
    }

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
        PowerUpIndicatorOffset();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Good powerup"))
        {
            GotGoodPowerUp();
        }

        if(other.CompareTag("Bad powerup"))
        {
            GotBadPowerUp();
        }
        
    }

    private void PlayerMovement()
    {
        if (gameManager.isGameActive && !HasBadPowerUp)
        {
            normalMovement();
        }
        else if (gameManager.isGameActive && HasBadPowerUp)
        {
            reverseMovement();
        }
        if (gameManager.isGameActive == false)
        {
            playerAnim.SetBool("Sit_b", true);
        }
    }

    private void GotGoodPowerUp()
    {
        HasGoodPowerUp = true;
        ShowGoodIndicator(true);
        StartCoroutine(GoodPowerUpCountDown());
    }

    private void GotBadPowerUp()
    {
        HasBadPowerUp = true;
        ShowBadIndicator(true);
        StartCoroutine(BadPowerUpCountDown());
    }

    private void ShowGoodIndicator(bool boolean)
    {
        goodIndicator.gameObject.SetActive(boolean);
    }

    private void ShowBadIndicator(bool boolean)
    {
        badIndicator.gameObject.SetActive(boolean);
    }

    private void PlayerBounds()
    {
        if (transform.position.x > xRange)
        {
            transform.position = OutOfBounds();
        }

        if (transform.position.x < -xRange)
        {
            transform.position = -OutOfBounds();
        }
    }

    private void PowerUpIndicatorOffset()
    {
        Vector3 indicatorOffset = transform.position + new Vector3(0, .03f, 0);
        goodIndicator.transform.position = indicatorOffset;
        badIndicator.transform.position = indicatorOffset;
    }

    private void normalMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movementDirection = new Vector3(horizontalInput, 0, 0);
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
    }

    private void reverseMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movementDirection = new Vector3(horizontalInput, 0, 0);

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

    Vector3 OutOfBounds()
    {
        return new Vector3(xRange, transform.position.y);
    }

    IEnumerator GoodPowerUpCountDown()
    {
        yield return new WaitForSeconds(7);
        HasGoodPowerUp = false;
        goodIndicator.gameObject.SetActive(false);
    }

    IEnumerator BadPowerUpCountDown()
    {
        yield return new WaitForSeconds(7);
        HasBadPowerUp = false;
        badIndicator.gameObject.SetActive(false);
    }
}
