using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma : DefaultBullet
{
    // constructor
    public Plasma() {
        speed = 200f; // how fast the bullet travels
        hitForce = 0.5f; // strength of impact
        destroyAfter = 5.0f; // how long the bullet object stays instantiated
        fleshMod = 2.0f; // modifier for damage against flesh enemies
        armorMod = 0.5f; // modifier for damage against armored enemies
        robotMod = 1.0f; // modifier for damage against robotic enemies
    }
}
