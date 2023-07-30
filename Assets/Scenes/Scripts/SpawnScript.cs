using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    private GameObject scene_object;
    
    public GameObject scene_script;
    public GameObject enemy_object;
    GameObject an_enemy;
    private int delta_time;
    public int num_of_enimies_max;
    private int num_of_enimies_current;   
    public float border_limit; //---represents a border beyond the game's screen
    
    // Start is called before the first frame update
    void Start()
    {
        //scene_script = GetComponent<SceneScript>();
        scene_object = GameObject.FindGameObjectWithTag("GameController");
        num_of_enimies_current = GameObject.FindGameObjectsWithTag("NPC").Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(scene_object.GetComponent<SceneScript>().current_game_state == "game_state_playing")
        {
            delta_time++;
            num_of_enimies_current = GameObject.FindGameObjectsWithTag("NPC").Length;
            if((delta_time % 60 == 0) && (num_of_enimies_current < num_of_enimies_max))
            {
                //---take a chance to spawn an enemy_object
                int chane_to_spawn = Random.Range(0,100);
                {
                    if(chane_to_spawn <= 35)
                    {
                        an_enemy = Instantiate(enemy_object, GenerateRandomPosition(), Quaternion.identity) as GameObject;
                        scene_object.GetComponent<SceneScript>().game_objects_list.Add(an_enemy);
                    }
                }
            }
        }
    }
    
    //---this function will generate the enemy's position on the screen
    Vector2 GenerateRandomPosition()
    {
        Vector2 enemy_pos;
        int which_side_to_spawn = Random.Range(0,3);
        //---options 0 and 2 represent the top and bottom respectively
        if((which_side_to_spawn == 0) || (which_side_to_spawn == 2))
        {
            enemy_pos.x = Random.Range((border_limit * -1), border_limit);
            if(which_side_to_spawn == 0)
            {
                enemy_pos.y = border_limit;
            }
            else
            {
                enemy_pos.y = (border_limit * -1);
            }
        }
        //---options 1 and 3 represent the right and left respectively
        else
        {
            enemy_pos.y = Random.Range((border_limit * -1), border_limit);
            if(which_side_to_spawn == 1)
            {
                enemy_pos.x = border_limit;
            }
            else
            {
                enemy_pos.x = (border_limit * -1);
            }
        }
        return enemy_pos;
    }
}