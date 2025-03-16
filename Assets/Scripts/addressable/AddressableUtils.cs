using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace addressable
{
    public static class AddressableUtils
    {
        public static T LoadImmediately<T>(string address)
        {
            var handle = Addressables.LoadAssetAsync<T>(address);
            handle.WaitForCompletion();

            if (handle.Status == AsyncOperationStatus.Succeeded) return handle.Result;

            throw new Exception($"Failed to load Addressable asset {address}");
        }
    }
}