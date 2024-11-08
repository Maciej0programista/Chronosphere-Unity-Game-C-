using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Dodane dla obsługi UI

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public Camera mainCamera;
    public float defaultFOV = 60f;
    public float zoomedFOV = 30f;
    private bool isZoomed = false;
    public enum CameraMode { Follow, Static };
    public CameraMode currentCameraMode = CameraMode.Follow;
    public Vector3 staticCameraPosition = new Vector3(0, 10, -20); 
    public Transform playerTransform;
    private bool isPaused = false;
    private Vector3 cameraOffset;


    public Text scoreText; // Referencja do tekstu wyświetlającego wynik
    private int score = 0;

    public static GameManager instance; // Singleton dla łatwego dostępu



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        cameraOffset = mainCamera.transform.position - playerTransform.position;

        if (scoreText!= null) { //sprawdzanie czy referencja została ustawiona
            scoreText.text = "Wynik: " + score; 


        }
         else
        {
            Debug.LogError("Referencja do scoreText nie jest ustawiona w inspektorze!");



        }

    }



    void Update()
    {
        if (Input.GetButtonDown("Zoom"))
        {
            ToggleZoom();
        }

        if (Input.GetButtonDown("ChangeCameraMode"))
        {
            ToggleCameraMode();
        }

        if (Input.GetButtonDown("Cancel")) 
        {
            TogglePauseMenu();
        }
    }

    void LateUpdate()
    {
        if (currentCameraMode == CameraMode.Follow)
        {
            mainCamera.transform.position = playerTransform.position + cameraOffset;
        }
    }

    public void StartGame(string gameMode)
    {
        if (gameMode == "level")
        {
            SceneManager.LoadScene("LevelScene");
        }
        else if (gameMode == "endless")
        {
            SceneManager.LoadScene("EndlessScene");
        }
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true); 
    }

    void ToggleZoom()
    {
        isZoomed = !isZoomed;
        mainCamera.fieldOfView = isZoomed ? zoomedFOV : defaultFOV;
    }

    void ToggleCameraMode()
    {
        currentCameraMode = (currentCameraMode == CameraMode.Follow) ? CameraMode.Static : CameraMode.Follow;
        Debug.Log("Tryb kamery: " + currentCameraMode);

        if (currentCameraMode == CameraMode.Static)
        {
            mainCamera.transform.position = staticCameraPosition;
        } else {
            cameraOffset = mainCamera.transform.position - playerTransform.position;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AddScore(int points)
    {
        score += points;

        if (scoreText != null)
        {

            scoreText.text = "Wynik: " + score;
        }



    }



}
