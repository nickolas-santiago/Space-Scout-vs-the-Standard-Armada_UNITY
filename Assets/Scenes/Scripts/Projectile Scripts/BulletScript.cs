using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private GameObject scene_object;
    public float border_limit_x; //---represents a border beyond the game's screen
    public float border_limit_y; //---represents a border beyond the game's screen
    
    public float force; //---this allows us to easily adjust the speed in unity
    public Vector3 direction;
    private Rigidbody2D bullet_rigidbody; //---used to give the bullet a velocity
    public int damage;
    
    // Start is called before the first frame update
    void Start()
    {
        scene_object = GameObject.FindGameObjectWithTag("GameController");
        border_limit_x = (scene_object.GetComponent<SceneScript>().screen_limit_x + 1);
        border_limit_y = (scene_object.GetComponent<SceneScript>().screen_limit_y + 1);
        scene_object.GetComponent<SceneScript>().game_objects_list.Add(this.gameObject);
        
        //---set the bullet's rigidbody component
        bullet_rigidbody = GetComponent<Rigidbody2D>();
        //---update the rigidbody's velocity with the caluculated direction and the public force
        //---normalized allows the bullets to travel at the same speed
        bullet_rigidbody.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }
    
    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(transform.position, direction, Color.red);
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