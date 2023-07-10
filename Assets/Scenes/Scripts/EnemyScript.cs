using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int enemy_max_health;
    private int enemy_current_health;
    
    public int cooldown_time;
    private int current_cooldown_time;
    public GameObject enemy_bullet_object;
    private GameObject enemy_bullet;
    
    // Start is called before the first frame update
    void Start()
    {
        enemy_current_health = enemy_max_health;
        current_cooldown_time = cooldown_time;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_current_health <= 0)
        {
            Object.Destroy(this.gameObject);
        }
        if(current_cooldown_time <= 0)
        {
            enemy_bullet = Instantiate(enemy_bullet_object, transform.position, Quaternion.identity) as GameObject;
            current_cooldown_time = cooldown_time;
        }
        if(current_cooldown_time > 0)
        {
            current_cooldown_time--;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("hello");
        if (coll.gameObject.tag == "PlayerBullet")
        {
            Debug.Log("NPC hit by bullet");
            Object.Destroy(coll.gameObject);
            enemy_current_health--;
        }
    }
}
