using _Scripts.Utility;
using Scenes.Sctips.Controllers;
using UnityEngine;

public class DoorController : Interactable
{
    public bool IsOpen { get; private set; }

    private Collider2D _collider2D;
    private SpriteRenderer _body;
    private Color _closedColor, _openColor;

    protected override string UpdateMessage()
    {
        return "Press 'F' to " + (IsOpen ? "close" : "open") + " the door";
    }

    private new void Start()
    {
        base.Start();

        _body = GetComponent<SpriteRenderer>();
        _closedColor = _body.color;
        _openColor = new Color(_closedColor.r * 0.5f, _closedColor.g * 0.5f, _closedColor.b * 0.5f, _closedColor.a);

        tag = "Door";
        
        var collider2Ds = GetComponents<Collider2D>();
        // TODO: loop is unnecessary here, but gets the actual collider
        foreach (var collider in collider2Ds)
        {
            if (!collider.isTrigger)
            {
                _collider2D = collider;
            }
        }
    }

    protected override void Interact()
    {
        IsOpen = !IsOpen;
        _collider2D.enabled = !_collider2D.enabled;
        if (IsOpen)
        {
            _body.color = _openColor;
            _body.sortingOrder = -1;
        }
        else
        {
            _body.color = _closedColor;
            _body.sortingOrder = 0;
        }
    }
    protected override void OnPlayerEnter() { }
    protected override void OnPlayerExit() { }
}
