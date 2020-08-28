using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ContinueGame()
    {
        // TODO: Tiene que cargar la información del fichero guardado
        SceneManager.LoadScene("MainScene");
    }
}
