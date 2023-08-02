﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject scene_object;
    public GameObject game_hud_object;
    
    //---set the public variables for player health
    public int health_max;
    public int health_current;
    private int iframe_max;
    private int iframe_current;
    private int damage_movement_time_max;
    private int damage_movement_time_current;
    public Vector2 damage_movement_vector_per_frame;
    
    //   scoremultiplier
    public int current_score;
    private int powerup_time_current_scoremultiplier;
    private int powerup_time_max_scoremultiplier;
    public int score_modifier;
    //---set variables for powerups
    public string powerup_current;
    
    //---set the public variables for player speed
    public float speedx = 25;
    public float speedy = 25;
    
    
    //---set some vars for shooting
    public GameObject projectile_prefab;
    public GameObject grenade_prefab;
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
    public WeaponClass weapon_grenade = new WeaponClass("grenade", 1, 40, 0);
    //   supercooldown
    private int supercooldown_time_current;
    private int supercooldown_time_max;
    
    // Start is called before the first frame update
    void Start()
    {
        game_hud_object = GameObject.FindGameObjectWithTag("GameHUD");
        scene_object = GameObject.FindGameObjectWithTag("GameController");
        //---health inits
        health_current = health_max = 3;
        iframe_max = 100;
        iframe_current = 0;
        //---for damage
        damage_movement_time_max = 15;
        //---score inits
        current_score = 0;
        powerup_time_max_scoremultiplier = 7;
        score_modifier = 1;
        //---powerup inits
        powerup_current = "";
        //---weapon inits
        weapons_list.Add(weapon_standard);
        weapons_list.Add(weapon_multishot);
        weapons_list.Add(weapon_grenade);
        current_weapon = 0;
        //---weapon cooldown inits
        supercooldown_time_max = 7;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 speed = new Vector2(speedx, speedy);
        //---these floats get set when Unity recognizes an input (keyboard, mouse, controller, etc.)
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        //---calculate and set the player's new position
        Vector3 movement = new Vector3((speed.x * inputX), (speed.y * inputY), 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);
        
        //---set score modifier
        if(powerup_time_current_scoremultiplier > 0)
        {
            score_modifier = 2;
        }
        else
        {
            score_modifier = 1;
        }
        
        if(scene_object.GetComponent<SceneScript>().current_game_state == "game_state_playing")
        {
            mouse_pos = main_camera.ScreenToWorldPoint(Input.mousePosition);
            aim_direction = (mouse_pos - transform.position);
            if(current_weapon == 0 || current_weapon == 2)
            {
                Debug.DrawLine(transform.position, mouse_pos, Color.red);
            }
            //---update iframes/health
            if(iframe_current > 0)
            {
                iframe_current--;
            }
            //---take pushback from damage
            if(damage_movement_time_current > 0)
            {
                damage_movement_time_current--;
                transform.position = new Vector2((transform.position.x + (damage_movement_vector_per_frame.x/damage_movement_time_max)), (transform.position.y + (damage_movement_vector_per_frame.y/damage_movement_time_max)));
            }
            
            //---use the Q key to use a powerup
            if(Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log(powerup_current);
                if(powerup_current != "")
                {
                    if(powerup_current == "shield")
                    {
                        health_current = 6;
                        Debug.Log(health_current);
                        game_hud_object.GetComponent<GameHUDScript>().UpdateUISetShield();
                    }
                    else if(powerup_current == "supercooldown")
                    {
                        supercooldown_time_current = (supercooldown_time_max * 60);
                        Debug.Log(supercooldown_time_current);
                    }
                    else if(powerup_current == "scoremultiplier")
                    {
                        powerup_time_current_scoremultiplier = (powerup_time_max_scoremultiplier * 60);
                    }
                    game_hud_object.GetComponent<GameHUDScript>().UpdateUIPowerup(null);
                }
            }
            
            //---update weapons
            if(current_weapon == 1) //---DEBUG 
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
                        myPJ = Instantiate(projectile_prefab, transform.position, Quaternion.identity) as GameObject;
                        myPJ.GetComponent<BulletScript>().direction = aim_direction;
                        myPJ.GetComponent<BulletScript>().damage = weapons_list[current_weapon].weapon_damage_;
                    }
                    else if(weapons_list[current_weapon].weapon_name_ == "multishot")
                    {
                        for(int shot = -1; shot <= 1; shot++)
                        {
                            myPJ = Instantiate(projectile_prefab, transform.position, Quaternion.identity) as GameObject;
                            float shot_float = ((float)shot * 30f);
                            Vector3 shot_dir = (Quaternion.Euler(0f, 0f, shot_float) * aim_direction);
                            Vector3 newpoint_pos = (transform.position + shot_dir);
                            myPJ.GetComponent<BulletScript>().direction = (newpoint_pos - transform.position);
                            myPJ.GetComponent<BulletScript>().damage = weapons_list[current_weapon].weapon_damage_;
                        }
                    }
                    else if(weapons_list[current_weapon].weapon_name_ == "grenade")
                    {
                        myPJ = Instantiate(grenade_prefab, transform.position, Quaternion.identity) as GameObject;
                        myPJ.GetComponent<GrenadeScript>().direction = aim_direction;
                        myPJ.GetComponent<GrenadeScript>().damage = weapons_list[current_weapon].weapon_damage_;
                    }
                    if(supercooldown_time_current > 0)
                    {
                        weapons_list[current_weapon].current_cooldown_time_ = Mathf.FloorToInt((float)weapons_list[current_weapon].max_cooldown_time_ * ((float)1/(float)2));
                    }
                    else
                    {
                        weapons_list[current_weapon].current_cooldown_time_ = weapons_list[current_weapon].max_cooldown_time_;
                    }
                }
            }
            //---switch weapons
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
            
            //---cooldown any weapons and powerups
            for(int weapon = 0; weapon < weapons_list.Count; weapon++)
            {
                if(powerup_time_current_scoremultiplier > 0)
                {
                    powerup_time_current_scoremultiplier--;
                }
                if(weapons_list[weapon].current_cooldown_time_ > 0)
                {
                    weapons_list[weapon].current_cooldown_time_--;
                }
                if(supercooldown_time_current > 0)
                {
                    supercooldown_time_current--;
                }
            }
        }
        
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(scene_object.GetComponent<SceneScript>().current_game_state == "game_state_playing")
            {
                scene_object.GetComponent<SceneScript>().PauseGame();
            }
            else
            {
                scene_object.GetComponent<SceneScript>().UnpauseGame();
            }
        }
    }
    
    //---set new health and update the UI
    private void TakeDamage(int damage_to_take_, Vector2 damage_direction_)
    {
        if(health_current > 0)
        {
            //---take damage and update the UI
            health_current -= damage_to_take_;
            game_hud_object.GetComponent<GameHUDScript>().SetNewHealth(health_current);
            //---set iframes
            iframe_current = iframe_max;
            //---set pushback from damage
            damage_movement_vector_per_frame = new Vector2(damage_direction_.x, damage_direction_.y).normalized * 0.35f;
            damage_movement_time_current = damage_movement_time_max;
        }
        else
        {
            scene_object.GetComponent<SceneScript>().EndGame();
        }
    }
    
    //---set new score and update the UI
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
            Debug.Log("hello");
            Debug.Log(coll.gameObject.GetComponent<EnemyScript>().direction);
            if(iframe_current <= 0)
            {
                TakeDamage(1, coll.gameObject.GetComponent<EnemyScript>().direction);
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "EnemyBullet")
        {
            Object.Destroy(coll.gameObject);
            if(iframe_current <= 0)
            {
                TakeDamage(coll.GetComponent<BulletScript>().damage, coll.GetComponent<BulletScript>().direction);
            }
        }
        if (coll.gameObject.tag == "Powerup")
        {
            powerup_current = coll.GetComponent<PowerupScript>().powerup_name;
            game_hud_object.GetComponent<GameHUDScript>().UpdateUIPowerup(coll.GetComponent<SpriteRenderer>().sprite);
            Object.Destroy(coll.gameObject);
            Debug.Log(powerup_current);
        }
    }
}