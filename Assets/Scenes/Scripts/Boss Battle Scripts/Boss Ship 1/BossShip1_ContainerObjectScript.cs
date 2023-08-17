using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShip1_ContainerObjectScript : MonoBehaviour
{
    private GameObject scene_object;
    public bool game_state_playing;
    
    public List<GameObject> list_of_boss_ship_health_points = new List<GameObject>();
    public int time_alive;
    
    // Start is called before the first frame update
    void Start()
    {
        scene_object = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        //---let the other ship parts know what state the game is in
        if(scene_object.GetComponent<SceneScript>().current_game_state == "game_state_playing")
        {
            game_state_playing = true;
            time_alive++;
        }
        else
        {
            game_state_playing = false;
        }
    }
    
    public void CheckIfDefeated()
    {
        Debug.Log("check if i have been defeated");
        bool defeated = false;
        foreach (GameObject health_point_ in list_of_boss_ship_health_points)
        {
            //Debug.Log(health_point_.GetComponent<BossShip1_HealthPointScript>().health_current);
            if(health_point_.GetComponent<BossShip1_HealthPointScript>().health_current > 0)
            {
                defeated = false;
                Debug.Log(defeated);
                return;
            }
            else
            {
                defeated = true;
            }
        }
        if(defeated == true)
        {
            //---begin some sort of animarion
            Object.Destroy(this.gameObject);
        }
    }
}