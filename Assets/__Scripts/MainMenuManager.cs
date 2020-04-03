using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // method to change the scene
    public void ChangeScene(int nextScene){
        SceneManager.LoadScene(nextScene);//changes scene to scene number sent in function
    }
}
