using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class PlayerMovement : MonoBehaviour
{
    public CharacterController control;

    public float speed = 12f; // movement speed
    public float grav = -9.81f; //gravity
    public float jumpHt = 3f; //jump height    

    Vector3 vel; //velocity
    bool isGrounded; //is the player in the ground

    public Transform groundTest;
    public float groundDist = 0.7f; //radius of ground check
    public LayerMask groundMask; 

    public GameObject grenadePrefab; // grenade object
    public Transform throwPoint; // where the throw comes from
    public float throwSpeed; // speed of thrown projectiles
    public float grenadeCooldown; // time between grenade throws

    public Text warningText; // text for any warnings
    public Text grenadeCount; // text for count of grenades

    public AudioClip jumpAudio; // sound for jumping
    public AudioClip walkAudio; // sound for walking
    public AudioClip throwAudio; // sound for throwing grenades

    private AudioSource _source; // source for audio
    private float _nextGrenadeTime = 0; // tracks time when the next grenade becomes available

    // Start is called before the first frame update
    void Start() {
    
        _source = GetComponent<AudioSource>(); // gets the audio
        _source.playOnAwake = false; // does not play on startup
        _source.spatialBlend = 1f; // makes the sound 3D
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundTest.position, groundDist, groundMask); // tests is player is on the ground
        if(isGrounded && vel.y < 0){ // sets velocity to when on/near the ground 
            vel.y = -10f; // brings the player down faster
        }

        float x = Input.GetAxis("Horizontal"); // creates variables for axis inputs
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z; // moves player based on direction they are facing 

        control.Move(move * speed * Time.deltaTime); // uses character controller to move player

        if (Input.GetButtonDown("Jump") && isGrounded) { // if player in on the ground and jump key is pressed
            
            Jump(); // jumps
        }

        if (Input.GetKeyDown(KeyCode.G)) { // if the player presses G and a grenade is available

            ThrowGrenade(); // throws grenade
        }

        vel.y += grav * Time.deltaTime; // gravity pulls player down

        control.Move(vel * Time.deltaTime); // moves player controller        
    }

    // function to jump player
    void Jump() {

        vel.y = Mathf.Sqrt(jumpHt * -2f * grav); // adds velocity to jump
        _source.clip = jumpAudio; // sets jump audio
        _source.Play(); // plays jump audio
    }

    // function to throw grenades
    void ThrowGrenade() {

        if (Time.time > _nextGrenadeTime) {

            _nextGrenadeTime = Time.time + grenadeCooldown; // set time for next grenade to be available  

            // throws grenade
            GameObject grenade = Instantiate(grenadePrefab, throwPoint.position, throwPoint.rotation); // instantiate a grenade object
            grenade.GetComponent<Rigidbody>().AddForce(throwPoint.transform.forward*throwSpeed); // get rigidbody of grenade and apply speed to it  

            _source.clip = throwAudio; // sets jump audio
            _source.Play(); // plays jump audio
        
        } else {

            ShowWarning("No grenades remaining", 5); // display warning for 1s
        }
        
    }

    // function to show a warning on screen for a certain amount of time
    IEnumerator ShowWarning (string msg, float delay) {
        
        warningText.text = msg; // set the message
        warningText.enabled = true; // display message
        yield return new WaitForSeconds(delay); // run for the delay
        warningText.enabled = false; // disable message
 }
}
