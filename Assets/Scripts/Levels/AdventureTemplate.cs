using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureTemplate : MonoBehaviour
{
    public GameObject MissionPrefab;
    public Transform MissionTarget;

    public GameObject MissionConfirmObj;

    private GameManager gameManager;
    private List<GameObject> MissionsList = new List<GameObject>();

    public void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void CreateMission(MissionTemplate mission)
    {
        GameObject NewMis = Instantiate(MissionPrefab, MissionTarget);
        NewMis.GetComponent<MissionUIControler>().currentTemplater = mission; 
        MissionsList.Add(NewMis);
    }

    public void OpenConfirmMenu()
    {
        MissionConfirmObj.GetComponent<ConfirmUIControler>().SelectedAdventure = this;
        MissionConfirmObj.SetActive(true);
    }

    public void LoadMissions()
    {
        for (int i = 0; i < MissionsList.Count; i++)
        {
            gameManager.missionTemplates.Add(MissionsList[i].GetComponent<MissionUIControler>().currentTemplater);
            
        }
    }
    
}
