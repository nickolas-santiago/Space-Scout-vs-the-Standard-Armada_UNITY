﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShip1_HealthPoint_GunScript : MonoBehaviour
{
    public GameObject boss_ship_container_object;
    
    //---vars for changing direction
    private float shooting_angle_max = (90 + 50);
    private float shooting_angle_min = (90 - 50);
    private float shooting_angle_max_diff_per_frame = 1;
    private float shooting_angle_current;
    private float shooting_angle_desired;
    private bool currently_aiming_at_player = false;
    
    //---vars for shooting
    public GameObject enemy_bullet_object;
    private GameObject player_object;
    
    // Start is called before the first frame update
    void Start()
    {
        player_object = GameObject.FindGameObjectWithTag("Player");
        shooting_angle_desired = GenerateNewDesiredAngle();
        Debug.Log(shooting_angle_desired);
    }

    // Update is called once per frame
    void Update()
    {
        if(boss_ship_container_object.GetComponent<BossShip1_ContainerObjectScript>().game_state_playing == true)
        {
            //---find the gun's current angle of rotation
            shooting_angle_current = gameObject.transform.localEulerAngles.z;
            //---find the angle between the player and the gun
            float angle_towards_player = Mathf.Atan2(transform.position.y - player_object.transform.position.y, transform.position.x - player_object.transform.position.x) * Mathf.Rad2Deg;
            //Debug.Log(Vector3.Distance(transform.position, player_object.transform.position));
            //---if player is close enough, angle_towards_player become the desired angle
            if((angle_towards_player >= shooting_angle_min) && (angle_towards_player <= shooting_angle_max))
            {
                shooting_angle_desired = angle_towards_player;
            }
            //---if not, reach the current desired position or find a new one
            else
            {
                if(Mathf.Approximately(shooting_angle_current, shooting_angle_desired) == false)
                {
                    shooting_angle_desired = shooting_angle_desired;
                }
                else
                {
                    float what_to_do = Random.Range(0,50);
                    if(what_to_do <= 0.05)
                    {
                        //---find a new desired angle
                        shooting_angle_desired = GenerateNewDesiredAngle();
                    }
                }
            }
            
            //---the gun should be working towards the desired angle
            if(Mathf.Abs(shooting_angle_desired - shooting_angle_current) <= shooting_angle_max_diff_per_frame)
            {
                shooting_angle_current = shooting_angle_desired;
            }
            else
            {
                if(shooting_angle_desired > shooting_angle_current)
                {
                    shooting_angle_current += shooting_angle_max_diff_per_frame;
                }
                else
                {
                    shooting_angle_current -= shooting_angle_max_diff_per_frame;
                }
            }
            
            //---update rotation
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, shooting_angle_current));
            
            //--shoot
            if((boss_ship_container_object.GetComponent<BossShip1_ContainerObjectScript>().time_alive % 60) == 0)
            {
                float chance_to_shoot = Random.Range(0,50);
                if(chance_to_shoot < 15)
                {
                    GameObject enemy_bullet = Instantiate(enemy_bullet_object, transform.position, Quaternion.identity) as GameObject;
                    //enemy_bullet.GetComponent<BulletScript>().direction = shooting_angle_current;
                    enemy_bullet.transform.right = transform.up;
                    enemy_bullet.GetComponent<BulletScript>().damage = 1;
                }
            }
            
        }
    }
    
    private float GenerateNewDesiredAngle()
    {
        float random_angle = Random.Range(shooting_angle_min, shooting_angle_max);
        return random_angle;
    }
}