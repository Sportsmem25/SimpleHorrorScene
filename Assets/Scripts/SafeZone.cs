using UnityEngine;

public class SafeZone : MonoBehaviour
{
    [SerializeField] private ControllerNPC npc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChaseManager.Instance?.StopChase();
            npc?.StopChasing();
        }
    }
}