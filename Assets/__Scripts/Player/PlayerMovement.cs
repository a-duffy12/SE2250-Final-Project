using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public AudioClip jumpAudio; // sound for jumping
    public AudioClip walkAudio; // sound for walking

    AudioSource _source; // source for audio

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
            vel.y = Mathf.Sqrt(jumpHt * -2f * grav); // adds velocity to jump
            _source.clip = jumpAudio; // sets jump audio
            _source.Play(); // plays jump audio
        }

        vel.y += grav * Time.deltaTime; // gravity pulls player down

        control.Move(vel * Time.deltaTime); // moves player controller        
    }
}
