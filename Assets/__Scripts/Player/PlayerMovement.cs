using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class PlayerMovement : MonoBehaviour
{
    public CharacterController control;

    static public PlayerMovement S; // singleton variable

    public float speed = 12f; // movement speed
    public float grav = -9.81f; //gravity
    public float jumpHt = 3f; //jump height    

    Vector3 vel; //velocity
    bool isGrounded; //is the player on the ground    

    public Transform groundTest;
    public float groundDist = 0.7f; //radius of ground check
    public LayerMask groundMask;

    public Transform FloatingObject;
    public float FloatingObjectDist = 0.7f;
    public LayerMask FloatingObjectMask;

    public GameObject grenadePrefab; // grenade object
    public Transform throwPoint; // where the throw comes from
    public float throwSpeed; // speed of thrown projectiles
    public float grenadeCooldown; // time between grenade throws

    public Text playerWarning; // text for any warnings
    public Text grenadeCount; // text for count of grenades

    public AudioClip jumpAudio; // sound for jumping
    public AudioClip walkAudio; // sound for walking
    public AudioClip throwAudio; // sound for throwing grenades

    private AudioSource _source; // source for player audio
    private float _nextGrenadeTime = 0; // tracks time when the next grenade becomes available

    void Awake() //makes class a singleton
    {
        if (S == null){
            S = this;        
        }
        else{
            print("Error: Attempted to create more than 1 player singleton");
        }
    }
    
    // Start is called before the first frame update
    void Start() {
    
        _source = GetComponent<AudioSource>(); // gets audio source
        _source.playOnAwake = false; // does not play on startup
        _source.spatialBlend = 1f; // makes the sound 3D]
        playerWarning.text = ""; // set default player warning
        grenadeCount.text = "[G] 1/1"; // set default grenade count
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundTest.position, groundDist, groundMask); // tests is player is on the ground
        if(isGrounded && vel.y < 0){ // sets velocity to when on/near the ground 
            vel.y = -10f; // brings the player down faster
        }

        Walk(); // walks

        if (((Input.GetButtonDown("Jump") && isGrounded)))
        { // if player in on the ground and jump key is pressed
            
            Jump(); // jumps
        }

     
        if (Input.GetKeyDown(KeyCode.G)) { // if the player presses G and a grenade is available

            ThrowGrenade(); // throws grenade
        }

        vel.y += grav * Time.deltaTime; // gravity pulls player down

        control.Move(vel * Time.deltaTime); // moves player controller down in accordance with gravity       
    }

    // function to move player
    void Walk() {

        // creates variables for axis inputs
        float x = Input.GetAxis("Horizontal"); 
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z; // moves player based on direction they are facing 

        control.Move(move * speed * Time.deltaTime); // uses character controller to move player

        if ((x != 0 || z != 0) && isGrounded && !_source.isPlaying) { // checks for walking motion, player on the ground, and walk sound not already in use

            _source.clip = walkAudio; // sets walk audio
            _source.Play(); // plays walk audio 
        }
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
            StartCoroutine(GrenadeCooldown()); // update grenade count  

            // throws grenade
            GameObject grenade = Instantiate(grenadePrefab, throwPoint.position, throwPoint.rotation); // instantiate a grenade object
            grenade.GetComponent<Rigidbody>().AddForce(throwPoint.transform.forward*throwSpeed); // get rigidbody of grenade and apply speed to it  

            _source.clip = throwAudio; // sets jump audio
            _source.Play(); // plays jump audio
        
        } else {

            StartCoroutine(ShowWarning("No grenades remaining", 1.0f)); // display warning for 1s
        } 
    }

    // function to show a warning on screen for a certain amount of time
    IEnumerator ShowWarning(string msg, float delay) {
        
        playerWarning.text = msg; // set the message
        yield return new WaitForSeconds(delay); // run for the delay
        playerWarning.text= ""; // remove the message
    }

    // function to display text of grenade count and cooldown
    IEnumerator GrenadeCooldown() {

        grenadeCount.text = "[G] 0/1"; // no grenades left
        yield return new WaitForSeconds(grenadeCooldown-0.1f);
        grenadeCount.text = "[G] 1/1"; // one grenade left
    }
}