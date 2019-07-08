using UnityEngine;
using System.Collections;

public class GameResources : MonoBehaviour
{
    public static GameResources Instance;

    public int CurrentGameLevel { get; internal set; }
  


    // Use this for initialization
    void Awake()
    {
        if (GameResources.Instance != null)
        {
            Debug.Log("Cant have more than one game resources");
            this.gameObject.SetActive(false);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
            CurrentGameLevel = 0;
            if (PlayerPrefs.HasKey("PlayerLevel"))
            {
                CurrentGameLevel = PlayerPrefs.GetInt("PlayerLevel");
            }
        }
    }
   

    // Update is called once per frame
    void Update()
    {

    }
}
