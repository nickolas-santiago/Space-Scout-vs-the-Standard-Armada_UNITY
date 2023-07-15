using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float force; //---this allows us to easily adjust the speed in unity
    public Vector3 direction;
    private Rigidbody2D bullet_rigidbody; //---used to give the bullet a velocity
    public int damage;
    
    // Start is called before the first frame update
    void Start()
    {
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
        if((transform.position.x > 7) || (transform.position.x < -7) || (transform.position.y > 7) || (transform.position.y < -7))
        {
            Object.Destroy(this.gameObject);
        }
    }
}