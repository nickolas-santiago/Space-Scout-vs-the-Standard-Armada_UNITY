using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    /*/ Start is called before the first frame update
    void Start(){}*/
    
    public float speedx;
    public float speedy;
    
    // Update is called once per frame
    void Update()
    {
        Vector2 speed = new Vector2(speedx, speedy);
        Vector3 movement = new Vector3(speedx, speedy, 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);
        if((transform.position.x > 7) || (transform.position.x < -7) || (transform.position.y > 7) || (transform.position.y < -7))
        {
            Object.Destroy(this.gameObject);
        }
    }
}
