using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{
    //Shoot FX
    [SerializeField] AudioClip _shootSFX;
    [SerializeField] ParticleSystem _shootFX;

    //Bullet impact FX
    [SerializeField] ObjectPool _impactFXSandPool;
    [SerializeField] ObjectPool _impactFXMetalPool;

    [SerializeField] float _shootRange;
    [SerializeField] float _shootFireDelay = 0.1f;
    [SerializeField] float _damage = 5f;
    private AudioSource _audioSource;

    public delegate void OnHit();
    public static event OnHit OnHitUnit;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _shootFX.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            Fire();
        }
    }

    private void Fire()
    {

        if (!_audioSource.isPlaying)
        {
            StartCoroutine(nameof(PlayShootFX));

            Hit();
        }
    }

    IEnumerator PlayShootFX()
    {
        _shootFX.gameObject.SetActive(true);
        _audioSource.Play();
        yield return new WaitForSeconds(_shootFireDelay);
        _shootFX.gameObject.SetActive(false);
        _audioSource.Stop();
    }  

    private void Hit()
    {
        RaycastHit hit;
        if(Physics.Raycast(gameObject.transform.position + transform.forward * 3.1f, transform.forward, out hit, _shootRange))
        {
            if (hit.collider.gameObject)
            {
                IDamageable damageTaker = hit.collider.gameObject.GetComponent<IDamageable>();
                GameObject _impactFX;

                if (damageTaker != null)
                {
                    damageTaker.TakeDamage(_damage);
                    _impactFX = _impactFXMetalPool.GetAvailableObject();
                    OnHitUnit?.Invoke();
                }
                else
                {
                    _impactFX = _impactFXSandPool.GetAvailableObject();
                }

                if (_impactFX != null)
                {
                    _impactFX.transform.position = transform.position + transform.TransformDirection(Vector3.forward) * hit.distance;
                    _impactFX.GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
                    _impactFX.gameObject.SetActive(true);
                }
            }


        }

    }
}
