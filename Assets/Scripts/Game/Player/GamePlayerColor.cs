using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game.Player
{
    [CreateAssetMenu(fileName = "Game Player Color", menuName = "Game/Game Player Color")]
    public class GamePlayerColor : ScriptableObject
    {
        [SerializeField]
        private string colorName;
        [SerializeField]
        private DartComponent dartPrefab;

        public string ColorName
            => this.colorName;

        public GameObject DartPrefab
            => this.dartPrefab.gameObject;

        public static GamePlayerColor Create(string colorName, DartComponent prefab)
        {
            GamePlayerColor newInstance = CreateInstance(typeof(GamePlayerColor)) as GamePlayerColor;
            newInstance.colorName = colorName;
            newInstance.dartPrefab = prefab;
            return newInstance;
        }
    }
}

