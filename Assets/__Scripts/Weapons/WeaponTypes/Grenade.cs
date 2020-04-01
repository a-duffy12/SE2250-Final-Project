using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float fuseTime; // how long until grenade explodes in s
    public float blastRadius; // radius of explosion in m
    public float damage; // damage of grenade
    public GameObject explosionParticle; // particle effect of grenade explosion

    // Start is called before the first frame update
    void Start() {
         
        StartCoroutine(ActiveGrenade(fuseTime)); // start fuse
    }

    // function to explode grenade
    IEnumerator ActiveGrenade(float timer) {
        
        yield return new WaitForSeconds(timer); // wait for grenade to explode

        GameObject effect = Instantiate(explosionParticle, transform.position, transform.rotation); // create explosion particles
        Destroy(effect, 1.5f); // destroy particle after the sound finishes
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius); // get all game objects in blast radius
        foreach (Collider col in colliders) { // runs for each entity in the blast radius
        
            IEntity entity = col.transform.GetComponent<IEntity>(); // get collider's parent game object

            if (entity != null) { // check if it is a valid entity

               entity.ApplyDamage(damage); // deals damage to the npc
                
            }
        }

        Destroy(gameObject); // destroys the grenade object 
    }
}
