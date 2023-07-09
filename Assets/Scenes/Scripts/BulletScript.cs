using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //---set the variables used for controlling bullet direction and speed
    public Camera main_camera; //---set the camera we want to mouse to be relative to
    public float force; //---this allows us to easily adjust the speed in unity
    private Vector3 mouse_pos;
    private Vector3 direction;
    private Rigidbody2D bullet_rigidbody; //---used to give the bullet a velocity
    
    // Start is called before the first frame update
    void Start()
    {
        //---find the mouse's position relative to the camera's position
        mouse_pos = main_camera.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("screentoworldpoint:  " + mouse_pos);
        //---calculate the bullet's direction 
        direction = (mouse_pos - transform.position);
        //Debug.Log(direction);
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
