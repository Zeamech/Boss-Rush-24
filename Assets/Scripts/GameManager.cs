using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<MissionTemplate> missionTemplates = new List<MissionTemplate>();

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this);

        instance = this;
    }
}
