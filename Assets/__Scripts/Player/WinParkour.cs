using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinParkour : MonoBehaviour
{
    public Transform groundTest;
    public float groundDist = 0.7f; //radius of ground check
    public LayerMask winMask;
    public Text winText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.CheckSphere(groundTest.position, groundDist, winMask)){
            winText.text = "You Win!";            
            Time.timeScale = 0f; //stops time so player cannot move
        }
        
    }
}
