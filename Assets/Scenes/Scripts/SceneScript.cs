using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneScript : MonoBehaviour
{
    public List<GameObject> healthbar_object_list = new List<GameObject>();
    
    public GameObject player_object;
    private GameObject ui_text_score;
    public int current_score;
    
    // Start is called before the first frame update
    void Start()
    {
        //UI CODE
        //---health
        for(int healthbar_object = 0; healthbar_object < GameObject.FindGameObjectsWithTag("UIImageHealthbar").Length; healthbar_object++)
        {
            healthbar_object_list.Add(GameObject.FindGameObjectsWithTag("UIImageHealthbar")[healthbar_object]);
        }
        for(int healthbar_list_item = 0; healthbar_list_item < healthbar_object_list.Count; healthbar_list_item++)
        {
            Debug.Log(healthbar_object_list[healthbar_list_item]);
            healthbar_object_list[healthbar_list_item].gameObject.SetActive(false);
        }
        
        //---score
        current_score = 0;
        ui_text_score = GameObject.FindGameObjectWithTag("UITextScore");
        ui_text_score.GetComponent<Text>().text = current_score.ToString();
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