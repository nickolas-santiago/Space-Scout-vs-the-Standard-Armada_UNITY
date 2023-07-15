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
    public Vector3 direction;
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
            Debug.Log("mouse:" + mouse_pos + " -  pivot:" + transform.position);
            Debug.Log(direction);
            
            
            float pivotangle = 90;
            float pivotangleRads = pivotangle * Mathf.Deg2Rad;
            Debug.Log(pivotangleRads);
            //Vector3 new_direction = Quaternion.Euler(0f, 0f, pivotangleRads) * direction;
            //Vector3 new_direction = Quaternion.Euler(pivotangleRads, pivotangleRads, 0f) * direction;
            //Vector3 new_direction = Quaternion.Euler(0f, pivotangleRads, 0f) * direction;
            //Debug.Log(new_direction);
            
            
            
            //Vector3 pointN = transform.position + new_direction;
            //Debug.Log(pointN);
            
            //direction = pointN;
            
            //Debug.Log(pointN);
            //direction = (pointN - transform.position);
            
            
            //var rot = Quaternion.LookRotation(direction);
            //rot *= Quaternion.Euler(0, 90, 0);
            //var adjrot = transform.rotation.y + 
            //transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime);
            //var rot = transform.rotation;
            
            //transform.rotation = rot * Quaternion.Euler(0, -90, 0);
            
            //Vector3 rotvect = Quaternion.Euler(0, -90, 0) * Vector3.forward;
            //Debug.Log(direction);
            //Debug.Log(rotvect);
            
            //float angle = Vector3.Angle(direction, transform.forward);
            /*/Debug.Log(angle);
            
            // Get the starting position and target position
            Vector3 startingPosition = transform.position; // Set the starting position
            Vector3 targetPosition = mouse_pos; // Set the target position

            // Calculate the direction vector from the starting position to the target position
            Vector3 directionn = targetPosition - startingPosition;

            // Calculate the perpendicular direction vector by rotating the direction vector 90 degrees
            Vector3 perpendicularDirection = new Vector3(-directionn.y, directionn.x, 0f).normalized;

            // Calculate the new target position by adding the perpendicular direction vector to the starting position
            Vector3 newTargetPosition = startingPosition + perpendicularDirection;

            // Use the new target position for your desired functionality
*/
            //Vector3 new_direction = Quaternion.Euler(0f, 0f, -180f) * direction;
            Vector3 new_direction = Quaternion.Euler(0f, 0f, -30f) * direction;
            Debug.Log(direction);
            Debug.Log(new_direction);
            Debug.Log(new_direction + transform.position);
            direction = new_direction + transform.position;
            
            
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
