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
    public Text ammoCount; // ammo remaining display value
    public Text reloadWarning; // warns player to reload their gun
    public AudioClip swapAudio; // sound of switching weapons

    public Image slot1;
    public Image slot2;
    public Image slot3;
    public Image slotHealth;
    Color green = new Color(0f, 1f, 0f, 0.3f);
    Color def = new Color(0.5f, 0.5f, 0.5f, 0.3f);

    public Weapon empSMG;
    public Weapon plasmaSMG;
    public Weapon railSMG;    
    public Weapon empAR;
    public Weapon plasmaAR;
    public Weapon railAR;
    public Weapon empSR;
    public Weapon plasmaSR;
    public Weapon railSR;
    

    [HideInInspector]
    public Weapon currentWeapon; // weapon the player is currently holding

    private AudioSource _source; // source for audio

    // Start is called before the first frame update
    void Start() {
        //sets first weapons
        firstWeapon = empSMG;
        secondWeapon = plasmaAR;        

        // set all weapons to false
        empSMG.ActivateWeapon(false);
        plasmaSMG.ActivateWeapon(false);
        railSMG.ActivateWeapon(false);    
        empAR.ActivateWeapon(false);
        plasmaAR.ActivateWeapon(false);
        railAR.ActivateWeapon(false);
        empSR.ActivateWeapon(false);
        plasmaSR.ActivateWeapon(false);
        railSR.ActivateWeapon(false); 

        // pistol is open by default
        defaultPistol.ActivateWeapon(true); // activates the pistol
        firstWeapon.ActivateWeapon(false); // 1st slot disabled
        secondWeapon.ActivateWeapon(false); // 2nd slot disabled
        currentWeapon = defaultPistol; // player has pistol in hand by default       
        
        // setting this file to the manager for all the weapon slots
        defaultPistol.manager = this; 
        firstWeapon.manager = this;
        secondWeapon.manager = this;        

        _source = GetComponent<AudioSource>(); // gets the audio
        _source.playOnAwake = false; // does not play on startup
        _source.spatialBlend = 1f; // makes the sound 3D

        ChangeColor(slot1, green);
        ChangeColor(slot2, def);
        ChangeColor(slot3, def);
        ChangeColor(slotHealth, def);
    }

    // runs when player is colliding with pickup
    void OnTriggerEnter(Collider other){ // runs when the player collides 
        if(other.gameObject.CompareTag("railSR")){ 
            if(firstWeapon == currentWeapon){
                firstWeapon.ActivateWeapon(false);
                firstWeapon = railSR;
                firstWeapon.ActivateWeapon(true);
            }else if(secondWeapon == currentWeapon){
                secondWeapon.ActivateWeapon(false);
                secondWeapon = railSR;
                secondWeapon.ActivateWeapon(true);
            }                
        }   
    }
    public void ChangeColor (Image img, Color col)
    {
        img.color = col;
    }

    // Update is called once per frame
    void Update() {
    
        // brings up pistol when pressing 1
        if (Input.GetKeyDown(KeyCode.Alpha1)) {

            defaultPistol.ActivateWeapon(true); // switch to pistol
            firstWeapon.ActivateWeapon(false); // 1st slot disabled
            secondWeapon.ActivateWeapon(false); // 2nd slot disabled            
            currentWeapon = defaultPistol; // player now has pistol in hand            

            _source.clip = swapAudio; // sets swap audio
            _source.Play(); // plays swap audio
            
            ChangeColor(slot1, green);
            ChangeColor(slot2, def);
            ChangeColor(slot3, def);
            ChangeColor(slotHealth, def);
        }
        
        // brings up first weapon when pressing 2
        if (Input.GetKeyDown(KeyCode.Alpha2)) {

            defaultPistol.ActivateWeapon(false); // pistol disabled
            firstWeapon.ActivateWeapon(true); // switch to 1st slot
            secondWeapon.ActivateWeapon(false); // 2nd slot disabled            
            currentWeapon = firstWeapon; // player now has weapon 1 in hand            
            
            _source.clip = swapAudio; // sets swap audio
            _source.Play(); // plays swap audio

            ChangeColor(slot1, def);
            ChangeColor(slot2, green);
            ChangeColor(slot3, def);
            ChangeColor(slotHealth, def);
        }

        // brings up second weapon when pressing 3
        if (Input.GetKeyDown(KeyCode.Alpha3)) {

            defaultPistol.ActivateWeapon(false); // pistol disabled
            firstWeapon.ActivateWeapon(false); // 1st slot disabled
            secondWeapon.ActivateWeapon(true); // switch to 2nd slot            
            currentWeapon = secondWeapon; // player now has weapon 2 in hand            
            
            _source.clip = swapAudio; // sets swap audio
            _source.Play(); // plays swap audio

            ChangeColor(slot1, def);
            ChangeColor(slot2, def);
            ChangeColor(slot3, green);
            ChangeColor(slotHealth, def);
        } 

        // brings up second weapon when pressing 3
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            
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
