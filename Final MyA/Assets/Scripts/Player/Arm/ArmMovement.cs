using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour {

    public Vector2 _dir;
    private Camera _cam;
    PlayerManager _playerManager;

    void Awake() {
        _cam = Camera.main;
        _playerManager = GetComponentInParent<PlayerManager>();

    }

    void Update() {
        MoveHand();
    }
    private void MoveHand() {
        var _currentMousePos = _cam.ScreenToWorldPoint(_playerManager.playerInputs.MousePos);
        _dir = _currentMousePos - transform.position;
        float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
    }
}
