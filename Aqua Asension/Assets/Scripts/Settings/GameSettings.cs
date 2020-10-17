using UnityEngine;

[CreateAssetMenu(menuName = "Settings/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private string version = "0.0.0";
    [SerializeField] private string nickName = "AquaA";

    public string Version { get => version; }
    public string NickName
    {
        get
        {
            int value = Random.Range(0,9999);
            return nickName + "#" + value.ToString("D4");
        }
    }
}

