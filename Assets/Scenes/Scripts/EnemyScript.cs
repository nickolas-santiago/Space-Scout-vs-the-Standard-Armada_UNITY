using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //---set the variables used for movement and rotation
    private GameObject player_object;
    public float force;
    private Vector2 direction;
    private Rigidbody2D enemy_rigidbody;
    
    //---set variables for enemy health
    public int enemy_max_health;
    private int enemy_current_health;
    
    //---set variables for shooting and for enemy projectiles
    public GameObject enemy_bullet_object;
    private GameObject enemy_bullet;
    public int cooldown_time;
    private int current_cooldown_time;
    private string current_state; //---States: "moving"  ||  "aiming"
    private int time_alive;
    
    // Start is called before the first frame update
    void Start()
    {
        //---initiate properties for movement and rotation
        player_object = GameObject.FindGameObjectWithTag("Player");
        enemy_rigidbody = GetComponent<Rigidbody2D>();
        //---initiate enemy properties
        enemy_current_health = enemy_max_health;
        current_cooldown_time = cooldown_time;
        current_state = "moving";
        time_alive = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time_alive++;
        
        
        
        
        
        //---calculate and set the enemy's direction 
        direction = (player_object.transform.position - transform.position);
        transform.up = direction;
        
        //---enemy can shoot
        if(current_state == "aiming")
        {
            if(current_cooldown_time <= 0)
            {
                enemy_bullet = Instantiate(enemy_bullet_object, transform.position, Quaternion.identity) as GameObject;
                current_cooldown_time = cooldown_time;
            }
        }
        else if(current_state == "moving")
        {
            //---update the rigidbody's velocity with the caluculated direction and the public force
            enemy_rigidbody.velocity = new Vector2(direction.x, direction.y).normalized * force;
            //---every second after spawning, the enemy decides if it will shoot or not
            if((time_alive % 60) == 0)
            {
                //Debug.Log(time_alive);
                float chance_to_shoot = Random.Range(0,50);
                Debug.Log(chance_to_shoot);
                if(chance_to_shoot <= 5)
                {
                    Debug.Log("taking my shot, takingmy chance.");
                    current_state = "aiming";
                    enemy_rigidbody.velocity = new Vector2(0,0);
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
        }
    }
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("hello");
        //---if an enemy collides with player bullet, destroy bullet and deplete enemy's health
        if (coll.gameObject.tag == "PlayerBullet")
        {
            Debug.Log("NPC hit by bullet");
            Object.Destroy(coll.gameObject);
            enemy_current_health--;
        }
    }
}
