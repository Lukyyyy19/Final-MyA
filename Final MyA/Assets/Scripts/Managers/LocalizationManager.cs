using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class LocalizationManager : MonoBehaviour {
    public static LocalizationManager instance;

    private Dictionary<string, string> localizedText;
    private bool isReady = false;
    private string missingTextString = "Localized text not found";
    private string filePath;
    public int currentLang;
    void Awake() {
        if (instance == null) {
            instance = this;
        }

        TextAsset temp = Resources.Load<TextAsset>("TextGameEs");
        if (!File.Exists(Application.persistentDataPath + "/TextGameEs.txt") || !File.Equals(Application.persistentDataPath + "/TextGameEs.txt", temp)) {
            File.WriteAllText(Application.persistentDataPath + "/TextGameEs.txt", temp.text);
        }
        filePath = Application.persistentDataPath + "/TextGameEs.txt";
        LoadLocalizedText();
        PlayerPrefs.SetInt("CurrentLang", 0);
        currentLang = PlayerPrefs.GetInt("CurrentLang");

    }

    public void LoadLocalizedText() {
        localizedText = new Dictionary<string, string>();
        if (File.Exists(filePath)) {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines) {
                string[] parts = line.Split('=');
                if (parts.Length == 2) {
                    localizedText.Add(parts[0], parts[1]);
                }
            }

            Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");
        } else {
            Debug.LogError("Cannot find file!");
        }

        isReady = true;
    }

    public string GetLocalizedValue(string key) {
        string result = missingTextString;
        if (localizedText.ContainsKey(key)) {
            switch (currentLang) {
                case 0:
                    result = key;
                    break;
                default:
                    result = localizedText[key];
                    break;
            }
        }

        return result;
    }
    public void UpdateLanguage(int value) {
        currentLang = value;
        PlayerPrefs.SetInt("CurrentLang", value);

    }
    public bool GetIsReady() {
        return isReady;
    }
}