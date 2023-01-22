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
    private void Start() {

    }
    public void CreateGunsForEnemy() {
        Debug.Log("Creando armas enemgias");
        _gunPool.IntantiateGuns(GunsType.Pistol, 3);
    }
    private void OnDisable() {
        _eventManager.RemoveAction("CreateGuns", CreateGunsForEnemy);
    }
}
