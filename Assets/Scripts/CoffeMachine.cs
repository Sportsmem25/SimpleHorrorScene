using System.Collections;
using UnityEngine;

public class CoffeMachine : MonoBehaviour
{
    [SerializeField] private Transform cupSlot;
    [SerializeField] private AudioClip fillSound;
    [SerializeField] private ParticleSystem psCoffe;
    private float fillTime = 20f;
    private bool isBusy = false;
    private Rigidbody rb;
    private Collider col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public bool TryStartFill(GameObject cupObj, System.Action onFinished)
    {
        if (isBusy) return false;

        var cup = cupObj.GetComponent<Cup>();
        if (cup == null || cup.IsFilled) return false;
        StartCoroutine(FillCoroutine(cup, onFinished));
        return true;
    }

    private IEnumerator FillCoroutine(Cup cup, System.Action onFinished)
    {
        isBusy = true;
        if (rb) rb.isKinematic = true;
        if (col) col.enabled = false;

        cup.transform.SetParent(cupSlot, false);
        cup.transform.localPosition = Vector3.zero;
        cup.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);

        // Play sound
        if (fillSound) AudioSource.PlayClipAtPoint(fillSound, transform.position);
        if (psCoffe != null)
        {
            psCoffe.Play();
        }

        yield return new WaitForSeconds(fillTime);

        cup.Fill();
        psCoffe.Stop();

        if (col) col.enabled = true;

        isBusy = false;
        onFinished?.Invoke();
    }
}
