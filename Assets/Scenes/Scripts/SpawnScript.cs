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
    public int max_num_of_enemies;
    private int current_num_of_enemies;   
    public float outer_border; //---represents a border beyond the game's screen
    
    // Start is called before the first frame update
    void Start()
    {
        //scene_script = GetComponent<SceneScript>();
        scene_object = GameObject.FindGameObjectWithTag("GameController");
        current_num_of_enemies = GameObject.FindGameObjectsWithTag("NPC").Length;
    }

    // Update is called once per frame
    void Update()
    {
        delta_time++;
        current_num_of_enemies = GameObject.FindGameObjectsWithTag("NPC").Length;
        if((delta_time % 60 == 0) && (current_num_of_enemies < max_num_of_enemies))
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
    
    //---this function will generate the enemy's position on the screen
    Vector2 GenerateRandomPosition()
    {
        Vector2 enemy_pos;
        int which_side_to_spawn = Random.Range(0,3);
        //---options 0 and 2 represent the top and bottom respectively
        if((which_side_to_spawn == 0) || (which_side_to_spawn == 2))
        {
            enemy_pos.x = Random.Range((outer_border * -1), outer_border);
            if(which_side_to_spawn == 0)
            {
                enemy_pos.y = outer_border;
            }
            else
            {
                enemy_pos.y = (outer_border * -1);
            }
        }
        //---options 1 and 3 represent the right and left respectively
        else
        {
            enemy_pos.y = Random.Range((outer_border * -1), outer_border);
            if(which_side_to_spawn == 1)
            {
                enemy_pos.x = outer_border;
            }
            else
            {
                enemy_pos.x = (outer_border * -1);
            }
        }
        return enemy_pos;
    }
}