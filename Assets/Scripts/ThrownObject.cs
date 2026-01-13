using UnityEngine;

public class ThrownObject : MonoBehaviour
{
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private GameObject splashPrefab;
    private bool isUsed = false;

    public void Initializate(LayerMask layer, GameObject go)
    {
        hitLayer = layer;
        splashPrefab = go;
        Destroy(gameObject, 6f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isUsed) return;

        if (((1 << collision.gameObject.layer) & hitLayer) != 0)
        {
            var npc = collision.gameObject.GetComponentInParent<ControllerNPC>();
            if (npc != null)
                npc.OnHitByCoffe();
        }
        if (splashPrefab)
        {
            Instantiate(splashPrefab, transform.position, Quaternion.identity);
            isUsed = true;
            Destroy(gameObject, 0.5f);
        }
    }
}