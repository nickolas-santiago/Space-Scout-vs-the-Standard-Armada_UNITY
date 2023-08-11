using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Vector2 direction;
    
    public GameObject spawner_object;
    public bool part_of_current_wave;
    public GameObject powerup_object_prefab;
    private GameObject scene_object;
    public bool game_state_playing;
    
    //---set the variables used for movement and rotation
    public GameObject player_object;
    public Rigidbody2D enemy_rigidbody;
    
    public int time_alive;
    //---set variables for enemy health
    public int enemy_health_current;
    private int iframe_max = 50;
    private int iframe_current;
    
    //---set variables for enemy points
    public int points_worth;
    
    // Start is called before the first frame update
    void Start()
    {
        spawner_object = GameObject.FindGameObjectWithTag("EnemySpawner");
        //---initiate properties for the target (player)
        player_object = GameObject.FindGameObjectWithTag("Player");
        enemy_rigidbody = GetComponent<Rigidbody2D>();
        
        time_alive = 0;
        scene_object = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        if(scene_object.GetComponent<SceneScript>().current_game_state == "game_state_playing")
        {
            game_state_playing = true;
            time_alive++;
            if(iframe_current > 0)
            {
                iframe_current--;
                GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        else
        {
            game_state_playing = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        //---if an enemy collides with player bullet, destroy bullet and deplete enemy's health
        if(coll.gameObject.tag == "PlayerBullet")
        {
            //Debug.Log("NPC hit by bullet");
            if(coll.GetComponent<BulletScript>() == null)
            {
                //---WILL COME BACK TO
                if(coll.GetComponent<GrenadeScript>().state_current == "state_exploding")
                {
                    if(iframe_current <= 0)
                    {
                        TakeDamage(coll.GetComponent<GrenadeScript>().damage);
                    }
                }
            }
            else
            {
                if(GetComponent<ShieldEnemyScript>() != null)
                {
                    if((GetComponent<ShieldEnemyScript>().current_state == "state_moving") && (iframe_current <= 0))
                    {
                        TakeDamage(coll.GetComponent<BulletScript>().damage);
                    }
                }
                else
                {
                    if(iframe_current <= 0)
                    {
                        TakeDamage(coll.GetComponent<BulletScript>().damage);
                    }
                }
                Object.Destroy(coll.gameObject);
            }
        }
    }
    
    private void OnCollisionStay2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "PlayerBullet")
        {
            Debug.Log("Helloo");
        }
    }
    
    private void TakeDamage(int damage_to_take_)
    {
        //---enemy either takes damage or destorys itself (and maybe srops loot)
        if(enemy_health_current > 0)
        {
            enemy_health_current -=  damage_to_take_;
            iframe_current = iframe_max;
        }
        else
        {
            //---if enemy is killed, remove it from game objects list...
            player_object.GetComponent<PlayerControls>().GenerateNewScore(points_worth);
            //---...decide if a powerup will be dropped...
            GenerateChanceToDropLoot();
            //---...and destory the enemy
            Object.Destroy(this.gameObject);
        }
    }
    private void GenerateChanceToDropLoot()
    {
        float chane_to_drop_loot = Random.Range(0,5);
        float check = 1;
        if(GetComponent<PrizeEnemyScript>() != null)
        {
            check = 5;
        }
        if(chane_to_drop_loot < check)
        {
            Debug.Log("drop a powerup");
            GameObject powerup_obj = Instantiate(powerup_object_prefab, transform.position, Quaternion.identity) as GameObject;
            Debug.Log(powerup_obj);
        }
    }
    
    private void OnDestroy()
    {
        if(spawner_object != null)
        {
            if(part_of_current_wave == true)
            {
                spawner_object.GetComponent<SpawnScript>().enemy_object_list_current_wave.Remove(this.gameObject);
            }
            else
            {
                spawner_object.GetComponent<SpawnScript>().enemy_object_list.Remove(this.gameObject);
            }
        }
    }
    
}