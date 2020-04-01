using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class PlayerExp : MonoBehaviour
{
    static public float playerXP = 0;
    public float abilityXP; // XP threshold to unlock ability
    public float abilityDuration = 5; // how long ability lasts for
    public float abilityCooldown = 10; // how long between abilities

    public Text XP; // text for xp
    public Text abilityText; // text for ability
    public Text abilityWarningText; // text for ability cooldown

    public AudioClip abilityAudio; // sound for ability

    private AudioSource _source; // source for audio
    private float _nextAbilityTime = 0; // time that the next ability can be used
    private bool _abilityAvailable = false; // is the ability able to be used
    private bool _abilityActive = false; // is the ability currently active

    // Start is called before the first frame update
    void Start() {

        _source = GetComponent<AudioSource>(); // gets audio source
        _source.playOnAwake = false; // does not play on startup
        _source.spatialBlend = 1f; // makes the sound 3D
        abilityText.text = "";
        abilityWarningText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        XP.text = "XP: " + playerXP.ToString();

        if (Input.GetKeyDown(KeyCode.E) && playerXP >= abilityXP) { // if the player presses G and a grenade is available

            UseAbility(); // throws grenade
        }

        if (playerXP >= abilityXP && !_abilityAvailable) { // check if player reaches ability unlock threshold

            _abilityAvailable = true; // sets ability to be available
            abilityText.text = "[E] 1/1"; // UI says ability is available
            StartCoroutine(ShowWarning("Ability now available [E]", 1.0f));
        }

        if (_abilityActive && !_source.isPlaying) { // check if the ability is active
            
            _source.clip = abilityAudio; // sets ability audio
            _source.Play(); // plays ability audio 
        }
    }

    // function to throw grenades
    void UseAbility() {

        if (Time.time > _nextAbilityTime) {

            _nextAbilityTime = Time.time + abilityDuration + abilityCooldown; // set time for next ability to be available
            _abilityActive = true;
            StartCoroutine(AbilityCooldown()); // update ability count, activate invulnerability
        
        } else if (((_nextAbilityTime - abilityCooldown) > Time.time) && (Time.time > (_nextAbilityTime - abilityCooldown - abilityDuration))) { // if ability is active

            StartCoroutine(ShowWarning("Ability is already active", 1.0f)); // display warning for 1s

        } else {

            StartCoroutine(ShowWarning("Ability is on cooldown", 1.0f)); // display warning for 1s
        } 
    }

    // function to show a warning on screen for a certain amount of time
    IEnumerator ShowWarning(string msg, float delay) {
        
        abilityWarningText.text = msg; // set the message
        yield return new WaitForSeconds(delay); // run for the delay
        abilityWarningText.text= ""; // remove the message
    }

    // function to display text of grenade count and cooldown
    IEnumerator AbilityCooldown() {

        DamageReceiver.invulnerable = true; // player becomes invulnerable
        abilityText.text = "[E] 0/1"; // no ability left
        yield return new WaitForSeconds(abilityDuration-0.1f); // wait until ability is done
        _abilityActive = false; // ability is no longer active
        DamageReceiver.invulnerable = false; // player becomes vulnerable again
        yield return new WaitForSeconds(abilityCooldown-0.1f); // wait until cooldown is done
        abilityText.text = "[E] 1/1"; // one ability left
    }
}
