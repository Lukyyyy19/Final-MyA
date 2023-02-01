using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Image _reloadTime;


    public TextMeshProUGUI attachNameCurr;
    public TextMeshProUGUI attachNameNew;

    public GameObject canvasUpgrade;







    void Awake() {
        _playerManager = GetComponent<PlayerManager>();
    }
    private void Start() {
        EventManager.instance.AddAction("OnNewLevel", NewLevelReached);
    }

    private void Update() {
        UpdateAmmo(_playerManager.gun.Ammo);
        _textDamage.text = _playerManager.gun.Damage.ToString();
        _textFireRate.text = _playerManager.gun.FireRate.ToString();
        _textName.text = _playerManager.gun.Name.ToString();
        _textSpread.text = _playerManager.gun.Spread.ToString();
        _textReloadTime.text = _playerManager.gun.ReloadTime.ToString();
        _reloadTime.fillAmount = _playerManager.reloadTimer / _playerManager.reloadTimerStart;
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

    public void NewLevelReached() {
        canvasUpgrade.SetActive(true);
    }



    public void UpdateNewPanel(MiddlePartSO gun) {

        attachNameNew.text = gun.gunpart.ToString();

    }
    public void UpdateNewPanel(CannonSO gun) {

        attachNameNew.text = gun.gunpart.ToString();
    }
    public void UpdateNewPanel(StockSO gun) {

        attachNameNew.text = gun.gunpart.ToString();
    }

    public void UpdateCurrentPanel(GunStats gun) {
        attachNameCurr.text = gun._currentCannon.name;
    }
}
