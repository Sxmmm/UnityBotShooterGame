using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletEmitter;
    public GameObject bulletPrefab;
    public float damage = 10.0f;
    public float range = 100.0f;
    public float critPercent = 30.0f;
    public float critMultiplier = 1.75f;
    public GunType typeOfGun;
    public float fireRate = 0.2f;

    private float timer = 0.0f;

    [SerializeField]
    private Camera Cam;

    private void Update()
    {
        if (typeOfGun == GunType.AUTOMATIC)
        {
            if (Input.GetButton("Fire1"))
            {
                timer += Time.deltaTime;
            }
            if (timer >= fireRate)
            {
                shoot();
                timer = 0.0f;
            }
        }

        if (typeOfGun == GunType.SOLO)
        {
            if (timer <= 0.0f)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    shoot();
                    timer = fireRate;
                }
            }
            if (timer >= -5.0f)
            {
                timer -= Time.deltaTime;
            }
        }


    }

    private void shoot()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range, layerMask))
        {
            targetPoint = hit.point;

            // TargetScript Target = hit.transform.GetComponent<TargetScript>();
            // if (Target != null)
            // {
            //     int critRand = Random.Range(0, 100);
            //     if (critRand <= critPercent)
            //     {
            //         Target.TakeDamage(damage * critMultiplier);
            //     }
            //     else
            //     {
            //         Target.TakeDamage(damage);
            //     }
            // }

            ///Impact Effect
            //GameObject imapctGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(imapctGO, 2.0f);
            // Debug.Log(hit.transform.gameObject.name);
        }
        else
        {
            targetPoint = Cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0)).GetPoint(range);
        }

        var bullet = Instantiate(bulletPrefab, bulletEmitter.transform.position, bulletEmitter.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = (targetPoint - bulletEmitter.transform.position).normalized * 30;
    }
}

public enum GunType
{
    SOLO,
    AUTOMATIC
}
