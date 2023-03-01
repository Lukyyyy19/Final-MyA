using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour, IPausable {

    [SerializeField]
    private PlayerManager _playerManager;

    [SerializeField]
    private Transform _crossHair;

    [SerializeField]
    private Animator _anim;


    [SerializeField]
    private GameObject _crossHairGo;
    [SerializeField]
    private Transform _tipUp, _tipDown, _tipLeft, _tipRight, _lines;
    [SerializeField]
    private float _maxDistance;
    [SerializeField, Range(0, 1)]
    float _speed;
    [SerializeField]
    float _timer;


    private Camera _cam;

    private bool _playAnimation;
    void Awake() {
        _cam = Camera.main;
        Cursor.visible = false;

    }
    private void Start() {
        ScreenManager.instance.AddPausable(this);
        EventManager.instance.AddAction("OnShoot", PlayAnimation);
        EventManager.instance.AddAction("OnCanShoot", PlayAnimationClose);
    }

    private void Update() {
        _crossHair.position = Vector3.one;
        _crossHair.position = (Vector2)_cam.ScreenToWorldPoint(_playerManager.playerInputs.MousePos);

        if (_playAnimation) {
            _timer += Time.deltaTime * _playerManager.GunStats.gun.FireRate;
            if (_timer >= 1) {
                _timer = 1;
                _playAnimation = false;
            }
        }
        _lines.localEulerAngles = Vector3.Lerp(_lines.localEulerAngles, new Vector3(0, 0, 90), _timer);
    }

    private void OpenAim(float targetDistance) {
        _tipUp.localPosition = Vector2.Lerp(_tipUp.localPosition, new Vector2(0, targetDistance), _timer);
        _tipDown.localPosition = Vector2.Lerp(_tipDown.localPosition, new Vector2(0, -targetDistance), _timer);
        _tipRight.localPosition = Vector2.Lerp(_tipRight.localPosition, new Vector2(targetDistance, 0), _timer);
        _tipLeft.localPosition = Vector2.Lerp(_tipLeft.localPosition, new Vector2(-targetDistance, 0), _timer);
    }
    private void CloseAim(float targetDistance) {
        _tipUp.localPosition = Vector2.Lerp(_tipUp.localPosition, Vector2.zero, _timer);
        _tipDown.localPosition = Vector2.Lerp(_tipDown.localPosition, Vector2.zero, _timer);
        _tipRight.localPosition = Vector2.Lerp(_tipRight.localPosition, Vector2.zero, _timer);
        _tipLeft.localPosition = Vector2.Lerp(_tipLeft.localPosition, Vector2.zero, _timer);
    }

    void PlayAnimation() {
        _lines.localEulerAngles = Vector3.zero;
        _timer = 0;
        _playAnimation = true;
        _anim.SetTrigger("Shoot");
    }
    public void PlayAnimationClose() {
        _anim.SetTrigger("Close 0");
    }

    private void OnDisable() {
        EventManager.instance.RemoveAction("OnShoot", PlayAnimation);
        EventManager.instance.RemoveAction("OnCanShoot", PlayAnimationClose);
        ScreenManager.instance.RemovePausable(this);

    }

    public void Pause() {
        _anim.speed = 0;
        Cursor.visible = true;
        _crossHairGo.SetActive(false);
    }

    public void Resume() {
        _anim.speed = 1;
        Cursor.visible = false;
        _crossHairGo.SetActive(true);
    }
}
