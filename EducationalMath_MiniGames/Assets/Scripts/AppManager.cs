using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
    /*
    This class need to be from the begining, in the Main Menu
    */
    private static AppManager instance;
    public static AppManager Instance{get => instance;}

    [Header("The Units")]
    public UnitElementsScriptable[] theUnits;
    [Header("Unit to play")]
    //Unit selected by the player int he main menu
    public UnitElementsScriptable unitSelected;

    [Header("The mini games")]
    public SO_BaseMiniGames[] miniGames;

    void Awake()
    {
        //Singleton
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MiniGamesSequence()
    {
        for (int i = 0; i < miniGames.Length; i++)
        {
            miniGames[i].listed = false;
            miniGames[i].completed = false;
        }

        int totalMiniGames = miniGames.Length;

        while(totalMiniGames > 0)
        {
            int randMiniGame = Random.Range(0, miniGames.Length);
            if(!miniGames[randMiniGame].listed)
            {
                MiniGame_Manager.Instance.miniGamesToPlay.Add(miniGames[randMiniGame]);
                miniGames[randMiniGame].listed = true;
                totalMiniGames--;
            }
        }
        MiniGame_Manager.Instance.curUnit = unitSelected;
    }

    
    public IEnumerator LoadTheScene(int index)
    {
        yield return new WaitForSeconds(1.5f);

        AsyncOperation async = SceneManager.LoadSceneAsync(index);

        while(!async.isDone)
        {
            yield return null;
        }
    }
}
