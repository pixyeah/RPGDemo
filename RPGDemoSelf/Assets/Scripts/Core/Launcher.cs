using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace RPG.Core
{
    public class Launcher : MonoBehaviour
    {
        [SerializeField] private AssetReference _loadScene = null;
        private void Awake()
        {
            Addressables.InitializeAsync().Completed += AddressableInitCompleted;

        }

        private void AddressableInitCompleted(AsyncOperationHandle<IResourceLocator> handle)
        {
            if (handle.IsDone)
            {
                Addressables.LoadSceneAsync(_loadScene);
            }
        }
    }

    
}