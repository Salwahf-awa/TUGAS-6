using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Materials")]
    public Material normalMaterial;
    public Material highlightMaterial;

    [Header("Interaction Settings")]
    public bool canInteract = true;           
    public bool requireTriggerZone = false;   // Centang ini khusus untuk objek bonus Anggota 5
    public string interactMessage = "Tekan [E] untuk interact";
    public string lockedMessage = "🔒 Masuk zona dulu!";
    public string interactedMessage = "✅ Sukses di-interact!";

    private MeshRenderer meshRenderer;
    private bool hasInteracted = false;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (normalMaterial != null && meshRenderer != null)
        {
            meshRenderer.material = normalMaterial;
        }
    }

    // Fungsi untuk mengubah warna saat dilihat oleh Raycast (Tugas Anggota 3)
    public void OnHighlight(ref string textToDisplay)
    {
        // Jika objek butuh trigger zone tapi player berada di luar (Tugas Anggota 5)
        if (requireTriggerZone && !TriggerZone.playerInsideTrigger)
        {
            if (meshRenderer != null && normalMaterial != null)
                meshRenderer.material = normalMaterial;

            textToDisplay = lockedMessage;
            return;
        }

        // Mengubah warna menjadi highlight (Tugas Anggota 3)
        if (meshRenderer != null && highlightMaterial != null)
            meshRenderer.material = highlightMaterial;

        textToDisplay = hasInteracted ? interactedMessage : interactMessage;
    }

    // Fungsi untuk mengembalikan warna saat pandangan beralih (Tugas Anggota 3)
    public void OnUnhighlight()
    {
        if (meshRenderer != null && normalMaterial != null)
        {
            meshRenderer.material = normalMaterial;
        }
    }

    // Fungsi aksi saat tombol E ditekan (Tugas Anggota 4)
    public void OnInteract()
    {
        if (requireTriggerZone && !TriggerZone.playerInsideTrigger)
        {
            Debug.Log("Interaksi ditolak: Player belum berada di dalam Trigger Zone!");
            return;
        }

        if (!canInteract || hasInteracted) return;

        hasInteracted = true;
        Debug.Log("Berhasil interaksi dengan: " + gameObject.name);

        // Efek visual feedback mikro (skala membesar sejenak)
        StartCoroutine(PulseEffect());
    }

    System.Collections.IEnumerator PulseEffect()
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale = originalScale * 1.15f;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = originalScale;
    }
}