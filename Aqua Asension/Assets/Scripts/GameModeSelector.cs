using UnityEngine;

namespace AquaAscension
{
    public enum GameMode
    {
        Arena, TeamedArena, Race, BattleRoyal
    }

    public class GameModeSelector : MonoBehaviour
    {
        public GameMode mode;
    }
}