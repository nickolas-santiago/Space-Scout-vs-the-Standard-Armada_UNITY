using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneScript : MonoBehaviour
{
    public GameObject player_object;
    private GameObject ui_text_score;
    public int current_score;
    
    
    
    
    public GameObject healthbar_object;
    public GameObject healthbarbpanel;
    public List<GameObject> healthbar_object_list = new List<GameObject>();
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        //UI CODE
        //---score
        current_score = 0;
        ui_text_score = GameObject.FindGameObjectWithTag("UITextScore");
        ui_text_score.GetComponent<Text>().text = current_score.ToString();
        
        
        
        //---health
        float ui_panel_healthbar_left_margin = 20.0f;
        float ui_panel_healthbar_left_spacing = 31.4f;
        Debug.Log(player_object.GetComponent<PlayerControls>().current_health);
        for(int health_bar = 0; health_bar < player_object.GetComponent<PlayerControls>().current_health; health_bar++)
        {
            float health_bar_xpos = 0;
            GameObject new_healthbar = Instantiate(healthbar_object) as GameObject;
            new_healthbar.gameObject.SetActive(true);
            new_healthbar.transform.SetParent(healthbarbpanel.transform);
            new_healthbar.GetComponent<RectTransform>().anchoredPosition = new Vector2((ui_panel_healthbar_left_margin + (ui_panel_healthbar_left_spacing * (health_bar))), -20);
            healthbar_object_list.Add(new_healthbar);
        }
        
        //Debug.Break();
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void GenerateNewScore(int _new_points_)
    {
        int new_current_score = (current_score + (_new_points_ * player_object.GetComponent<PlayerControls>().score_modifier));
        current_score = new_current_score;
        ui_text_score.GetComponent<Text>().text = current_score.ToString();
    }
}