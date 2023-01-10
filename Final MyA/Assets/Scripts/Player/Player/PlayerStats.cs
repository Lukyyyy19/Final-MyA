using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour {

    [SerializeField]
    PlayerManager _playerManager;

    [SerializeField]
    private TextMeshProUGUI _text;
    void Start() {
        _playerManager = GetComponent<PlayerManager>();
        Shoot(_playerManager.gun.Ammo);

    }

    private void Shoot(int bullet) {
        _text.text = bullet.ToString();
    }

    private void OnEnable() {
        _playerManager.OnShoot += Shoot;
    }
    private void OnDisable() {
        _playerManager.OnShoot -= Shoot;
    }
}
