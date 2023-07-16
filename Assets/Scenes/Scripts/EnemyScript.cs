﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //---set the variables used for movement and rotation
    public GameObject player_object;
    public float force;
    public Vector2 direction;
    public Rigidbody2D enemy_rigidbody;
    
    //---set variables for enemy health
    public int enemy_current_health;
    public int time_alive;
    //---set variables for enemy points
    private GameObject scene_object;
    public int points_worth;
    
    // Start is called before the first frame update
    void Start()
    {
        //---initiate properties for the target (player)
        player_object = GameObject.FindGameObjectWithTag("Player");
        enemy_rigidbody = GetComponent<Rigidbody2D>();
        
        time_alive = 0;
        scene_object = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        time_alive++;
        //---if an enemy's health is 0, delete it
        if(enemy_current_health <= 0)
        {
            Object.Destroy(this.gameObject);
            scene_object.GetComponent<SceneScript>().GenerateNewScore(points_worth);
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