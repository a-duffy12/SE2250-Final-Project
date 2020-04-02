using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class DamageReceiver : MonoBehaviour, IEntity
{
    public static float playerHP = 100;
    public static bool invulnerable = false;  
    public Text DeathText;
    public Text HP; 
    public AudioClip damagePlayerAudio; // sound for taking damage
    public AudioClip killPlayerAudio; // sound upon death

    private AudioSource _source; // source for player audio
    private float _deathXP = 0;

    // Start is called before the first frame update
    void Start() {

        _source = GetComponent<AudioSource>(); // gets audio source
        _source.playOnAwake = false; // does not play on startup
        _source.spatialBlend = 1f; // makes the sound 3D
        _deathXP = PlayerExp.playerXP;
        Time.timeScale = 1;
        HP.text = "HP: " + playerHP.ToString();
    }

    public void Update()
    {
        if(playerHP <= 0)
        {
            playerHP = 0;
            _source.clip = killPlayerAudio; // sets death audio
            _source.Play(); // plays death audio            
            DeathText.text = "You Died";
            StartCoroutine(ExecuteAfterTime(0.00003f)); // waits , then runs ExecuteAfterTime function
            Time.timeScale = 0.00001f; //stops time so player cannot move
        }   
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
        PlayerExp.playerXP = _deathXP;
        Application.LoadLevel(SceneManager.GetActiveScene().buildIndex); // reloads the level
    }    
}