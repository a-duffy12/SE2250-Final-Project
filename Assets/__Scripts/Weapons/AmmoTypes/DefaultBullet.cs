using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : MonoBehaviour
{
    // generic bullet variables (specific to each ammo type)
    public float speed; // how fast the bullet travels
    public float hitForce; // strength of impact
    public float destroyAfter; // how long the bullet object stays instantiated

    private float _currentTime = 0; // game time
    private Vector3 _oldPos; // intial position
    private Vector3 _newPos; // final position
    private bool _hasHit = false; // did the bullet connect

    private float _damagePoints; // amount of damage the bullet does

    // Start is called before the first frame update
    IEnumerator Start() {
    
        _newPos = transform.position; // sets the final position
        _oldPos = _newPos; // updates initial position to be the starting point

        // runs as long as the bullet is not ready to be destroyed and hasn't hit anything
        while (_currentTime < destroyAfter && !_hasHit) {
            
            Vector3 velocity = transform.forward * speed; // velocity vector
            _newPos += velocity * Time.deltaTime; // changes bullet position with respect to time
            Vector3 direction = _newPos - _oldPos; // direction is the starting postion - the firing position
            float distance = direction.magnitude; // how far the bullet has travelled
            RaycastHit hit; // declaring a hit raycast

            // check for bullet contacting anything
            if (Physics.Raycast(_oldPos, direction, out hit, distance)) {

                if (hit.rigidbody != null) { // check for contact on a rigidbody

                    hit.rigidbody.AddForce(direction*hitForce); // apply the bullet's force to the rigidbody
                    IEntity npc = hit.transform.GetComponent<IEntity>(); // get the entity the contacted rigidbody belongs to
                    
                    if (npc != null) { // check if it is a valid entity

                        npc.ApplyDamage(_damagePoints); // deals damage to the npc
                    }
                }

                _newPos = hit.point; // sets the new end position
                StartCoroutine(DestroyBullet());
            }

            _currentTime += Time.deltaTime; // updating the game time
            yield return new WaitForFixedUpdate(); // waits until fixed update runs

            transform.position = _newPos; // sets the final position
            _oldPos = _newPos; // sets the initial position to be the same as final position
        }

        if (!_hasHit) { // checks if the bullet hasn't hit anything

            StartCoroutine(DestroyBullet()); // destroys the bullet
        }
    }

    // function to destroy the bullet
    IEnumerator DestroyBullet() {

        _hasHit = true; // sets to true so that the destroy does not continually run
        yield return new WaitForSeconds(0.5f); // waits for a tiny bit
        Destroy(gameObject); // destroys the bullet instance
    }

    // function to set bullet damage
    public void SetDamage(float points) {

        _damagePoints = points;
    }
}
