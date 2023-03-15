using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

public class SavingWrapper : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
        
        
    }

    private void Load()
    {
        GetComponent<SavingSystem>().Load(Constants.DEFAULT_SAVE_FILE);
        
    }

    private void Save()
    {
        GetComponent<SavingSystem>().Save(Constants.DEFAULT_SAVE_FILE);
        
    }
}
