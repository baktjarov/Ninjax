using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Threading.Tasks;

namespace Services
{
    public class SceneLoader : MonoBehaviour
    {
        public async void LoadScene(string sceneName, Action afterSceneLoader)
        {
            var sceneLoadingProcess = SceneManager.LoadSceneAsync(sceneName);
            while (sceneLoadingProcess.isDone == false)
            {
                await Task.Yield();
            }

            afterSceneLoader?.Invoke();
        }
    }
}