using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEnemyScript : MonoBehaviour
{
    //---set the variables used for movement and rotation
    private GameObject player_object;
    public float force;
    public float reactive_force;
    private Vector2 direction;
    private Rigidbody2D enemy_rigidbody;
    
    //---set variables for enemy health
    public int enemy_max_health;
    private int enemy_current_health;
    
    //---set variables for making enemy reactive
    private string current_state; //---States: "moving"  ||  "aiming"
    private int time_alive;
    private bool is_reactive;
    
    // Start is called before the first frame update
    void Start()
    {
        //---initiate properties for movement and rotation
        player_object = GameObject.FindGameObjectWithTag("Player");
        enemy_rigidbody = GetComponent<Rigidbody2D>();
        //---initiate enemy properties
        enemy_current_health = enemy_max_health;
        time_alive = 0;
        is_reactive = false;
    }

    // Update is called once per frame
    void Update()
    {
        //---calculate and set the enemy's direction 
        direction = (player_object.transform.position - transform.position);
        transform.up = direction;
        
        if(is_reactive == false)
        {
            time_alive++;
            //---every second after spawning, the enemy decide if it will become reactive and become faster
            if((time_alive % 60) == 0)
            {
                float chance_to_turn = Random.Range(0,50);
                if(chance_to_turn <= 5)
                {
                    is_reactive = true;
                    Debug.Log("im reactive now");
                }
            }
            //---update the rigidbody's velocity with the caluculated direction and the public regular force
            enemy_rigidbody.velocity = new Vector2(direction.x, direction.y).normalized * force;
        }
        else
        {
            //---update the rigidbody's velocity with the caluculated direction and the public reactive force
            enemy_rigidbody.velocity = new Vector2(direction.x, direction.y).normalized * reactive_force;
        }
        //---if an enemy's health is 0, delete it
        if(enemy_current_health <= 0)
        {
            Object.Destroy(this.gameObject);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D coll)
    {
        //---if the bomber is reactive and hits the player, it also takes damage
        if(is_reactive == true)
        {
            if(coll.gameObject.tag == "Player")
            {
                enemy_current_health--;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        //---if an enemy collides with player bullet, destroy bullet and deplete enemy's health
        if(coll.gameObject.tag == "PlayerBullet")
        {
            Debug.Log("NPC hit by bullet");
            Object.Destroy(coll.gameObject);
            enemy_current_health--;
        }
    }
}