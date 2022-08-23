using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    AsyncOperation sceneAO;
    [SerializeField] Image m_loadingImg;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    // Update is called once per frame
    void Update()
    {
    }


    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1);
        sceneAO = SceneManager.LoadSceneAsync("PianoKid");

        // disable scene activation while loading to prevent auto load
        sceneAO.allowSceneActivation = false;

        while (!sceneAO.isDone)
        {
            m_loadingImg.fillAmount = sceneAO.progress;

            if (sceneAO.progress >= 0.9f)
            {
                m_loadingImg.fillAmount = 1f;
                
                //if (Input.GetKeyDown(KeyCode.Space))
                {
                    sceneAO.allowSceneActivation = true;
                }
            }
            //Debug.Log(sceneAO.progress);
            yield return null;
        }
    }

}
