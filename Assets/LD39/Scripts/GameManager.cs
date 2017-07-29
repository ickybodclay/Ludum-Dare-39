using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public UIManager uiManager;
    public PlayerMotor player;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        InitGame();
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
         
    void OnDisable() {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
         
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        Debug.Log(string.Format("Level Loaded [{0} - {1}]", scene.name, System.Enum.GetName(typeof(LoadSceneMode), mode)));

        if (this != Instance) return;

        InitGame();
    }

    private void InitGame() {
        uiManager = GameObject.Find("Main Canvas").GetComponent<UIManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
    }

    private void FixedUpdate() {
        if (uiManager != null) {
            uiManager.SetPlayerHealth(Mathf.PingPong(Time.time / 5f, 1f));
            uiManager.SetEnemyHealth(Mathf.PingPong(Time.time / 10f, 1f));
        }
    }
}
