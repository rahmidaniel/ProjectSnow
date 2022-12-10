using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using _Scripts.Environment;
using _Scripts.Utility;
using LDtkUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private int nextLevel;
    [SerializeField] private bool spawnPlayer = true;
    private bool _enter;

    private static readonly int start = Animator.StringToHash("Start");

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !_enter) // player isnt inside already
        {
            _enter = true;
            LoadLevel();
        } 
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        _enter = false;
    }

    private void LoadLevel()
    {
        StartCoroutine(Loader());
    }
    
    private IEnumerator Loader()
    {
        transition.SetTrigger(start);
        Player.Instance.DisableMovement(true);
        
        yield return new WaitForSeconds(transitionTime);
        
        // Teleport to other gate
        SceneManager.LoadScene(nextLevel);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (spawnPlayer)
        {
            Player.Instance.TeleportToLevel();
            _enter = true;
        }
        Player.Instance.DisableMovement(false);
    }
}
