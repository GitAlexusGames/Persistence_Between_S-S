using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTest : MonoBehaviour
{


    public SaveObject so;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))

        {
            SafeManager.Save(so);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            so = SafeManager.Load();

        }
    }
}




   