using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntelligentBullet : Bullet {
    [SerializeField]
    int enemiesCollide;
    [SerializeField]
    Transform closestEnemy;
    [SerializeField]
    float _radius;
    [SerializeField]
    LayerMask _enemyLayer;
    [SerializeField]
    Collider2D _currentEnemy;
    bool goToEnemy;
    protected override void Update() {
        if (!move) return;
        if (!goToEnemy)
            transform.position += direction * 2 * Time.deltaTime;
        else
            transform.position += (direction - transform.position) * 5 * Time.deltaTime;
        timer = timer + 1 * Time.deltaTime;
        if (timer >= destroyTime) {
            TimeCompleted();
        }
        // transform.position = direction - transform.position * currentSpeed * Time.deltaTime;

    }
    private void OnEnable() {

        StartCoroutine("GoToEnemy");

    }

    IEnumerator GoToEnemy() {
        var enimiesNear = Physics2D.OverlapCircleAll(transform.position, _radius, _enemyLayer);
        closestEnemy = GetClosestEnemy(enimiesNear);
        yield return new WaitForSeconds(.5f);
        direction = closestEnemy.transform.position.normalized;
        goToEnemy = true;
    }
    // protected override void OnTriggerEnter2D(Collider2D other) {
    //     // if (other.GetComponent<IDamageable>() != null) {
    //     //     _currentEnemy = other;
    //     //     other.GetComponent<IDamageable>().TakeDamage((int)damage);
    //     // }
    //     // if (enemiesCollide < 1) {
    //     //     enemiesCollide++;
    //     //     currentSpeed = 3;

    //     // } else {
    //     var enimiesNear = Physics2D.OverlapCircleAll(transform.position, _radius, _enemyLayer);
    //     closestEnemy = GetClosestEnemy(enimiesNear);
    //     InstantiateBullets.instance.bulletPool.Get("Intelligent Bullets", transform.position, Vector2.Reflect(direction, closestEnemy.transform.position - transform.position));
    //     base.OnTriggerEnter2D(other);


    // }


    Transform GetClosestEnemy(Collider2D[] enemies) {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Collider2D potentialTarget in enemies) {
            if (potentialTarget != _currentEnemy && potentialTarget.CompareTag("Enemy")) {
                Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr) {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget.transform;
                }
            }
        }

        return bestTarget;
    }

    protected override void ResetStats() {
        enemiesCollide = 0;
        _currentEnemy = null;
        goToEnemy = false;
        base.ResetStats();
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);

    }
}
