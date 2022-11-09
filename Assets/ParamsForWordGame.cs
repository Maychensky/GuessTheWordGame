using UnityEngine;

namespace WordGame
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ParamsForWordGame", order = 1)]
    public class ParamsForWordGame : ScriptableObject
    {
        public string Name;
        public int minSizeWord;
        public int numberTrying;
    }
}