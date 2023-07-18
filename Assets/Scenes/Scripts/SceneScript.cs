using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneScript : MonoBehaviour
{
    public GameObject player_object;
    private GameObject ui_text_score;
    public int current_score;
    
    public GameObject healthbar;
    public Transform paneltransform;
    
    public Image hbimage;
    
    
    
    
    public GameObject healthbarbpanel;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        current_score = 0;
        ui_text_score = GameObject.FindGameObjectWithTag("UITextScore");
        ui_text_score.GetComponent<Text>().text = current_score.ToString();
        
        GameObject myhealthbar = Instantiate(healthbar) as GameObject;
        //myhealthbar.transform.position = (0f,0f,0f);
        
        
        //health
        myhealthbar.gameObject.SetActive(true);
        myhealthbar.transform.SetParent(healthbarbpanel.transform);
        Debug.Log(player_object.transform.position);
        
        Debug.Log(myhealthbar.GetComponent<RectTransform>().anchoredPosition);
        myhealthbar.GetComponent<RectTransform>().anchoredPosition = new Vector2(51, -20);
        
        
        Debug.Break();
        
        
        Debug.Log(player_object.transform.position);
        //Debug.Log(player_object.transform.position);
        Debug.Log(myhealthbar);
        Debug.Log(myhealthbar.transform.position);
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