using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]

public class BossAI : MonoBehaviour
{
    public float minRange; // minimum distance boss keeps between itself and player
    public float maxRange; // maximum distance boss keeps between itself and player
    public float npcHP; 
    public float cycleTime; // how long between the start of each attack cycle
    public float experienceGain;
    public bool giveXP = true;
    public Transform FirePoint_R;
    public Transform FirePoint_L;
    public AudioClip damageBossAudio; // sound for taking damage
    public AudioClip killBossAudio; // sound upon death
    public AudioClip bossAlertAudio; // sound for enemy alerted to player
    public AudioClip bossSummonAudio; // sound for summoning enemies in

    private AudioSource _source; // source for enemy audio
    private float _nextAttackTime = 0;
    [HideInInspector]
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
