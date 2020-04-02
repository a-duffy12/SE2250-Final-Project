using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject skillPointUI;
    public Text availSkillPointsText;

    // Update is called once per frame
    void Update()
    {
        availSkillPointsText.text = "Available Skill Points: " + PlayerSkillManager.availSkillPoints.ToString();
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        skillPointUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameIsPaused = false;
    }
    void Pause()
    {
        skillPointUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameIsPaused = true;
        
    }
}
