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
        enemy_script.enemy_health_current = enemy_max_health;
        points_worth = 75;
        enemy_script.points_worth = points_worth;
        enemy_max_health = 6;
        force = 1;
        current_state = "state_moving";
        attack_time_max = 120;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_script.game_state_playing == true)
        {
            //Debug.Log(enemy_script.direction);
            //---calculate and set the enemy's direction 
            enemy_script.direction = (enemy_script.player_object.transform.position - transform.position);
            transform.up = enemy_script.direction;
            
            if(current_state == "state_moving")
            {
                //---update the rigidbody's velocity with the caluculated direction and the public force
                enemy_script.enemy_rigidbody.velocity = new Vector2(enemy_script.direction.x, enemy_script.direction.y).normalized * force;

                float chance_to_start_aiming = Random.Range(0,50);
                if(chance_to_start_aiming >= 20)
                {
                    current_state = "state_aiming";
                    enemy_script.enemy_rigidbody.velocity = new Vector2(0,0);
                }
            }
            if(current_state == "state_aiming")
            {
                Debug.Log("aiming");
                float chance_to_attack = Random.Range(0,75);
                if(chance_to_attack <= 25)
                {
                    current_state = "state_attacking";
                    attack_time_current = attack_time_max;
                    Vector2 attack_direction = new Vector2(enemy_script.direction.x, enemy_script.direction.y).normalized * 3;
                    ddd = new Vector2(enemy_script.direction.x, enemy_script.direction.y).normalized * 6;
                    //attack_direction_per_frame = new Vector2(((transform.position.x + attack_direction.x)/1),((transform.position.y + attack_direction.y)/1));
                    Debug.Log(ddd);
                    Debug.Break();
                }
            }
            else if(current_state == "state_attacking")
            {
                Debug.Log("attacking");
                Debug.Log(attack_direction_per_frame);
                //transform.position = new Vector2(attack_direction_per_frame.x, attack_direction_per_frame.y);
                
                Debug.Log(((transform.position.x + ddd.x)/attack_time_max));
                transform.position = new Vector2(transform.position.x + (ddd.x/attack_time_max), transform.position.y + (ddd.y/attack_time_max));
                
                attack_time_current--;
                if(attack_time_current <= 0)
                {
                    current_state = "state_aiming";
                }
                
            }
            
        }
    }
}
