using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]

public class BossAI : MonoBehaviour, IEntity
{
    static public BossAI S; // singleton variable

    public float minRange; // minimum distance boss keeps between itself and player
    public float maxRange; // maximum distance boss keeps between itself and player
    public float npcHP; 
    public float npcSpeed;
    public float volleySize; // how many projectiles per volley
    public float volleyDelay; // how long between projectiles in a volley
    public float experienceGain;
    public bool giveXP = true;
    public Transform firePoint_R;
    public Transform firePoint_L;
    public GameObject bossProjectile; // projectile boss shoots
    public GameObject explosionParticle; // particle effect for explosion upon death
    public AudioClip damageBossAudio; // sound for taking damage
    public AudioClip killBossAudio; // sound upon death
    public AudioClip bossAlertAudio; // sound for enemy alerted to player
    public AudioClip bossSummonAudio; // sound for summoning enemies in

    private AudioSource _source; // source for enemy audio
    private float _nextAttackTime = 8; // boss gives you 10 seconds before attacking
    [HideInInspector]
    public Transform playerTransform;

    void Awake() //makes class a singleton
    {
        if (S == null){
            S = this;        
        }
        else{
            print("Error: Attempted to create more than one boss singleton");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _source = GetComponent<AudioSource>(); // gets audio source
        _source.playOnAwake = false; // does not play on startup
        _source.spatialBlend = 1f; // makes the sound 3D

        _source.clip = bossAlertAudio; // sets alert audio
        _source.Play(); // plays alert audio 
    }

    // Update is called once per frame
    void Update()
    {
        // boss always looks at player
        transform.LookAt(new Vector3(playerTransform.position.x, playerTransform.position.y + 3.8f, playerTransform.position.z));

        Move();
        ChooseNextAttack();
    }

    // function to adjust positioning of boss
    void Move() {

        // distance between boss and player
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance < minRange) { // if the boss is too close to the player

            // boss moves away from player
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, -1*npcSpeed*Time.deltaTime);

        } else if (distance > maxRange) { // if the boss is too far from the player

            // boss moves towards player
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, npcSpeed*Time.deltaTime);
        }
    }

    // funcion to initiate boss' attack cycle
    void ChooseNextAttack() {
        
        float nextAttack = Random.Range(0.0f, 1.0f); // rng to decide next attack

        if (Time.time > _nextAttackTime) {

            _nextAttackTime = Time.time + volleyDelay*volleySize + 3.0f; // next attack cycle is allowed after current attack cycle finishes

            if (nextAttack > 0.3f) {
                
                StartCoroutine(ProjectileVolley()); // run attack cycle combo
                Debug.Log("projectiles");
            } else {
                Debug.Log("summon");
            } 
        }
    }

    // function to control boss' attack cycle
    IEnumerator ProjectileVolley() {
        
        for (int i=0; i < volleySize; i++) {

            GameObject proj_L = Instantiate(bossProjectile, firePoint_L.position, firePoint_L.rotation); // instantiate a grenade object
            GameObject proj_R = Instantiate(bossProjectile, firePoint_R.position, firePoint_R.rotation); // instantiate a grenade object
            
            yield return new WaitForSeconds(volleyDelay); // time between each volley
        }
    }

    // function to summon robot enemies
    void Summon() {
        // TODO
    }

    public void ApplyDamage(float points) {
        npcHP -= points;
        _source.clip = damageBossAudio; // sets hurt audio
        _source.Play(); // plays hurt audio 

        if (npcHP <= 0)
        {
            _source.clip = killBossAudio; // sets death audio
            _source.Play(); // plays death audio 

            if(giveXP){
                //TODO
                //code removed when playerXP was changed to static
                //GameObject.Find("Player").GetComponent<PlayerExp>().playerXP += experienceGain;
                PlayerExp.playerXP += experienceGain;
                giveXP = false;
            }            
            // play explosion upon death
            GameObject effect = Instantiate(explosionParticle, transform.position, transform.rotation); // create explosion particles
            Destroy(gameObject, 0.5f);
            Destroy(effect, 1.5f); // destroy particle after the sound finishes
                        
        }
    }
}
