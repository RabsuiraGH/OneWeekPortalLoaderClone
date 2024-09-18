using SceneFieldTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.Level
{
    public class LevelLoader
    {
        [Inject(Id = "IngameMenu")]
        [SerializeField] private SceneField _ingameMenuScene = null;

        public void LoadLevel(LevelData level)
        {
            SceneManager.LoadScene(level.LevelScene);
            SceneManager.LoadScene(_ingameMenuScene, LoadSceneMode.Additive);
        }
    }
}