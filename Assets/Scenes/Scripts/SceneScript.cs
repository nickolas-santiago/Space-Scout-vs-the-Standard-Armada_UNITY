using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour
{
    public GameObject player_object;
    public int current_score;
    
    // Start is called before the first frame update
    void Start()
    {
        current_score = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void GenerateNewScore(int _new_points_)
    {
        int new_current_score = (current_score + (_new_points_ * player_object.GetComponent<PlayerControls>().score_modifier));
        current_score = new_current_score;
        Debug.Log(current_score);
    }
}