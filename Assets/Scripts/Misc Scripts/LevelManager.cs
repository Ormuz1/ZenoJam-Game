using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Eflatun.SceneReference;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public static SceneReference gameOverScene;
    [SerializeField] public static SceneReference firstLevel;

    private static LevelManager instance;
    public static LevelManager I 
    {
        get 
        {
            return instance;
        }
    }
 
    public virtual void Awake ()
    {
        DontDestroyOnLoad(this);
        if (instance == null) 
        {
            instance = this;
        }
        else
        {
            Destroy (gameObject);
        }
    }   

    public static void SwitchLevel(SceneReference scene)
    {
        if(!scene.IsSafeToUse)
        {
            Debug.LogError("The scene you are trying to switch to isn't safe to use!");
            return;
        }

        SceneManager.LoadScene(scene.BuildIndex);
    }

}
