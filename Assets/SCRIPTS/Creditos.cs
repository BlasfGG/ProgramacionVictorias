
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Creditos");
            Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
            Cursor.visible = true; // Hace visible el cursor
        }
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }
}
