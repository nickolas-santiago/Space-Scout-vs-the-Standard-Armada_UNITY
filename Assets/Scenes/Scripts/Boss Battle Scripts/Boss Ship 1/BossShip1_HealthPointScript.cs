using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShip1_HealthPointScript : MonoBehaviour
{
    public GameObject boss_ship_container_object;
    
    //---vars for health
    public int health_current;
    private int iframes_max;
    private int iframes_current;
    
    public Sprite sprite_damaged;
    
    // Start is called before the first frame update
    void Start()
    {
        //---vars for health
        health_current = 10;
        iframes_max = 50;
        iframes_current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(boss_ship_container_object.GetComponent<BossShip1_ContainerObjectScript>().game_state_playing == true)
        {
            //---turn the enemy red if the iframes are on
            if(iframes_current > 0)
            {
                iframes_current--;
                GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
    
    private void TakeDamage(int damage_to_take_)
    {
        health_current -= damage_to_take_;
        if(health_current > 0)
        {
            iframes_current = iframes_max;
        }
        else
        {
            //---switch to defeated sprite
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite_damaged;
            //---check other boss ship parts
            boss_ship_container_object.GetComponent<BossShip1_ContainerObjectScript>().CheckIfDefeated();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "PlayerBullet")
        {
            if(iframes_current <= 0 && health_current > 0)
            {
                TakeDamage(coll.GetComponent<BulletScript>().damage);
            }
            Object.Destroy(coll.gameObject);
        }
    }
}