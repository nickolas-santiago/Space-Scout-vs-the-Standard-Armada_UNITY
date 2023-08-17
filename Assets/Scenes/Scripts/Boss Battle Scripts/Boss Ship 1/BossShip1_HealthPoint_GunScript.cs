using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShip1_HealthPoint_GunScript : MonoBehaviour
{
    public GameObject boss_ship_container_object;
    private GameObject player_object;
    
    // Start is called before the first frame update
    void Start()
    {
        player_object = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //---rotate towards the player
        if(boss_ship_container_object.GetComponent<BossShip1_ContainerObjectScript>().game_state_playing == true)
        {
            float angle_towards_player = Mathf.Atan2(transform.position.y - player_object.transform.position.y, transform.position.x - player_object.transform.position.x) * Mathf.Rad2Deg;
            Debug.Log(angle_towards_player);
            //gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle_towards_player));
            if((angle_towards_player >= (90 - 50)) && (angle_towards_player <= (90 + 50)))
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle_towards_player));
            }
        }
    }
}