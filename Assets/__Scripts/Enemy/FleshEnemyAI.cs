using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]

public class FleshEnemyAI : MonoBehaviour, IEntity
{
    public float attackDistance;
    public float lookDistance;
    public float npcHP;
    public float npcDamage;
    public float attackRate;
    public float experienceGain;
    public bool giveXP = true;
    public Transform firePoint;
    public AudioClip damageEnemyAudio; // sound for taking damage
    public AudioClip killEnemyAudio; // sound upon death
    public AudioClip enemyAttackAudio; // sound for enemy attack
    public AudioClip enemyAlertAudio; // sound for enemy altered to player

    private AudioSource _source; // source for enemy audio
    private float _nextAttackTime = 0;
    [HideInInspector]
    public Transform playerTransform;

    //private static float playerXP;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _source = GetComponent<AudioSource>(); // gets audio source
        _source.playOnAwake = false; // does not play on startup
        _source.spatialBlend = 1f; // makes the sound 3D
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        // if within a certain distance then looks at target
        if (distance <= lookDistance)
        {
            // look at player
            transform.LookAt(new Vector3(playerTransform.position.x, playerTransform.position.y + 0.6f, playerTransform.position.z));

            _source.clip = enemyAlertAudio; // sets alert audio
            _source.Play(); // plays alert audio

            // if close enough then enemy actually attacks
            if (distance <= attackDistance)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        // only shoots once the time allows it
        if (Time.time > _nextAttackTime)
        {
            _nextAttackTime = Time.time + attackRate;

            _source.clip = enemyAttackAudio; // sets attack audio
            _source.Play(); // plays attack audio 

            // Attack
            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, attackDistance))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Debug.DrawLine(firePoint.position, firePoint.position + firePoint.forward * attackDistance, Color.cyan);

                    IEntity player = hit.transform.GetComponent<IEntity>();
                    player.ApplyDamage(npcDamage*PlayerSkillManager.dmgReductionMult);  // multiplies the damage by the player's modifier
                }
            }
        }
    }

    public void ApplyDamage(float points)
    {
        npcHP -= points;
        _source.clip = damageEnemyAudio; // sets hurt audio
        _source.Play(); // plays hurt audio 

        if (npcHP <= 0)
        {
            _source.clip = killEnemyAudio; // sets death audio
            _source.Play(); // plays death audio 

            if (giveXP)
            {
                //increases player xp
                PlayerExp.playerXP += experienceGain;
                if(Math.Floor(PlayerExp.playerXP/PlayerSkillManager.expNeeded) >= 1) // Checks if the player has enough experience to obtain a skill point
                {
                    PlayerSkillManager.availSkillPoints++;
                    PlayerSkillManager.expNeeded+=PlayerSkillManager.expNeeded; // increases the amount of experience needed to next interval
                }
                giveXP = false;
            }
            //Slightly bounce the npc dead prefab up
            gameObject.GetComponent<Rigidbody>().velocity = (-(playerTransform.position - transform.position).normalized * 8) + new Vector3(0, 0.6f, 0);
            Destroy(gameObject, 1);
        }
    }
}
