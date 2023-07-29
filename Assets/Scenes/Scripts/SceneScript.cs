using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneScript : MonoBehaviour
{
    public string current_game_state; //---states: game_state_playing  ||  game_state_menu
    
    public GameObject ui_screen_mainmenu_container;
    public GameObject ui_screen_mainmenu;
    public GameObject ui_screen_controls;
    public GameObject ui_screen_credits;
    public GameObject game_hud;
    public GameObject ui_screen_game_over;
    
    private bool current_ui_screen_is_mainmenu;
    private GameObject current_active_ui_screen;
    private bool is_moving_ui_screen;
    
    public List<GameObject> game_objects_list = new List<GameObject>();
    public GameObject player_object;
    GameObject player_obj;
    public GameObject enemy_spawner_object;
    GameObject enemy_spawner_obj;
    
    // Start is called before the first frame update
    void Start()
    {
        current_game_state = "game_state_menu";
        current_ui_screen_is_mainmenu = true;
        current_active_ui_screen = null;
        is_moving_ui_screen = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(is_moving_ui_screen == true)
        {
            if(current_ui_screen_is_mainmenu == false)
            {
                if(ui_screen_mainmenu.GetComponent<RectTransform>().anchoredPosition.x > -730f)
                {
                    ui_screen_mainmenu.GetComponent<RectTransform>().anchoredPosition += new Vector2(-20f,0f);
                    current_active_ui_screen.GetComponent<RectTransform>().anchoredPosition += new Vector2(-20f,0f);
                }
                else
                {
                    is_moving_ui_screen = false;
                }
            }
            else
            {
                if(ui_screen_mainmenu.GetComponent<RectTransform>().anchoredPosition.x < 0)
                {
                    ui_screen_mainmenu.GetComponent<RectTransform>().anchoredPosition += new Vector2(20f,0f);
                    current_active_ui_screen.GetComponent<RectTransform>().anchoredPosition += new Vector2(20f,0f);
                }
                else
                {
                    is_moving_ui_screen = false;
                    current_active_ui_screen.SetActive(false);
                    current_active_ui_screen = null;
                }
            }
        }
    }
    
    //UI METHODS
    public void UpdateUIScreenControls()
    {
        ui_screen_controls.SetActive(true);
        current_active_ui_screen = ui_screen_controls;
        current_ui_screen_is_mainmenu = false;
        is_moving_ui_screen = true;
    }
    public void UpdateUIScreenCredits()
    {
        ui_screen_credits.SetActive(true);
        current_active_ui_screen = ui_screen_credits;
        current_ui_screen_is_mainmenu = false;
        is_moving_ui_screen = true;
    }
    public void UpdateUIScreenMainMenu()
    {
        current_ui_screen_is_mainmenu = true;
        is_moving_ui_screen = true;
    }
    
    //GAME METHODS
    public void StartGame()
    {
        Debug.Log("start game here");
        //---manage UI and game state
        ui_screen_mainmenu_container.SetActive(false);
        current_game_state = "game_state_playing";
        game_hud.SetActive(true);
        //---instantiate game objects
        //Vector3 spawn_pos = 
        //GameObject player = Instantiate(player_object, Vector3.zero, Quaternion.identity) as GameObject;
        player_obj = Instantiate(player_object, Vector3.zero, Quaternion.identity) as GameObject;
        enemy_spawner_obj = Instantiate(enemy_spawner_object) as GameObject;
        //---add games objects to list
        
    }
    public void EndGame()
    {
        Debug.Log("END GAME HERE");
        current_game_state = "game_state_menu";
        game_hud.SetActive(false);
        Destroy(enemy_spawner_obj);
        Destroy(player_obj);
        for(int game_object_ = (game_objects_list.Count - 1); game_object_ >= 0; game_object_--)
        {
            GameObject obj = game_objects_list[game_object_];
            Debug.Log(obj);
            Destroy(obj);
        }
        game_objects_list.Clear();
        ui_screen_game_over.SetActive(true);
    }
}