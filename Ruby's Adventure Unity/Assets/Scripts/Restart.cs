    using UnityEngine;
    using UnityEngine.SceneManagement;
    using System.Collections;
    
    public class Restart: MonoBehaviour {
    bool restart;
    void Awake() {
        restart = false;
    }

    void Update() {
        Time.timeScale = 0.0f;
        if (Input.GetKeyDown(KeyCode.R) && !restart) {
            Time.timeScale = 1.0f;
            restart = true;  
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}