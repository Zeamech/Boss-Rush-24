using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmUIControler : MonoBehaviour
{
    public AdventureTemplate SelectedAdventure;


    public void StartRun()
    {
        SelectedAdventure.LoadMissions();
        SceneManager.LoadScene(1);
    }

    public void CancelRun()
    {
        SelectedAdventure = null;
        gameObject.SetActive(false);
    }
}
