using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GunsPool _gunPool;
    public HashSet<Enemy> enemies;
    private void Awake()
    {
        instance = this;
        enemies = new HashSet<Enemy>();
    }
    private void OnEnable()
    {
        EventManager.instance.AddAction("CreateGuns", CreateGunsForEnemy);
    }
    public void CreateGunsForEnemy()
    {
        Debug.Log("Creando armas enemgias");
        _gunPool.IntantiateGuns(GunsType.Pistol, 3);
    }
    private void OnDisable()
    {
        EventManager.instance.RemoveAction("CreateGuns", CreateGunsForEnemy);
    }
}
