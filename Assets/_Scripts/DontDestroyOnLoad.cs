using UnityEngine;

namespace Core.Utility
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            Destroy(this);
        }
    }
}