/*
 * Auth: Ian
 * 
 * Proj: Robins
 * 
 * Date: 3/5/24
 * 
 * Desc: Handles Scene operations
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
            Restart(); // RESTART ON R PRESSED FOR TESTING
    }
}
