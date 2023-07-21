using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneScript : MonoBehaviour
{
    public GameObject player_object;
    //UI VAR DECLARATIONS
    //---health declarations
    public List<GameObject> healthbar_object_list = new List<GameObject>();
    //---score declarations
    private GameObject ui_text_score;
    public int current_score;
    //---powerup declarations
    public GameObject ui_image_powerup;
    //---weapon declarations
    //---weapon choice declarations
    public List<GameObject> weaponchoice_image_object_list = new List<GameObject>();
    //---weapon cooldown declarations
    public GameObject ui_image_mask_weapon_cooldown_bar_object;
    public float ui_image_weapon_cooldown_bar_maxheight;
    public float ui_image_weapon_cooldown_bar_width;
    public List<GameObject> weapon_cooldown_image_object_list = new List<GameObject>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        //UI ASSIGNMENTS
        //---health assignments
        for(int healthbar_object = 0; healthbar_object < GameObject.FindGameObjectsWithTag("UIImageHealthbar").Length; healthbar_object++)
        {
            healthbar_object_list.Add(GameObject.FindGameObjectsWithTag("UIImageHealthbar")[healthbar_object]);
        }
        //---score assignments
        current_score = 0;
        ui_text_score = GameObject.FindGameObjectWithTag("UITextScore");
        ui_text_score.GetComponent<Text>().text = current_score.ToString();
        //---powerup assignments
        Debug.Log(ui_image_powerup.GetComponent<Image>().sprite);
        //---weapons assignments
        //---weapon choices assignments
        Debug.Log(player_object.GetComponent<PlayerControls>().current_weapon);
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
    
        //Debug.Break();
    }

    // Update is called once per frame
    void Update()
    {
        //---update the weapons' cooldown bars
        for(int player_weapon = 0; player_weapon < player_object.GetComponent<PlayerControls>().weapons_list.Count; player_weapon++)
        {
            Debug.Log(player_weapon);
            float cooldown_percentage = ((float)player_object.GetComponent<PlayerControls>().weapons_list[player_weapon].current_cooldown_time_/(float)player_object.GetComponent<PlayerControls>().weapons_list[player_weapon].max_cooldown_time_);
            weapon_cooldown_image_object_list[player_weapon].GetComponent<RectTransform>().sizeDelta = new Vector2(ui_image_weapon_cooldown_bar_width, (ui_image_weapon_cooldown_bar_maxheight * cooldown_percentage));
        }
    }
    
    //UI METHODS
    //---health methods
    public void SetNewHealth(int _which_healthbar_)
    {
        //Debug.Log(_which_healthbar_);
        healthbar_object_list[_which_healthbar_].gameObject.SetActive(false);
    }
    //---score methods
    public void GenerateNewScore(int _new_points_)
    {
        int new_current_score = (current_score + (_new_points_ * player_object.GetComponent<PlayerControls>().score_modifier));
        current_score = new_current_score;
        ui_text_score.GetComponent<Text>().text = current_score.ToString();
    }
    //---powerup methods
    public void UpdateUIPowerup(Sprite powerup_sprite)
    {
        Debug.Log(powerup_sprite);
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
        weaponchoice_image_object_list[current_weapon_].GetComponent<RectTransform>().localScale = new Vector3(2f,2f,1f);
        weaponchoice_image_object_list[previous_weapon_].GetComponent<RectTransform>().localScale = new Vector3(1f,1f,1f);
    }
}