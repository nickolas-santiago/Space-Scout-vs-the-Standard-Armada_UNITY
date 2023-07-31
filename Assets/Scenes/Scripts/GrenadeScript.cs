using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    private GameObject scene_object;
    //---standard bullet declarations
    public Vector3 direction;
    private Rigidbody2D grenade_rigidbody; //---used to give the bullet a velocity
    public int damage;
    
    //---for moving and exploding
    public string state_current; //---states: state_moving || state_counting_down || state_exploding
    private int time_to_move_in_frames = 60;
    private int time_to_explosion_in_frames = 60;
    
    // Start is called before the first frame update
    void Start()
    {
        //---add the object to the game's object list
        scene_object =  GameObject.FindGameObjectWithTag("GameController");
        scene_object.GetComponent<SceneScript>().game_objects_list.Add(this.gameObject);
        
        //---set the bullet's rigidbody component
        grenade_rigidbody = GetComponent<Rigidbody2D>();
        //---update the rigidbody's velocity with the caluculated direction and the public force
        //---normalized allows the bullets to travel at the same speed
        grenade_rigidbody.velocity = new Vector2(direction.x, direction.y).normalized * 1;
        
        state_current = "state_moving";
        
        Debug.Log(time_to_move_in_frames);
    }

    // Update is called once per frame
    void Update()
    {
        if(scene_object.GetComponent<SceneScript>().current_game_state == "game_state_playing")
        {
            if(time_to_move_in_frames > 0)
            {
                time_to_move_in_frames--;
            }
            else
            {
                grenade_rigidbody.velocity = new Vector2(0,0);
            }
            /*if(state_current == "state_moving")
            {
                time_to_move_in_frames--;
                if(time_to_move_in_frames <= 0)
                {
                    state_current = "state_counting_down";
                    grenade_rigidbody.velocity = new Vector2(0,0);
                }
            }
            else if(state_current == "state_counting_down")
            {
                time_to_explosion_in_frames--;
                if(time_to_explosion_in_frames <= 0)
                {
                    Explode();
                }
            }
            */
            //Debug.Log(state_current);
        }
        if((transform.position.x > 7) || (transform.position.x < -7) || (transform.position.y > 7) || (transform.position.y < -7))
        {
            Object.Destroy(this.gameObject);
        }
    }
    
    public void Explode()
    {
        if(state_current == "state_moving")
        {
            grenade_rigidbody.velocity = new Vector2(0,0);
        }
        state_current = "state_exploding";
    }
    
    private void OnDestroy()
    {
        GameObject scene_object =  GameObject.FindGameObjectWithTag("GameController");
        scene_object.GetComponent<SceneScript>().game_objects_list.Remove(this.gameObject);
    }
}
