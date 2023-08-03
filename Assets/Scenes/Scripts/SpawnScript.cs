using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    private GameObject scene_object;
    public GameObject scene_script;
    public List<GameObject> enemy_object_list = new List<GameObject>();
    
    
    public GameObject standard_enemy_object_prefab;
    public GameObject bomber_enemy_object_prefab;
    public GameObject tank_enemy_object_prefab;
    public GameObject shield_enemy_object_prefab;
    public GameObject prize_enemy_object_prefab;
    private List<GameObject> enemy_objects_list_all_types = new List<GameObject>();
    
    private int delta_time;
    public int num_of_enimies_max;  
    public float border_limit; //---represents a border beyond the game's screen
    
    
    private int[] wave0 = {0, 0, 0, 0};
    public class WaveClass
    {
        public int pNTSNW = 75;
    }
    
    private int[][] jagged_array = new int[2][];
    
    
    
    private int wave_current = 0;
    private int wave_size_max;
    private int percentage_needed_to_spawn_next_wave;
    public List<GameObject> enemy_object_list_current_wave = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        scene_object = GameObject.FindGameObjectWithTag("GameController");
        
        enemy_objects_list_all_types.Add(standard_enemy_object_prefab);
        enemy_objects_list_all_types.Add(bomber_enemy_object_prefab);
        enemy_objects_list_all_types.Add(tank_enemy_object_prefab);
        enemy_objects_list_all_types.Add(shield_enemy_object_prefab);
        enemy_objects_list_all_types.Add(prize_enemy_object_prefab);
        
        jagged_array[0] = new int[]{1, 2, 0};
        jagged_array[1] = new int[]{0, 1, 1, 0, 0};
        Debug.Log(jagged_array[0][0]);
        Debug.Log(jagged_array[0][1]);
        for(int nnn = 0; nnn < jagged_array.Length; nnn++)
        {
            Debug.Log(jagged_array[nnn]);
        }
        for(int nnn = 0; nnn < jagged_array[0].Length; nnn++)
        {
            Debug.Log(jagged_array[0][nnn]);
            Debug.Log(enemy_objects_list_all_types[jagged_array[0][nnn]]);
        }
        GenerateWave();
    }

    // Update is called once per frame
    void Update()
    {
        if(scene_object.GetComponent<SceneScript>().current_game_state == "game_state_playing")
        {
            delta_time++;
            if((delta_time % 60 == 0) && (enemy_object_list.Count < num_of_enimies_max))
            {
                //---take a chance to spawn an enemy_object
                /*int chane_to_spawn = Random.Range(0,100);
                {
                    if(chane_to_spawn <= 35)
                    {
                        GameObject an_enemy = Instantiate(GenerateRandomEnemy(), GenerateRandomPosition(), Quaternion.identity) as GameObject;
                        enemy_object_list.Add(an_enemy);
                    }
                }
                */
            }
            
            //---WAVE TYPE A
            /*if(enemy_object_list_current_wave.Count <= 1)
            {
                Debug.Log("spawn next wave");
            }*/
        }
    }
    
    GameObject GenerateRandomEnemy()
    {
        float random = Random.Range(0,100);
        if(random <= 40)
        {
            return standard_enemy_object_prefab;
        }
        else if(random <= 59.9f)
        {
            return bomber_enemy_object_prefab;
        }
        else if(random <= 75)
        {
            return tank_enemy_object_prefab;
        }
        else if(random <= 90)
        {
            return shield_enemy_object_prefab;
        }
        else
        {
            return prize_enemy_object_prefab;
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
    
    
    private void GenerateWave()
    {
        /*for(int enemy_to_spawn = 0; enemy_to_spawn < enemies_to_spawn_references.Length; enemy_to_spawn++)
        {
           // GameObject an_enemy = Instantiate(enemy_objects_list_all_types[enemies_to_spawn_references[enemy_to_spawn]], GenerateRandomPosition(), Quaternion.identity) as GameObject;
            //enemy_object_list_current_wave.Add(an_enemy);
        }
        wave_size_max = enemies_to_spawn_references.Length;
        */
        for(int nn = 0; nn < jagged_array[wave_current].Length; nn++)
        {
            GameObject an_enemy = Instantiate(enemy_objects_list_all_types[jagged_array[wave_current][nn]], GenerateRandomPosition(), Quaternion.identity) as GameObject;
            enemy_object_list_current_wave.Add(an_enemy);
        }
    }
    
}