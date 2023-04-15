using _Scripts.Utility;
using _Scripts.Utility.Serialization;
using FMOD.Studio;
using UnityEngine;

public class DoorController : Interactable, IPersistentData
{
    private SpriteRenderer _body;
    private Color _closedColor, _openColor;

    private Collider2D _collider2D;
    private EventInstance _doorSound;
    public bool IsOpen { get; private set; }

    private void Start()
    {
        _body = GetComponent<SpriteRenderer>();
        _closedColor = _body.color;
        _openColor = new Color(_closedColor.r * 0.5f, _closedColor.g * 0.5f, _closedColor.b * 0.5f, _closedColor.a);

        tag = "Door";

        _doorSound = SoundManager.Instance.CreateEventInstance(FMODEvents.Instance.Door);

        var collider2Ds = GetComponents<Collider2D>();
        // TODO: loop is unnecessary here, but gets the actual collider
        foreach (var collider in collider2Ds)
            if (!collider.isTrigger)
                _collider2D = collider;
    }

    public void SaveData(ref GameData data)
    {
        data.doorOpen = IsOpen;
    }

    public void LoadData(GameData data)
    {
        IsOpen = data.doorOpen;
    }

    protected override string UpdateMessage()
    {
        return "Press 'F' to " + (IsOpen ? "close" : "open") + " the door";
    }

    protected override void Interact()
    {
        IsOpen = !IsOpen;

        _doorSound.setParameterByName("door", IsOpen ? 0 : 1);
        _doorSound.getPlaybackState(out var playbackState);
        if (playbackState == PLAYBACK_STATE.PLAYING)
            _doorSound.stop(STOP_MODE.ALLOWFADEOUT);
        _doorSound.start();

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
}