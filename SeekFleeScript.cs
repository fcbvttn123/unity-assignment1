using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SeekFleeScript : MonoBehaviour
{
    public GameObject character;
    public GameObject target;

    //Run from target
    public bool fleeMode = false;

    //(rate and direction of movement)
    private Vector3 velocity;
    //weight
    public float Mass = 15;
    //max (rate and direction of movement)
    public float MaxVelocity = 3;
    //max force
    public float MaxForce = 15;

    // Start is called before the first frame update
    void Start()
    {
        //Velocity we are currently starting off at
        velocity = Vector3.zero;
    }

   


    // Update is called once per frame
    void Update()
    {
      
        //Shortest path between the character and the target 
        Vector3 desiredVelocity = target.transform.position - character.transform.position;

        //Normalized is the desired velocity with a length of 1,  multiply that by the max velocity (rate and direction of movement)
        desiredVelocity = desiredVelocity.normalized * MaxVelocity;

        //Steering force for a curved path
        Vector3 steering = desiredVelocity - velocity;

        //Clamp the max magniture of the steering vector to max force
        steering = Vector3.ClampMagnitude(steering, MaxForce);

        //Divide-equal steering by mass of character  
        steering /= Mass;

        //Clamp the vector for velocity + steering to max velocity
        velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);

        //If flee mode, reverse the movement
        if(fleeMode == false)
            character.transform.position += velocity * Time.deltaTime;
        else
            character.transform.position += (-1 * velocity) * Time.deltaTime;

        //Set position
        character.transform.position = new Vector3(character.transform.position.x, 1, character.transform.position.z);

        //Set direction of character to velocity normalized
        character.transform.forward = velocity.normalized;

       
    }
}
