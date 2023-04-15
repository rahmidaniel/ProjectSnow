using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Utility.Serialization
{
    [Serializable]
    public class GameData
    {
        public bool dead;
        public int logCount;
        public bool doorOpen;
        public bool axeUnlocked;
        public float fuel, maxFuel;
        public Vector3 playerPosition;
        public int sceneIndex;
        public List<Vector3> treePositions;
        public Vector3 time, temperature;

        public bool fullscreenOn = true;
        public int graphicsQuality;
        public float masterVolume ;

        public GameData()
        {
            dead = false;
            logCount = 0;
            playerPosition = Vector3.zero;
            sceneIndex = 0;
            doorOpen = false;
            axeUnlocked = false;
            fuel = maxFuel;
            treePositions = new List<Vector3>();
            time = Vector3.zero;
            temperature = Vector3.zero;
        }
    }
}