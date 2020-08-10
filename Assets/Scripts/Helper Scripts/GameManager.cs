using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameData gameData;

    [HideInInspector]
    public int starScore, score_Count, selected_Index; //< I don't like to do in this way but it's better than put an HideInInspector each line...

    [HideInInspector]
    public bool[] heroes;

    [HideInInspector]
    public bool playSound = true;

    private string gameData_Path = "GameData.dat";

    private void Awake()
    {
        MakeSingleton();

        InitializeGameData();
    }

    private void Start() //< Just a check
    {
        print(Application.persistentDataPath + gameData_Path); //< Check if GameData.dat is correctly created when we start the game

        if (gameData != null) //< If the GameData.dat already exist
            print("Data as been correctly loaded.");
    }

    private void MakeSingleton()
    {
        if (instance)
            Destroy(gameObject);
        else if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void InitializeGameData()
    {
        LoadGameData();

        if (gameData == null) //< Check if we are running the game for the first time, if yes set up initial values
        {
            starScore = 0;
            score_Count = 0;
            selected_Index = 0;

            heroes = new bool[9]; //< Because there is nine heroes (just in case)
            heroes[selected_Index] = true;

            for (int i = 1; i < heroes.Length; i++)
                heroes[i] = false;

            gameData = new GameData();
            gameData.StarScore = starScore;
            gameData.ScoreCount = score_Count;
            gameData.Heroes = heroes;
            gameData.SelectedIndex = selected_Index;

            SaveGameData();
        }
    }

    public void SaveGameData()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Create(Application.persistentDataPath + gameData_Path); //< Actually I don't know if there is a "maybe" better way or more securised way to handle those data

            if (gameData != null)
            {
                gameData.Heroes = heroes;
                gameData.StarScore = starScore;
                gameData.ScoreCount = score_Count;
                gameData.SelectedIndex = selected_Index;

                bf.Serialize(file, gameData);
            }
        }
        catch (Exception) { } //< Not used so it's more cleaner to put it in that way
        finally
        {
            if (file != null)
                file.Close();
        }
    }

    private void LoadGameData()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Open(Application.persistentDataPath + gameData_Path, FileMode.Open);

            gameData = (GameData)bf.Deserialize(file);

            if (gameData != null)
            {
                starScore = gameData.StarScore;
                score_Count = gameData.ScoreCount;
                heroes = gameData.Heroes;
                selected_Index = gameData.SelectedIndex;
            }
        }
        catch (Exception) { } //< Not used so it's more cleaner to put it in that way
        finally
        {
            if (file != null)
                file.Close();
        }
    }
}
