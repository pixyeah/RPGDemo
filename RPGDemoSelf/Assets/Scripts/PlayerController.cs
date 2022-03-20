using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _traTarget = null;

    private NavMeshAgent _playAgent = null;
    private BlendTree _playerAnimTree = null;
    private Animator _playerAnim = null;

    // Start is called before the first frame update
    void Awake()
    {
        _playAgent = GetComponent<NavMeshAgent>();
        _playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Move2Cursor();
        }
        UpdateAnimator();
    }

    private void Move2Cursor()
    {
        Ray lastHitRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit lastHitRayCast;
        if (Physics.Raycast(lastHitRay, out lastHitRayCast))
        {
            _playAgent.destination = lastHitRayCast.point;
        }

    }
    private void UpdateAnimator()
    {
        Vector3 v = _playAgent.velocity;
        _playerAnim.SetFloat("forwardSpeed", transform.InverseTransformDirection(v).z);
        //_playerAnim.SetFloat("forwardSpeed", Mathf.Abs(v.z));
    }
}
