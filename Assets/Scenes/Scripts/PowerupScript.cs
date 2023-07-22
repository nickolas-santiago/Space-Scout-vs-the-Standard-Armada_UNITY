using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    private int powerup;
    public string powerup_name;
    private int current_lifespan;
    public int max_lifespan;
    private SpriteRenderer powerup_spriterenderer;
    public Sprite powerup_sprite_shield;
    public Sprite powerup_sprite_supercooldown;
    public Sprite powerup_sprite_scoremultiplier;
    public class PowerupClass
    {
        public string powerup_name_;
        public Sprite powerup_sprite_;
        public PowerupClass(string _powerup_name_, Sprite _powerup_sprite_)
        {
            powerup_name_ = _powerup_name_;
            powerup_sprite_ = _powerup_sprite_;
        }
    }
    public List<PowerupClass> powerup_list = new List<PowerupClass>();
    
    // Start is called before the first frame update
    void Start()
    {
        //---initiate new powerups
        PowerupClass powerup_shield = new PowerupClass("shield", powerup_sprite_shield);
        PowerupClass powerup_supercooldown = new PowerupClass("supercooldown", powerup_sprite_supercooldown);
        PowerupClass powerup_scoremultiplier = new PowerupClass("scoremultiplier", powerup_sprite_scoremultiplier);
        //---add new powerups to the powerup list
        powerup_list.Add(powerup_shield);
        powerup_list.Add(powerup_supercooldown);
        powerup_list.Add(powerup_scoremultiplier);
        //---select a random powerup
        //powerup = Random.Range(2, (powerup_list.Count - 1));
        powerup = 0;
        powerup_name = powerup_list[powerup].powerup_name_;
        //---render sprite for powerup
        powerup_spriterenderer = GetComponent<SpriteRenderer>();
        powerup_spriterenderer.sprite = powerup_list[powerup].powerup_sprite_;
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