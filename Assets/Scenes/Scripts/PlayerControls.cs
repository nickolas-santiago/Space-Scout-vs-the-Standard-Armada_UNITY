using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    //---set the public variables for player speed
    public float speedx = 25;
    public float speedy = 25;
    
    //---set some vars for shooting
    public GameObject projectile;
    GameObject myPJ;
    public int cooldown_time = 20;
    private int current_cooldown_time = 0;
    
    // Start is called before the first frame update
    /*void Start() {    }*/

    // Update is called once per frame
    void Update()
    {
        //---set the movement speed
        Vector2 speed = new Vector2(speedx, speedy);

        //---these floats get set when Unity recognizes an input (kayboard, mouse, controller, etc.)
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        
        //---calculate and set the player's new position
        Vector3 movement = new Vector3((speed.x * inputX), (speed.y * inputY), 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);
        
        //---if the keyboard is being pressed, attempt to shoot
        if(Input.GetKey("space"))
        {
            //---a successful shot happens when cooldown is at 0
            if(current_cooldown_time == 0)
            {
                current_cooldown_time = cooldown_time;
                myPJ = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            }            
        }
        //---cooldown any weapons
        if(current_cooldown_time > 0)
        {
            current_cooldown_time--;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "NPC")
        {
            //Debug.Log("hello");
            //Debug.Log(coll);
        }
        Vector3 collPosition = coll.transform.position;
       /* if(collPosition.y > transform.position.y)
        {
            Debug.Log("The object that hit me is above me!");
        }
        else
        { 
            Debug.Log("The object that hit me is below me!");
        }

        if (collPosition.x > transform.position.x)
        {
            Debug.Log ("The object that hit me is to my right!");
        } 
        else 
        {
            Debug.Log("The object that hit me is to my left!");
        }
        */
    }
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        //Debug.Log("hello");
        if (coll.gameObject.tag == "Trigger")
        {
            //Debug.Log("hello again buddy");
        }
    }
}