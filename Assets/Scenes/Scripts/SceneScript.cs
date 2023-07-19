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
    
    // Start is called before the first frame update
    void Start()
    {
        //UI CODE
        //---health
        for(int healthbar_object = 0; healthbar_object < GameObject.FindGameObjectsWithTag("UIImageHealthbar").Length; healthbar_object++)
        {
            healthbar_object_list.Add(GameObject.FindGameObjectsWithTag("UIImageHealthbar")[healthbar_object]);
        }
        //---score
        current_score = 0;
        ui_text_score = GameObject.FindGameObjectWithTag("UITextScore");
        ui_text_score.GetComponent<Text>().text = current_score.ToString();
        //---powerup
        Debug.Log(ui_image_powerup.GetComponent<Image>().sprite);
    }

    /*/ Update is called once per frame
    void Update(){}*/
    
    //UI METHODS
    public void SetNewHealth(int _which_healthbar_)
    {
        Debug.Log(_which_healthbar_);
        healthbar_object_list[_which_healthbar_].gameObject.SetActive(false);
    }
    public void GenerateNewScore(int _new_points_)
    {
        int new_current_score = (current_score + (_new_points_ * player_object.GetComponent<PlayerControls>().score_modifier));
        current_score = new_current_score;
        ui_text_score.GetComponent<Text>().text = current_score.ToString();
    }
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
    
}