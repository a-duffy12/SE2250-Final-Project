using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLifts : MonoBehaviour
{
    //Creates a variable called speed 
    float speed = 1.5f;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        //Puts current position of object into a variable so we can access it later with less code
        Vector3 pos = transform.position;

        //Sets current position of object
        pos.x = 0;
        pos.y = 9.2f;
        pos.z = -93f;

        //Transforms it into it's position
        transform.position = new Vector3(pos.x, pos.y, pos.z);


        //calculate what the new Y position will be
        float newY = Mathf.Sin((Time.time * speed))*7+pos.y;
        

        //set the object's Y to the new calculated Y
        transform.position = new Vector3(pos.x, newY, pos.z);

        //If the object tries to go through the floor, don't let it 
        if(newY<7.5)
        {
            pos.x = 0;
            pos.y = 7.6f;
            pos.z = -93f;

            transform.position = new Vector3(pos.x, pos.y, pos.z);
        }

        
        




    }

}


// declare this and initialize outside your function


