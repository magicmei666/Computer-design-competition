using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class tt : MonoBehaviour
{
    // Start is called before the first frame update
    public VideoPlayer mov;
    void Start()
    {
        mov.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (mov.isPaused)
        {
             
                SceneManager.LoadScene("1_WoShi");
            
        }
    }
}
