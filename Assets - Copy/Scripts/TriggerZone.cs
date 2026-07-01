using UnityEngine;
using TMPro;

public class TriggerZone : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI statusText;

    [Header("Messages")]
    public string insideMessage = "Kamu MASUK area interaksi!";
    public string outsideMessage = "Kamu di luar area";

    [Header("Colors")]
    public Color insideColor = Color.green;
    public Color outsideColor = Color.white;

    // Akses global static agar bisa dicek secara real-time oleh script interaksi lain
    public static bool playerInsideTrigger = false;

    void Start()
    {
        UpdateUI(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = true;
            UpdateUI(true);
            Debug.Log("Player masuk Trigger Zone");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = false;
            UpdateUI(false);
            Debug.Log("Player keluar Trigger Zone");
        }
    }

    void UpdateUI(bool isInside)
    {
        if (statusText == null) return;
        statusText.text = isInside ? insideMessage : outsideMessage;
        statusText.color = isInside ? insideColor : outsideColor;
    }
}