using UnityEngine;

public class Cup : MonoBehaviour, IPickable
{
    public bool IsFilled => isFilled;
    
    [SerializeField] private Transform lidAttachPoint;
    [SerializeField] private Renderer liquidRenderer;
    [SerializeField] private ParticleSystem fillParticles;

    private Rigidbody rb;
    private Collider col;
    private bool isFilled = false;
    private bool hasLid = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        if (liquidRenderer) liquidRenderer.enabled = false;
    }

    public GameObject OnPick(Transform player, Transform holdPoint)
    {
        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
        rb.isKinematic = true;
        col.enabled = false;
        return gameObject;
    }

    public void Fill()
    {
        if (isFilled) return;
        isFilled = true;
        if (liquidRenderer)
            liquidRenderer.enabled = true;
    }

    public void Attachlid(Lid lid)
    {
        if (hasLid) return;

        hasLid = true;
        lid.transform.SetParent(lidAttachPoint != null ? lidAttachPoint : null);
        lid.transform.localPosition = Vector3.zero;
        lid.transform.localRotation = Quaternion.identity;
        lid.SetPhysics(false);
    }
}