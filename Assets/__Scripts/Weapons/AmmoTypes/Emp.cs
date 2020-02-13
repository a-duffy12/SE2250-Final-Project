using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emp : DefaultBullet
{
    // constructor 
    public Emp() {
        speed = 100f; // how fast the bullet travels
        hitForce = 1f; // strength of impact
        destroyAfter = 5.0f; // how long the bullet object stays instantiated
    }
}
