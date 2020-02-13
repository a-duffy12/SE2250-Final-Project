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
    }
}
