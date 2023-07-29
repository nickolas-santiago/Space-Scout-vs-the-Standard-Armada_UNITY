using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject scene_object;
    public GameObject game_hud_object;
    
    //---set the public variables for player health
    public int max_health;
    public int current_health;
    private int iframe_max;
    private int iframe_current;
    
    //---set the public variables for player speed
    public float speedx = 25;
    public float speedy = 25;
    
    //   scoremultiplier
    public int current_score;
    private int current_powerup_time_scoremultiplier;
    private int max_powerup_time_scoremultiplier;
    public int score_modifier;
    //---set variables for powerups
    private string current_powerup;
    
    //---set some vars for shooting
    public GameObject projectile;
    GameObject myPJ;
    //---set the variables used for controlling bullet direction and speed
    public Camera main_camera; //---set the camera we want to mouse to be relative to
    private Vector3 mouse_pos;
    private Vector3 aim_direction;
    //---set a custom class for the player's weapons
    public int current_weapon;
    public class WeaponClass
    {
        public string weapon_name_;
        public int weapon_damage_;
        public int max_cooldown_time_;
        public int current_cooldown_time_;
        public WeaponClass(string _weapon_name_, int _weapon_damage_, int _max_cooldown_time_, int _current_cooldown_time_)
        {
            weapon_name_ = _weapon_name_;
            weapon_damage_ = _weapon_damage_;
            max_cooldown_time_ = _max_cooldown_time_;
            current_cooldown_time_ = _current_cooldown_time_;
        }
    }
    public List<WeaponClass> weapons_list = new List<WeaponClass>();
    public WeaponClass weapon_standard = new WeaponClass("standard", 2, 50, 0);
    public WeaponClass weapon_multishot = new WeaponClass("multishot", 1, 40, 0);
    //   supercooldown
    private int current_supercooldown_time;
    private int max_supercooldown_time;
    
    // Start is called before the first frame update
    void Start()
    {
        game_hud_object = GameObject.FindGameObjectWithTag("GameHUD");
        scene_object = GameObject.FindGameObjectWithTag("GameController");
        //---health inits
        current_health = max_health = 3;
        iframe_max = 50;
        iframe_current = 0;
        //---score inits
        current_score = 0;
        max_powerup_time_scoremultiplier = 7;
        score_modifier = 1;
        //---powerup inits
        current_powerup = "";
        //---weapon inits
        weapons_list.Add(weapon_standard);
        weapons_list.Add(weapon_multishot);
        current_weapon = 1;
        max_supercooldown_time = 7;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 speed = new Vector2(speedx, speedy);
        
        if(iframe_current > 0)
        {
            iframe_current--;
        }

        //---these floats get set when Unity recognizes an input (keyboard, mouse, controller, etc.)
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        
        //---calculate and set the player's new position
        Vector3 movement = new Vector3((speed.x * inputX), (speed.y * inputY), 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);
        
        mouse_pos = main_camera.ScreenToWorldPoint(Input.mousePosition);
        aim_direction = (mouse_pos - transform.position);
        if(current_weapon == 0)
        {
            Debug.DrawLine(transform.position, mouse_pos, Color.red);
        }
        if(current_weapon == 1)
        {
            Debug.DrawLine(transform.position, mouse_pos, Color.red);
            //---positive
            Vector3 newdir_pos = Quaternion.Euler(0f, 0f, 30f) * aim_direction;
            Vector3 newpoint_pos = transform.position + newdir_pos;
            Debug.DrawLine(transform.position, newpoint_pos, Color.green);
            //---negative
            Vector3 newdir_neg = Quaternion.Euler(0f, 0f, -30f) * aim_direction;
            Vector3 newpoint_neg = transform.position + newdir_neg;
            Debug.DrawLine(transform.position, newpoint_neg, Color.yellow);
        }
        
        //---if the space key is being pressed, attempt to shoot
        if(Input.GetKey("space"))
        {
            //---a successful shot happens when cooldown is at 0
            if(weapons_list[current_weapon].current_cooldown_time_ == 0)
            {
                if(weapons_list[current_weapon].weapon_name_ == "standard")
                {
                    myPJ = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
                    myPJ.GetComponent<BulletScript>().direction = aim_direction;
                    myPJ.GetComponent<BulletScript>().damage = weapons_list[current_weapon].weapon_damage_;
                }
                else if(weapons_list[current_weapon].weapon_name_ == "multishot")
                {
                    for(int shot = -1; shot <= 1; shot++)
                    {
                        myPJ = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
                        float shot_float = ((float)shot * 30f);
                        Vector3 shot_dir = (Quaternion.Euler(0f, 0f, shot_float) * aim_direction);
                        Vector3 newpoint_pos = (transform.position + shot_dir);
                        myPJ.GetComponent<BulletScript>().direction = (newpoint_pos - transform.position);
                        myPJ.GetComponent<BulletScript>().damage = weapons_list[current_weapon].weapon_damage_;
                    }
                }
                if(current_supercooldown_time > 0)
                {
                    weapons_list[current_weapon].current_cooldown_time_ = Mathf.FloorToInt((float)weapons_list[current_weapon].max_cooldown_time_ * ((float)1/(float)2));
                }
                else
                {
                    weapons_list[current_weapon].current_cooldown_time_ = weapons_list[current_weapon].max_cooldown_time_;
                }
            }
        }
        //---HERE we switch weapons
        if(Input.GetKeyDown(KeyCode.E))
        {
            int previous_weapon = current_weapon;
            current_weapon--;
            if(current_weapon < 0)
            {
                current_weapon = (weapons_list.Count - 1);
            }
            game_hud_object.GetComponent<GameHUDScript>().UpdateUIWeaponchoice(current_weapon, previous_weapon);
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            int previous_weapon = current_weapon;
            current_weapon++;
            if(current_weapon > (weapons_list.Count - 1))
            {
                current_weapon = 0;
            }
            game_hud_object.GetComponent<GameHUDScript>().UpdateUIWeaponchoice(current_weapon, previous_weapon);
            
        }
        //---use the Q key to use a powerup
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(current_powerup);
            if(current_powerup != "")
            {
                if(current_powerup == "shield")
                {
                    current_health = 6;
                    Debug.Log(current_health);
                    game_hud_object.GetComponent<GameHUDScript>().UpdateUISetShield();
                }
                else if(current_powerup == "supercooldown")
                {
                    current_supercooldown_time = (max_supercooldown_time * 60);
                    Debug.Log(current_supercooldown_time);
                }
                else if(current_powerup == "scoremultiplier")
                {
                    current_powerup_time_scoremultiplier = (max_powerup_time_scoremultiplier * 60);
                }
                game_hud_object.GetComponent<GameHUDScript>().UpdateUIPowerup(null);
            }
        }
        
        //---cooldown any weapons and powerups
        for(int weapon = 0; weapon < weapons_list.Count; weapon++)
        {
            if(weapons_list[weapon].current_cooldown_time_ > 0)
            {
                weapons_list[weapon].current_cooldown_time_--;
            }
            if(current_supercooldown_time > 0)
            {
                current_supercooldown_time--;
            }
            if(current_powerup_time_scoremultiplier > 0)
            {
                current_powerup_time_scoremultiplier--;
            }
        }
        //---set score modifier
        if(current_powerup_time_scoremultiplier > 0)
        {
            score_modifier = 2;
        }
        else
        {
            score_modifier = 1;
        }
    }
    
    public void GenerateNewScore(int new_points_)
    {
        int new_current_score = (current_score + (new_points_ * score_modifier));
        current_score = new_current_score;
        game_hud_object.GetComponent<GameHUDScript>().UpdateUIScore(current_score);
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
        if (coll.gameObject.tag == "EnemyBullet")
        {
            Object.Destroy(coll.gameObject);
            if(iframe_current <= 0)
            {
                TakeDamage(coll.GetComponent<BulletScript>().damage);
            }
            //Debug.Log(current_health);
        }
        if (coll.gameObject.tag == "Powerup")
        {
            current_powerup = coll.GetComponent<PowerupScript>().powerup_name;
            game_hud_object.GetComponent<GameHUDScript>().UpdateUIPowerup(coll.GetComponent<SpriteRenderer>().sprite);
            Object.Destroy(coll.gameObject);
            Debug.Log(current_powerup);
        }
    }
    
    //---set new health and update the UI
    private void TakeDamage(int damage_to_take_)
    {
        if(current_health > 0)
        {
            current_health -= damage_to_take_;
            game_hud_object.GetComponent<GameHUDScript>().SetNewHealth(current_health);
            iframe_current = iframe_max;
        }
        else
        {
            scene_object.GetComponent<SceneScript>().EndGame();
        }
    }
}