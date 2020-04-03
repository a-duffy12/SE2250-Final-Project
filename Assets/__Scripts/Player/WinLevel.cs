using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLevel : MonoBehaviour
{
    public Transform groundTest;
    public float groundDist = 0.7f; //radius of ground check
    public LayerMask winMask;
    public Text winText;

    // Update is called once per frame
    void Update()
    {
        if(Physics.CheckSphere(groundTest.position, groundDist, winMask)){
            winText.text = "Level Complete!\nPress Enter to continue to the next level";
            DamageReceiver.invulnerable = false;          
            Time.timeScale = 0f; //stops time so player cannot move
            if(Input.GetKeyDown(KeyCode.Return)){
                PlayerExp.playerXP += 20;
                // The cursor shows up so the player click start in start menu
                Cursor.lockState = CursorLockMode.None; 
                Cursor.visible = true;
                Application.LoadLevel((SceneManager.GetActiveScene().buildIndex)+1);
            }
        }
        
    }
}
