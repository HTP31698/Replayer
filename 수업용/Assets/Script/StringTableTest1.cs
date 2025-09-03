using UnityEngine;
using TMPro;

public class StringTableTest1 : MonoBehaviour
{
    public string id;
    public TextMeshProUGUI textMeshPro;

    private void Start()
    {
        var stringTable = DataTableManager.Get<StringTable>("String");
        textMeshPro.text = stringTable.Get(id);
    }

}
