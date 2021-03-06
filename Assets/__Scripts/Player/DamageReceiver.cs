﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class DamageReceiver : MonoBehaviour, IEntity
{
    public static float playerHP = 100;
    public static float maxHP = 100;
    public static bool invulnerable = false; 
    public static bool dead = false; 
    public Text DeathText;
    public Text HP; 
    public AudioClip damagePlayerAudio; // sound for taking damage
    public AudioClip killPlayerAudio; // sound upon death

    private AudioSource _source; // source for player audio
    private float _deathXP = 0; // xp at death
    private int _deathHealthKits = 0; // healthkits at death

    // Start is called before the first frame update
    void Start() {

        _source = GetComponent<AudioSource>(); // gets audio source
        _source.playOnAwake = false; // does not play on startup
        _source.spatialBlend = 1f; // makes the sound 3D
        _deathXP = PlayerExp.playerXP; // set amount of xp
        _deathHealthKits = Inventory.healthKits; // set amount of healthkits
        Time.timeScale = 1;
        int temp = (int)playerHP;
        HP.text = "HP: " + temp.ToString();
        dead = false;
    }

    public void Update()
    {
        if(playerHP <= 0)
        {
            playerHP = 0;
            dead = true;
            _source.clip = killPlayerAudio; // sets death audio
            _source.Play(); // plays death audio            
            DeathText.text = "You Died";
            StartCoroutine(ExecuteAfterTime(0.00003f)); // waits , then runs ExecuteAfterTime function
            Time.timeScale = 0.00001f; //stops time so player cannot move            
        }

        if(playerHP > maxHP) // if health goes above max
        {
            playerHP = maxHP; // set HP to max value            
        }

        int temp = (int)playerHP;
        HP.text = "HP: " + temp.ToString(); // display player HP   
    }

    public void ApplyDamage(float dmg)
    {
        if (!invulnerable) {
            playerHP -= dmg;
            HP.text = "HP: " + playerHP.ToString();

            _source.clip = damagePlayerAudio; // sets hurt audio
            _source.Play(); // plays hurt audio             
        }
        
    }

    IEnumerator ExecuteAfterTime(float time){ 
        yield return new WaitForSeconds(time); // waits for time seconds
        PlayerExp.playerXP = _deathXP; // sets xp to what it was at the start of the level
        playerHP = maxHP; // restarts player with max health
        Inventory.healthKits = _deathHealthKits; // sets healht kit count to what it was at the start of the level
        Application.LoadLevel(SceneManager.GetActiveScene().buildIndex); // reloads the level        
    }    
}