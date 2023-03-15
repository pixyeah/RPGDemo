using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Move
{
    public class Mover : MonoBehaviour,IAction
    {
        private NavMeshAgent _agent = null;
        private Animator _animator = null;
        private ActionScheduler _actionScheduler;
        [SerializeField] private float _maxSpeed = 6f;


        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _actionScheduler = GetComponent<ActionScheduler>();

            GetComponent<Health>()._dieAction += DieAction;
            if (_agent == null)
            {
                print("Warning : mover has no navmesh agent !");
            }
        }

        private void DieAction()
        {
            _agent.enabled = false;
            GetComponent<Health>()._dieAction -= DieAction;
        }
        
        public void StartMoveAction(Vector3 destination,float speedFraction)
        {
            _actionScheduler.StartAction(this);
            MoveTo(destination,speedFraction);
        }

        public void MoveTo(Vector3 destination,float speedFraction)
        {
            _agent.isStopped = false;
            _agent.destination = destination;
            _agent.speed = _maxSpeed * Mathf.Clamp01(speedFraction);
            // _agent.transform.LookAt(hit.point);
        }

        // Update is called once per frame
        void Update()
        {

            // if(_agent.velocity != Vector3.zero) print(_agent.velocity);
            _animator.SetFloat(Animator.StringToHash(Constants.ANIMATION_PARA_CHAR_FOWARDSPEED), transform.InverseTransformDirection(_agent.velocity).z);
        }

        public void Cancel()
        {
            _agent.isStopped = true;
        }
        
    }

}