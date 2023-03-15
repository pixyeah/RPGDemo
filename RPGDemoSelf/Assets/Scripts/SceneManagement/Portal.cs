using System.Collections;
using RPG.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    enum DestinationIdentity
    {
        A,B,C,D,E,F
    }
    public Transform PlaySpawnPoint;
    public AssetReference LocationScene2Load;
    public AssetReference SceneFader;
    public bool IsLoadFirstScene = false;
    [SerializeField] private DestinationIdentity _destination;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.TAG_PLAYER))
        {
            print(PlaySpawnPoint.position);
            StartCoroutine(LoadScene2());
            DontDestroyOnLoad(gameObject);
            // Addressables.LoadSceneAsync("Assets/Scenes/Sandbox2.unity").Completed += OnSceneLoadComplete;
        }
    }

    private IEnumerator LoadScene2()
    {
        AsyncOperationHandle<GameObject> fader =  Addressables.InstantiateAsync(SceneFader);
        yield return fader;

        Fader f = fader.Result.GetComponent<Fader>();

        yield return new WaitForSeconds(f._fadeTime);
        
        
        if (IsLoadFirstScene)
        {
            yield return SceneManager.LoadSceneAsync(0);
        }
        else
        {
            AsyncOperationHandle<SceneInstance> op = Addressables.LoadSceneAsync(LocationScene2Load);
            yield return op;
        }

        fader.Result.GetComponent<Fader>().FadeOut();
        

        UpdatePlayerBySpawnPoint();
        Destroy(gameObject);
    }

    private void UpdatePlayerBySpawnPoint()
    {
        GameObject[] portals = GameObject.FindGameObjectsWithTag(Constants.TAG_PORTAL);
        for (int i = 0; i < portals.Length; i++)
        {
            Portal otherPortal = portals[i].GetComponent<Portal>();
            if (otherPortal != this && otherPortal._destination == _destination)
            {
                print(otherPortal.PlaySpawnPoint.position);
                // GameObject.FindWithTag(Constants.TAG_PLAYER).transform.SetPositionAndRotation(otherPortal.PlaySpawnPoint.position,otherPortal.PlaySpawnPoint.rotation);
                GameObject.FindWithTag(Constants.TAG_PLAYER).GetComponent<NavMeshAgent>()
                    .Warp(otherPortal.PlaySpawnPoint.position);
                break;
            }
        }

    }

    private void OnSceneLoadComplete(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.IsDone)
        {
            print("scene load complete");
            // handle.Result.ActivateAsync();
        }
    }
}
