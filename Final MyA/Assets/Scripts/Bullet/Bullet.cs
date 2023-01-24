using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour, IPausable {
    public float speed = 10;
    public float currentSpeed;
    public float damage = 3;
    string key;
    float timer;
    public float destroyTime = 2f;
    private Vector3 direction;
    Action<string, Bullet> notify;

    protected delegate void TimeToDestory();
    protected event TimeToDestory OnTime;

    public void Configure(string _key, Action<string, Bullet> notify) {
        key = _key;
        ScreenManager.instance.AddPausable(this);
        currentSpeed = speed;
        this.notify = notify;
    }
    public void Move(Vector3 pos, Vector3 dir) {
        transform.position = pos;
        direction = dir.normalized;
    }
    bool move;
    public virtual void Prender() {
        move = true;
    }
    public virtual void Apagar() {
        move = false;
    }

    protected virtual void Update() {
        if (!move) return;
        transform.position = transform.position + direction * currentSpeed * Time.deltaTime;

        timer = timer + 1 * Time.deltaTime;
        if (timer > destroyTime) {
            timer = 0;
            notify.Invoke(key, this);
            OnTime?.Invoke();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.name);
        notify.Invoke(key, this);
        if (other.GetComponent<IDamageable>() != null) {
            other.GetComponent<IDamageable>().TakeDamage((int)damage);
        }
    }


    public void Pause() {
        move = false;
    }

    public void Resume() {
        move = true;
    }
}
