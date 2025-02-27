using UnityEngine;
using TMPro;

public class DialogueLine : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int duration = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
}
