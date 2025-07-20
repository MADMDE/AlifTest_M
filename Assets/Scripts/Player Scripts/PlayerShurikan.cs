using System;
using UnityEngine;

public class PlayerShurikan : MonoBehaviour
{
    [Header("Shurikan Properties:")]
    [SerializeField] int damage = 20;
    [SerializeField] float shootForce = 10;
    [SerializeField] float rotationSpeed = 200;

    [Header("References:")]
    [SerializeField] GameObject VFXDestruct;
    [SerializeField] GameObject VFXHit;

    private Rigidbody _rigidbody;
    private Camera _camera;

    private const string _NPCTag = "Enemy";
    private const string _PlayerTag = "Player";

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
        transform.position = _camera.transform.forward + _camera.transform.position;

        Shoot();
    }

    private void Shoot()
    {
        _rigidbody.AddForce(_camera.transform.forward * shootForce, ForceMode.Impulse);
    }

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.gameObject;

        if(go.CompareTag(_NPCTag))
        {
            NPCHealth npcHealth = go.GetComponent<NPCHealth>();
            npcHealth.TakeDamage(damage);
            CreateHitVFX();
            SelfDestruct();
        }
        else if(!go.CompareTag(_PlayerTag))
        {
            CreateDestructVFX();
            SelfDestruct();
        }
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }

    private void CreateDestructVFX()
    {
        Instantiate(VFXDestruct, transform.position, Quaternion.identity);
    }
    private void CreateHitVFX()
    {
        Instantiate(VFXHit, transform.position, Quaternion.identity);
    }

}
