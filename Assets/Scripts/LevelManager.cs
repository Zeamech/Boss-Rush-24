using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public List<MissionTemplate> missionTemplates = new List<MissionTemplate>();
    public Transform GridObj;
    public GameObject PlayerObj;

    public Slider SliderPlayerHealth;
    public Slider SliderPlayerStamina;

    private GameObject activeRoom;
    private GameObject activeBoss;
    private GameObject activePlayer;

    private void Awake()
    {
        LoadRoom();
    }

    private void Update()
    {
        if(!activeBoss.activeSelf)
        {
            NextRoom();
        }
    }

    //Holds adventure list (all missions up next on this adventure)
    //Loads the room for the mission, then spawns the boss in
    //removes the loaded mission from the list
    private void LoadRoom()
    {
        activeRoom = Instantiate(missionTemplates[0].LevelRoom, GridObj);
        activeBoss = Instantiate(missionTemplates[0].LevelBoss);
        activeBoss.transform.position = new Vector2(5, 0);

        activePlayer = Instantiate(PlayerObj);
        activePlayer.transform.position = new Vector2(-5, 0);
        activePlayer.GetComponent<HealthBar>().healthBarSlider = SliderPlayerHealth;
        activePlayer.GetComponent<Player>().staminaBarSlider = SliderPlayerStamina;
        FindObjectOfType<CamController>().SetTarget(activePlayer);

        missionTemplates.Remove(missionTemplates[0]);
    }
    //when level is complete unload level then restarts loading sequence for next mission
    private void UnloadRoom()
    {
        Destroy(activeRoom);
        Destroy(activeBoss);
        Destroy(activePlayer);
        activeRoom = null;
        activeBoss = null;
    }

    public void NextRoom()
    {
        UnloadRoom();
        LoadRoom();
    }

}
