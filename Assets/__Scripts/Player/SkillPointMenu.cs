using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject skillPointUI;
    public Text availSkillPointsText;
    public Text gunUpgrades;
    public Text nadeUpgrades;
    public Text healthUpgrades;
    public Text dmgRedUpgrades;
    public Text msUpgrades;
    public Text shieldUpgrades;

    // Update is called once per frame
    void Update()
    {
        availSkillPointsText.text = "Available Skill Points: " + PlayerSkillManager.availSkillPoints.ToString(); // updates text to display the number of skill points
        //Updates number of times player has upgraded a skill
        gunUpgrades.text = "Times Upgraded: " + PlayerSkillManager.gunDmgUpgrades.ToString();
        nadeUpgrades.text = "Times Upgraded: " + PlayerSkillManager.grenadeDmgUpgrades.ToString();
        healthUpgrades.text = "Times Upgraded: " + PlayerSkillManager.healthUpgrades.ToString();
        dmgRedUpgrades.text = "Times Upgraded: " + PlayerSkillManager.dmgReductionUpgrades.ToString();
        msUpgrades.text = "Times Upgraded: " + PlayerSkillManager.movementSpeedUpgrades.ToString();
        shieldUpgrades.text = "Times Upgraded: " + PlayerSkillManager.shieldTimerUpgrades.ToString();

        // Checks when the player wants to open up the skill points menu
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            if (gameIsPaused)
            {
                // if the game is paused then resume the game
                Resume();
            }
            else
            {
                // if the game is not paused then pause the game
                Pause();
            }
        }
    }

    
    void Resume()
    {
        skillPointUI.SetActive(false); // when the game resumes, then disable the skill points menu
        Time.timeScale = 1f; // Uses time scale to resume the game
        // removes the cursor from screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameIsPaused = false; // game is no longer paused
    }
    void Pause()
    {
        skillPointUI.SetActive(true); // when the game is paused, enable the skill points menu
        Time.timeScale = 0f; // Uses time scale to pause the game
        // The cursor shows up so the player can upgrade their skills
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
        gameIsPaused = true; // game is paused
        
    }
}
