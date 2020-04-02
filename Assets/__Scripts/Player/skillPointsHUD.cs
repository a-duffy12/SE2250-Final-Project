using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillPointsHUD : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // displays the HUD to notify player if they have available skill points
        if (PlayerSkillManager.availSkillPoints > 0)
        {
            if (SkillPointMenu.gameIsPaused) // if game is paused then the HUD disappears
            {
                gameObject.SetActive(false);
            }
            else// if game is not paused, the HUD will notify player of available skill points
            {
                gameObject.SetActive(true);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
