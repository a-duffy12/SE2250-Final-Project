using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public Camera playerCamera; // player's camera
    public Weapon defaultPistol; // default weapon the player always has
    public Weapon firstWeapon; // first weapon slot
    public Weapon secondWeapon; // second weapon slot

    [HideInInspector]
    public Weapon currentWeapon; // weapon the player is currently holding

    // Start is called before the first frame update
    void Start() {
    
        // pistol is open by default
        defaultPistol.ActivateWeapon(true); // activates the pistol
        firstWeapon.ActivateWeapon(false); // 1st slot disabled
        secondWeapon.ActivateWeapon(false); // 2nd slot disabled
        currentWeapon = defaultPistol; // player has pistol in hand by default
        
        // setting this file to the manager for all the weapon slots
        defaultPistol.manager = this; 
        firstWeapon.manager = this;
        secondWeapon.manager = this;
    }

    // Update is called once per frame
    void Update() {
    
        // brings up pistol when pressing 1
        if (Input.GetKeyDown(KeyCode.Alpha1)) {

            defaultPistol.ActivateWeapon(true); // switch to pistol
            firstWeapon.ActivateWeapon(false); // 1st slot disabled
            secondWeapon.ActivateWeapon(false); // 2nd slot disabled
            currentWeapon = defaultPistol; // player now has pistol in hand
        }
        
        // brings up first weapon when pressing 2
        if (Input.GetKeyDown(KeyCode.Alpha2)) {

            defaultPistol.ActivateWeapon(false); // pistol disabled
            firstWeapon.ActivateWeapon(true); // switch to 1st slot
            secondWeapon.ActivateWeapon(false); // 2nd slot disabled
            currentWeapon = firstWeapon; // player now has weapon 1 in hand
        }

        // brings up second weapon when pressing 3
        if (Input.GetKeyDown(KeyCode.Alpha3)) {

            defaultPistol.ActivateWeapon(false); // pistol disabled
            firstWeapon.ActivateWeapon(false); // 1st slot disabled
            secondWeapon.ActivateWeapon(true); // switch to 2nd slot
            currentWeapon = secondWeapon; // player now has weapon 2 in hand
        } 
    }
}
