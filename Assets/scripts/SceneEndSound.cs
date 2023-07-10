using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEndSound : MonoBehaviour
{
    AudioSource m_AudioSource;
    public string scene;
    // Start is called before the first frame update

    private void OnEnable()
    {
        if(m_AudioSource == null)
            m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.time = 0;
    }

    private void OnDisable()
    {
        fadeBlabk.setIntencity(0);
    }


    // Update is called once per frame
    void Update()
    {

        fadeBlabk.setIntencity( m_AudioSource.time/ m_AudioSource.clip.length);
        if (!m_AudioSource.isPlaying )
        {
            SceneManager.LoadScene(scene);
        }
    }
}
