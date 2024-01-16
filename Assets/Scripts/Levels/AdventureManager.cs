using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureManager : MonoBehaviour
{
    //create 3 adventures
    public GameObject[] AdventurePrefabs;
    public List<MissionTemplate> missionTemplatesPool1 = new List<MissionTemplate>();
    public List<MissionTemplate> missionTemplatesPool2 = new List<MissionTemplate>();
    public List<MissionTemplate> missionTemplatesPool3 = new List<MissionTemplate>();
    public List<MissionTemplate> missionTemplatesPool4 = new List<MissionTemplate>();

    private void Start()
    {
        CreateAdventure(AdventurePrefabs[0], Random.Range(3,6));
        CreateAdventure(AdventurePrefabs[1], Random.Range(5, 9));
        CreateAdventure(AdventurePrefabs[2], Random.Range(7, 11));
    }

    public void CreateAdventure(GameObject Adventure, int MissionCount)
    {
        for (int i = 0; i < MissionCount; i++)
        {
            if (i <= 2)Adventure.GetComponent<AdventureTemplate>().CreateMission(missionTemplatesPool1[Random.Range(0, missionTemplatesPool1.Count)]);
            else if (i <= 5) Adventure.GetComponent<AdventureTemplate>().CreateMission(missionTemplatesPool2[Random.Range(0, missionTemplatesPool2.Count)]);
            else if (i <= 8) Adventure.GetComponent<AdventureTemplate>().CreateMission(missionTemplatesPool3[Random.Range(0, missionTemplatesPool3.Count)]);
            else if (i >= 8) Adventure.GetComponent<AdventureTemplate>().CreateMission(missionTemplatesPool4[Random.Range(0, missionTemplatesPool4.Count)]);
        }
    }

}
