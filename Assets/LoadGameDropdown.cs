using System.IO;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class LoadGameDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    void Start()
    {
        dropdown.gameObject.SetActive(false);
    }

    public void ShowDropdown()
    {
        dropdown.ClearOptions();

        List<string> names = new List<string>();

        string[] files = Directory.GetFiles(
        Application.persistentDataPath,
        "save_*.json"
        );

        if (files.Length == 0)
        {
            Debug.Log("Tidak ada save data");
            return;
        }

        foreach (string file in files)
        {
            string name = Path.GetFileNameWithoutExtension(file)
                .Replace("save_", "");
            names.Add(name);
        }

        dropdown.AddOptions(names);

        dropdown.value = 0;
        dropdown.RefreshShownValue();

        // 🔥 AUTO CONFIRM
        ConfirmSelection();

        dropdown.gameObject.SetActive(true);
    }

    public void OnValueChanged(int index)
    {
        ConfirmSelection();
    }

    void ConfirmSelection()
    {
        string selectedName = dropdown.options[dropdown.value].text;

        GManager.instance.LoadPlayer(selectedName);

        Debug.Log("CONFIRMED player: " + selectedName);
    }
}
