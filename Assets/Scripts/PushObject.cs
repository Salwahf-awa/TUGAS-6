using UnityEngine;
using TMPro;

public class PushObject : MonoBehaviour
{
    [Header("Push Settings")]
    public float pushForce = 400f;
    public float pushDistance = 5f;
    public LayerMask pushableLayer;

    [Header("UI Reference")]
    public TextMeshProUGUI pushHintText;

    void Update()
    {
        HandlePushInput();
        HandlePushHint();
    }

    void HandlePushInput()
    {
        // Klik Kiri Mouse (0)
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, pushDistance, pushableLayer))
            {
                Rigidbody rb = hit.rigidbody;
                if (rb != null)
                {
                    // Menghitung arah dorongan dari posisi kamera ke titik hantaman raycast
                    Vector3 pushDir = hit.point - transform.position;
                    pushDir.y = 0; // Mencegah objek terdorong terbang ke langit
                    pushDir.Normalize();

                    rb.AddForce(pushDir * pushForce);
                    Debug.Log("Mendorong objek: " + hit.collider.name);
                }
            }
        }
    }

    void HandlePushHint()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pushDistance, pushableLayer))
        {
            if (pushHintText != null && hit.rigidbody != null)
            {
                pushHintText.text = "🖱️ Klik mouse untuk mendorong";
            }
        }
        else
        {
            // Auto-clear: Menghapus teks petunjuk dorong jika pandangan beralih ke area lain
            if (pushHintText != null && pushHintText.text == "🖱️ Klik mouse untuk mendorong")
            {
                pushHintText.text = "";
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * pushDistance);
    }
}