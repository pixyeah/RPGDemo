using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicTrigger : MonoBehaviour
{
    private bool _isTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!_isTriggered && other.gameObject.CompareTag("Player"))
        {
            _isTriggered = true;
            GetComponent<PlayableDirector>().Play();
        }
    }
}
