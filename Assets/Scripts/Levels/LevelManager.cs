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
    public GameObject activeBoss;
    private GameObject activePlayer;

    private void Awake()
    {
        LoadRoom();
    }

    //Holds adventure list (all missions up next on this adventure)
    //Loads the room for the mission, then spawns the boss in
    //removes the loaded mission from the list
    private void LoadRoom()
    {
        activeRoom = Instantiate(missionTemplates[0].LevelRoom, GridObj);
        activeBoss = Instantiate(missionTemplates[0].LevelBoss);

        activePlayer = Instantiate(PlayerObj);
        activePlayer.transform.position = FindObjectOfType<LevelEnd>().startPoint.position;
        activePlayer.GetComponent<HealthBar>().healthBarSlider = SliderPlayerHealth;
        activePlayer.GetComponent<Player>().staminaBarSlider = SliderPlayerStamina;
        FindObjectOfType<CamController>().SetTarget(activePlayer);

        missionTemplates.Remove(missionTemplates[0]);

        FindAnyObjectByType<ScreenEffects>().ScreenWipe(false);
        Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);
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
