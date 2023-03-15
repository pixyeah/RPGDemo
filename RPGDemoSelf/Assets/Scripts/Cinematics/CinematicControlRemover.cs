using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicControlRemover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayableDirector dir = GetComponent<PlayableDirector>();
        dir.played += OnControlDisable;
        dir.stopped += OnControlEnable;
    }

    void OnControlDisable(PlayableDirector director)
    {
        print("OnControlDisable");

        GameObject player = GameObject.FindWithTag(Constants.TAG_PLAYER);
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<ActionScheduler>().CancelCurAction();
    }

    void OnControlEnable(PlayableDirector director)
    {
        print("OnControlEnable");
        GameObject player = GameObject.FindWithTag(Constants.TAG_PLAYER);
        player.GetComponent<PlayerController>().enabled = true;
    }
}
