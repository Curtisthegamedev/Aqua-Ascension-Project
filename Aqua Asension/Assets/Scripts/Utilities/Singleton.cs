using System.Collections.Generic;
using UnityEngine;

namespace AquaAscension.Utils
{
    public class Singleton : MonoBehaviour
    {
        private static Dictionary<string, Singleton> Instances = new Dictionary<string, Singleton>();

        public static Singleton Get(string name)
        {
            Singleton instance;
            Instances.TryGetValue(name, out instance);
            return instance;
        }

        private void Awake()
        {
            if (!Instances.ContainsKey(name))
                Instances.Add(name, this);
            else
                DestroyImmediate(gameObject);
        }

        private void OnDestroy()
        {
            if (Instances.ContainsKey(name) && Instances[name] == this)
                Instances.Remove(name);
        }
    }
}
