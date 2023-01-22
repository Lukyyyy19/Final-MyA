using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour, IPausable {

    public Vector2 _dir;
    private Camera _cam;
    PlayerManager _playerManager;
    private bool canMoveHand = true;
    void Awake() {
        _cam = Camera.main;
        _playerManager = GetComponentInParent<PlayerManager>();
        ScreenManager.instance.AddPausable(this);
    }

    void Update() {
        if (canMoveHand)
            MoveHand();


    }
    private void MoveHand() {
        var _currentMousePos = _cam.ScreenToWorldPoint(PlayerManager.instance.playerInputs.MousePos);
        _dir = _currentMousePos - transform.position;
        float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));

    }

    public void Pause() {
        canMoveHand = false;
    }

    public void Resume() {
        canMoveHand = true;
    }
}
