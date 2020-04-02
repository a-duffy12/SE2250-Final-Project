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
    public Text pickUp;

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

        empSMG.manager = this;
        plasmaSMG.manager = this;
        railSMG.manager = this;
        empAR.manager = this;
        plasmaAR.manager = this;
        railAR.manager = this;
        empSR.manager = this;
        plasmaSR.manager = this;
        railSR.manager = this;

        _source = GetComponent<AudioSource>(); // gets the audio
        _source.playOnAwake = false; // does not play on startup
        _source.spatialBlend = 1f; // makes the sound 3D

        ChangeColor(slot1, green);
        ChangeColor(slot2, def);
        ChangeColor(slot3, def);
        ChangeColor(slotHealth, def);
    }

    // runs when player is colliding with pickup
    void OnTriggerStay(Collider other){ // runs when the player collides 
        if(other.gameObject.CompareTag("empSR") || other.gameObject.CompareTag("plasmaSR") || other.gameObject.CompareTag("railSR") || other.gameObject.CompareTag("empSMG") || other.gameObject.CompareTag("plasmaSMG") || other.gameObject.CompareTag("railSMG") || other.gameObject.CompareTag("empAR") || other.gameObject.CompareTag("plasmaAR") || other.gameObject.CompareTag("railAR")){
            pickUp.text = "Press Q to pickup weapon";
        }

        if (Input.GetKeyDown(KeyCode.Q)){
            if(other.gameObject.CompareTag("empSR")){ 
                if(firstWeapon == currentWeapon){
                    firstWeapon.ActivateWeapon(false);
                    firstWeapon = empSR;
                    firstWeapon.ActivateWeapon(true);
                }else if(secondWeapon == currentWeapon){
                    secondWeapon.ActivateWeapon(false);
                    secondWeapon = empSR;
                    secondWeapon.ActivateWeapon(true);
                }
                other.gameObject.SetActive(false);
                pickUp.text = "";                
            }
            else if(other.gameObject.CompareTag("plasmaSR")){ 
                if(firstWeapon == currentWeapon){
                    firstWeapon.ActivateWeapon(false);
                    firstWeapon = plasmaSR;
                    firstWeapon.ActivateWeapon(true);
                }else if(secondWeapon == currentWeapon){
                    secondWeapon.ActivateWeapon(false);
                    secondWeapon = plasmaSR;
                    secondWeapon.ActivateWeapon(true);
                }
                other.gameObject.SetActive(false);
                pickUp.text = "";                
            } 
            else if(other.gameObject.CompareTag("railSR")){ 
                if(firstWeapon == currentWeapon){
                    firstWeapon.ActivateWeapon(false);
                    firstWeapon = railSR;
                    firstWeapon.ActivateWeapon(true);
                }else if(secondWeapon == currentWeapon){
                    secondWeapon.ActivateWeapon(false);
                    secondWeapon = railSR;
                    secondWeapon.ActivateWeapon(true);
                }
                other.gameObject.SetActive(false);
                pickUp.text = "";                            
            }
            else if(other.gameObject.CompareTag("empSMG")){ 
                if(firstWeapon == currentWeapon){
                    firstWeapon.ActivateWeapon(false);
                    firstWeapon = empSMG;
                    firstWeapon.ActivateWeapon(true);
                }else if(secondWeapon == currentWeapon){
                    secondWeapon.ActivateWeapon(false);
                    secondWeapon = empSMG;
                    secondWeapon.ActivateWeapon(true);
                }
                other.gameObject.SetActive(false);
                pickUp.text = "";                
            }
            else if(other.gameObject.CompareTag("plasmaSMG")){ 
                if(firstWeapon == currentWeapon){
                    firstWeapon.ActivateWeapon(false);
                    firstWeapon = plasmaSMG;
                    firstWeapon.ActivateWeapon(true);
                }else if(secondWeapon == currentWeapon){
                    secondWeapon.ActivateWeapon(false);
                    secondWeapon = plasmaSMG;
                    secondWeapon.ActivateWeapon(true);
                }
                other.gameObject.SetActive(false);
                pickUp.text = "";                
            } 
            else if(other.gameObject.CompareTag("railSMG")){ 
                if(firstWeapon == currentWeapon){
                    firstWeapon.ActivateWeapon(false);
                    firstWeapon = railSMG;
                    firstWeapon.ActivateWeapon(true);
                }else if(secondWeapon == currentWeapon){
                    secondWeapon.ActivateWeapon(false);
                    secondWeapon = railSMG;
                    secondWeapon.ActivateWeapon(true);
                }
                other.gameObject.SetActive(false);
                pickUp.text = "";                            
            }
            else if(other.gameObject.CompareTag("empAR")){ 
                if(firstWeapon == currentWeapon){
                    firstWeapon.ActivateWeapon(false);
                    firstWeapon = empAR;
                    firstWeapon.ActivateWeapon(true);
                }else if(secondWeapon == currentWeapon){
                    secondWeapon.ActivateWeapon(false);
                    secondWeapon = empAR;
                    secondWeapon.ActivateWeapon(true);
                }
                other.gameObject.SetActive(false);
                pickUp.text = "";                
            }
            else if(other.gameObject.CompareTag("plasmaAR")){ 
                if(firstWeapon == currentWeapon){
                    firstWeapon.ActivateWeapon(false);
                    firstWeapon = plasmaAR;
                    firstWeapon.ActivateWeapon(true);
                }else if(secondWeapon == currentWeapon){
                    secondWeapon.ActivateWeapon(false);
                    secondWeapon = plasmaAR;
                    secondWeapon.ActivateWeapon(true);
                }
                other.gameObject.SetActive(false);
                pickUp.text = "";                
            } 
            else if(other.gameObject.CompareTag("railAR")){ 
                if(firstWeapon == currentWeapon){
                    firstWeapon.ActivateWeapon(false);
                    firstWeapon = railAR;
                    firstWeapon.ActivateWeapon(true);
                }else if(secondWeapon == currentWeapon){
                    secondWeapon.ActivateWeapon(false);
                    secondWeapon = railAR;
                    secondWeapon.ActivateWeapon(true);
                }
                other.gameObject.SetActive(false);
                pickUp.text = "";                            
            }               
        }          
    }

    void OnTriggerExit(Collider other){ // runs when the player collides 
        if(other.gameObject.CompareTag("empSR") || other.gameObject.CompareTag("plasmaSR") || other.gameObject.CompareTag("railSR") || other.gameObject.CompareTag("empSMG") || other.gameObject.CompareTag("plasmaSMG") || other.gameObject.CompareTag("railSMG") || other.gameObject.CompareTag("empAR") || other.gameObject.CompareTag("plasmaAR") || other.gameObject.CompareTag("railAR")){
            pickUp.text = "";
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

            //bonusWeapon.ActivateWeapon(false);

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

            //bonusWeapon.ActivateWeapon(false);
            
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

            //bonusWeapon.ActivateWeapon(false);
            
            _source.clip = swapAudio; // sets swap audio
            _source.Play(); // plays swap audio

            ChangeColor(slot1, def);
            ChangeColor(slot2, def);
            ChangeColor(slot3, green);
            ChangeColor(slotHealth, def);
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
