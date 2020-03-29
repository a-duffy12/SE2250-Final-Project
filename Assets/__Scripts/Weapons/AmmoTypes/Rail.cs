using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : DefaultBullet
{
    // constructor
    public Rail() {
        speed = 900f; // how fast the bullet travels
        hitForce = 100f; // strength of impact
        destroyAfter = 2.0f; // how long the bullet object stays instantiated
        fleshMod = 1.0f; // modifier for damage against flesh enemies
        armorMod = 2.0f; // modifier for damage against armored enemies
        robotMod = 0.5f; // modifier for damage against robotic enemies
    }
}
