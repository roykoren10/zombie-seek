using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Gun : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform cam;

    public ParticleSystem muzzleFlash;

    public AudioSource ourAudioSource;
    public AudioClip audioClipShoot;
    public AudioClip audioClipReload;

    [SerializeField] private Animator myGunAnim;


    float timeSinceLastShot;

    private void Start()
    {
        // Functions to handle shoot reload
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;

        ourAudioSource = GetComponent<AudioSource>();
        Assert.IsNotNull(ourAudioSource);
        Assert.IsNotNull(audioClipShoot);
        Assert.IsNotNull(audioClipReload);

        myGunAnim = GetComponent<Animator>();
    }

    private void OnDisable() => gunData.reloading = false;
    public int CurrentAmmo() => gunData.currentAmmo;
    public int MagSize() => gunData.magSize;

    public void StartReload()
    {
        if (!gunData.reloading && this.gameObject.activeSelf)
            StartCoroutine(Reload());
    }


    private IEnumerator Reload()
    {
        gunData.reloading = true;
        ourAudioSource.PlayOneShot(audioClipReload);
        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    private void Shoot()
    {
   
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                muzzleFlash.Play();
                myGunAnim.SetBool("Active", true);
                ourAudioSource.PlayOneShot(audioClipShoot);
                var ray = new Ray(cam.position, cam.forward);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    Debug.Log(hitInfo.transform.name);
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);
                    if (damageable != null)
                    {
                        Debug.Log(damageable);
                    }
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
        
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(cam.position, cam.forward * gunData.maxDistance);
        if (!Input.GetMouseButton(0))
            myGunAnim.SetBool("Active", false);
    }

    private void OnGunShot() { }
}
