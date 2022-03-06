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
    public int highScores;
    

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
        highScores = PlayerPrefs.GetInt("HighScore");
        playerNameJson.text = Persistence.instance.nameWithJson.text;   //To pass the Info Between Scenes, Name of the player.  
        persistenceText.text = Persistence.instance.nameWithJson.text;  // Name of the player too
        bestScoreJson.text = highScores.ToString();
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

    public void AddScore(int point)    // To track the score 
    {
        scoreJson += point;
        scoreWithJson.text = scoreJson.ToString();


    }

    public void UpdateHighScore()
    {
        if (scoreJson > highScores)
        {
            highScores = scoreJson;
            PlayerPrefs.SetInt("HighScore", scoreJson);  // We need to try to save this info with Json not with PlayerPrefs.   Score and highScore pending
           
        }



    }


    public void ResetScore()
    {   
        scoreJson = 0;
        bestScoreJson.text = highScores.ToString();
    }

    public void GameOver()
    {

        
        
        m_GameOver = true;
        GameOverText.SetActive(true);
       // bestScoreJson.text = highScores.ToString();
       
      


    }


    public void BackTOMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        bestScoreJson.text = $"Best Score:  " + "  0  ";
        persistenceText.text = Persistence.instance.display_Player_Name.text;

    }

    public void SetPlayerName()
    {
        
            persistenceText.text = Persistence.instance.display_Player_Name.text;

            d_Player_Name.text = PlayerPrefs.GetInt("HighScore").ToString();
            

        
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


















