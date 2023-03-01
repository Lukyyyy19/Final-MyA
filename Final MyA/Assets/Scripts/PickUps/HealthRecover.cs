using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRecover : MonoBehaviour, IPickeupable {
    public void OnPickUp(PlayerManager player) {
        player.RecoverHealth();
        Destroy(gameObject);
    }
}
