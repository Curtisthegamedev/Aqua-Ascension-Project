using UnityEngine;

namespace AquaAscension.Utils
{
    public class SimpleRotator : MonoBehaviour
    {
        [SerializeField] private Vector3 rotator = Vector3.zero;
        [SerializeField] private float drag = 0.01f;

        Vector3 rotation = Vector3.zero;

        private void Update() {
            rotation += Vector3.Lerp(Vector3.zero, rotator, Time.timeSinceLevelLoad * drag) * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(rotation);    
        }
    }
}
