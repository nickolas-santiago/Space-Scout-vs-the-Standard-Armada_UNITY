using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    private int powerup;
    private int current_lifespan;
    public int max_lifespan;
    private SpriteRenderer powerup_spriterenderer;
    public Sprite powerup_sprite_shield;
    public class PowerupClass
    {
        public string powerup_name;
        public Sprite powerup_sprite;
        public PowerupClass(string _powerup_name, Sprite _powerup_sprite)
        {
            powerup_name = _powerup_name;
            powerup_sprite = _powerup_sprite;
        }
    }
    public List<PowerupClass> powerup_list = new List<PowerupClass>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        PowerupClass powerup_shield = new PowerupClass("shield", powerup_sprite_shield);
        powerup_list.Add(powerup_shield);
        powerup = Random.Range(0, (powerup_list.Count - 1));
        powerup_spriterenderer = GetComponent<SpriteRenderer>();
        powerup_spriterenderer.sprite = powerup_list[powerup].powerup_sprite;
        
        
        
        
        Debug.Log(powerup_list.Count);
        Debug.Log(powerup_list);
        Debug.Log(powerup);
        Debug.Log(powerup_list[powerup].powerup_sprite);
    }

    // Update is called once per frame
    void Update()
    {
        current_lifespan++;
        if(current_lifespan >= (60 * max_lifespan))
        {
            Object.Destroy(this.gameObject);
        }
    }
}