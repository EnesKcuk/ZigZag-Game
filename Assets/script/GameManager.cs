using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameStarted { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        gameStarted = true;
    }
    // Update is called once per frame
    public void RestartGame()
    {
        Invoke("Load", 1f);
    }

    void Load()
    {
        SceneManager.LoadScene(0);
    }
}
