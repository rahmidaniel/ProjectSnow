using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using _Scripts.Environment;
using LDtkUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private int nextLevel;

    private static readonly int start = Animator.StringToHash("Start");

    private void OnTriggerEnter2D(Collider2D col)
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        StartCoroutine(Loader());
    }

    private IEnumerator Loader()
    {
        transition.SetTrigger(start);
        GameManager.Instance.DisablePlayerMovement(true);
        
        yield return new WaitForSeconds(transitionTime);
        
        // Teleport to other gate
        SceneManager.LoadScene(nextLevel);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        GameManager.Instance.TeleportToLevel();
        GameManager.Instance.DisablePlayerMovement(false);
    }
}
