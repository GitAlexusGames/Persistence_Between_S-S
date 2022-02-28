using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;






public static class SafeManager
{
    
    

    public static void Save(SaveObject so)


    {
       


        string json = JsonUtility.ToJson(so);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);


        


    }

    public static SaveObject Load()

    {
        string fullpath = Application.persistentDataPath + "/savefile.json";
        SaveObject so = new SaveObject();

        if (File.Exists(fullpath))
        {
            string json = File.ReadAllText(fullpath);
            so = JsonUtility.FromJson<SaveObject>(json);
            
        }
        else
        {
            Debug.Log("Save FIle Doesn't exist");
        }
        return so;
    }
}