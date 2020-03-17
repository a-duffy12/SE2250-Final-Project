using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageReceiver : MonoBehaviour, IEntity
{
    public float playerHP = 100;
    public Text DeathText;    

    public void ApplyDamage(float dmg)
    {
        playerHP -= dmg;

        if(playerHP <= 0)
        {
            playerHP = 0;
            DeathText.text = "You Died";
            StartCoroutine(ExecuteAfterTime(5)); // waits 5 seconds, then runs ExecuteAfterTime function
        }
    }

    IEnumerator ExecuteAfterTime(float time){ 
        yield return new WaitForSeconds(time); // waits for time seconds
        Application.LoadLevel(0); // reloads the level
    }
}