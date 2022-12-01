using System.Collections.Generic;
using _Scripts.Utility;
using Scenes.Sctips.Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Environment
{
    public class GameManager: MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameObject player;
        public InputController Controller { get; private set; }
        private Move _movementController;
        public HouseInfo HouseInfo;
        
        //----//
        public List<TreeController> grabObjects;
        public List<TreeController> pickUpObjects;
        public int logCount;
        
        private void Awake()
        {
            Instance = this;
            Controller = player.GetComponent<Controller>().input;
            _movementController = player.GetComponent<Move>();
            grabObjects = new List<TreeController>();

            HouseInfo.State = HouseState.Outside; //TODO: defined house state here
        }
        
        public void TeleportToLevel()
        {
            SceneManager.MoveGameObjectToScene(player, SceneManager.GetActiveScene());
            
            var position = GameObject.FindWithTag("Gate").transform.position;
            player.transform.position = position;
        }

        public void DisablePlayerMovement(bool value)
        {
            _movementController.blocked = value;
        }
    }
}