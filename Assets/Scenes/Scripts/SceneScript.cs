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
    //---weapon choice declarations
    public List<GameObject> weaponchoice_image_object_list = new List<GameObject>();
    
    
    public GameObject nnn;
    public float hhh_max;
    
    
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
        Debug.Log(player_object.GetComponent<PlayerControls>().current_weapon);
        for(int weaponchoice_image_object = 0; weaponchoice_image_object < GameObject.FindGameObjectsWithTag("UIImageWeaponchoice").Length; weaponchoice_image_object++)
        {
            weaponchoice_image_object_list.Add(GameObject.FindGameObjectsWithTag("UIImageWeaponchoice")[weaponchoice_image_object]);
        }
        //Debug.Log(weaponchoice_image_object_list[1]);
        //Debug.Log(weaponchoice_image_object_list[1].GetComponent<RectTransform>().localScale);
        //weaponchoice_image_object_list[player_object.GetComponent<PlayerControls>().current_weapon].GetComponent<RectTransform>().localScale = new Vector3(2f,2f,1f);
        
        
        
        /*
        Debug.Log(nnn.GetComponent<RectTransform>().sizeDelta.y * 0.5);
        float www = (nnn.GetComponent<RectTransform>().sizeDelta.x * 0.5f);
        float hhh = (nnn.GetComponent<RectTransform>().sizeDelta.y * 0.5f);
        nnn.GetComponent<RectTransform>().sizeDelta = new Vector2(www, hhh);
        Debug.Log(nnn.GetComponent<RectTransform>().sizeDelta);
        */
        
        float weaponchoice_cooldown_image_width = nnn.GetComponent<RectTransform>().sizeDelta.x;
        float weaponchoice_cooldown_image_height = (nnn.GetComponent<RectTransform>().sizeDelta.y * player_object.GetComponent<PlayerControls>().weapons_list[0].current_cooldown_time_);
        hhh_max = nnn.GetComponent<RectTransform>().sizeDelta.y;
        nnn.GetComponent<RectTransform>().sizeDelta = new Vector2(nnn.GetComponent<RectTransform>().sizeDelta.x, weaponchoice_cooldown_image_height);
        
        
        Debug.Log(hhh_max);
        //Debug.Log(player_object.GetComponent<PlayerControls>().weapons_list[0].current_cooldown_time_);

        //Debug.Break();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(hhh_max);
        //Debug.Log(hhh_max );
        //11Debug.Log((float)player_object.GetComponent<PlayerControls>().weapons_list[0].current_cooldown_time_/(float)player_object.GetComponent<PlayerControls>().weapons_list[0].max_cooldown_time_);
        
        //Debug.Log((float)player_object.GetComponent<PlayerControls>().weapons_list[0].current_cooldown_time_);
        
        float jjj = ((float)player_object.GetComponent<PlayerControls>().weapons_list[0].current_cooldown_time_/(float)player_object.GetComponent<PlayerControls>().weapons_list[0].max_cooldown_time_);
        
        
        
        
        float weaponchoice_cooldown_image_height = (nnn.GetComponent<RectTransform>().sizeDelta.y * player_object.GetComponent<PlayerControls>().weapons_list[0].current_cooldown_time_);
        //nnn.GetComponent<RectTransform>().sizeDelta = new Vector2(nnn.GetComponent<RectTransform>().sizeDelta.x, hhh_max);
        nnn.GetComponent<RectTransform>().sizeDelta = new Vector2(nnn.GetComponent<RectTransform>().sizeDelta.x, (hhh_max * jjj));
        //Debug.Log(hhh_max);
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
        /*
        weaponchoice_image_object_list[current_weapon_].GetComponent<RectTransform>().localScale = new Vector3(2f,2f,1f);
        weaponchoice_image_object_list[previous_weapon_].GetComponent<RectTransform>().localScale = new Vector3(1f,1f,1f);
        */
    }
}