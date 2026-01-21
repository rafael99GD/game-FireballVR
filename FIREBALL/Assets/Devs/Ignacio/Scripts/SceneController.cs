using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Nombres de las escenas (ajústalos según tus escenas en Build Settings)
    private const string SCENE_MENU = "Inicio";
    private const string SCENE_TUTO = "Tutorial";
    private const string SCENE_GAME = "Game";
    private const string SCENE_CREDITOS = "Creditos";


    // Método para cargar la escena del menú principal
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(SCENE_MENU);
    }

    // Método para cargar la escena de créditos
    public void LoadCreditosScene()
    {
        SceneManager.LoadScene(SCENE_CREDITOS);
    }

    // Método para cargar la escena de créditos
    public void LoadTutorialScene()
    {
        SceneManager.LoadScene(SCENE_TUTO);
    }

    // Método para cargar la escena del juego principal
    public void LoadGameScene()
    {
        SceneManager.LoadScene(SCENE_GAME);
    }

    // Método genérico para cargar cualquier escena por nombre
    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("El nombre de la escena no puede estar vacío");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    // Método para recargar la escena actual
    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // Método para salir de la aplicación
    public void QuitApplication()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}