using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class FadeAndSwitchScene : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;  
    [SerializeField] private float fadeDuration = 10.0f;  
    [SerializeField] private float waitTimeBeforeSwitch = 3.0f; 
    [SerializeField] private string nextSceneName;        

    private Graphic uiElement;         
    private float timer;                
    private bool isFading;             

    void Start()
    {
        uiElement = GetComponent<Graphic>();
        director = GetComponent<PlayableDirector>();
        timer = 0.0f;
        isFading = true;
    }

    void Update()
    {
        if (isFading)
        {
            if (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(timer / fadeDuration);
                Color color = uiElement.color;
                color.a = normalizedTime; 
                uiElement.color = color;
            }
            else
            {
                isFading = false;
                StartCoroutine(SwitchSceneAfterDelay(waitTimeBeforeSwitch));
            }
        }
    }

    IEnumerator SwitchSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName);
    }

    void OnEnable()
    {
        if (director != null)
        {
            director.stopped += OnPlayableDirectorStopped;
        }
    }

    void OnDisable()
    {
        if (director != null)
        {
            director.stopped -= OnPlayableDirectorStopped;
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
