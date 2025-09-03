using TMPro;
using UnityEngine;

public class CSVTest2 : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Start()
    {
        var stringTable = new StringTable();
        stringTable.Load("StringTableKr");
        text.text = stringTable.Get("HELLO");
    }
}
