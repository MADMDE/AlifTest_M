using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRobot : MonoBehaviour
{
    FirstPersonController player;
    private NavMeshAgent agent;

    const string PlayerTag = "Player";

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>();
    }

    private void Update()
    {
        if (!player)
            return;

        agent.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(PlayerTag))
        {
            GetComponent<EnemyHealth>().SelfDestruct();
        }
    }
}
