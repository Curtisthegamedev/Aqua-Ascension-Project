using UnityEngine;
using TMPro;

namespace AquaAscension
{
    public enum GameMode
    {
        Arena, TeamedArena, Race, BattleRoyal, TestPuzzle
    }

    public class GameModeSelector : MonoBehaviour
    {
        public GameMode mode;

        void Start()
        {
            var textComponent = GetComponentInChildren<TextMeshProUGUI>();
            textComponent.text = "Start " + mode.ToString("F");
        }
    }
}