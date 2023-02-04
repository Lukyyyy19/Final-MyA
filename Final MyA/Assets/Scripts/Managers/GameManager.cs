using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
using System;
public class GameManager : MonoBehaviour {
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

    private void Awake() {
        instance = this;
        enemies = new HashSet<Enemy>();
        _eventManager.AddAction("CreateGuns", CreateGunsForEnemy);
        _menuManagerUI = GetComponent<UIMenuManager>();
        isPaused = false;
    }
    private void Start() {
        _enemyPool.IntantiateEnemys("Shooter", _enemiesTypePrefab[0], 3);
        _enemyPool.IntantiateEnemys("Melee", _enemiesTypePrefab[1], 3);
    }
    private void Update() {
        isPaused = ScreenManager.instance.isPaused;
        if (Input.GetButtonDown("Pause")) {

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
        if (enemies.Count < 3)
            //StartCoroutine("InstantiateEnemies");
            InstantiateEnemies();


    }

    private void InstantiateEnemies() {
        if (isPaused) return;
        enemyCount++;
        Vector2 randPos;
        float x = UnityEngine.Random.Range(-_width, _width);
        float y = UnityEngine.Random.Range(-_height, _height);
        randPos = new Vector2(x, y);
        var randEnemy = UnityEngine.Random.Range(1, 3);
        // var ps = Instantiate(_particleEnemiesAppear, randPos, Quaternion.identity);
        // yield return null/*new WaitUntil(() => ps.isStopped);*/;
        switch (randEnemy) {
            case 1:
                enemies.Add(_enemyPool.Get("Shooter", randPos));
                break;
            default:
                enemies.Add(_enemyPool.Get("Melee", randPos));
                break;
        }
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
