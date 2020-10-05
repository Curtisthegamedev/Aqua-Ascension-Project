using UnityEngine;

namespace AquaAscension.Utils
{
    public class Persist : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
