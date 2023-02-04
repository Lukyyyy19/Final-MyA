using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
public abstract class Entity : MonoBehaviour, IDamageable, IPausable {

    protected float _speed;
    [SerializeField]
    protected float _maxSpeed;
    protected Rigidbody2D _rb;

    [SerializeField]
    protected int _health;

    [SerializeField]
    protected int _maxHealth;

    [SerializeField]
    protected Transform _hand;

    [SerializeField]
    protected bool paused;

    protected Collider2D colldier;

    protected SpriteRenderer sr;
    private Color _mainColor;

    public bool isMoving;

    protected virtual void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _health = _maxHealth;
        _speed = _maxSpeed;
        colldier = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        _mainColor = sr.color;
    }
    protected virtual void Start() {
        ScreenManager.instance.AddPausable(this);
    }


    public virtual void TakeDamage(int damage) {
        _health -= damage;
        sr.color = Color.white;
        Invoke("ResetColor", 0.05f);
        if (_health <= 0) {
            Die();
        }
    }
    protected virtual void Die() {
        gameObject.SetActive(false);
    }


    protected void ResetColor() {
        sr.color = _mainColor;
    }

    protected void Move(Vector2 dir) {
        _rb.velocity = dir.normalized * _speed;
        isMoving = _rb.velocity.magnitude > 0;

    }


    protected virtual void OnDisable() {
        ScreenManager.instance.RemovePausable(this);
    }

    public virtual void Pause() {
        _rb.velocity = Vector2.zero;
        _speed = 0;
        _rb.isKinematic = true;
        paused = true;

    }

    public virtual void Resume() {
        _speed = _maxSpeed;
        _rb.isKinematic = false;
        paused = false;

    }
}
