using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

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
            if (audioS.name=="BGAudioClip"){
                 audioS.Play();
            }
           
        }
    }
    // Start is called before the first frame update
    public void Pause(){
        pauseMenu.SetActive(true);
        publicVars.paused = true;
        Time.timeScale = 0f;
        PauseAllAudio();
 
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        publicVars.paused = false;
        Time.timeScale = 1f;
        StartAllAudio();
        
    }

    public void Home(){

        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
 
    }

    public void Quit(){
        Application.Quit();
    }
   
}
