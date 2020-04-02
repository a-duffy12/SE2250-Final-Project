using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class Inventory : MonoBehaviour
{

    public Camera playerCamera; // player's camera
    public Weapon defaultPistol; // default weapon the player always has
    public Weapon firstWeapon; // first weapon slot
    public Weapon secondWeapon; // second weapon slot
    public static string slot1Wep; // static variable for slot 1
    public static string slot2Wep; // static variable for slot 2
    public Text ammoCount; // ammo remaining display value
    public Text reloadWarning; // warns player to reload their gun
    public AudioClip swapAudio; // sound of switching weapons
    public AudioClip healthKitAudio; // sound for using health kit
    public Text pickUp;
    public Text healthKit;
    public Text slot1Text;
    public Text slot2Text;
    public static float healthKitAmount = 50; // sets health kit heal amount

    public Image slot1;
    public Image slot2;
    public Image slot3;
    public Image slotHealth;
    Color green = new Color(0f, 1f, 0f, 0.3f);
    Color def = new Color(0.5f, 0.5f, 0.5f, 0.3f);

    public Weapon empSMG; //set all weapons
    public Weapon plasmaSMG;
    public Weapon railSMG;    
    public Weapon empAR;
    public Weapon plasmaAR;
    public Weapon railAR;
    public Weapon empSR;
    public Weapon plasmaSR;
    public Weapon railSR;

    public GameObject empSMGP; //set all weapon pickups
    public GameObject plasmaSMGP;
    public GameObject railSMGP;    
    public GameObject empARP;
    public GameObject plasmaARP;
    public GameObject railARP;
    public GameObject empSRP;
    public GameObject plasmaSRP;
    public GameObject railSRP;

    public int healthKits = 0;    

    [HideInInspector]
    public Weapon currentWeapon; // weapon the player is currently holding

    private AudioSource _source; // source for audio

    // Start is called before the first frame update
    void Start() {
        //sets first weapons
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Parkour") 
        {
            firstWeapon = plasmaSMG;
            secondWeapon = empAR;
            slot1Wep = "plasmaSMG";
            slot2Wep = "empAR";
        }else{
            if(slot1Wep == "empSMG"){firstWeapon = empSMG;} // checks static string ands sets weapon accordingly in slot 1
            else if(slot1Wep == "plasmaSMG"){firstWeapon = plasmaSMG;}
            else if(slot1Wep == "railSMG"){firstWeapon = railSMG;}
            else if(slot1Wep == "empAR"){firstWeapon = empAR;}
            else if(slot1Wep == "plasmaAR"){firstWeapon = plasmaAR;}
            else if(slot1Wep == "railAR"){firstWeapon = railAR;}
            else if(slot1Wep == "empSR"){firstWeapon = empSR;}
            else if(slot1Wep == "plasmaSR"){firstWeapon = plasmaSR;}
            else if(slot1Wep == "railSR"){firstWeapon = railSR;}

            if(slot2Wep == "empSMG"){secondWeapon = empSMG;} // checks static string ands sets weapon accordingly in slot 2
            else if(slot2Wep == "plasmaSMG"){secondWeapon = plasmaSMG;}
            else if(slot2Wep == "railSMG"){secondWeapon = railSMG;}
            else if(slot2Wep == "empAR"){secondWeapon = empAR;}
            else if(slot2Wep == "plasmaAR"){secondWeapon = plasmaAR;}
            else if(slot2Wep == "railAR"){secondWeapon = railAR;}
            else if(slot2Wep == "empSR"){secondWeapon = empSR;}
            else if(slot2Wep == "plasmaSR"){secondWeapon = plasmaSR;}
            else if(slot2Wep == "railSR"){secondWeapon = railSR;}
        }   

        UpdateSlotText(); // updates UI text        

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

        ChangeColor(slot1, green); //changes colors of slots so that active slot is visible
        ChangeColor(slot2, def);
        ChangeColor(slot3, def);
        ChangeColor(slotHealth, def);
    }

    // runs when player is colliding with pickup
    void OnTriggerStay(Collider other){ // runs when the player collides 
        if(other.gameObject.CompareTag("empSR") || other.gameObject.CompareTag("plasmaSR") || other.gameObject.CompareTag("railSR") || other.gameObject.CompareTag("empSMG") || other.gameObject.CompareTag("plasmaSMG") || other.gameObject.CompareTag("railSMG") || other.gameObject.CompareTag("empAR") || other.gameObject.CompareTag("plasmaAR") || other.gameObject.CompareTag("railAR")){
            pickUp.text = "Press Q to pickup weapon"; // shows pickup message to player 
        }

        if(other.gameObject.CompareTag("health")){ // checks if the pickup is a health kit 
                other.gameObject.SetActive(false); //destroys health kit
                pickUp.text = "";
                healthKits += 1; // adds 1 to health kit value
                healthKit.text = healthKits.ToString(); // updates UI               
        }
        
        if(currentWeapon != defaultPistol){ // check is current weapon is a pistol        
            if (Input.GetKeyDown(KeyCode.Q)){ // checks for when player clicks Q button                       
                if(other.gameObject.CompareTag("empSR")){ // if the weapon tag is empSR 
                    if(firstWeapon == currentWeapon){ //checks if the active slot is slot1
                        DropWeapon(); // drops current weapon                   
                        firstWeapon.ActivateWeapon(false); // sets slot1 weapon to false
                        firstWeapon = empSR; // sets slot1 weapon to empSR
                        currentWeapon = empSR; // sets current weapon to empSR
                        firstWeapon.ActivateWeapon(true); // activates slot1 weapon (now empSR)
                        slot1Wep = "empSR";
                    }else if(secondWeapon == currentWeapon){ //checks if the active slot is slot2
                        DropWeapon(); // drops current weapon 
                        secondWeapon.ActivateWeapon(false); // sets slot2 weapon to false
                        secondWeapon = empSR; //sets slot2 and current weapon to empSR
                        currentWeapon = empSR;
                        secondWeapon.ActivateWeapon(true); // activates slot2 weapon (now empSR)
                        slot2Wep = "empSR";
                    }
                    other.gameObject.SetActive(false); // destroys pickup 
                    pickUp.text = ""; // sets pickup text to nothing                
                }
                // the following code has the same structure as above but works on each of the nice weapons
                else if(other.gameObject.CompareTag("plasmaSR")){ 
                    if(firstWeapon == currentWeapon){
                        DropWeapon();
                        firstWeapon.ActivateWeapon(false);
                        firstWeapon = plasmaSR;
                        currentWeapon = plasmaSR;
                        firstWeapon.ActivateWeapon(true);
                        slot1Wep = "plasmaSR";
                    }else if(secondWeapon == currentWeapon){
                        DropWeapon();
                        secondWeapon.ActivateWeapon(false);
                        secondWeapon = plasmaSR;
                        currentWeapon = plasmaSR;
                        secondWeapon.ActivateWeapon(true);
                        slot2Wep = "plasmaSR";
                    }
                    other.gameObject.SetActive(false);
                    pickUp.text = "";                
                } 
                else if(other.gameObject.CompareTag("railSR")){ 
                    if(firstWeapon == currentWeapon){
                        DropWeapon();
                        firstWeapon.ActivateWeapon(false);
                        firstWeapon = railSR;
                        currentWeapon = railSR;
                        firstWeapon.ActivateWeapon(true);
                        slot1Wep = "railSR";
                    }else if(secondWeapon == currentWeapon){
                        DropWeapon();
                        secondWeapon.ActivateWeapon(false);
                        secondWeapon = railSR;
                        currentWeapon = railSR;
                        secondWeapon.ActivateWeapon(true);
                        slot2Wep = "railSR";
                    }
                    other.gameObject.SetActive(false);
                    pickUp.text = "";                            
                }
                else if(other.gameObject.CompareTag("empSMG")){ 
                    if(firstWeapon == currentWeapon){
                        DropWeapon();
                        firstWeapon.ActivateWeapon(false);
                        firstWeapon = empSMG;
                        currentWeapon = empSMG;
                        firstWeapon.ActivateWeapon(true);
                        slot1Wep = "empSMG";
                    }else if(secondWeapon == currentWeapon){
                        DropWeapon();
                        secondWeapon.ActivateWeapon(false);
                        secondWeapon = empSMG;
                        currentWeapon = empSMG;
                        secondWeapon.ActivateWeapon(true);
                        slot2Wep = "empSMG";
                    }
                    other.gameObject.SetActive(false);
                    pickUp.text = "";                
                }
                else if(other.gameObject.CompareTag("plasmaSMG")){ 
                    if(firstWeapon == currentWeapon){
                        DropWeapon();
                        firstWeapon.ActivateWeapon(false);
                        firstWeapon = plasmaSMG;
                        currentWeapon = plasmaSMG;
                        firstWeapon.ActivateWeapon(true);
                        slot1Wep = "plasmaSMG";
                    }else if(secondWeapon == currentWeapon){
                        DropWeapon();
                        secondWeapon.ActivateWeapon(false);
                        secondWeapon = plasmaSMG;
                        currentWeapon = plasmaSMG;
                        secondWeapon.ActivateWeapon(true);
                        slot2Wep = "plasmaSMG";
                    }
                    other.gameObject.SetActive(false);
                    pickUp.text = "";                
                } 
                else if(other.gameObject.CompareTag("railSMG")){ 
                    if(firstWeapon == currentWeapon){
                        DropWeapon();
                        firstWeapon.ActivateWeapon(false);
                        firstWeapon = railSMG;
                        currentWeapon = railSMG;
                        firstWeapon.ActivateWeapon(true);
                        slot1Wep = "railSMG";
                    }else if(secondWeapon == currentWeapon){
                        DropWeapon();
                        secondWeapon.ActivateWeapon(false);
                        secondWeapon = railSMG;
                        currentWeapon = railSMG;
                        secondWeapon.ActivateWeapon(true);
                        slot2Wep = "railSMG";
                    }
                    other.gameObject.SetActive(false);
                    pickUp.text = "";                            
                }
                else if(other.gameObject.CompareTag("empAR")){ 
                    if(firstWeapon == currentWeapon){
                        DropWeapon();
                        firstWeapon.ActivateWeapon(false);
                        firstWeapon = empAR;
                        currentWeapon = empAR;
                        firstWeapon.ActivateWeapon(true);
                        slot1Wep = "empAR";
                    }else if(secondWeapon == currentWeapon){
                        DropWeapon();
                        secondWeapon.ActivateWeapon(false);
                        secondWeapon = empAR;
                        currentWeapon = empAR;
                        secondWeapon.ActivateWeapon(true);
                        slot2Wep = "empAR";
                    }
                    other.gameObject.SetActive(false);
                    pickUp.text = "";                
                }
                else if(other.gameObject.CompareTag("plasmaAR")){ 
                    if(firstWeapon == currentWeapon){
                        DropWeapon();
                        firstWeapon.ActivateWeapon(false);
                        firstWeapon = plasmaAR;
                        currentWeapon = plasmaAR;
                        firstWeapon.ActivateWeapon(true);
                        slot1Wep = "plasmaAR";
                    }else if(secondWeapon == currentWeapon){
                        DropWeapon();
                        secondWeapon.ActivateWeapon(false);
                        secondWeapon = plasmaAR;
                        currentWeapon = plasmaAR;
                        secondWeapon.ActivateWeapon(true);
                        slot2Wep = "plasmaAR";
                    }
                    other.gameObject.SetActive(false);
                    pickUp.text = "";                
                } 
                else if(other.gameObject.CompareTag("railAR")){ 
                    if(firstWeapon == currentWeapon){
                        DropWeapon();
                        firstWeapon.ActivateWeapon(false);
                        firstWeapon = railAR;
                        currentWeapon = railAR;
                        firstWeapon.ActivateWeapon(true);
                        slot1Wep = "railAR";
                    }else if(secondWeapon == currentWeapon){
                        DropWeapon();
                        secondWeapon.ActivateWeapon(false);
                        secondWeapon = railAR;
                        currentWeapon = railAR;
                        secondWeapon.ActivateWeapon(true);
                        slot2Wep = "railAR";
                    }
                    other.gameObject.SetActive(false);
                    pickUp.text = "";                            
                }               
            }
        }          
    }

    void OnTriggerExit(Collider other){ // runs when the player collides 
        if(other.gameObject.CompareTag("empSR") || other.gameObject.CompareTag("plasmaSR") || other.gameObject.CompareTag("railSR") || other.gameObject.CompareTag("empSMG") || other.gameObject.CompareTag("plasmaSMG") || other.gameObject.CompareTag("railSMG") || other.gameObject.CompareTag("empAR") || other.gameObject.CompareTag("plasmaAR") || other.gameObject.CompareTag("railAR")){
            pickUp.text = ""; //sets pickup text to nothing when the player is not colliding            
        }
    }

    public void ChangeColor (Image img, Color col) //color change method
    {
        img.color = col; // changes img color to col
    }

    public void DropWeapon() // method for dropping current weapon 
    {   GameObject playerObj = GameObject.FindGameObjectWithTag("Player"); // gets player object
        Vector3 pos = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y - 1, playerObj.transform.position.z); // gets player position 

        _source.clip = swapAudio; // sets swap audio
        _source.Play(); // plays swap audio

        if(currentWeapon == empSMG){ // checks for current weapon and drops it at player position (for all weapons)
            Instantiate(empSMGP, pos, Quaternion.identity);
        }else if(currentWeapon == plasmaSMG){
            Instantiate(plasmaSMGP, pos, Quaternion.identity);
        }else if(currentWeapon == railSMG){
            Instantiate(railSMGP, pos, Quaternion.identity);
        }else if(currentWeapon == empAR){
            Instantiate(empARP, pos, Quaternion.identity);
        }else if(currentWeapon == plasmaAR){
            Instantiate(plasmaARP, pos, Quaternion.identity);
        }else if(currentWeapon == railAR){
            Instantiate(railARP, pos, Quaternion.identity);
        }else if(currentWeapon == empSR){
            Instantiate(empSRP, pos, Quaternion.identity);
        }else if(currentWeapon == plasmaSR){
            Instantiate(plasmaSRP, pos, Quaternion.identity);
        }else if(currentWeapon == railSR){
            Instantiate(railSRP, pos, Quaternion.identity);
        }
    }

    public void UpdateSlotText(){ // method for updating inventory UI
        Color emp = new Color(0.145f, 0.722f, 1f); // creates three new colors
        Color plasma = new Color(0.780f, 0.0235f, 0.0274f);
        Color rail = new Color(0.607f, 0.345f, 0.788f);

        // checks for all possible weapons in the first slot and sets UI text accordingly 
        if(firstWeapon == empSMG){ // checks for empSMG in first slot
            slot1Text.text = "SMG"; // sets text to SMG
            slot1Text.color = emp; // sets color to emp color (created above)
        }else if(firstWeapon == plasmaSMG){
            slot1Text.text = "SMG";
            slot1Text.color = plasma;
        }else if(firstWeapon == railSMG){
            slot1Text.text = "SMG";
            slot1Text.color = rail;
        }else if(firstWeapon == empAR){
            slot1Text.text = "AR";
            slot1Text.color = emp;
        }else if(firstWeapon == plasmaAR){
            slot1Text.text = "AR";
            slot1Text.color = plasma;
        }else if(firstWeapon == railAR){
            slot1Text.text = "AR";
            slot1Text.color = rail;
        }else if(firstWeapon == empSR){
            slot1Text.text = "SR";
            slot1Text.color = emp;
        }else if(firstWeapon == plasmaSR){
            slot1Text.text = "SR";
            slot1Text.color = plasma;
        }else if(firstWeapon == railSR){
            slot1Text.text = "SR";
            slot1Text.color = rail;
        }

        // same as above but for all possible weapons in the second slot
        if(secondWeapon == empSMG){
            slot2Text.text = "SMG";
            slot2Text.color = emp;
        }else if(secondWeapon == plasmaSMG){
            slot2Text.text = "SMG";
            slot2Text.color = plasma;
        }else if(secondWeapon == railSMG){
            slot2Text.text = "SMG";
            slot2Text.color = rail;
        }else if(secondWeapon == empAR){
            slot2Text.text = "AR";
            slot2Text.color = emp;
        }else if(secondWeapon == plasmaAR){
            slot2Text.text = "AR";
            slot2Text.color = plasma;
        }else if(secondWeapon == railAR){
            slot2Text.text = "AR";
            slot2Text.color = rail;
        }else if(secondWeapon == empSR){
            slot2Text.text = "SR";
            slot2Text.color = emp;
        }else if(secondWeapon == plasmaSR){
            slot2Text.text = "SR";
            slot2Text.color = plasma;
        }else if(secondWeapon == railSR){
            slot2Text.text = "SR";
            slot2Text.color = rail;
        }
    }

    // Update is called once per frame
    void Update() {

        UpdateSlotText();
    
        if (Input.GetKeyDown(KeyCode.H)) { // runs when player presses H key
        
            healthKits -= 1; // subtracts health kit

            _source.clip = healthKitAudio; // sets health kit audio
            _source.Play(); // plays health kit audio

            healthKit.text = healthKits.ToString(); // updates UI            
            DamageReceiver.playerHP += healthKitAmount;
        }
        
        // brings up pistol when pressing 1
        if (Input.GetKeyDown(KeyCode.Alpha1)) {

            defaultPistol.ActivateWeapon(true); // switch to pistol
            firstWeapon.ActivateWeapon(false); // 1st slot disabled
            secondWeapon.ActivateWeapon(false); // 2nd slot disabled
            currentWeapon = defaultPistol; // player now has pistol in hand            

            _source.clip = swapAudio; // sets swap audio
            _source.Play(); // plays swap audio
            
            ChangeColor(slot1, green); //changes colors of slots so that active slot is visible
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

            ChangeColor(slot1, def); //changes colors of slots so that active slot is visible
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

            ChangeColor(slot1, def); //changes colors of slots so that active slot is visible
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
