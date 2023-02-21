using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerVisuals {
    SpriteRenderer _sr;
    Transform _playerTransform;
    public UpdatePlayerVisuals(SpriteRenderer sr, Transform playerTrasnsfrom) {
        _sr = sr;
        _playerTransform = playerTrasnsfrom;
    }

    public void UpdateVisualHealth(float currentHealth, float maxHealth) {
        float healthVisual = currentHealth / maxHealth;
        _sr.material.SetFloat("_Health", healthVisual);
    }


    public Color GetHealthColor() {
        var damagedColor = _sr.material.GetColor("_damaged");
        var fullHealthColor = _sr.material.GetColor("_fullHealth");
        var t = _sr.material.GetFloat("_Health");
        return Color.Lerp(damagedColor, fullHealthColor, t);
    }

    public void FlipPlayer(float xValue) {
        Vector3 localScale = _playerTransform.localScale;
        if (xValue < 0) localScale.x = Mathf.Abs(localScale.x) * -1;
        else if (xValue > 0) localScale.x = Mathf.Abs(localScale.x);
        _playerTransform.localScale = localScale;
    }
}
