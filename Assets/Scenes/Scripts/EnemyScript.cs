using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int enemy_max_health;
    private int enemy_current_health;
    
    // Start is called before the first frame update
    void Start()
    {
        enemy_current_health = enemy_max_health;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_current_health <= 0)
        {
            Object.Destroy(this.gameObject);
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
