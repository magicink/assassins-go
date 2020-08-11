using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Board _board;
    private PlayerManager _playerManager;
    [SerializeField] private UnityEvent onLevelEnded;
    [SerializeField] private UnityEvent onLevelStarted;
    [SerializeField] private UnityEvent onLevelInitialized;
    [SerializeField] private float stateChangeDelay = 1.0f;

    private bool GameEnded { get; set; }

    private bool LevelInitialized { get; set; }

    private bool LevelEnded { get; set; }


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
    }

    private IEnumerator StartGame()
    {
        yield return StartCoroutine(nameof(InitializeLevel));
        yield return StartCoroutine(nameof(StartLevel));
        yield return StartCoroutine(nameof(EndLevel));
    }

    private IEnumerator InitializeLevel()
    {
        _playerManager.PlayerInput.InputEnabled = false;
        while (!LevelInitialized)
        {
            yield return null;
        }
        onLevelInitialized?.Invoke();
    }

    private IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(stateChangeDelay);
        _playerManager.PlayerInput.InputEnabled = true;
        onLevelStarted?.Invoke();
        while (!GameEnded)
        {
            GameEnded = GameEndConditionMet();
            yield return null;
        }
        Debug.Log("Game ended");
    }

    private IEnumerator EndLevel()
    {
        onLevelEnded?.Invoke();
        while (!LevelEnded)
        {
            yield return null;
        }
        RestartLevel();
    }

    private static void RestartLevel()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void HandleStartLevel()
    {
        LevelInitialized = true;
        _board.InitializeBoard();
    }

    public bool GameEndConditionMet()
    {
        var gameOver = false;
        if (_board.PlayerNode != null && _board.GoalNode != null)
        {
            gameOver = _board.PlayerNode == _board.GoalNode;
            if (gameOver)
            {
                _playerManager.PlayerInput.InputEnabled = false;
            }
            return gameOver;
        }

        return gameOver;
    }
}