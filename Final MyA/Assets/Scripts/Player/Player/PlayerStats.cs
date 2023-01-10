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
    public TextMeshProUGUI _textReloadTime;

    void Start() {
        _playerManager = GetComponent<PlayerManager>();
        UpdateAmmo(_playerManager.gun.Ammo);

    }

    private void Update() {
        _textDamage.text = _playerManager.gun.Damage.ToString();
        _textFireRate.text = _playerManager.gun.FireRate.ToString();
        _textName.text = _playerManager.gun.Name.ToString();
        _textSpread.text = _playerManager.gun.Spread.ToString();
        _textReloadTime.text = _playerManager.gun.ReloadTime.ToString();
    }

    private void UpdateAmmo(int bullet) {
        _text.text = bullet.ToString();
    }

    private void OnEnable() {
        _playerManager.OnUpdateAmmo += UpdateAmmo;
    }
    private void OnDisable() {
        _playerManager.OnUpdateAmmo -= UpdateAmmo;
    }
}
