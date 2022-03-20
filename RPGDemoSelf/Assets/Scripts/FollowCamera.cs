using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _traPlayer = null;
    Camera _mainCamera = null;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    private void LateUpdate()
    {
        transform.position = _traPlayer.position;
    }
}
