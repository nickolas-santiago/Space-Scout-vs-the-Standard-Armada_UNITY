using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneScript : MonoBehaviour
{
    public string current_game_state; //---states: game_state_playing  ||  game_state_menu || game_state_paused
    
    public GameObject ui_screen_mainmenu_container;
    public GameObject ui_screen_mainmenu;
    public GameObject ui_screen_controls;
    public GameObject ui_screen_credits;
    public GameObject game_hud;
    public GameObject ui_screen_game_paused;
    public GameObject ui_screen_game_over;
    
    private bool current_ui_screen_is_mainmenu;
    private GameObject current_active_ui_screen;
    private bool ui_screen_is_moving;
    
    public List<GameObject> game_objects_list = new List<GameObject>();
    public GameObject player_object_prefab;
    GameObject player_obj;
    public GameObject enemy_spawner_object_prefab;
    GameObject enemy_spawner_obj;
    
    // Start is called before the first frame update
    void Start()
    {
        current_game_state = "game_state_menu";
        current_ui_screen_is_mainmenu = true;
        current_active_ui_screen = null;
        ui_screen_is_moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(ui_screen_is_moving == true)
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
                    ui_screen_is_moving = false;
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
                    ui_screen_is_moving = false;
                    current_active_ui_screen.SetActive(false);
                    current_active_ui_screen = null;
                }
            }
        }
    }
    
    //UI BUTTON METHODS
    public void UpdateUIScreen(GameObject ui_screen_)
    {
        Debug.Log(ui_screen_);
        ui_screen_.SetActive(true);
        current_active_ui_screen = ui_screen_;
        current_ui_screen_is_mainmenu = false;
        ui_screen_is_moving = true;
    }
    public void UpdateUIScreenBackToMainMenu()
    {
        if(current_game_state == "game_state_paused")
        {
            Time.timeScale = 1;
            ui_screen_game_paused.SetActive(false);
            EndGame();
        }
        else
        {
            ui_screen_game_over.SetActive(false);
        }
        current_ui_screen_is_mainmenu = true;
        ui_screen_mainmenu_container.SetActive(true);
    }
    public void UpdateUIScreenMainMenu()
    {
        current_ui_screen_is_mainmenu = true;
        ui_screen_is_moving = true;
    }
    
    //GAME METHODS
    public void StartGame()
    {
        Time.timeScale = 1;
        if(current_ui_screen_is_mainmenu == true)
        {
            ui_screen_mainmenu_container.SetActive(false);
            current_ui_screen_is_mainmenu = false;
        }
        else
        {
            ui_screen_game_over.SetActive(false);
        }
        //---manage UI and game state
        current_game_state = "game_state_playing";
        //---instantiate game objects
        player_obj = Instantiate(player_object_prefab, Vector3.zero, Quaternion.identity) as GameObject;
        //enemy_spawner_obj = Instantiate(enemy_spawner_object_prefab) as GameObject;
        //---add games objects to list
        game_objects_list.Add(player_obj);
        game_objects_list.Add(enemy_spawner_obj);
        
        game_hud.SetActive(true);
        game_hud.GetComponent<GameHUDScript>().player_object = player_obj;
        game_hud.GetComponent<GameHUDScript>().UpdateUIResetGameHUD();
    }
    public void PauseGame()
    {
        current_game_state = "game_state_paused";
        Time.timeScale = 0;
        ui_screen_game_paused.SetActive(true);
        game_hud.SetActive(false);
        GameObject pause_screen_score_text = GameObject.FindGameObjectWithTag("UITextScore");
        pause_screen_score_text.GetComponent<Text>().text = player_obj.GetComponent<PlayerControls>().current_score.ToString();
    }
    public void UnpauseGame()
    {
        current_game_state = "game_state_playing";
        Time.timeScale = 1;
        ui_screen_game_paused.SetActive(false);
        game_hud.SetActive(true);
    }
    public void EndGame()
    {
        Debug.Log("END GAME HERE");
        
        //---if the game ended from playing, get rid of the hud, show the game over screen, and set the score
        if(current_game_state == "game_state_playing")
        {
            //---get rid of the HUD
            game_hud.SetActive(false);
            //---show the game over screen
            ui_screen_game_over.SetActive(true);
            //---set the score
            int score_from_round = player_obj.GetComponent<PlayerControls>().current_score;
            GameObject game_over_screen_score_text = GameObject.FindGameObjectWithTag("GameOverScreenTextScore");
            game_over_screen_score_text.GetComponent<Text>().text = score_from_round.ToString();
        }
        //---change the game state
        current_game_state = "game_state_menu";
        //---destroy all game objects, enemies first
        for(int enemy_object = 0; enemy_object < enemy_spawner_obj.GetComponent<SpawnScript>().enemy_object_list.Count; enemy_object++)
        {
            Destroy(enemy_spawner_obj.GetComponent<SpawnScript>().enemy_object_list[enemy_object]);
        }
        for(int game_object_ = (game_objects_list.Count - 1); game_object_ >= 0; game_object_--)
        {
            GameObject obj = game_objects_list[game_object_];
            Destroy(obj);
        }
        game_objects_list.Clear();
    }
}