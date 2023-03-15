using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActionScheduler : MonoBehaviour
{
    private IAction _curAction;
    public void StartAction(IAction action)
    {
        if (_curAction != null && _curAction != action)
        {
            _curAction .Cancel();
        }
        _curAction = action;
    }

    public void Stop()
    {
        if (_curAction != null)
        {
            _curAction .Cancel();
        }
    }

    public void CancelCurAction()
    {
        StartAction(null);
    }
}
