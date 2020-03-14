using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class Inventory : MonoBehaviour
{

    public Camera playerCamera; // player's camera
    public Weapon defaultPistol; // default weapon the player always has
    public Weapon firstWeapon; // first weapon slot
    public Weapon secondWeapon; // second weapon slot
    public Weapon bonusWeapon; // bonus weapon to show differences in all ammo types //TODO
    public Text ammoCount; // ammo remaining display value
    public Text reloadWarning; // warns player to reload their gun
    public AudioClip swapAudio; // sound of switching weapons

    [HideInInspector]
    public Weapon currentWeapon; // weapon the player is currently holding

    private AudioSource _source; // source for audio

    // Start is called before the first frame update
    void Start() {
    
        // pistol is open by default
        defaultPistol.ActivateWeapon(true); // activates the pistol
        firstWeapon.ActivateWeapon(false); // 1st slot disabled
        secondWeapon.ActivateWeapon(false); // 2nd slot disabled
        currentWeapon = defaultPistol; // player has pistol in hand by default

        bonusWeapon.ActivateWeapon(false);
        
        // setting this file to the manager for all the weapon slots
        defaultPistol.manager = this; 
        firstWeapon.manager = this;
        secondWeapon.manager = this;

        bonusWeapon.manager = this;

        _source = GetComponent<AudioSource>(); // gets the audio
        _source.playOnAwake = false; // does not play on startup
        _source.spatialBlend = 1f; // makes the sound 3D
    }

    // Update is called once per frame
    void Update() {
    
        // brings up pistol when pressing 1
        if (Input.GetKeyDown(KeyCode.Alpha1)) {

            defaultPistol.ActivateWeapon(true); // switch to pistol
            firstWeapon.ActivateWeapon(false); // 1st slot disabled
            secondWeapon.ActivateWeapon(false); // 2nd slot disabled
            currentWeapon = defaultPistol; // player now has pistol in hand

            bonusWeapon.ActivateWeapon(false);

            _source.clip = swapAudio; // sets swap audio
            _source.Play(); // plays swap audio
        }
        
        // brings up first weapon when pressing 2
        if (Input.GetKeyDown(KeyCode.Alpha2)) {

            defaultPistol.ActivateWeapon(false); // pistol disabled
            firstWeapon.ActivateWeapon(true); // switch to 1st slot
            secondWeapon.ActivateWeapon(false); // 2nd slot disabled
            currentWeapon = firstWeapon; // player now has weapon 1 in hand

            bonusWeapon.ActivateWeapon(false);
            
            _source.clip = swapAudio; // sets swap audio
            _source.Play(); // plays swap audio
        }

        // brings up second weapon when pressing 3
        if (Input.GetKeyDown(KeyCode.Alpha3)) {

            defaultPistol.ActivateWeapon(false); // pistol disabled
            firstWeapon.ActivateWeapon(false); // 1st slot disabled
            secondWeapon.ActivateWeapon(true); // switch to 2nd slot
            currentWeapon = secondWeapon; // player now has weapon 2 in hand

            bonusWeapon.ActivateWeapon(false);
            
            _source.clip = swapAudio; // sets swap audio
            _source.Play(); // plays swap audio
        } 

        // brings up second weapon when pressing 3
        if (Input.GetKeyDown(KeyCode.Alpha4)) {

            defaultPistol.ActivateWeapon(false); // pistol disabled
            firstWeapon.ActivateWeapon(false); // 1st slot disabled
            secondWeapon.ActivateWeapon(false); // switch to 2nd slot
            currentWeapon = bonusWeapon; // player now has weapon 2 in hand

            bonusWeapon.ActivateWeapon(true);
            
            _source.clip = swapAudio; // sets swap audio
            _source.Play(); // plays swap audio
        }

        // display the current ammo and the reserve ammo
        ammoCount.text = currentWeapon.GetAmmo().ToString() + " / " + currentWeapon.magSize.ToString();

        // display reload warning when current ammo is 20% or less than mag size
        if (((float)currentWeapon.GetAmmo()/(float)currentWeapon.magSize) <= 0.2) { // if they have minimal ammo remaining

            reloadWarning.text = "[R] Reload"; // tells player to reload

        } else { // if they have significant ammo remaining

            reloadWarning.text = ""; // does not tell player to reload
        }
    }
}
