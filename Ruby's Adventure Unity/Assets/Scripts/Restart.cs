    using UnityEngine;
    using UnityEngine.SceneManagement;
    using System.Collections;
    
    public class Restart: MonoBehaviour {
    bool restart;
    void Awake() {
        restart = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.R) && !restart) {
            restart = true;  
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}