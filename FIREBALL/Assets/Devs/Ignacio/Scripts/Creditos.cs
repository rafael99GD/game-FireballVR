using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("WaitForEnd", 10);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)){
            Debug.Log("Esc pulsado");
            //SceneManager.LoadScene("MainMenu");
            //Poner la escena del menú de inicio, la del tutorial.
        }
    }

    public void WaitForEnd(){
        //SceneManager.LoadScene("MainMenu");
        //Poner la escena del menú de inicio, la del tutorial.
    }
}
