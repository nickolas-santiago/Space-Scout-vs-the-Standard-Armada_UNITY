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
    
    //---powerup declarations
    public GameObject ui_image_powerup_case;
    public GameObject ui_image_powerup;
    public GameObject ui_image_powerup_glass;
    private int powerup_diff_time_in_frames = 10; //---the time in frames it takes to get fromn one state to the other
    private string powerup_state_current;
    //---declarations for the powerup casing
    private float powerup_casing_scale_max = 1f;
    private float powerup_casing_scale_min = 0.6f;
    private float powerup_casing_scale_diff_per_frame;
    private float powerup_casing_alpha_max = 0.6f;
    private float powerup_casing_alpha_min = 0.3f;
    private float powerup_casing_alpha_diff_per_frame;
    //---declarations for the powerup image
    private float powerup_image_scale_max = 1f;
    private float powerup_image_alpha_max = 1f;
    private float powerup_image_scale_diff_per_frame;
    private float powerup_image_alpha_diff_per_frame;
    
    //---weapon declarations
    //---weapon choice declarations
    public List<GameObject> weaponchoice_image_object_list = new List<GameObject>();
    private int weapon_choice_diff_time_in_frames = 10; //---the time in frames it takes to get fromn one state to the other
    private float weapon_choice_scale_max = 2.5f;
    private float weapon_choice_scale_min = 1.3f;
    private float weapon_choice_scale_diff_per_frame;
    private float weapon_choice_alpha_max = 0.8f;
    private float weapon_choice_alpha_min = 0.3f;
    private float weapon_choice_alpha_diff_per_frame;
    //---weapon cooldown declarations
    public GameObject ui_image_mask_weapon_cooldown_bar_object;
    public float ui_image_weapon_cooldown_bar_maxheight;
    public float ui_image_weapon_cooldown_bar_width;
    public List<GameObject> weapon_cooldown_image_object_list = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        player_current_weapon = player_object.GetComponent<PlayerControls>().current_weapon;
        //Debug.Log(player_object);
        
        //---health assignments
        for(int healthbar_object = 0; healthbar_object < GameObject.FindGameObjectsWithTag("UIImageHealthbar").Length; healthbar_object++)
        {
            healthbar_object_list.Add(GameObject.FindGameObjectsWithTag("UIImageHealthbar")[healthbar_object]);
        }
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
        ui_text_score = GameObject.FindGameObjectWithTag("UITextScore");
        
        //---powerup assignments
        powerup_casing_scale_diff_per_frame = ((powerup_casing_scale_max - powerup_casing_scale_min)/powerup_diff_time_in_frames);
        powerup_casing_alpha_diff_per_frame = ((powerup_casing_alpha_max - powerup_casing_alpha_min)/powerup_diff_time_in_frames);
        powerup_image_scale_diff_per_frame = (powerup_image_scale_max/powerup_diff_time_in_frames);
        powerup_image_alpha_diff_per_frame = (powerup_image_alpha_max/powerup_diff_time_in_frames);
        ui_image_powerup_case.GetComponent<Image>().GetComponent<RectTransform>().localScale = new Vector3(powerup_casing_scale_min, powerup_casing_scale_min, 1f);
        ui_image_powerup.GetComponent<Image>().GetComponent<RectTransform>().localScale = new Vector3(0f, 0f, 1f);
        ui_image_powerup.GetComponent<Image>().GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        ui_image_powerup_glass.GetComponent<Image>().GetComponent<RectTransform>().localScale = new Vector3(powerup_casing_scale_min, powerup_casing_scale_min, 1f);
        
        //---weapons assignments
        //---weapon choices assignments
        weapon_choice_scale_diff_per_frame = ((weapon_choice_scale_max - weapon_choice_scale_min)/weapon_choice_diff_time_in_frames);
        weapon_choice_alpha_diff_per_frame = ((weapon_choice_alpha_max - weapon_choice_alpha_min)/weapon_choice_diff_time_in_frames);
        for(int weaponchoice_image_object = 0; weaponchoice_image_object < GameObject.FindGameObjectsWithTag("UIImageWeaponchoice").Length; weaponchoice_image_object++)
        {
            weaponchoice_image_object_list.Add(GameObject.FindGameObjectsWithTag("UIImageWeaponchoice")[weaponchoice_image_object]);
        }
        
        
        //---weapon cooldowns assignments
        ui_image_weapon_cooldown_bar_maxheight = ui_image_mask_weapon_cooldown_bar_object.GetComponent<RectTransform>().sizeDelta.y;
        ui_image_weapon_cooldown_bar_width = ui_image_mask_weapon_cooldown_bar_object.GetComponent<RectTransform>().sizeDelta.x;
        for(int weapon_cooldown_image_mask_object = 0; weapon_cooldown_image_mask_object < GameObject.FindGameObjectsWithTag("UIImageMaskWeaponCooldown").Length; weapon_cooldown_image_mask_object++)
        {
            weapon_cooldown_image_object_list.Add(GameObject.FindGameObjectsWithTag("UIImageMaskWeaponCooldown")[weapon_cooldown_image_mask_object]);
            weapon_cooldown_image_object_list[weapon_cooldown_image_mask_object].GetComponent<RectTransform>().sizeDelta = new Vector2(ui_image_weapon_cooldown_bar_width, ui_image_weapon_cooldown_bar_maxheight);
        }
        UpdateUIResetGameHUD();
    }

    // Update is called once per frame
    void Update()
    {
        //---updates for powerup section
        if(powerup_state_current == "powerup_indicator_activating")
        {
            //---increase the powerup casing layer's size and visibility when a powerup is picked up
            ui_image_powerup_case.GetComponent<Image>().GetComponent<RectTransform>().localScale = new Vector3((ui_image_powerup_case.GetComponent<Image>().GetComponent<RectTransform>().localScale.x + powerup_casing_scale_diff_per_frame), (ui_image_powerup_case.GetComponent<Image>().GetComponent<RectTransform>().localScale.y + powerup_casing_scale_diff_per_frame), 1f);
            ui_image_powerup_case.GetComponent<Image>().GetComponent<Image>().color = new Color(1f, 1f, 1f, (ui_image_powerup_case.GetComponent<Image>().color.a + powerup_casing_alpha_diff_per_frame));
            //---increase the powerup image's size when a powerup is picked up
            ui_image_powerup.GetComponent<Image>().GetComponent<RectTransform>().localScale = new Vector3((ui_image_powerup.GetComponent<Image>().GetComponent<RectTransform>().localScale.x + powerup_image_scale_diff_per_frame), (ui_image_powerup.GetComponent<Image>().GetComponent<RectTransform>().localScale.y + powerup_image_scale_diff_per_frame), 1f);
            ui_image_powerup.GetComponent<Image>().GetComponent<Image>().color = new Color(1f, 1f, 1f, (ui_image_powerup.GetComponent<Image>().color.a + powerup_image_alpha_diff_per_frame));
            //---increase the powerup glass layer's size and visibility when a powerup is picked up
            ui_image_powerup_glass.GetComponent<Image>().GetComponent<RectTransform>().localScale = new Vector3((ui_image_powerup_glass.GetComponent<Image>().GetComponent<RectTransform>().localScale.x + powerup_casing_scale_diff_per_frame), (ui_image_powerup_glass.GetComponent<Image>().GetComponent<RectTransform>().localScale.y + powerup_casing_scale_diff_per_frame), 1f);
            ui_image_powerup_glass.GetComponent<Image>().GetComponent<Image>().color = new Color(1f, 1f, 1f, (ui_image_powerup_glass.GetComponent<Image>().color.a + powerup_casing_alpha_diff_per_frame));
            
            if(ui_image_powerup_case.GetComponent<Image>().GetComponent<RectTransform>().localScale.x >= powerup_casing_scale_max)
            {
                ui_image_powerup_case.GetComponent<Image>().GetComponent<RectTransform>().localScale = new Vector3(powerup_casing_scale_max, powerup_casing_scale_max, 1f);
                ui_image_powerup_case.GetComponent<Image>().GetComponent<Image>().color = new Color(1f, 1f, 1f, powerup_casing_alpha_max);
                ui_image_powerup_glass.GetComponent<Image>().GetComponent<RectTransform>().localScale = new Vector3(powerup_casing_scale_max, powerup_casing_scale_max, 1f);
                ui_image_powerup_glass.GetComponent<Image>().GetComponent<Image>().color = new Color(1f, 1f, 1f, powerup_casing_alpha_max);
                powerup_state_current = "";
            }
        }
        else if(powerup_state_current == "powerup_indicator_deactivating")
        {
            //---decrease the powerup casing layer's size and visibility when a powerup is used
            ui_image_powerup_case.GetComponent<Image>().GetComponent<RectTransform>().localScale = new Vector3((ui_image_powerup_case.GetComponent<Image>().GetComponent<RectTransform>().localScale.x - powerup_casing_scale_diff_per_frame), (ui_image_powerup_case.GetComponent<Image>().GetComponent<RectTransform>().localScale.y - powerup_casing_scale_diff_per_frame), 1f);
            ui_image_powerup_case.GetComponent<Image>().GetComponent<Image>().color = new Color(1f, 1f, 1f, (ui_image_powerup_case.GetComponent<Image>().color.a - powerup_casing_alpha_diff_per_frame));
            //---decrease the powerup image's size and visibility when a powerup is used
            ui_image_powerup.GetComponent<Image>().GetComponent<RectTransform>().localScale = new Vector3((ui_image_powerup.GetComponent<Image>().GetComponent<RectTransform>().localScale.x - powerup_image_scale_diff_per_frame), (ui_image_powerup.GetComponent<Image>().GetComponent<RectTransform>().localScale.y - powerup_image_scale_diff_per_frame), 1f);
            ui_image_powerup.GetComponent<Image>().GetComponent<Image>().color = new Color(1f, 1f, 1f, (ui_image_powerup.GetComponent<Image>().color.a - powerup_image_alpha_diff_per_frame));
            //---decrease the powerup glass layer's size and visibility when a powerup is used
            ui_image_powerup_glass.GetComponent<Image>().GetComponent<RectTransform>().localScale = new Vector3((ui_image_powerup_glass.GetComponent<Image>().GetComponent<RectTransform>().localScale.x - powerup_casing_scale_diff_per_frame), (ui_image_powerup_glass.GetComponent<Image>().GetComponent<RectTransform>().localScale.y - powerup_casing_scale_diff_per_frame), 1f);
            ui_image_powerup_glass.GetComponent<Image>().GetComponent<Image>().color = new Color(1f, 1f, 1f, (ui_image_powerup_glass.GetComponent<Image>().color.a - powerup_casing_alpha_diff_per_frame));
            
            if(ui_image_powerup_case.GetComponent<Image>().GetComponent<RectTransform>().localScale.x <= powerup_casing_scale_min)
            {
                ui_image_powerup_case.GetComponent<Image>().GetComponent<RectTransform>().localScale = new Vector3(powerup_casing_scale_min, powerup_casing_scale_min, 1f);
                ui_image_powerup_case.GetComponent<Image>().GetComponent<Image>().color = new Color(1f, 1f, 1f, powerup_casing_alpha_min);
                ui_image_powerup_glass.GetComponent<Image>().GetComponent<RectTransform>().localScale = new Vector3(powerup_casing_scale_min, powerup_casing_scale_min, 1f);
                ui_image_powerup_glass.GetComponent<Image>().GetComponent<Image>().color = new Color(1f, 1f, 1f, powerup_casing_alpha_min);
                powerup_state_current = "";
                ui_image_powerup.GetComponent<Image>().sprite = null;
                ui_image_powerup.SetActive(false);
            }
        }
        
        //---updates for weapon choice section
        player_current_weapon = player_object.GetComponent<PlayerControls>().current_weapon;
        for(int weapon_choice = 0; weapon_choice <= (weaponchoice_image_object_list.Count - 1); weapon_choice++)
        {
            if(weapon_choice == player_current_weapon)
            {
                //---find the current weapon selection and make it big/opaque
                if(weaponchoice_image_object_list[weapon_choice].transform.localScale.x < weapon_choice_scale_max)
                {
                    weaponchoice_image_object_list[weapon_choice].transform.localScale = new Vector3((weaponchoice_image_object_list[weapon_choice].transform.localScale.x + weapon_choice_scale_diff_per_frame), (weaponchoice_image_object_list[weapon_choice].transform.localScale.y + weapon_choice_scale_diff_per_frame), 1f);
                    weaponchoice_image_object_list[weapon_choice].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 0f, 0f, (weaponchoice_image_object_list[weapon_choice].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color.a + weapon_choice_alpha_diff_per_frame));
                    weaponchoice_image_object_list[weapon_choice].transform.GetChild(1).GetComponent<Image>().color = new Color(1f, 1f, 1f, (weaponchoice_image_object_list[weapon_choice].transform.GetChild(1).GetComponent<Image>().color.a + weapon_choice_alpha_diff_per_frame));
                }
                else
                {
                    weaponchoice_image_object_list[weapon_choice].transform.localScale = new Vector3(weapon_choice_scale_max, weapon_choice_scale_max, 1f);
                    weaponchoice_image_object_list[weapon_choice].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, weapon_choice_alpha_max);
                    weaponchoice_image_object_list[weapon_choice].transform.GetChild(1).GetComponent<Image>().color = new Color(1f, 1f, 1f, weapon_choice_alpha_max);
                }
            }
            else
            {
                //---find the other weapon selections and make them small/invisible
                if(weaponchoice_image_object_list[weapon_choice].transform.localScale.x > weapon_choice_scale_min)
                {
                    weaponchoice_image_object_list[weapon_choice].transform.localScale = new Vector3((weaponchoice_image_object_list[weapon_choice].transform.localScale.x - weapon_choice_scale_diff_per_frame), (weaponchoice_image_object_list[weapon_choice].transform.localScale.y - weapon_choice_scale_diff_per_frame), 1f);
                    weaponchoice_image_object_list[weapon_choice].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, (weaponchoice_image_object_list[weapon_choice].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color.a - weapon_choice_alpha_diff_per_frame));
                    weaponchoice_image_object_list[weapon_choice].transform.GetChild(1).GetComponent<Image>().color = new Color(1f, 1f, 1f, (weaponchoice_image_object_list[weapon_choice].transform.GetChild(1).GetComponent<Image>().color.a - weapon_choice_alpha_diff_per_frame));
                }
                else
                {
                    weaponchoice_image_object_list[weapon_choice].transform.localScale = new Vector3(weapon_choice_scale_min, weapon_choice_scale_min, 1f);
                    weaponchoice_image_object_list[weapon_choice].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, weapon_choice_alpha_min);
                    weaponchoice_image_object_list[weapon_choice].transform.GetChild(1).GetComponent<Image>().color = new Color(1f, 1f, 1f, weapon_choice_alpha_min);
                }
            }
        }
        //---updates for weapon choice section - for cooldowns
        for(int player_weapon = 0; player_weapon < player_object.GetComponent<PlayerControls>().weapons_list.Count; player_weapon++)
        {
            float cooldown_percentage = ((float)player_object.GetComponent<PlayerControls>().weapons_list[player_weapon].current_cooldown_time_/(float)player_object.GetComponent<PlayerControls>().weapons_list[player_weapon].max_cooldown_time_);
            weapon_cooldown_image_object_list[player_weapon].GetComponent<RectTransform>().sizeDelta = new Vector2(ui_image_weapon_cooldown_bar_width, (ui_image_weapon_cooldown_bar_maxheight * cooldown_percentage));
        }
    }
    
    public void UpdateUIResetGameHUD()
    {
        //---reset healthbars
        for(int health_bar = 0; health_bar < healthbar_object_list.Count; health_bar++)
        {
            healthbar_object_list[health_bar].SetActive(true);
        }
        //---NEED TO RESET HEALTHBAR PANEL HERE!!! --- //
        //---reset shieldbars
        for(int shield_bar = 0; shield_bar < shieldbar_object_list.Count; shield_bar++)
        {
            shieldbar_object_list[shield_bar].SetActive(false);
        }
        //---reset UI score
        if(ui_text_score != null)
        {
            UpdateUIScore(player_object.GetComponent<PlayerControls>().current_score);
        }
        //---reset powerup image
        ui_image_powerup_case.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.3f);
        if(player_object.GetComponent<PlayerControls>().powerup_current == "")
        {
            UpdateUIPowerup(null);
        }
        //---reset weapon choices
        for(int player_weapon_ = 0; player_weapon_ < weaponchoice_image_object_list.Count; player_weapon_++)
        {
            if(player_weapon_ == player_current_weapon)
            {weaponchoice_image_object_list[player_weapon_].transform.localScale = new Vector3(weapon_choice_scale_max, weapon_choice_scale_max, 1f);
                weaponchoice_image_object_list[player_weapon_].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, weapon_choice_alpha_max);
                weaponchoice_image_object_list[player_weapon_].transform.GetChild(1).GetComponent<Image>().color = new Color(1f, 1f, 1f, weapon_choice_alpha_max);
            }
            else
            {
                weaponchoice_image_object_list[player_weapon_].transform.localScale = new Vector3(weapon_choice_scale_min, weapon_choice_scale_min, 1f);
                weaponchoice_image_object_list[player_weapon_].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, weapon_choice_alpha_min);
                weaponchoice_image_object_list[player_weapon_].transform.GetChild(1).GetComponent<Image>().color = new Color(1f, 1f, 1f, weapon_choice_alpha_min);
            }
        }
        
    }
    
    public void SetNewHealth(int which_healthbar_)
    {
        //---if shield is active, remove a shield bar...
        if(which_healthbar_ > 2)
        {
            shieldbar_object_list[(which_healthbar_ - 3)].gameObject.SetActive(false);
            //---if shield is up, move the healthbar panel back
            if(which_healthbar_ == 3)
            {
                //---move healthbar panel over
                //healthbar_panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(healthbar_panel_xpos_shield_inactive, healthbar_panel_ypos);
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
        //healthbar_panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(healthbar_panel_xpos_shield_active, healthbar_panel_ypos);
        //---set all shieldbars to active
        for(int shieldbar_object = 0; shieldbar_object < shieldbar_object_list.Count; shieldbar_object++)
        {
            shieldbar_object_list[shieldbar_object].SetActive(true);
        }
    }
    //---score methods
    public void UpdateUIScore(int new_score_)
    {
        ui_text_score.GetComponent<Text>().text = new_score_.ToString();
    }
    //---powerup methods
    public void UpdateUIPowerup(Sprite powerup_sprite)
    {
        if(powerup_sprite == null)
        {
            powerup_state_current = "powerup_indicator_deactivating";
        }
        else
        {
            powerup_state_current = "powerup_indicator_activating";
            ui_image_powerup.GetComponent<Image>().sprite = powerup_sprite;
            ui_image_powerup.SetActive(true);
        }
    }
}
