using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public static PlayerSkillManager instantiated = null; // used to create a singleton for this script
    public static int expNeeded = 50;
    // variables to increase the stats of the character
    public float gunDmgMult = 1f;
    public float grenadeDmgMult = 1f;
    public float maxHealthIncrease = 0;
    public float dmgReductionMult = 1f;
    public float movementSpeedIncrease = 0;
    public int shieldTimerIncrease = 0;
    public static int availSkillPoints = 0;

    void Awake()
    {
        // creates a singleton for this class
        // Singleton needed so upgraded player stats do not get reset
        if (instantiated == null)
        {
            instantiated = this;
        }
        else if (instantiated != null)
        {
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
