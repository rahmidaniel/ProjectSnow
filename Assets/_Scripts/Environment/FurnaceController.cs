using _Scripts;
using _Scripts.Environment;
using _Scripts.Utility;
using Scenes.Sctips.Controllers;
using UnityEngine;

public class FurnaceController : Interactable
{
    [SerializeField] private float fuel = 80f;
    [SerializeField] private float maxFuel = 100f;
    [SerializeField] private float fallRate = 1f;
    [SerializeField] private float openMulti = 3f;
    [SerializeField] private float gainLog = 25f;

    [SerializeField] private float ttl = 90f;

    private Collider2D _collider2D;
    private SpriteRenderer _body;

    protected override string UpdateMessage()
    {
        return "Press 'F' to feed the flames. (" + (int) fuel + "/" + (int) maxFuel + ")";
    }

    private new void Start()
    {
        base.Start();

        _body = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        if (fuel > 0)
        {
            var diff = Time.deltaTime * maxFuel / ttl;

            switch (GameManager.Instance.HouseInfo.State)
            {
                case HouseState.InsideOpen or HouseState.OutsideOpen:
                    fuel -= diff * fallRate * openMulti;
                    break;
                case HouseState.Inside or HouseState.Outside:
                    fuel -= diff * fallRate;
                    break;
            }
        }

        GameManager.Instance.HouseInfo.Integrity = fuel / maxFuel;
    }

    protected override void Interact()
    {
        if (GameManager.Instance.logCount == 0) return;

        GameManager.Instance.logCount--;
        fuel += gainLog;
    }
    
    protected override void OnPlayerEnter() { }
    protected override void OnPlayerExit() { }
}