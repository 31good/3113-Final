using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject quitButton;

    private AudioSource[] allAudioSources;
    void Start(){
        //Resume();
        
        #if UNITY_WEBGL
        quitButton.SetActive(false);
        #endif
    }

    //pause all audio when menue comes out
    void PauseAllAudio() {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach( AudioSource audioS in allAudioSources) {
            audioS.Pause();
        }
    }

    //play all audio when game resume
    void StartAllAudio() {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach( AudioSource audioS in allAudioSources) {
            audioS.Play();
        }
    }
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (publicVars.paused){
                Resume();

            }
            else{
                pauseMenu.SetActive(true);
                publicVars.paused = true;
                Time.timeScale = 0;
                PauseAllAudio();
            }
        }
        
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        publicVars.paused = false;
        Time.timeScale = 1;
        StartAllAudio();
    }

    public void Restart(){
        donotdestory[] yourObjects = FindObjectsOfType<donotdestory>();
        foreach (var thing in yourObjects){
            Destroy(thing.gameObject);
        }
        SceneManager.LoadScene(1);
    Resume();
    }

    public void Quit(){
        Application.Quit();
    }

    public void StartGame(){
        SceneManager.LoadScene(1);
    }
    public void TryAgain(){
        SceneManager.LoadScene(1);
    }

    public void Replay(){
        SceneManager.LoadScene(1);
    }
}
