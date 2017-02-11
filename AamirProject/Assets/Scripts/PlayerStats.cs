using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    public int playerLevel = 1;

    private void Start()
    {
        LoadLevel();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Delete))
        {
            DeleteKey("playerLevel");
        }
    }

    private void SaveLevel()
    {
        if(PlayerPrefs.HasKey("playerLevel") == false)
        {
            PlayerPrefs.SetInt("playerLevel", playerLevel);
        }
    }

    private void LoadLevel()
    {
        if(PlayerPrefs.HasKey("playerLevel") == true)
        {
            playerLevel = PlayerPrefs.GetInt("playerLevel");
        }
        else
        {
            Debug.Log("No Key Found! Creating new Key...");
            SaveLevel();
        }
    }

    private void DeleteKey(string keyName)
    {
        PlayerPrefs.DeleteKey(keyName);
        Debug.Log("Deleted Key: " + keyName);
    }

}