using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : DefaultBullet
{
    // constructor
    public Bullet() {
        speed = 300f; // how fast the bullet travels
        hitForce = 20f; // strength of impact
        destroyAfter = 2.0f; // how long the bullet object stays instantiated
        fleshMod = 1.0f; // modifier for damage against flesh enemies
        armorMod = 1.0f; // modifier for damage against armored enemies
        robotMod = 1.0f; // modifier for damage against robotic enemies
    }
}
