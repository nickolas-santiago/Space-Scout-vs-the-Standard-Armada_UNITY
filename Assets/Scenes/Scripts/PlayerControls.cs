using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    //---set the public variables for player speed
    public float speedx = 25;
    public float speedy = 25;
    
    //---set some vars for shooting
    public GameObject projectile;
    GameObject myPJ;
    
    
    
    
    public int cooldown_time = 20;
    private int current_cooldown_time = 0;
    
    
    private int current_weapon;
    
    public class WeaponClass
    {
        public string weapon_name;
        public int weapon_damage;
        public int max_cooldown_time;
        public int current_cooldown_time;
        public WeaponClass(string _weapon_name, int _weapon_damage)
        {
            weapon_name = _weapon_name;
            weapon_damage = _weapon_damage;
        }
    }
    //WeaponClass weapons;
    //public WeaponClass[] weapons;
    
    /* weapons = new WeaponClass()
    {
        weapon_name = "standard";
    }*/
    
    //public WeaponClass[] nn;
    //WeaponClass nn[] = new WeaponClass[0];
    //public fixed nn WeaponClass
    
    public WeaponClass ww = new WeaponClass("ww",4);
    //public WeaponClass ww = new WeaponClass.WeaponClass("ww",4);
    
    //public WeaponClass nn = new WeaponClass();
    
    
    //public WeaponClass nn = new WeaponClass();
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        
        
        Debug.Log(ww);
        
        WeaponClass nn = new WeaponClass("nn",2);
        Debug.Log(nn);
        Debug.Log(nn.weapon_damage);
        
        
        
        
        //nn = new string[] {"mm","kk","ll","oo","pp"};
        
        //WeaponClass mm = new WeaponClass();
        
        
        //nn.weapon_damage = 1;
        
        
        
        //Debug.Log(nn.weapon_damage);
        //Debug.Log(nn);
        //Debug.Log(mm);
        //Debug.Log(nn.Length);
        
        
        //var weapons = new WeaponClass[3];
        /*
        WeaponClass[,] weapons = new WeaponClass[]
        {
            new WeaponClass()
            {
                weapon_name = "standard",
                weapon_damage = 2,
                max_cooldown_time = 50,
                current_cooldown_time = 0
            },
            new WeaponClass()
            {
                weapon_name = "multi-shot",
                weapon_damage = 1,
                max_cooldown_time = 50,
                current_cooldown_time = 0
            }
        };
        */
        //Debug.Log(weapons);
        //Debug.Log(WeaponClass);  //literally game breaking
        current_weapon = 0;
        //Debug.Log(weapons[0].weapon_name);
        //Debug.Log(weapons.Length);
        //Debug.Log(current_weapon);
        //Debug.Log(WeaponClass);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(nn);
        //Debug.Log(mm);
        Debug.Log(ww.weapon_damage);
        //Debug.Log(nn.Length);
        //Debug.Log(nn[3]);
        //Debug.Log(weapons);
        //---set the movement speed
        Vector2 speed = new Vector2(speedx, speedy);

        //---these floats get set when Unity recognizes an input (kayboard, mouse, controller, etc.)
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        
        //---calculate and set the player's new position
        Vector3 movement = new Vector3((speed.x * inputX), (speed.y * inputY), 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);
        
        //---if the space key is being pressed, attempt to shoot
        if(Input.GetKey("space"))
        {
            //---a successful shot happens when cooldown is at 0
            if(current_cooldown_time == 0)
            {
                current_cooldown_time = cooldown_time;
                myPJ = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            }            
        }
        //---HERE we switch weapons
        if(Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log(WeaponClass);
            current_weapon--;
            if(current_weapon <= 0)
            {
                //Debug.Log(weapons);
                //current_weapon = (weapons.Length - 1);
            }
            Debug.Log(current_weapon);
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Hellllooooo");
        }
        
        
        //---cooldown any weapons
        if(current_cooldown_time > 0)
        {
            current_cooldown_time--;
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
    }
}