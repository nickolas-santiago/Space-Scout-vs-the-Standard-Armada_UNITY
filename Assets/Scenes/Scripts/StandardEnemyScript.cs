using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemyScript : MonoBehaviour
{
    //---set the variables used for movement and rotation
    private GameObject player_object;
    public float force;
    private Vector2 direction;
    private Rigidbody2D enemy_rigidbody;
    
    //---set variables for enemy health
    public int enemy_max_health;
    private int enemy_current_health;
    
    //---set variables for enemy points
    private GameObject scene_object;
    public int points_earned;
    
    //---set variables for shooting and for enemy projectiles
    public GameObject enemy_bullet_object;
    private GameObject enemy_bullet;
    public int cooldown_time;
    private int current_cooldown_time;
    private string current_state; //---States: "moving"  ||  "aiming"
    private int time_alive;
    
    public int bullet_damage;
    public int max_num_of_shots;
    private int num_of_shots_left;
    
    // Start is called before the first frame update
    void Start()
    {
        //---initiate properties for movement and rotation
        player_object = GameObject.FindGameObjectWithTag("Player");
        enemy_rigidbody = GetComponent<Rigidbody2D>();
        //---initiate enemy properties
        enemy_current_health = enemy_max_health;
        scene_object = GameObject.FindGameObjectWithTag("GameController");
        points_earned = 50;
        
        current_cooldown_time = cooldown_time;
        current_state = "moving";
        time_alive = 0;
        bullet_damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        time_alive++;
        
        //---calculate and set the enemy's direction 
        direction = (player_object.transform.position - transform.position);
        transform.up = direction;
        
        //---if the enemy is currently aiming, it can shoot if it has bullets left
        if(current_state == "aiming")
        {
            if(num_of_shots_left > 0)
            {
                if(current_cooldown_time <= 0)
                {
                    num_of_shots_left--;
                    enemy_bullet = Instantiate(enemy_bullet_object, transform.position, Quaternion.identity) as GameObject;
                    //Debug.Log(enemy_bullet.transform.up);
                    enemy_bullet.transform.up = transform.up;
                    //enemy_bullet.transform.up = transform.up;
                    //Debug.Log(enemy_bullet.GetComponent<BulletScript>().direction);
                    enemy_bullet.GetComponent<BulletScript>().direction = direction;
                    enemy_bullet.GetComponent<BulletScript>().damage = bullet_damage;
                    current_cooldown_time = cooldown_time;
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
            enemy_rigidbody.velocity = new Vector2(direction.x, direction.y).normalized * force;
            //---every second after spawning, the enemy decides if it will shoot or not
            if((time_alive % 60) == 0)
            {
                float chance_to_shoot = Random.Range(0,50);
                //Debug.Log(chance_to_shoot);
                if(chance_to_shoot <= 5)
                {
                    current_state = "aiming";
                    enemy_rigidbody.velocity = new Vector2(0,0);
                    num_of_shots_left = Random.Range(1,max_num_of_shots);
                }
            }
        }
        //---cooldown any weapons
        if(current_cooldown_time > 0)
        {
            current_cooldown_time--;
        }
        //---if an enemy's health is 0, delete it
        if(enemy_current_health <= 0)
        {
            Object.Destroy(this.gameObject);
            scene_object.GetComponent<SceneScript>().GenerateNewScore(points_earned);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        //---if an enemy collides with player bullet, destroy bullet and deplete enemy's health
        if(coll.gameObject.tag == "PlayerBullet")
        {
            //Debug.Log("NPC hit by bullet");
            Object.Destroy(coll.gameObject);
            enemy_current_health -= coll.GetComponent<BulletScript>().damage;
        }
    }
}