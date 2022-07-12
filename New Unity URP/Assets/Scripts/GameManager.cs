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
            Vector3 startPosition = gameObject.Find("PlayerStart").transform.position;
            
            Instantiate(playerAndCameraPrefab, startPosition, Quaternion.identity);
        };
        
    }
    
}