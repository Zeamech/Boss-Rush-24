using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionUIControler : MonoBehaviour
{
    public MissionTemplate currentTemplater;
    public Image DifficultyDislay;
    public Image IconDislay;

    public Sprite Teir1Spr;
    public Sprite Teir2Spr;
    public Sprite Teir3Spr;
    public Sprite Teir4Spr;

    void Start()
    {
        if(currentTemplater.MissionTeir <= 1)
        {
            DifficultyDislay.sprite = Teir1Spr;
        }
        if (currentTemplater.MissionTeir == 2)
        {
            DifficultyDislay.sprite = Teir2Spr;
        }
        if (currentTemplater.MissionTeir == 3)
        {
            DifficultyDislay.sprite = Teir3Spr;
        }
        if (currentTemplater.MissionTeir >= 4)
        {
            DifficultyDislay.sprite = Teir4Spr;
        }
    }

}
