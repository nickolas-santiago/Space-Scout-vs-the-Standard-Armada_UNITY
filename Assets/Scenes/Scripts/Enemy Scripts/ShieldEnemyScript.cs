using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemyScript : MonoBehaviour
{
    public float force;
    
    //---set variables for enemy
    private EnemyScript enemy_script;
    public int enemy_max_health;
    //---set variables for enemy points
    public int points_worth;
    
    public string current_state = "state_moving";  //---options: state_moving || state_aiming || state_attacking
    
    public Vector2 attack_direction_per_frame;
    public Vector2 ddd;
    
    
    private int attack_time_max;
    private int attack_time_current;
    
    // Start is called before the first frame update
    void Start()
    {
        //---instantiate vars for enemy script
        enemy_script = GetComponent<EnemyScript>();
        points_worth = 75;
        enemy_script.points_worth = points_worth;
        enemy_max_health = 6;
        enemy_script.enemy_health_current = enemy_max_health;
        force = 1;
        current_state = "state_moving";
        attack_time_max = 120;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_script.game_state_playing == true)
        {
            //---calculate and set the enemy's direction 
            enemy_script.direction = (enemy_script.player_object.transform.position - transform.position);
            transform.up = enemy_script.direction;
            
            if(current_state == "state_moving")
            {
                //---update the rigidbody's velocity with the caluculated direction and the public force
                enemy_script.enemy_rigidbody.velocity = new Vector2(enemy_script.direction.x, enemy_script.direction.y).normalized * force;
                //---decide if its time to start aiming
                if((enemy_script.time_alive % 60) == 0)
                {
                    float chance_to_start_aiming = Random.Range(0,50);
                    if(chance_to_start_aiming <= 25)
                    {
                        Debug.Log(chance_to_start_aiming);
                        current_state = "state_aiming";
                        enemy_script.enemy_rigidbody.velocity = new Vector2(0,0);
                    }
                }
            }
            if(current_state == "state_aiming")
            {
                Debug.Log("aiming");
                if((enemy_script.time_alive % 60) == 0)
                {
                    float chance_to_attack = Random.Range(0,50);
                    if(chance_to_attack <= 20)
                    {
                        current_state = "state_attacking";
                        attack_time_current = attack_time_max;
                        ddd = new Vector2(enemy_script.direction.x, enemy_script.direction.y).normalized * 6;
                    }
                    else if(chance_to_attack <= 45)
                    {
                        current_state = "state_moving";
                        Debug.Log("moving");
                    }
                }
            }
            else if(current_state == "state_attacking")
            {
                transform.position = new Vector2(transform.position.x + (ddd.x/attack_time_max), transform.position.y + (ddd.y/attack_time_max));
                //---work towards next state
                attack_time_current--;
                if(attack_time_current <= 0)
                {
                    current_state = "state_aiming";
                }
                
            }
            
        }
    }
}
