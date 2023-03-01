using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour, IPausable {

    public Vector2 _dir;
    private Camera _cam;
    PlayerManager _playerManager;
    [SerializeField]
    SpriteRenderer gunSr;
    private bool canMoveHand = true;
    private bool IsLookingUp;
    void Awake() {
        _cam = Camera.main;
        _playerManager = GetComponentInParent<PlayerManager>();
        ScreenManager.instance.AddPausable(this);
        gunSr = GetComponentInChildren<SpriteRenderer>();

    }
    private void Start() {
        _playerManager.OnUpdateAmmo += UpdateGunVisuals;
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

        if (angle < 120 && angle > 50) {
            gunSr.sortingOrder = _playerManager.isMoving ? -1 : 1;
            IsLookingUp = true;
        } else {
            gunSr.sortingOrder = 1;
            IsLookingUp = false;
        }
        _playerManager.playerAnimation.SetBoolParam("IsLookingUp", IsLookingUp);
        if (!IsLookingUp)
            FlipGun(angle);
    }

    private void FlipGun(float angle) {
        Vector3 localScale = Vector3.one;
        if (angle > 90 || angle < -90) {
            _playerManager.playerVisuals.FlipPlayer(1);
        } else {
            _playerManager.playerVisuals.FlipPlayer(-1);
        }
        transform.localScale = localScale;
    }

    public void Pause() {
        canMoveHand = false;
    }

    public void Resume() {
        canMoveHand = true;
    }

    void UpdateGunVisuals(int Ammo) {
        float newAmmo = (float)Ammo / ((float)_playerManager.GunStats.gun.MaxAmmo / 2f);
        gunSr.material.SetFloat("_Ammo", newAmmo);
    }
}
