using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    
}