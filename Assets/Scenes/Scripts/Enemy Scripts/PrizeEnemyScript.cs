using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeEnemyScript : MonoBehaviour
{
    public float force;
    
    //---set variables for enemy
    private EnemyScript enemy_script;
    public int enemy_max_health;
    //---set variables for enemy points
    public int points_worth;
    
    //---declare vars spexccific to the Prize Enemy
    public string current_state = "state_generate_direction";  //---options: state_moving || state_generate_direction
    
    
    // Start is called before the first frame update
    void Start()
    {
        //---instantiate vars for enemy script
        enemy_script = GetComponent<EnemyScript>();
        //---init health values
        enemy_max_health = 6;
        enemy_script.enemy_health_current = enemy_max_health;
        //---init points values
        points_worth = 75;
        enemy_script.points_worth = points_worth;
        
        //---init values for movement
        force = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_script.game_state_playing == true)
        {
            //---generate a random position within the game's border to go towards
            if(current_state == "state_generate_direction")
            {
                Vector2 nnn = new Vector2(Random.Range(-6,6), Random.Range(-6,6));
                Vector2 random_dir_normalized = new Vector2((transform.position.x + nnn.x), (transform.position.y + nnn.y)).normalized;
                enemy_script.direction = (enemy_script.player_object.transform.position - transform.position);
                current_state = "state_moving";
            }
            else
            {
                //---move towards this random direction for a while and decide if a change in direction wanted every second
                transform.up = enemy_script.direction;
                enemy_script.enemy_rigidbody.velocity = new Vector2(enemy_script.direction.x, enemy_script.direction.y).normalized * force;
                if((enemy_script.time_alive % 60) == 0)
                {
                    float chance_to_change_direction = Random.Range(0,50);
                    if(chance_to_change_direction <= 23)
                    {
                        current_state = "state_generate_direction";
                    }
                }
            }
        }
    }
}
