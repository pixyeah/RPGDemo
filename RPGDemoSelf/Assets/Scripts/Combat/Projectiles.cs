using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] private Transform _traTarget = null;

    [SerializeField] private float _speed = 1;

    private Fighter _targetFighter = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (_traTarget == null) return;
        
        transform.LookAt(GetAimPosition());
        transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
    }

    private Vector3 GetAimPosition()
    {
        if (_targetFighter == null)
        {
            _targetFighter = _traTarget.GetComponent<Fighter>();
        }

        return _targetFighter.GetAimPosition();
    }
}
