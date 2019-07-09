using UnityEngine;
using System.Collections;

public class GameResources : MonoBehaviour
{
    public static GameResources Instance;

    public int CurrentGameLevel { get {
            int x = 1;
            if (PlayerPrefs.HasKey("PlayerLevel"))
            {
                x = PlayerPrefs.GetInt("PlayerLevel");
            }
            return x;


        } internal set {
            PlayerPrefs.SetInt("PlayerLevel", value);

        } }
    public int MaxLevels { get; internal set; }



    // Use this for initialization
    void Awake()
    {
        if (GameResources.Instance != null)
        {
            Debug.Log("Cant have more than one game resources");
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
            CurrentGameLevel = 1;
            
        }
    }
   

    // Update is called once per frame
    void Update()
    {

    }
}
