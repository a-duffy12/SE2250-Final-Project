using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointsHUD : MonoBehaviour
{
    public Text skillNotifier;
    // Update is called once per frame
    void Update()
    {
        // displays the HUD to notify player if they have available skill points
        if (PlayerSkillManager.availSkillPoints > 0)
        {
            if (SkillPointMenu.gameIsPaused) // if game is paused then the HUD disappears
            {
                skillNotifier.text = "";
            }
            else// if game is not paused, the HUD will notify player of available skill points
            {
                skillNotifier.text = "Skill Point Available! Press [=] to Apply";
            }
        }
        else
        {
            skillNotifier.text = "";
        }
    }
}
