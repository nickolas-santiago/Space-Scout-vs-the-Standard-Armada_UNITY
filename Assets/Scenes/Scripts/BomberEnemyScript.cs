using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEnemyScript : MonoBehaviour
{
    //---set the variables used for movement and rotation
    public float force;
    public float reactive_force;
    private Vector2 direction;
    
    //---set variables for enemy
    private EnemyScript enemy_script;
    public int enemy_max_health;
    //---set variables for enemy points
    public int points_worth;
    
    //---set variables for making enemy reactive
    private bool is_reactive;
    
    // Start is called before the first frame update
    void Start()
    {
        //---instantiate vars for enemy script
        enemy_script = GetComponent<EnemyScript>();
        enemy_script.enemy_health_current = enemy_max_health;
        points_worth = 25;
        enemy_script.points_worth = points_worth;
        
        //---initiate enemy properties
        is_reactive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_script.game_state_playing == true)
        {
            //---calculate and set the enemy's direction 
            direction = (enemy_script.player_object.transform.position - transform.position);
            transform.up = direction;
            
            if(is_reactive == false)
            {
                //---every second after spawning, the enemy decide if it will become reactive and become faster
                if((enemy_script.time_alive % 60) == 0)
                {
                    float chance_to_turn = Random.Range(0,50);
                    if(chance_to_turn <= 5)
                    {
                        is_reactive = true;
                        //Debug.Log("im reactive now");
                    }
                }
                //---update the rigidbody's velocity with the caluculated direction and the public regular force
                enemy_script.enemy_rigidbody.velocity = new Vector2(direction.x, direction.y).normalized * force;
            }
            else
            {
                //---update the rigidbody's velocity with the caluculated direction and the public reactive force
                enemy_script.enemy_rigidbody.velocity = new Vector2(direction.x, direction.y).normalized * reactive_force;
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D coll)
    {
        //---if the bomber is reactive and hits the player, it also takes damage
        if(is_reactive == true)
        {
            if(coll.gameObject.tag == "Player")
            {
                enemy_script.enemy_health_current--;
            }
        }
    }
}