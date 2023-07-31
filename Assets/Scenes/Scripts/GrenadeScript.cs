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
    
    private Vector2 endpoint;
    private Vector2 offset_per_frame;
    
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
        //grenade_rigidbody.velocity = new Vector2(direction.x, direction.y).normalized * 1;
        
        
        state_current = "state_moving";
        
        //Debug.Log(time_to_move_in_frames);
        
        Vector2 pos_starting = transform.position;
        Vector2 nnn = new Vector2(direction.x, direction.y).normalized * 1;
        Debug.Log(nnn);
        endpoint = new Vector2((pos_starting.x + nnn.x), (pos_starting.y + nnn.y));
        Debug.Log(endpoint);
        offset_per_frame = new Vector2(((endpoint.x - pos_starting.x)/(float)time_to_move_in_frames), ((endpoint.y - pos_starting.y)/time_to_move_in_frames));
        Debug.Log((pos_starting.x + nnn.x)/60f);
        Debug.Log((pos_starting.y + nnn.y)/60f);
        //offset_per_frame = new Vector2((pos_starting.x + nnn.x), (pos_starting.y + nnn.y));
        /*
        Debug.Log(offset_per_frame);
        Debug.Log(offset_per_frame/60);
        Debug.Log(offset_per_frame/60);
        */
        
        //Vector2 vvv =  new Vector2(1,0);
        //Vector2 vvv =  new Vector2((pos_starting.x + nnn.x) * 0.60f,0); --- gives a number at least
        Vector2 vvv =  new Vector2((pos_starting.x + nnn.x)/(float)60.0,0); 
        //vvv.x = (pos_starting.x + nnn.x)/60f;
        Debug.Log(vvv);
        Debug.Log(new Vector2(1,1)/1);
    }

    // Update is called once per frame
    void Update()
    {
        if(scene_object.GetComponent<SceneScript>().current_game_state == "game_state_playing")
        {
            if(time_to_move_in_frames > 0)
            {
                transform.position = new Vector2((transform.position.x + offset_per_frame.x), (transform.position.y + offset_per_frame.y));
                time_to_move_in_frames--;
            }
            
            
            
            /*if(time_to_move_in_frames > 0)
            {
                time_to_move_in_frames--;
            }
            else
            {
                grenade_rigidbody.velocity = new Vector2(0,0);
            }*/
            
            
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
