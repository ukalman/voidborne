using UnityEngine;
using Utilities;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        private GameStates _currentGameState = GameStates.None;
        public GameStates CurrentGameState => _currentGameState;
    }
}