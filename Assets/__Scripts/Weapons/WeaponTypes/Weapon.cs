using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Weapon : MonoBehaviour
{

    public GameObject bulletType; // bullet prefab object, different types of bullet exist
    public Transform firePoint; // where the bullets come from
    public int ammoType; // 0 is pistol, 1 is plasma, 2 is rail, 3 is emp
    public bool singleFire; // is the weapon automatic or semi-automatic
    public float fireRate; // rate of fire in rpm
    public int magSize; // how many bullets per magazine
    public float reloadTime; // how long to reload
    public float damage; // how much damage per bullet
    public int range; // how far can the gun shoot
    public AudioClip bulletAudio; // audio for bullet fires
    public AudioClip plasmaAudio; // audio for plasma fires
    public AudioClip railAudio; // audio for rail fires
    public AudioClip empAudio; // audio for emp fires
    public AudioClip reloadAudio; // aduio for reload

    [HideInInspector]
    public Inventory manager; // instantiate a weapon manager

    private float _nextFireTime = 0; // how long until the next bullet is fired
    private bool _hasAmmo = true; // whether or not the mag has ammo left
    private int _currentMagSize = 0; 
    AudioSource _source;

    // Start is called before the first frame update
    void Start() {
    
        _currentMagSize = magSize; // sets base magazine size to the magazine size
        _source = GetComponent<AudioSource>(); // gets the audio
        _source.playOnAwake = false; // does not play on startup
        _source.spatialBlend = 1f; // makes the sound 3D
    }

    // Update is called once per frame
    void Update() {
    
        // semi-auto firing
        if (Input.GetMouseButtonDown(0) && singleFire) { // mouse button down returns true when mouse button is released

            Fire(); // shoots weapon
        } 
        
        // auto firing
        if (Input.GetMouseButton(0) && !singleFire) { // mouse button returns true as long as the button is held down

            Fire(); // fires weapon
        }

        // reload
        if (Input.GetKeyDown(KeyCode.R) && _hasAmmo) { // R to reload

            StartCoroutine(Reload()); // reloads weapon
        }
    }

    // function to fire a weapon
    void Fire() {

        if (_hasAmmo) { // if the weapon has ammo

            if (Time.time > _nextFireTime) { // if the weapon has waited long enough to fire

                _nextFireTime = Time.time + 60/fireRate; //once firing, update the time to the next time gun is allowed to fire
            
                if (_currentMagSize > 0) {

                    // set the firepoint location to the camera's center, extending the length of the weapon's range
                    Vector3 firePointPosition = manager.playerCamera.transform.position + manager.playerCamera.transform.forward * range;
                    RaycastHit hit; // create a raycast to track hits

                    // checks if there is a line of sight to shoot (should basically always be true)
                    if (Physics.Raycast(manager.playerCamera.transform.position, manager.playerCamera.transform.forward, out hit, range)) {
                        
                        firePointPosition = hit.point; // moves firepoint to where the raycast can hit
                    }

                    firePoint.LookAt(firePointPosition); // moves actual firepoint to where the bullet should go

                    // firing bullet projectile
                    if (ammoType == 0) {

                        GameObject bulletObject = Instantiate(bulletType, firePoint.position, firePoint.rotation);
                        Bullet bullet = bulletObject.GetComponent<Bullet>(); // gets an instance of the bullet object
                        bullet.SetDamage(damage); // sets the damage based of the weapon
                        _currentMagSize--; // removes a bullet from the magazine
                        _source.clip = bulletAudio; // sets firing audio
                        _source.Play(); // plays firing audio

                    } else if (ammoType == 1) {

                        GameObject bulletObject = Instantiate(bulletType, firePoint.position, firePoint.rotation);
                        Plasma plasmaBullet = bulletObject.GetComponent<Plasma>(); // gets an instance of the bullet object
                        plasmaBullet.SetDamage(damage); // sets the damage based of the weapon
                        _currentMagSize--; // removes a bullet from the magazine
                        _source.clip = plasmaAudio; // sets firing audio
                        _source.Play(); // plays firing audio

                    } else if (ammoType == 2) {

                        GameObject bulletObject = Instantiate(bulletType, firePoint.position, firePoint.rotation);
                        Rail railBullet = bulletObject.GetComponent<Rail>(); // gets an instance of the bullet object
                        railBullet.SetDamage(damage); // sets the damage based of the weapon
                        _currentMagSize--; // removes a bullet from the magazine
                        _source.clip = railAudio; // sets firing audio
                        _source.Play(); // plays firing audio

                    } else if (ammoType == 3) {

                        GameObject bulletObject = Instantiate(bulletType, firePoint.position, firePoint.rotation);
                        Emp empBullet = bulletObject.GetComponent<Emp>(); // gets an instance of the bullet object
                        empBullet.SetDamage(damage); // sets the damage based of the weapon
                        _currentMagSize--; // removes a bullet from the magazine
                        _source.clip = empAudio; // sets firing audio
                        _source.Play(); // plays firing audio
                    }
                    
                } else { // no bullets left

                    // could have it reload, instead play a message saying to reload
                    // automatic reloads are waste
                    //bprevents weapon from firing   
                }          
            }
        }
    }

    // ienum to reload weapon
    IEnumerator Reload() {

        _hasAmmo = false; // prevents gun from being fired mid reload

        _source.clip = reloadAudio; // sets reload audio
        _source.Play(); // plays reload audio

        yield return new WaitForSeconds(reloadTime); // how long to wait for reload

        if (_currentMagSize > 0) { // reload with one in the chamber

            _currentMagSize = magSize + 1;
        
        } else { // reload with none in the chamber

            _currentMagSize = magSize;
        } 

        _hasAmmo = true; // allows gun to fire again post reload
    }


    // function to make player hold active weapon, deal with weapon switching
    public void ActivateWeapon(bool activate) {

        StopAllCoroutines(); // cancels reload to switch weapons
        _hasAmmo = true; // lets weapons fire, as new weapon amy have ammo
        gameObject.SetActive(activate); // activates the weapon
    }

    // function to get current ammo count value
    public int GetAmmo() {

        return _currentMagSize; // returns current ammo count
    }

}
