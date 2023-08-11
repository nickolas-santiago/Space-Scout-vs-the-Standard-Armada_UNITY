using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemyScript : MonoBehaviour
{
    //---set the variables used for movement
    public float force;
    
    //---set variables for enemy
    private EnemyScript enemy_script;
    public int enemy_max_health;
    public Sprite sprite_damaged;
    //---set variables for enemy points
    public int points_worth;
    
    //---set variables for shooting and for enemy projectiles
    public GameObject enemy_bullet_object;
    private GameObject enemy_bullet;
    public int cooldown_time;
    private int current_cooldown_time;
    private string current_state; //---States: "moving"  ||  "aiming"
    
    public int bullet_damage;
    public int max_num_of_shots;
    private int num_of_shots_left;
    
    // Start is called before the first frame update
    void Start()
    {
        //---instantiate vars for enemy script
        enemy_script = GetComponent<EnemyScript>();
        enemy_script.enemy_health_current = enemy_max_health;
        points_worth = 50;
        enemy_script.points_worth = points_worth;
        
        //---instantiate vars for weapons
        current_cooldown_time = cooldown_time;
        current_state = "moving";
        bullet_damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_script.game_state_playing == true)
        {
            //---calculate and set the enemy's direction 
            enemy_script.direction = (enemy_script.player_object.transform.position - transform.position);
            transform.up = enemy_script.direction;
            
            //---if the enemy is currently aiming, it can shoot if it has bullets left
            if(current_state == "aiming")
            {
                if(num_of_shots_left > 0)
                {
                    if(current_cooldown_time <= 0)
                    {
                        float chance_to_take_shot = Random.Range(0,100);
                        if(chance_to_take_shot <= 5)
                        {
                            num_of_shots_left--;
                            enemy_bullet = Instantiate(enemy_bullet_object, transform.position, Quaternion.identity) as GameObject;
                            enemy_bullet.transform.up = transform.up;
                            enemy_bullet.GetComponent<BulletScript>().direction = enemy_script.direction;
                            enemy_bullet.GetComponent<BulletScript>().damage = bullet_damage;
                            current_cooldown_time = cooldown_time;
                        }
                    }
                }
                else
                {
                    current_state = "moving";
                }
            }
            else if(current_state == "moving")
            {
                //---update the rigidbody's velocity with the caluculated direction and the public force
                enemy_script.enemy_rigidbody.velocity = new Vector2(enemy_script.direction.x, enemy_script.direction.y).normalized * force;
                
                //---every second after spawning, the enemy decides if it will shoot or not
                if((enemy_script.time_alive % 60) == 0)
                {
                    float chance_to_shoot = Random.Range(0,50);
                    if(chance_to_shoot <= 5)
                    {
                        current_state = "aiming";
                        enemy_script.enemy_rigidbody.velocity = new Vector2(0,0);
                        num_of_shots_left = Random.Range(1,max_num_of_shots);
                    }
                }
            }
            //---cooldown any weapons
            if(current_cooldown_time > 0)
            {
                current_cooldown_time--;
            }
        }
    }
}