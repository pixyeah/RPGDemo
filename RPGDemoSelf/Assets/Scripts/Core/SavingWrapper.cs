using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

public class SavingWrapper : MonoBehaviour
{

    IEnumerator Start()
    {
        yield return GetComponent<SavingSystem>().LoadLastScene(Constants.DEFAULT_SAVE_FILE);
    }

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

    public void Load()
    {
        GetComponent<SavingSystem>().Load(Constants.DEFAULT_SAVE_FILE);
        
    }

    public void Save()
    {
        GetComponent<SavingSystem>().Save(Constants.DEFAULT_SAVE_FILE);
        
    }
}
