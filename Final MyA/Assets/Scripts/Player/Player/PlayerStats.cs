using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour {

    [SerializeField]
    PlayerManager _playerManager;

    [SerializeField]
    private TextMeshProUGUI _text;
    public TextMeshProUGUI _textName;
    public TextMeshProUGUI _textFireRate;
    public TextMeshProUGUI _textDamage;
    public TextMeshProUGUI _textSpread;

    void Start() {
        _playerManager = GetComponent<PlayerManager>();
        Shoot(_playerManager.gun.Ammo);

    }

    private void Update() {
        _textDamage.text = _playerManager.gun.Damage.ToString();
        _textFireRate.text = _playerManager.gun.FireRate.ToString();
        _textName.text = _playerManager.gun.Name.ToString();
        _textSpread.text = _playerManager.gun.Spread.ToString();
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
