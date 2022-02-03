//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Facebook.Unity;

/*public class FbManager : MonoBehaviour
{
    public void LevelEnded(int lvl)
    {
        var tutParams = new Dictionary<string, object>();
        tutParams[AppEventParameterName.ContentID] = "tutorial_step_1";
        tutParams[AppEventParameterName.Description] = "First step in the tutorial, clicking the first button!";
        tutParams[AppEventParameterName.Success] = "1";

        FB.LogAppEvent (
            AppEventName.CompletedTutorial,
            parameters: tutParams
        );
    }
    
    // Awake function from Unity's MonoBehavior
    void Awake ()
    {
        if (!FB.IsInitialized) {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        } else {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }

        DontDestroyOnLoad(this);
    }

    private void InitCallback ()
    {
        if (FB.IsInitialized) {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        } else {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity (bool isGameShown)
    {
        if (!isGameShown) {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        } else {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }
}*/
