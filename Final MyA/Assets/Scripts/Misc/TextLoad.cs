using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextLoad : MonoBehaviour {
    public TextMeshProUGUI localizedText;
    [SerializeField] Vector3 en_Pos;
    [SerializeField] Vector3 es_Pos;
    [SerializeField] string key;
    void OnEnable() {
        // Cargar el archivo de texto con las cadenas localizadas
        localizedText = GetComponent<TextMeshProUGUI>();

    }

    void Update() {
        // Si el archivo se ha cargado correctamente, actualizar el texto en pantalla
        // con el valor localizado correspondiente a la clave "hello"
        if (LocalizationManager.instance.GetIsReady()) {
            localizedText.text = LocalizationManager.instance.GetLocalizedValue(key);
        }

        switch (LocalizationManager.instance.currentLang) {
            case 0:
                ChangePos(en_Pos);
                break;
            default:
                ChangePos(es_Pos);
                break;

        }
    }

    private void ChangePos(Vector3 newPos) {
        localizedText.rectTransform.localPosition = newPos;
    }
}
