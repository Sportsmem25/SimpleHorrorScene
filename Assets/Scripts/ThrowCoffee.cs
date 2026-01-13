using UnityEngine;

public class ThrowCoffee : MonoBehaviour
{
    [SerializeField] private LayerMask hitLayer;

    private float throwForce = 10f;
    private float tossUp = 1f;
    private PlayerInteraction playerInteraction;
    private GameObject splashObject;

    private void Start()
    {
        playerInteraction = GetComponentInParent<PlayerInteraction>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var held = playerInteraction.GetHeldObject();
            Debug.Log(held);
            if (held == null) return;

            var cup = held.GetComponent<Cup>();
            if (cup != null && cup.IsFilled)
            {
                Throw(held);
                playerInteraction.SetHeldObject(null);
            }
        }
    }

    private void Throw(GameObject obj)
    {
        obj.transform.SetParent(null);
        var rb = obj.GetComponent<Rigidbody>();
        if (!rb) rb = obj.AddComponent<Rigidbody>();
        rb.isKinematic = false;

        var col = obj.GetComponent<Collider>();
        if (col) col.enabled = true;

        Vector3 dir = (Camera.main.transform.forward + Vector3.up * (tossUp * 0.1f)).normalized;
        rb.AddForce(dir * throwForce, ForceMode.VelocityChange);
        var thrown = obj.AddComponent<ThrownObject>();
        thrown.Initializate(hitLayer, splashObject);
    }
}