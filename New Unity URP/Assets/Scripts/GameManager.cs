using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerAndCameraPrefab;

    [SerializeField] private string locationToLoad;
    [SerializeField] private string guiScene;
    
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
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