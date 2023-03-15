using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _grp = null;
        public float _fadeTime = 1;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            _grp.alpha = 0;
            StartCoroutine(FadeIn(_fadeTime));
        }

        public void FadeOut()
        {
            
            StartCoroutine(FadeOut(_fadeTime));
        }

        IEnumerator FadeOut(float time)
        {
            print("fadeout ");
            float t = time;
            time = 0;
            while (time <= t)
            {
                yield return null;
                time += Time.deltaTime;
                _grp.alpha = Mathf.Lerp(1, 0, time / t);
            }
            Destroy(gameObject);
        }
        IEnumerator FadeIn(float time)
        {
            float t = time;
            time = 0;
            while (time <= t)
            {
                yield return null;
                time += Time.deltaTime;
                _grp.alpha = Mathf.Lerp(0, 1, time / t);
            }
        }

        public static void FadeImmediate()
        {
            
        }


        // private static IEnumerator LoadScene2()
        // {
        //     AsyncOperationHandle<GameObject> fader =  Addressables.InstantiateAsync(SceneFader);
        //     yield return fader;
        //
        //     Fader f = fader.Result.GetComponent<Fader>();
        //
        //     yield return new WaitForSeconds(f._fadeTime);
        //
        //     GameObject.FindWithTag(Constants.TAG_SAVING).GetComponent<SavingWrapper>().Save();
        //
        //
        //     if (IsLoadFirstScene)
        //     {
        //         yield return SceneManager.LoadSceneAsync(0);
        //     }
        //     else
        //     {
        //         AsyncOperationHandle<SceneInstance> op = Addressables.LoadSceneAsync(LocationScene2Load);
        //         yield return op;
        //     }
        //
        //     fader.Result.GetComponent<Fader>().FadeOut();
        //     GameObject.FindWithTag(Constants.TAG_SAVING).GetComponent<SavingWrapper>().Load();
        //
        //
        //     UpdatePlayerBySpawnPoint();
        //     GameObject.FindWithTag(Constants.TAG_SAVING).GetComponent<SavingWrapper>().Save();
        //     Destroy(gameObject);
        // }

    }

    
}