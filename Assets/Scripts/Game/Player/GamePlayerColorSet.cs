using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game.Player
{
    [CreateAssetMenu(fileName = "Game Player Color Set", menuName = "Game/Game Player Color Set")]
    public class GamePlayerColorSet : ScriptableObject
    {
        [SerializeField]
        private List<GamePlayerColor> colors;
        [SerializeField]
        private DartComponent defaultDartPrefab;

        /// <summary>
        /// Generates a random color.
        /// </summary>
        /// <returns>A Game player color.</returns>
        public GamePlayerColor GenerateRandomColor()
        {
            string randomColor = "random_color_" + ((int)Random.value * 1000);
            return GamePlayerColor.Create(randomColor, defaultDartPrefab);
        }

        public GamePlayerColor GetColor(int colorIndex)
        {
            if(colorIndex >= this.colors.Count
                || colorIndex < 0)
            {
                return null;
            }
            return this.colors[colorIndex];
        }

        public GamePlayerColor GetColorFromName(string colorName)
        {
            return this.colors.Find((GamePlayerColor a) =>
            {
                return a.ColorName == colorName;
            });
        }
    }
}
