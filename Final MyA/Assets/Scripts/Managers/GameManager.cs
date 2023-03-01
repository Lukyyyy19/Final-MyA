using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
using System;
public class GameManager : MonoBehaviour, IPausable {
    public static GameManager instance;
    public GunsPool _gunPool;
    public EnemyPool _enemyPool;
    public HashSet<Enemy> enemies;
    private int enemyCount;
    public Enemy[] _enemiesTypePrefab;
    [SerializeField]
    private ParticleSystem _particleEnemiesAppear;
    [SerializeField]
    public EventManager _eventManager;
    [SerializeField]
    public UIMenuManager _menuManagerUI;
    public bool isPaused = true;
    [SerializeField, Range(1, 50)]
    private int _width;
    [SerializeField, Range(1, 50)]
    private float _height;

    private float _timeelapsed;
    private TimeSpan _realTime;

    private void Awake() {
        instance = this;
        enemies = new HashSet<Enemy>();
        _eventManager.AddAction("CreateGuns", CreateGunsForEnemy);
        _menuManagerUI = GetComponent<UIMenuManager>();
        isPaused = false;
    }
    private void Start() {
        foreach (var enemy in _enemiesTypePrefab) {
            _enemyPool.IntantiateEnemys(enemy.name, enemy, 3);
        }
        ScreenManager.instance.AddPausable(this);
    }
    private void Update() {
        if (!isPaused)
            _timeelapsed += Time.deltaTime;
        _realTime = TimeSpan.FromSeconds(_timeelapsed);
        _menuManagerUI.TimeText = _realTime.ToString("mm':'ss");

        if (Input.GetButtonDown("Pause") && !_menuManagerUI.choosingAbility) {

            switch (isPaused) {
                case false:
                    ScreenManager.instance.Pause();
                    _menuManagerUI.ShowPauseMenu();
                    break;
                default:
                    _menuManagerUI.DeactivatePauseMenu();
                    ScreenManager.instance.Resume();
                    break;
            }
        }

        if (enemies.Count < GetEnemyAmount())
            InstantiateEnemies();

    }

    private void InstantiateEnemies() {
        if (isPaused) return;
        enemyCount++;
        Vector2 randPos;
        float x = UnityEngine.Random.Range(-_width, _width);
        float y = UnityEngine.Random.Range(-_height, _height);
        randPos = new Vector2(x, y);
        int randEnemy = UnityEngine.Random.Range(0, 3);
        enemies.Add(_enemyPool.Get(_enemiesTypePrefab[randEnemy].name, randPos));

    }


    int GetEnemyAmount() {
        int min = 2 + Mathf.FloorToInt(GetMinutes() / 2);
        int max = 4 + Mathf.FloorToInt(GetMinutes() / 1.5f);
        return UnityEngine.Random.Range(min, max + 1);
    }

    int GetMinutes() {
        return (int)_timeelapsed / 60;
    }

    IEnumerator DieStopTime() {
        Debug.Log("Stop TIme");
        ScreenManager.instance.Pause();
        yield return new WaitForSeconds(.05f);
        ScreenManager.instance.Resume();
    }

    public void CreateGunsForEnemy() {

        _gunPool.IntantiateGuns(GunsType.Pistol, 3);
    }
    private void OnDisable() {
        _eventManager.RemoveAction("CreateGuns", CreateGunsForEnemy);
    }

    public void RemoveEnemyFormHash(Enemy enemy) {
        if (enemies.Remove(enemy))
            Debug.Log("Removing Enemy");
    }
    public void Quit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    public void GameOver() {
        ScreenManager.instance.Pause();
        _menuManagerUI.ShowPauseMenu();
    }

    public void Pause() {
        isPaused = true;
    }

    public void Resume() {
        isPaused = false;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        Vector3 topLeft = new Vector3(-_width, _height);
        Vector3 topRight = new Vector3(_width, _height);
        Vector3 botRight = new Vector3(_width, -_height);
        Vector3 botLeft = new Vector3(-_width, -_height);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, botRight);
        Gizmos.DrawLine(botRight, botLeft);
        Gizmos.DrawLine(botLeft, topLeft);
    }
}
