using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : Items
{
    [SerializeField] private GameObject extraLifeIndicator;

    private void OnTriggerEnter(Collider other)
    {
        //In the future, best to break ObjectBehavior script into seperate object specific scripts and use switch statements if possible
        //Also instead of using tags, try to create variables instead and replace tags for best practice https://forum.unity.com/threads/switch-case-using-comparetag.1313106/
        if (gameManager.isGameActive)
        {
            if (other.CompareTag("Player") && gameObject.CompareTag("Extra life"))
            {
                GotExtraLife();
            }
        }

        else
        {
            Destroy(gameObject);
        }
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
}
