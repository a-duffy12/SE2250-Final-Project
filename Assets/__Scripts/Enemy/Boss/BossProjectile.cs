using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class BossProjectile : MonoBehaviour
{
    public float projectileSpeed; // how fast missle travels
    public float fuseTime; // how long until grenade explodes in s
    public float blastRadius; // radius of explosion in m
    public float damage; // damage of grenade
    public GameObject explosionParticle; // particle effect of grenade explosion
    public AudioClip fireAudio; // sound of missile being fired
    
    private AudioSource _source; // source for audio
    private float _fireTime; // keeps track of when fired
    [HideInInspector]
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _source = GetComponent<AudioSource>(); // gets audio source
        _source.playOnAwake = false; // does not play on startup
        _source.spatialBlend = 1f; // makes the sound 3D

        _fireTime = Time.time; // set fire time

        _source.clip = fireAudio; // sets firing audio
        _source.Play(); // plays firing audio
    }

    // Update is called once per frame
    void Update()
    {
        // boss moves towards player
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, projectileSpeed*Time.deltaTime);

        // distance between missile and player
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        // check for proximity to player
        if (distance < blastRadius - 0.5f) {

            Detonate(); // detonates the missile
        }

        // if its fuse time is over
        if (Time.time > (_fireTime + fuseTime)) {

            Detonate(); // detonates the missile
        }
    }

    // function to detonate the missile
    void Detonate() {

        GameObject effect = Instantiate(explosionParticle, transform.position, transform.rotation); // create explosion particles
        Destroy(effect, 1.5f); // destroy particle after the sound finishes
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius); // get all game objects in blast radius
        foreach (Collider col in colliders) { // runs for each entity in the blast radius
        
            IEntity entity = col.transform.GetComponent<IEntity>(); // get collider's parent game object

            if (entity != null) { // check if it is a valid entity

                entity.ApplyDamage(damage); // deals damage
            }
        }

        Destroy(gameObject); // destroys the missile

    }
}
