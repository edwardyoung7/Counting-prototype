using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : Items
{
    [SerializeField] private int pointValue;
    [SerializeField] private GameObject loseLifeIndicator;

    // Update is called once per frame
    public override void Update()
    {
        GoodPowerUp();
        base.ObjectFallSpeed();
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

    private void EatGoodSnack()
    {
        Destroy(gameObject);
        gameManager.UpdateScore(pointValue);
        GameSound.Instance.GoodSnackSound();
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

    private void GoodPowerUp()
    {
        if (playerController.HasGoodPowerUp == true)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            transform.Translate(lookDirection * Time.deltaTime * 10);
        }
    }
}
