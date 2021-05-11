using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{
    [SerializeField] AudioClip _shootSFX;
    [SerializeField] float _shootSFXVolume = 1;
    [SerializeField] float _shootFireDelay = 0.1f;

    [SerializeField] ParticleSystem _shootFX;
    [SerializeField] ObjectPool _impactFXPool;

    //[SerializeField] ParticleSystem _impactFX;

    [SerializeField] float _shootRange;
    private AudioSource _audioSource;

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

            HitEffects();


        }
    }

    IEnumerator PlayShootFX()
    {
        _shootFX.gameObject.SetActive(true);
        _audioSource.PlayOneShot(_shootSFX, _shootSFXVolume);
        yield return new WaitForSeconds(_shootFireDelay);
        _shootFX.gameObject.SetActive(false);
        _audioSource.Stop();
    }  

    private void HitEffects()
    {
        RaycastHit hit;
        if(Physics.Raycast(gameObject.transform.position + transform.forward * 3.1f, transform.forward, out hit, _shootRange))
        {
            //Debug: find hit object
            if (hit.collider.gameObject)
            {
                Debug.Log(hit.collider.gameObject.name);
            }
            GameObject _impactFX = _impactFXPool.GetAvailableObject();
            if (_impactFX != null)
            {
                _impactFX.transform.position = transform.position + transform.TransformDirection(Vector3.forward) * hit.distance;
                _impactFX.gameObject.SetActive(true);
            }
        }

    }
}
