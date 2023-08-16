using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShip1_ContainerObjectScript : MonoBehaviour
{
    public List<GameObject> list_of_boss_ship_health_points = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CheckIfDefeated()
    {
        Debug.Log("check if i have been defeated");
        bool defeated = false;
        foreach (GameObject health_point_ in list_of_boss_ship_health_points)
        {
            //Debug.Log(health_point_.GetComponent<BossShip1_HealthPointScript>().health_current);
            if(health_point_.GetComponent<BossShip1_HealthPointScript>().health_current > 0)
            {
                defeated = false;
                Debug.Log(defeated);
                return;
            }
            else
            {
                defeated = true;
            }
        }
        if(defeated == true)
        {
            //---begin some sort of animarion
        }
    }
}