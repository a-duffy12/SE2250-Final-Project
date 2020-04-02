using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public static PlayerSkillManager instantiated = null; // used to create a singleton for this script
    public static int expNeeded = 50; // Amount of experience needed to obtain next skill point
    // variables to increase the stats of the character
    public static float gunDmgMult = 1f;
    public static int gunDmgUpgrades = 0;
    public static float grenadeDmgMult = 1f;
    public static int grenadeDmgUpgrades = 0;
    public static float maxHealthIncrease = 100; // default health is 100
    public static int healthUpgrades = 0;
    public static float dmgReductionMult = 1f;
    public static int dmgReductionUpgrades = 0;
    public static float movementSpeedIncrease = 12; // default speed is 12
    public static int movementSpeedUpgrades = 0;
    public static float shieldTimerIncrease = 0;
    public static int shieldTimerUpgrades = 0;
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

    // All upgrades modify the stats by roughly 10%
    // Player can only upgrade if skill points are available, and each upgrade uses 1 point
    // The stat is modified where they are called in other scripts
    public void UpgradeGunDmg()
    {
        if (availSkillPoints >= 1)
        {
            gunDmgMult += 0.1f; // gun damage multiplier increases by 10%
            gunDmgUpgrades++;
            availSkillPoints--;
        }
    }
    public void UpgradeNadeDmg()
    {
        if (availSkillPoints >= 1)
        {
            grenadeDmgMult += 0.1f; // grenade damage multiplier increases by 10%
            grenadeDmgUpgrades++;
            availSkillPoints--;
        }
    }
    public void UpgradeMaxHealth()
    {
        if (availSkillPoints >= 1)
        {
            maxHealthIncrease += 10; // increases player max health by 10
            DamageReceiver.playerHP = maxHealthIncrease; // resets the player's health
            healthUpgrades++;
            availSkillPoints--;

        }
    }
    public void UpgradeDmgReduction()
    {
        if (availSkillPoints >= 1)
        {
            dmgReductionMult -= 0.1f; // decreases the amount of damage player takes by 10%
            dmgReductionUpgrades++;
            availSkillPoints--;
        }
    }
    public void UpgradeMovementSpeed()
    {
        if (availSkillPoints >= 1)
        {
            movementSpeedIncrease += 1; // increases movement speed by 1
            PlayerMovement.speed = movementSpeedIncrease; // sets movement speed to new movement speed
            movementSpeedUpgrades++;
            availSkillPoints--;
        }
    }
    public void UpgradeShieldTimer()
    {
        if (availSkillPoints >= 1)
        {
            shieldTimerIncrease += 0.5f;// increases shield timer by half a second
            shieldTimerUpgrades++;
            availSkillPoints--;
        }
    }
}
