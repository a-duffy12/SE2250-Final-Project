using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnParkour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "RespawnLight"){
            transform.position = new Vector3(0, 17.5f, -101.2f);
        }
        else if(other.gameObject.tag == "RespawnLight2"){
            transform.position = new Vector3(-12.9f, 17.5f, -47.9f);
        }
        else if(other.gameObject.tag == "RespawnLight3"){
            transform.position = new Vector3(10f, 17.5f, -47.6f);
        }
        else if(other.gameObject.tag == "RespawnLight4"){
            transform.position = new Vector3(-14, 17.5f, 12.8f);
        }
    }
}
