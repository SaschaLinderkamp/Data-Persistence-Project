using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{

    public InputField playerNameInput;
    public Text playerText;

    private string playerName;
    private int highscore;

    private void Start()
    {
        LoadData();
        UpdateUI();
    }

    public void StartGame()
    {
        playerName = playerNameInput.text;
        SaveData();
        SceneManager.LoadScene("main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void SaveData()
    {
        PlayerData playerData = new PlayerData(playerName, highscore);
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText("playerData.json", json);
    }

    private void LoadData()
    {
        if (File.Exists("playerData.json"))
        {
            string json = File.ReadAllText("playerData.json");
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
            playerName = playerData.playerName;
            highscore = playerData.highscore;
        }
        else
        {
            playerName = "";
            highscore = 0;
        }
    }

    private void UpdateUI()
    {
        playerText.text = "Spielername: " + playerName + " Highscore: " + highscore.ToString();
    }
}

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int highscore;

    public PlayerData(string name, int score)
    {
        playerName = name;
        highscore = score;
    }
}
