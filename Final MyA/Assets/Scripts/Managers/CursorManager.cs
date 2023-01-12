using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour {

    [SerializeField]
    private PlayerManager _playerManager;


    [SerializeField]
    Texture2D[] _mouseTexArray;

    [SerializeField]
    private int frameCount;
    [SerializeField]
    private float frameRate;

    private int currentFrame;
    private float frameTimer;

    private bool _playAnimation;
    void Start() {
        Cursor.SetCursor(_mouseTexArray[0], Vector2.one * _mouseTexArray[0].width / 2, CursorMode.Auto);
    }

    private void Update() {
        frameRate = _playerManager.gun.FireRate * .13f;
        if (_playAnimation)
            CursorAnimation();
    }

    private void CursorAnimation() {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0) {
            frameTimer += frameRate;
            currentFrame = (currentFrame + 1) % frameCount;
            Cursor.SetCursor(_mouseTexArray[currentFrame], Vector2.one * _mouseTexArray[currentFrame].width / 2, CursorMode.Auto);
            _playAnimation = currentFrame > 0;


        }
    }

    void PlayAnimation() {
        _playAnimation = true;
    }

    private void OnEnable() {
        _playerManager.OnShoot += PlayAnimation;
    }
    private void OnDisable() {
        _playerManager.OnShoot -= PlayAnimation;
    }
}
