using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
    public bool isMenuShowing = true;
    public CanvasGroup menuCanvasGroup;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void LoadLevel (int sceneIndex)
    {
        if (SceneManager.GetActiveScene().buildIndex == sceneIndex)
        {
            return;
        }
        SceneManager.LoadScene(sceneIndex);
        menuCanvasGroup.alpha = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    public void ToggleMenu()
    {
        isMenuShowing = !isMenuShowing;
        menuCanvasGroup.alpha = isMenuShowing ? 0 : 1;
        Time.timeScale = isMenuShowing ? 1 : 0;
        Cursor.lockState = isMenuShowing ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
