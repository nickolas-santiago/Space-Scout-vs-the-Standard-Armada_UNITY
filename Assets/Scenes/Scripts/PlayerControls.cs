using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    //---set the public variables for player health
    public float max_health;
    private float current_health;
    
    //---set the public variables for player speed
    public float speedx = 25;
    public float speedy = 25;
    
    //---set some vars for shooting
    public GameObject projectile;
    GameObject myPJ;
    //---set the variables used for controlling bullet direction and speed
    public Camera main_camera; //---set the camera we want to mouse to be relative to
    private Vector3 mouse_pos;
    private Vector3 aim_direction;
    //---set a custom class for the player's weapons
    private int current_weapon;
    public class WeaponClass
    {
        public string weapon_name;
        public int weapon_damage;
        public int max_cooldown_time;
        public int current_cooldown_time;
        public WeaponClass(string _weapon_name, int _weapon_damage, int _max_cooldown_time, int _current_cooldown_time)
        {
            weapon_name = _weapon_name;
            weapon_damage = _weapon_damage;
            max_cooldown_time = _max_cooldown_time;
            current_cooldown_time = _current_cooldown_time;
        }
    }
    public List<WeaponClass> weapons_list = new List<WeaponClass>();
    public WeaponClass weapon_standard = new WeaponClass("standard", 2, 50, 0);
    public WeaponClass weapon_multishot = new WeaponClass("multishot", 1, 100, 0);
    //---set variables for powerups
    private string current_powerup;
    
    // Start is called before the first frame update
    void Start()
    {
        weapons_list.Add(weapon_standard);
        weapons_list.Add(weapon_multishot);
        current_weapon = 1;
        current_powerup = "";
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
        
        mouse_pos = main_camera.ScreenToWorldPoint(Input.mousePosition);
        aim_direction = (mouse_pos - transform.position);
        if(current_weapon == 0)
        {
            Debug.DrawLine(transform.position, mouse_pos, Color.red);
        }
        if(current_weapon == 1)
        {
            Debug.DrawLine(transform.position, mouse_pos, Color.red);
            Vector3 leftdir = Quaternion.Euler(0f, 0f, -30f) * aim_direction;
            Debug.DrawLine(transform.position, leftdir, Color.green);
            Vector3 rihdir = Quaternion.Euler(0f, 0f, 30f) * aim_direction;
            Debug.DrawLine(transform.position, rihdir, Color.green);
        }
        
        //---if the space key is being pressed, attempt to shoot
        if(Input.GetKey("space"))
        {
            //---a successful shot happens when cooldown is at 0
            if(weapons_list[current_weapon].current_cooldown_time == 0)
            {
                if(weapons_list[current_weapon].weapon_name == "standard")
                {
                    myPJ = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
                    myPJ.GetComponent<BulletScript>().direction = aim_direction;
                    myPJ.GetComponent<BulletScript>().damage = weapons_list[current_weapon].weapon_damage;
                }
                else if(weapons_list[current_weapon].weapon_name == "multishot")
                {
                    for(int shot = -1; shot <= 1; shot++)
                    {
                        float shot_float = ((float)shot * 30f);
                        Vector3 shot_dir = (Quaternion.Euler(0f, 0f, shot_float) * aim_direction);
                        myPJ = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
                        myPJ.GetComponent<BulletScript>().direction = (shot_dir + transform.position);
                        myPJ.GetComponent<BulletScript>().damage = weapons_list[current_weapon].weapon_damage;
                    }
                }
                weapons_list[current_weapon].current_cooldown_time = weapons_list[current_weapon].max_cooldown_time;
            }
        }
        //---HERE we switch weapons
        if(Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log(current_weapon);
            current_weapon--;
            if(current_weapon < 0)
            {
                current_weapon = (weapons_list.Count - 1);
            }
            Debug.Log(weapons_list[current_weapon].weapon_name);
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            //Debug.Log(current_weapon);
            current_weapon++;
            if(current_weapon > (weapons_list.Count - 1))
            {
                current_weapon = 0;
            }
            Debug.Log(weapons_list[current_weapon].weapon_name);
        }
        //---use the Q key to use a powerup
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(current_powerup != "")
            {
                if(current_powerup == "shield")
                {
                    current_health = 6;
                }
                current_powerup = "";
            }
        }
        
        //---cooldown any weapons
        for(int weapon = 0; weapon < weapons_list.Count; weapon++)
        {
            if(weapons_list[weapon].current_cooldown_time > 0)
            {
                weapons_list[weapon].current_cooldown_time--;
            }
        }
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
            current_health -= coll.GetComponent<BulletScript>().damage;
            Debug.Log(current_health);
        }
    }
}