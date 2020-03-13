using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Grenade : MonoBehaviour
{
    public float fuseTime; // how long until grenade explodes in s
    public float blastRadius; // radius of explosion in m
    public float damage; // damage of grenade
    public GameObject explosionParticle; // particle effect of grenade explosion
    public AudioClip explosionSound; // sound effect of grenade explosion

    private float _countdown; // time remaining on countdown unti explosion
    private bool _hasExploded = false; // check if grenade has already gone off
    private AudioSource _source;

    // Start is called before the first frame update
    void Start() {
         
        _countdown = fuseTime; // set initial countdown to be the fuse time
        _source = GetComponent<AudioSource>(); // gets the audio
        _source.playOnAwake = false; // does not play on startup
        _source.spatialBlend = 1f; // makes the sound 3D
    }

    // Update is called once per frame
    void Update() {
        
        _countdown -= Time.deltaTime; // reduces the countdown

        // detonates grenade after countdown so long as it hasn't akready gone off
        if (_countdown <= 0 && !_hasExploded) { 

            // calls Explode()
            Explode();
        }
    }

    // function to explode grenade
    void Explode() {
        
        GameObject effect = Instantiate(explosionParticle, transform.position, transform.rotation); // create explosion particles
        Destroy(effect, 1.5f); // destroy particle after the sound finishes
        
        _source.clip = explosionSound; // sets explosion audio
        _source.Play(); // plays explosion audio
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius); // get all game objects in blast radius
        foreach (Collider col in colliders) { // runs for each entity in the blast radius
        
            IEntity entity = col.transform.GetComponent<IEntity>(); // get collider's parent game object

            if (entity != null) { // check if it is a valid entity

                entity.ApplyDamage(damage); // deals damage to the npc
            }
        }

        _hasExploded = true; // grenade has now gone off
        Destroy(gameObject); // destroys the grenade object 
    }
}
