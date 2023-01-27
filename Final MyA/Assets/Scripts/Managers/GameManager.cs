using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public GunsPool _gunPool;
    public HashSet<Enemy> enemies;
    [SerializeField]
    public EventManager _eventManager;
    private void Awake() {
        instance = this;
        enemies = new HashSet<Enemy>();
        _eventManager.AddAction("CreateGuns", CreateGunsForEnemy);
    }

    IEnumerator DieStopTime() {
        Debug.Log("Stop TIme");
        ScreenManager.instance.Pause();
        yield return new WaitForSeconds(.05f);
        ScreenManager.instance.Resume();
    }

    public void CreateGunsForEnemy() {
        Debug.Log("Creando armas enemgias");
        _gunPool.IntantiateGuns(GunsType.Pistol, 3);
    }
    private void OnDisable() {
        _eventManager.RemoveAction("CreateGuns", CreateGunsForEnemy);
    }
}
