using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riffle : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint = default;
    [SerializeField] private GameObject bullet = default;
    [SerializeField] private GameObject bulletSoundPrefab = default;
    [SerializeField] private float bulletSpawnCD = .1f;
    [SerializeField] private float bulletForce = 20f;

    [SerializeField] private Transform grenadeSpawnPoint = default;
    [SerializeField] private GameObject grenade = default;
    [SerializeField] private GameObject grenadeSoundPrefab = default;
    [SerializeField] private float grenadeSpawnCD = 1f;
    [SerializeField] private float grenadeForce = 20f;

    [SerializeField] private Transform laserSpawnPoint = default;
    [SerializeField] private GameObject laserSoundPrefab = default;

    [SerializeField] private float destroyDelay = 5f;

    private bool isShootingBullets = false;
    private bool isShootingGrenades = false;
    private bool isShootingLasers = false;


    private void Start()
    {
        StartCoroutine(nameof(ShootBullets));
        StartCoroutine(nameof(ShootGrenades));
        StartCoroutine(nameof(ShootLasers));
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            isShootingBullets = true;
        if (Input.GetButtonDown("Fire2"))
            isShootingGrenades = true;
        if (Input.GetButtonDown("Fire3"))
            isShootingLasers = true;
        if (Input.GetButtonUp("Fire1"))
            isShootingBullets = false;
        if (Input.GetButtonUp("Fire2"))
            isShootingGrenades = false;
        if (Input.GetButtonUp("Fire3"))
            isShootingLasers = false;
    }

    private IEnumerator ShootBullets()
    {
        while (true)
        {
            if (isShootingBullets)
            {
                GameObject tmp = Instantiate(bullet, bulletSpawnPoint.position, transform.rotation);
                tmp.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * bulletForce, ForceMode.Impulse);
                GameObject sound = Instantiate(bulletSoundPrefab, transform.position, Quaternion.identity, transform);
                sound.GetComponent<AudioSource>().volume += Random.Range(-0.1f, 0.1f);
                sound.GetComponent<AudioSource>().pitch += Random.Range(-0.1f, 0.1f);
                sound.GetComponent<AudioSource>().Play();
                Destroy(tmp, destroyDelay);
                Destroy(sound, 5f);
                yield return new WaitForSeconds(bulletSpawnCD);
            }
            yield return null;
        }
    }
    
    private IEnumerator ShootGrenades()
    {
        while (true)
        {
            if (isShootingGrenades)
            {
                GameObject tmp = Instantiate(grenade, grenadeSpawnPoint.position, transform.rotation);
                tmp.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * grenadeForce, ForceMode.Impulse);
                GameObject sound = Instantiate(grenadeSoundPrefab, transform.position, Quaternion.identity, transform);
                sound.GetComponent<AudioSource>().volume += Random.Range(-0.1f, 0.1f);
                sound.GetComponent<AudioSource>().pitch += Random.Range(-0.1f, 0.1f);
                sound.GetComponent<AudioSource>().Play();
                Destroy(tmp, destroyDelay);
                Destroy(sound, 5f);
                yield return new WaitForSeconds(grenadeSpawnCD);
            }
            yield return null;
        }
    }

    private IEnumerator ShootLasers()
    {
        yield return null;
        //while (true)
        //{
        //    if (isShootingBullets)
        //    {
        //        GameObject tmp = Instantiate(ball, transform.position, transform.rotation);
        //        tmp.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);
        //        GameObject sound = Instantiate(soundPrefab, transform.position, Quaternion.identity, transform);
        //        sound.GetComponent<AudioSource>().volume += Random.Range(-0.1f, 0.1f);
        //        sound.GetComponent<AudioSource>().pitch += Random.Range(-0.1f, 0.1f);
        //        sound.GetComponent<AudioSource>().Play();
        //        Destroy(tmp, destroyDelay);
        //        Destroy(sound, 5f);
        //        yield return new WaitForSeconds(spawnCD);
        //    }
        //    yield return null;
        //}
    }

}
