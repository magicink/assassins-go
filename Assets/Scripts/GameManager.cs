using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Board _board;
    private PlayerManager _playerManager;

    [SerializeField] private float stateChangeDelay = 1.0f;
    
    public bool GameEnded { get; set; }

    public bool LevelStarted { get; set; }
    
    public bool LevelEnded { get; set; }

    private void Awake()
    {
        _board = FindObjectOfType<Board>().GetComponent<Board>();
        _playerManager = FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
    }

    private void Start()
    {
        if (_board && _playerManager)
        {
            StartCoroutine(nameof(StartGame));
        }
        else
        {
            Debug.Log("Could not find board or player manager");
        }
    }

    private IEnumerator StartGame()
    {
        Debug.Log("Starting game");
        yield return StartCoroutine(nameof(StartLevel));
        yield return StartCoroutine(nameof(PlayLevel));
        yield return StartCoroutine(nameof(EndLevel));
    }

    private IEnumerator StartLevel()
    {
        Debug.Log("Awaiting the start of the level");
        _playerManager.PlayerInput.InputEnabled = false;
        while (!LevelStarted)
        {
            yield return null;
        }
    }

    private IEnumerator PlayLevel()
    {
        Debug.Log("Level started");
        yield return new WaitForSeconds(stateChangeDelay);
        _playerManager.PlayerInput.InputEnabled = true;
        while (!GameEnded)
        {
            yield return null;
        }
    }

    private IEnumerator EndLevel()
    {
        _playerManager.PlayerInput.InputEnabled = false;
        while (!LevelEnded)
        {
            yield return null;
        }
        RestartLevel();
    }

    private void RestartLevel()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void HandleStartLevel()
    {
        LevelStarted = true;
    }
}
