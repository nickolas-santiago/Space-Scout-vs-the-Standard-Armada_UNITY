using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    private GameObject scene_object;
    public float border_limit_x; //---represents a border beyond the game's screen
    public float border_limit_y; //---represents a border beyond the game's screen
    
    //---standard bullet declarations
    public Vector3 direction;
    public int damage;
    
    //---for moving and exploding
    public string state_current; //---states: state_moving || state_counting_down || state_exploding
    private int moving_time_in_frames = 60;
    private int countdown_time_in_frames = 60;
    private int explosion_time_in_frames = 150;
    
    //private Vector2 endpoint;
    private Vector2 offset_per_frame;
    private int end_scale_difference = 60;
    private Vector2 scale_offset_per_frame;
    
    // Start is called before the first frame update
    void Start()
    {
        //---add the object to the game's object list
        scene_object =  GameObject.FindGameObjectWithTag("GameController");
        border_limit_x = (scene_object.GetComponent<SceneScript>().screen_limit_x + 1);
        border_limit_y = (scene_object.GetComponent<SceneScript>().screen_limit_y + 1);
        scene_object.GetComponent<SceneScript>().game_objects_list.Add(this.gameObject);
        
        state_current = "state_moving";
        //---set up distance and speed
        Vector2 pos_starting = transform.position;
        Vector2 direction_normalized = new Vector2(direction.x, direction.y).normalized * 10;
        Vector2 endpoint = new Vector2((pos_starting.x + direction_normalized.x), (pos_starting.y + direction_normalized.y));
        offset_per_frame = new Vector2(((endpoint.x - pos_starting.x)/(float)moving_time_in_frames), ((endpoint.y - pos_starting.y)/moving_time_in_frames));
        //---set up explosion size
        Vector2 scale_starting = transform.localScale;
        Vector2 end_scale = new Vector2((scale_starting.x + end_scale_difference), (scale_starting.y + end_scale_difference));
        scale_offset_per_frame = new Vector2(((end_scale.x - scale_starting.x)/(float)explosion_time_in_frames),((end_scale.y - scale_starting.y)/(float)explosion_time_in_frames));
    }

    // Update is called once per frame
    void Update()
    {
        if(scene_object.GetComponent<SceneScript>().current_game_state == "game_state_playing")
        {
            if(state_current == "state_moving")
            {
                transform.position = new Vector2((transform.position.x + offset_per_frame.x), (transform.position.y + offset_per_frame.y));
                moving_time_in_frames--;
                if(moving_time_in_frames <= 0)
                {
                    state_current = "state_counting_down";
                }
            }
            else if(state_current == "state_counting_down")
            {
                countdown_time_in_frames--;
                if(countdown_time_in_frames <= 0)
                {
                    state_current = "state_exploding";
                }
            }
            else if(state_current == "state_exploding")
            {
                explosion_time_in_frames--;
                transform.localScale = new Vector2((transform.localScale.x + scale_offset_per_frame.x), (transform.localScale.y + scale_offset_per_frame.y));
                if(explosion_time_in_frames <= 0)
                {
                    Object.Destroy(this.gameObject);
                }
            }
        }
        if((transform.position.x < (border_limit_x * -1)) || (transform.position.x > border_limit_x) || (transform.position.y < (border_limit_y * -1)) || (transform.position.y > border_limit_y))
        {
            Object.Destroy(this.gameObject);
        }
    }
    
    private void OnDestroy()
    {
        GameObject scene_object =  GameObject.FindGameObjectWithTag("GameController");
        scene_object.GetComponent<SceneScript>().game_objects_list.Remove(this.gameObject);
    }
}
