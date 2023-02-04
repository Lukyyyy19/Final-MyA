using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyPool : MonoBehaviour {
    Dictionary<string, PoolObject<Enemy>> pools = new Dictionary<string, PoolObject<Enemy>>();


    public void IntantiateEnemys(string _key, Enemy enemyType, int prewarm) {
        Debug.Log("CreandoEnemigos");
        var enemyPoolTemp = new GameObject();
        enemyPoolTemp.name = "Pool de " + _key;
        Func<Enemy> EnemyFunc = () => {
            Enemy enemy = GameObject.Instantiate(enemyType, Vector2.zero, Quaternion.identity);
            enemy.transform.parent = enemyPoolTemp.transform;
            enemy.Configure(_key, Return);
            return enemy;
        };

        if (pools != null) {
            if (!pools.ContainsKey(_key)) {
                PoolObject<Enemy> currentPool = new PoolObject<Enemy>();
                currentPool.Intialize(TurnOnEnemy, TurnOffEnemy, EnemyFunc, prewarm);
                pools.Add(_key, currentPool);
            }
        }
    }

    public Enemy Get(string _key, Vector2 pos) {
        Enemy myEnemy = pools[_key].Get();
        myEnemy.SetInitalPos(pos);
        StartCoroutine(myEnemy.OnAppear());
        return myEnemy;
    }


    public void Return(string _key, Enemy obj) {
        pools[_key].Return(obj);
    }


    void TurnOnEnemy(Enemy enemy) {
        enemy.gameObject.SetActive(true);
        enemy.Reactivate();
    }

    void TurnOffEnemy(Enemy enemy) {
        enemy.gameObject.SetActive(false);
        //Enemy.Apagar();
    }
}
