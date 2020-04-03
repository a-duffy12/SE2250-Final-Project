using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]

public class RobotEnemyAI : MonoBehaviour, IEntity
{
    public float lookDistance;
    public float npcHP;
    public float npcSpeed; // speed of enemy
    public float suicideDamage; // enemy death damage
    public float suicideRadius; // radius of death damage
    public float experienceGain;
    public bool giveXP = true;
    public GameObject explosionParticle; // particle effect for suicide explosion
    public AudioClip damageEnemyAudio; // sound for taking damage
    public AudioClip killEnemyAudio; // sound upon death
    public AudioClip enemyAlertAudio; // sound for enemy altered to player

    private AudioSource _source; // source for enemy audio
    private bool _huntPlayer; // whether or not the enemy is hunting the player
    [HideInInspector]
    public Transform playerTransform;
    
    //private static float playerXP;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _huntPlayer = false; // enemy is not hunting the player
        _source = GetComponent<AudioSource>(); // gets audio source
        _source.playOnAwake = false; // does not play on startup
        _source.spatialBlend = 1f; // makes the sound 3D
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        // if within a certain distance then looks it starts hunting the player
        if ((distance <= lookDistance) && !_huntPlayer)
        {
            // enemy is now going to actively hunt the player
            _huntPlayer = true; 

            _source.clip = enemyAlertAudio; // sets alert audio
            _source.Play(); // plays alert audio
        }

        if (_huntPlayer) { // enemy is hunting player

            // look at player
            transform.LookAt(new Vector3(playerTransform.position.x, playerTransform.position.y + 0.4f, playerTransform.position.z));
            
            // enemy moves towards player
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, npcSpeed*Time.deltaTime);

            if (distance <= (suicideRadius-0.5))
            {
                Suicide();
            }
        }
    }

    void Suicide() {

        Destroy(gameObject); // destroys the enemy

        GameObject effect = Instantiate(explosionParticle, transform.position, transform.rotation); // create explosion particles
        Destroy(effect, 1.5f); // destroy particle after the sound finishes
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, suicideRadius); // get all game objects in blast radius
        foreach (Collider col in colliders) { // runs for each entity in the blast radius
        
            IEntity entity = col.transform.GetComponent<IEntity>(); // get collider's parent game object

            if (entity != null) { // check if it is a valid entity

                entity.ApplyDamage(suicideDamage*PlayerSkillManager.dmgReductionMult); // deals damage,decreasing based depending on player's modifier, to the npc
            }
        }
    }

    public void ApplyDamage(float points) {
        npcHP -= points;
        _source.clip = damageEnemyAudio; // sets hurt audio
        _source.Play(); // plays hurt audio 

        if (npcHP <= 0)
        {
            _source.clip = killEnemyAudio; // sets death audio
            _source.Play(); // plays death audio 

            if(giveXP){
                //increases player xp
                PlayerExp.playerXP += experienceGain;
                if(Math.Floor(PlayerExp.playerXP/PlayerSkillManager.expNeeded) >= 1) // checks if the player reached interval for next skill point
                {
                    PlayerSkillManager.availSkillPoints++;
                    PlayerSkillManager.expNeeded+=PlayerSkillManager.expInterval; // increases experience needed to next interval
                }
                giveXP = false;
            }            
            //Slightly bounce the npc dead prefab up
            gameObject.GetComponent<Rigidbody>().velocity = (-(playerTransform.position - transform.position).normalized * 8) + new Vector3(0, 0.2f, 0);
            Destroy(gameObject, 1);            
        }
    }
}
