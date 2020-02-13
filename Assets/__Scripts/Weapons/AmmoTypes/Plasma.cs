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
    }
}
