using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //---set the variables used for controlling bullet direction and speed
    public Camera main_camera; //---set the camera we want to mouse to be relative to
    private GameObject player_object;
    public float force; //---this allows us to easily adjust the speed in unity
    private Vector3 mouse_pos;
    private Vector3 direction;
    private Rigidbody2D bullet_rigidbody; //---used to give the bullet a velocity
    
    // Start is called before the first frame update
    void Start()
    {
        //---if the bullet is a playerbullet, the go towards mouse
        if(gameObject.tag == "PlayerBullet")
        {
            //---find the mouse's position relative to the camera's position
            mouse_pos = main_camera.ScreenToWorldPoint(Input.mousePosition);
            //---calculate the bullet's direction 
            direction = (mouse_pos - transform.position);
        }
        if(gameObject.tag == "EnemyBullet")
        {
            player_object = GameObject.FindGameObjectWithTag("Player");
            direction = (player_object.transform.position - transform.position);
        }
        //---set the bullet's rigidbody component
        bullet_rigidbody = GetComponent<Rigidbody2D>();
        //---update the rigidbody's velocity with the caluculated direction and the public force
        //---normalized allows the bullets to travel at the same speed
        bullet_rigidbody.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }
    
    // Update is called once per frame
    void Update()
    {
        if((transform.position.x > 7) || (transform.position.x < -7) || (transform.position.y > 7) || (transform.position.y < -7))
        {
            Object.Destroy(this.gameObject);
        }
    }
}
