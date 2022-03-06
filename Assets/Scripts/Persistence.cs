using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class Persistence : MonoBehaviour
{

    
    public static  Persistence instance;
    
    
    public TextMeshProUGUI highScoreWithJson, nameWithJson, scorejson;
    public int highScore;
    public int scoreJson;
    


    public TMP_InputField inputField;
    public TextMeshProUGUI display_Player_Name;
    public TextMeshProUGUI textBack;
   
   


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadPlayer();





    }
    




    private void Start()
    {

        highScoreWithJson.text.ToString();
        
        textBack.text = PlayerPrefs.GetInt("HighScore").ToString();    // puntaje pref en pantalla al iniciar el juego
        highScoreWithJson.text = highScore.ToString();
        scorejson.text = instance.scorejson.ToString();
        highScoreWithJson.text = Persistence.instance.highScoreWithJson.text;
        
    }

    public void SetName()
    {
        display_Player_Name.text = inputField.text;
        

    }
    public void SetNameWithJson()
    {
        nameWithJson.text = inputField.text;
    }





    public void SavePlayer()
    {
        SaveData data = new SaveData();
        data.display_Player_Name = display_Player_Name.text;
        data.nameWithJson = nameWithJson.text;
        data.highScoreWithJson = highScoreWithJson.text;
        data.highScore = highScore;
        data.scorejson = scorejson.text;
        data.scoreJson = scoreJson;
        //Anadida recientemente para testear score points en le menu principal
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

    }

    public void LoadPlayer()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            display_Player_Name.text = data.display_Player_Name;
            nameWithJson.text = data.nameWithJson;
            highScoreWithJson.text = data.highScoreWithJson;
            highScore = data.highScore;
            scoreJson = data.scoreJson;
            scorejson.text = data.scorejson;
        }
    }


    public void SaveNameJson()
    {
        SavePlayer();
    }

    public void LoadNameJson()
    {
        LoadPlayer();
    }

}


[System.Serializable]  // we create a class to save only we want to save not everithing in the Monobehavior Class
public class SaveData
{
    public string display_Player_Name;
    public string highScoreWithJson;
    public string nameWithJson;
    public string scorejson;
    public int highScore;
    public int highScores;
    public int scoreJson;
}
   