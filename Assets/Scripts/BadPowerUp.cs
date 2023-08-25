using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadPowerUp : Items
{
    private void OnTriggerEnter(Collider other)
    {
        //In the future, best to break ObjectBehavior script into seperate object specific scripts and use switch statements if possible
        //Also instead of using tags, try to create variables instead and replace tags for best practice https://forum.unity.com/threads/switch-case-using-comparetag.1313106/
        if (gameManager.isGameActive)
        {
            if (other.CompareTag("Player") && gameObject.CompareTag("Bad powerup"))
            {
                GotBadPowerUp();
            }
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void GotBadPowerUp()
    {
        Destroy(gameObject);
        GameSound.Instance.BadSnackSound();
    }

}
