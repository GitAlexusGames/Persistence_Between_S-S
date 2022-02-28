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

public class MainManager : MonoBehaviour
{


    public TextMeshProUGUI playerNameJson;
    public TextMeshProUGUI bestScoreJson;
    public TextMeshProUGUI scoreWithJson;
    public int scoreJson;
    public int highScore;


    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public TextMeshProUGUI d_Player_Name;
    public TextMeshProUGUI persistenceText;
    public Text ScoreText;
    
    public GameObject GameOverText;
    public int GameScore = 0;
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    

    // Start is called before the first frame update


    private void Awake()
    {
        

        LoadClicked();
        
        highScore = PlayerPrefs.GetInt("HighScore", GameScore);
        bestScoreJson.text = highScore.ToString();
        //playerNameJson.text = Persistence.instance.nameWithJson.text;



    }




    void Start()
    {

        


        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
                brick.onDestroyed.AddListener(AddScore);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
                
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
               
            }
            
            

           

            
        }
    }


   



    void AddPoint(int point)
    {
        GameScore += point;
        ScoreText.text = $"Score : {GameScore}";


        if (GameScore > PlayerPrefs.GetInt("HighScore", 0))
        {
           
            PlayerPrefs.SetInt("HighScore", GameScore);
            
        }

        


    }

    public void AddScore(int point)
    {
        scoreJson += point;
        
        scoreWithJson.text = scoreJson.ToString();
    }

    public void UpdateHighScore()
    {
        if (scoreJson > highScore)
        {
            highScore = scoreJson;
            bestScoreJson.text = highScore.ToString();
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        else if(highScore > scoreJson)
        {
            Persistence.instance.nameWithJson.ToString();
            SaveCLicked();
        }
            

        
    }


    public void ResetScore()
    {
        scoreJson = 0;
    }

    public void GameOver()
    {
       

        SetJsonPlayerInfo();
        UpdateHighScore();
        m_GameOver = true;
        GameOverText.SetActive(true);
        bestScoreJson.text = highScore.ToString();
       



    }


    public void BackTOMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        bestScoreJson.text = $"Best Score:  " + "  0  ";
        //persistenceText.text = Persistence.instance.display_Player_Name.text;

    }

    public void SetPlayerName()
    {
        if (Persistence.instance != null)
        {
            persistenceText.text = Persistence.instance.display_Player_Name.text;
            d_Player_Name.text = PlayerPrefs.GetInt("HighScore").ToString();
            

        }
    }

    public void SetJsonPlayerInfo()
    {

        playerNameJson.text = Persistence.instance.nameWithJson.text;
        bestScoreJson.text = Persistence.instance.highScoreWithJson.ToString();
    }


    public void SetNameAndScore()
    {

    }


    public void SaveCLicked()
    {



        Persistence.instance.SavePlayer();

    }

    public void LoadClicked()
    {
        
       
            Persistence.instance.LoadPlayer();

        

    }

    public void QuitGame()
    {
        //mainMenuScript = GameObject.Find("NewScene").GetComponent<MainMenu>();

#if UNITY_EDITOR
EditorApplication.ExitPlaymode();
#else
        
        Application.Quit();
#endif
        }
    }
    

    // if(Persistence.instance != null)
    // below you will find the information with regard to the process of persist info between sessions










// 




//   }


















