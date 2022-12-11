using System.Collections.Generic;
using _Scripts.Environment;
using UnityEngine;

namespace _Scripts.Utility.Serialization
{
    [System.Serializable]
    public class GameData
    {
        public bool dead;
        public int logCount;
        public bool doorOpen;
        public float fuel, maxFuel;
        public Vector3 playerPosition;
        public int sceneIndex;
        public List<Vector3> treePositions;
        public Vector3 time;
        public Vector3 temperature;

        public GameData()
        {
            dead = false;
            logCount = 0;
            playerPosition = Vector3.zero;
            sceneIndex = 0;
            doorOpen = false;
            fuel = maxFuel;
            treePositions = new List<Vector3>();
            time = Vector3.zero;
            temperature = Vector3.zero;
        }
    }
}