using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLifts : MonoBehaviour
{

    float speed = 1.5f;

    //adjust this to change how high it goes
    

    // Start is called before the first frame update
    void Start()
    {
        
    }




   
    // Update is called once per frame
    void Update()
    {

        

        //get the objects current position and put it in a variable so we can access it later with less code

        Vector3 pos = transform.position;
        pos.x = 0;
        pos.y = 7.5f;
        pos.z = -93f;

        transform.position = new Vector3(pos.x, pos.y, pos.z);



        //calculate what the new Y position will be
        float newY = Mathf.Sin((Time.time * speed))*3;

        //set the object's Y to the new calculated Y

        
            transform.position = new Vector3(pos.x, newY*pos.y, pos.z);
        




    }

}


// declare this and initialize outside your function


