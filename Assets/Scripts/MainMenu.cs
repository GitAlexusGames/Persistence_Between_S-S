using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif



public class MainMenu : MonoBehaviour
{

  
    public TextMeshProUGUI user_name;
    private TMP_InputField user_inputField;
    


 public void SetName()
    {
        
    }



    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }




    public void Exit()
    {
#if UNITY_EDITOR
EditorApplication.ExitPlaymode();
#else

        Application.Quit();
#endif
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(5);
        

    }

}
