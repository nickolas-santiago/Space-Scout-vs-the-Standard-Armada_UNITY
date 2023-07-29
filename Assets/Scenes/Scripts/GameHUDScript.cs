using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUDScript : MonoBehaviour
{
    public GameObject player_object;
    private int player_current_weapon;
    //UI VAR DECLARATIONS
    //---health declarations
    public GameObject healthbar_panel;
    private float healthbar_panel_xpos_shield_inactive;
    private float healthbar_panel_xpos_shield_active;
    private float healthbar_panel_ypos;
    public List<GameObject> healthbar_object_list = new List<GameObject>();
    //---health declarations -- shield
    public List<GameObject> shieldbar_object_list = new List<GameObject>();
    //---score declarations
    private GameObject ui_text_score;
    public int current_score;
    //---powerup declarations
    public GameObject ui_image_powerup;
    //---weapon declarations
    //---weapon choice declarations
    private float ui_scale_for_current_weapon222;
    public float ui_scale_for_current_weapon;
    public List<GameObject> weaponchoice_image_object_list = new List<GameObject>();
    //---weapon cooldown declarations
    public GameObject ui_image_mask_weapon_cooldown_bar_object;
    public float ui_image_weapon_cooldown_bar_maxheight;
    public float ui_image_weapon_cooldown_bar_width;
    public List<GameObject> weapon_cooldown_image_object_list = new List<GameObject>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        player_object = GameObject.FindGameObjectWithTag("Player");
        player_current_weapon = player_object.GetComponent<PlayerControls>().current_weapon;
        
        //UI ASSIGNMENTS
        //---health assignments
        for(int healthbar_object = 0; healthbar_object < GameObject.FindGameObjectsWithTag("UIImageHealthbar").Length; healthbar_object++)
        {
            healthbar_object_list.Add(GameObject.FindGameObjectsWithTag("UIImageHealthbar")[healthbar_object]);
            
        }
        Debug.Log(healthbar_object_list.Count);
        Debug.Log(healthbar_object_list[2]);
        //---health assignments -- shield
        healthbar_panel_xpos_shield_inactive = -69f;
        healthbar_panel_xpos_shield_active = 5f;
        healthbar_panel_ypos = healthbar_panel.GetComponent<RectTransform>().anchoredPosition.y;
        for(int shieldbar_object = 0; shieldbar_object < GameObject.FindGameObjectsWithTag("UIImageShieldbar").Length; shieldbar_object++)
        {
            shieldbar_object_list.Add(GameObject.FindGameObjectsWithTag("UIImageShieldbar")[shieldbar_object]);
        }
        //---set the shield bars as inactive
        for(int shieldbar_object = 0; shieldbar_object < shieldbar_object_list.Count; shieldbar_object++)
        {
            shieldbar_object_list[shieldbar_object].SetActive(false);
        }
        //---score assignments
        current_score = 0;
        ui_text_score = GameObject.FindGameObjectWithTag("UITextScore");
        ui_text_score.GetComponent<Text>().text = current_score.ToString();
        //---powerup assignments
        //Debug.Log(ui_image_powerup.GetComponent<Image>().sprite);
        //---weapons assignments
        //---weapon choices assignments
        ui_scale_for_current_weapon = 2f;
        for(int weaponchoice_image_object = 0; weaponchoice_image_object < GameObject.FindGameObjectsWithTag("UIImageWeaponchoice").Length; weaponchoice_image_object++)
        {
            weaponchoice_image_object_list.Add(GameObject.FindGameObjectsWithTag("UIImageWeaponchoice")[weaponchoice_image_object]);
        }
        weaponchoice_image_object_list[player_current_weapon].GetComponent<RectTransform>().localScale = new Vector3(ui_scale_for_current_weapon, ui_scale_for_current_weapon, 1f);
        //---weapon cooldowns assignments
        ui_image_weapon_cooldown_bar_maxheight = ui_image_mask_weapon_cooldown_bar_object.GetComponent<RectTransform>().sizeDelta.y;
        ui_image_weapon_cooldown_bar_width = ui_image_mask_weapon_cooldown_bar_object.GetComponent<RectTransform>().sizeDelta.x;
        for(int weapon_cooldown_image_mask_object = 0; weapon_cooldown_image_mask_object < GameObject.FindGameObjectsWithTag("UIImageMaskWeaponCooldown").Length; weapon_cooldown_image_mask_object++)
        {
            weapon_cooldown_image_object_list.Add(GameObject.FindGameObjectsWithTag("UIImageMaskWeaponCooldown")[weapon_cooldown_image_mask_object]);
            weapon_cooldown_image_object_list[weapon_cooldown_image_mask_object].GetComponent<RectTransform>().sizeDelta = new Vector2(ui_image_weapon_cooldown_bar_width, ui_image_weapon_cooldown_bar_maxheight);
        }
        ui_scale_for_current_weapon222 = 222f;
        Debug.Log("HEALTH BAR COUNNNTTT  " + healthbar_object_list.Count);
        Debug.Log("HEALTH  COUNNNTTT  " + ui_scale_for_current_weapon);
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log("HEALTH BAR COUNNNTTT  " + healthbar_object_list.Count);
        //if(current_game_state == "game_state_playing")
        //{
            //---update the weapons' cooldown bars
            for(int player_weapon = 0; player_weapon < player_object.GetComponent<PlayerControls>().weapons_list.Count; player_weapon++)
            {
                float cooldown_percentage = ((float)player_object.GetComponent<PlayerControls>().weapons_list[player_weapon].current_cooldown_time_/(float)player_object.GetComponent<PlayerControls>().weapons_list[player_weapon].max_cooldown_time_);
                weapon_cooldown_image_object_list[player_weapon].GetComponent<RectTransform>().sizeDelta = new Vector2(ui_image_weapon_cooldown_bar_width, (ui_image_weapon_cooldown_bar_maxheight * cooldown_percentage));
            }
        //}
        
        
        //SetNewHealth(4);
    }
    
    //GAME HUD METHODS
    //---health methods
    
    
    
    
    
    public void SetNewHealth(int which_healthbar_)
    {
        
        Debug.Log("THIS SJOULD BE HEALTH BAR COUNT   " + healthbar_object_list.Count);
        //Debug.Log(healthbar_object_list[2]);
       // Debug.Log(which_healthbar_);
        //---if shield is active, remove a shield bar...
        if(which_healthbar_ > 2)
        {
            shieldbar_object_list[(which_healthbar_ - 3)].gameObject.SetActive(false);
            //---if shield is up, move the healthbar panel back
            if(which_healthbar_ == 3)
            {
                healthbar_panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(healthbar_panel_xpos_shield_inactive, healthbar_panel_ypos);
            }
        }
        //...else just remove a healthbar
        else
        {
            healthbar_object_list[which_healthbar_].gameObject.SetActive(false);
        }
    }
    public void UpdateUISetShield()
    {
        //---set all healthbars to active
        for(int healthbar_object = 0; healthbar_object < healthbar_object_list.Count; healthbar_object++)
        {
            healthbar_object_list[healthbar_object].SetActive(true);
        }
        //---move healthbar panel over
        healthbar_panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(healthbar_panel_xpos_shield_active, healthbar_panel_ypos);
        //---set all shieldbars to active
        for(int shieldbar_object = 0; shieldbar_object < shieldbar_object_list.Count; shieldbar_object++)
        {
            shieldbar_object_list[shieldbar_object].SetActive(true);
        }
    }
    //---score methods
    public void GenerateNewScore(int _new_points_)
    {
        Debug.Log("HEALTH BAR COUNNNTTT  " + healthbar_object_list.Count);
        int new_current_score = (current_score + (_new_points_ * player_object.GetComponent<PlayerControls>().score_modifier));
        current_score = new_current_score;
        ui_text_score.GetComponent<Text>().text = current_score.ToString();
    }
    //---powerup methods
    public void UpdateUIPowerup(Sprite powerup_sprite)
    {
        ui_image_powerup.GetComponent<Image>().sprite = powerup_sprite;
        if(powerup_sprite == null)
        {
            ui_image_powerup.SetActive(false);
        }
        else
        {
            ui_image_powerup.SetActive(true);
        }
    }
    //---weapon choice methods
    public void UpdateUIWeaponchoice(int current_weapon_, int previous_weapon_)
    {
        Debug.Log(weaponchoice_image_object_list.Count);
        weaponchoice_image_object_list[current_weapon_].GetComponent<RectTransform>().localScale = new Vector3(ui_scale_for_current_weapon, ui_scale_for_current_weapon, 1f);
        weaponchoice_image_object_list[previous_weapon_].GetComponent<RectTransform>().localScale = new Vector3(1f,1f,1f);
    }
}
