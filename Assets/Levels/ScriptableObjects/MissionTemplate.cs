using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionTEmplate", menuName = "Missions/Template")]
public class MissionTemplate : ScriptableObject
{
    public int MissionId;
    public string MissionName;
    public string MissionDescription;

    public GameObject LevelRoom;
    public GameObject LevelBoss;
}
