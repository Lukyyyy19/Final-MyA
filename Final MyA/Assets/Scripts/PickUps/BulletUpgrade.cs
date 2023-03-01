using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUpgrade : MonoBehaviour, IPickeupable {
    [SerializeField]
    string _bulletType = "Disperse Bullets";
    [SerializeField]
    float _timeToRestart = 3;
    public void OnPickUp(PlayerManager player) {
        StartCoroutine(player.GunStats.UpgradeGunBulletType(_bulletType, _timeToRestart));
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        Destroy(gameObject, _timeToRestart + .5f);
        //gameObject.SetActive(false);

    }
}
