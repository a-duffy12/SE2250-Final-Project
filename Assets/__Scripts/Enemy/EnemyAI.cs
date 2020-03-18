using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour, IEntity
{
    public float attackDistance = 7f;
    public float lookDistance = 10f;
    public float npcHP = 100;
    public float npcDamage = 5;
    public float attackRate = 0.5f;
    public Transform firePoint;
    float nextAttackTime = 0;
    [HideInInspector]
    public Transform playerTransform;
    Rigidbody r;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        r = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(playerTransform.position, transform.position);
        // if within a certain distance then looks at target
        if (distance <= lookDistance)
        {
            transform.LookAt(new Vector3(playerTransform.transform.position.x, transform.position.y, playerTransform.position.z));
            // if close enough then enemy actually tries shoots
            if (distance <= attackDistance)
            {
                // only shoots 
                if (Time.time > nextAttackTime)
                {
                    nextAttackTime = Time.time + attackRate;

                    //Attack
                    RaycastHit hit;
                    if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, attackDistance))
                    {
                        if (hit.transform.CompareTag("Player"))
                        {
                            Debug.DrawLine(firePoint.position, firePoint.position + firePoint.forward * attackDistance, Color.cyan);

                            IEntity player = hit.transform.GetComponent<IEntity>();
                            player.ApplyDamage(npcDamage);
                        }
                    }
                }
            }
        }
    }
    public void ApplyDamage(float points)
    {
        npcHP -= points;
        if (npcHP <= 0)
        {
            //Slightly bounce the npc dead prefab up
            gameObject.GetComponent<Rigidbody>().velocity = (-(playerTransform.position - transform.position).normalized * 8) + new Vector3(0, 5, 0);
            Destroy(gameObject, 10);
        }
    }
}
