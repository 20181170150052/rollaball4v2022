using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    

    [SerializeField]
    private GameObject playerAndCameraPrefab;

    [SerializeField] private string locationToLoad;
    [SerializeField] private string guiScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Initialization")
        {
            StartGameFromInitialization();
        }
        else
        {
            StartGameFromLevel();
        }
    }

    private void StartGameFromLevel()
    {
        SceneManager.LoadScene(guiScene, LoadSceneMode.Additive);
        
        Vector3 startPosition = GameObject.Find("PlayerStart").transform.position;
            
        Instantiate(playerAndCameraPrefab, startPosition, Quaternion.identity);
    }

    // Start is called before the first frame update
    void StartGameFromInitialization()
    {
        
        SceneManager.LoadScene(guiScene);
        //SceneManager.LoadScene(locationToLoad, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(locationToLoad, LoadSceneMode.Additive).completed += operation =>
        {
            Scene locationScene = default;
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name == locationToLoad)
                {
                    locationScene = SceneManager.GetSceneAt(i);
                    break;
                }
            }
            
            if (locationScene != default) SceneManager.SetActiveScene(locationScene);
            
            Vector3 startPosition = GameObject.Find("PlayerStart").transform.position;
            
            Instantiate(playerAndCameraPrefab, startPosition, Quaternion.identity);
        };
        
    }
    
}