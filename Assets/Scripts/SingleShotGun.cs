using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Single shot gun item
/// </summary>
[RequireComponent(typeof(PhotonView))]
public class SingleShotGun : Gun
{
    /// <summary>
    /// Camera from player
    /// </summary>
    [SerializeField] new Camera camera;

    /// <summary>
    /// Photon view
    /// </summary>
    PhotonView photonView;



    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }



    /// <summary>
    /// Using single shot gun
    /// </summary>
    public override void Use()
    {
        Shoot();
    }



    /// <summary>
    /// Shoot the gun
    /// </summary>
    private void Shoot()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = camera.transform.position;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.collider.gameObject.GetComponent<IDamagable>()?.TakeDamage(((GunInfo)itemInfo).damage);
            photonView.RPC("RpcShoot", RpcTarget.All, hit.point, hit.normal);
        }
    }

    /// <summary>
    /// RPC, Shoot the gun (to make a bullet impact effect)
    /// </summary>
    /// <param name="hitPosition">Position of bullet hit</param>
    /// <param name="hitNormal">Normal of bullet hit surface</param>
    [PunRPC]
    private void RpcShoot(Vector3 hitPosition, Vector3 hitNormal)
    {
        var colliders = Physics.OverlapSphere(hitPosition, 0.3f);
        if (colliders.Length > 0)
        {
            var bulletImpactObject = Instantiate(
                bulletImpactPrefab,
                hitPosition + hitNormal * 0.0001f,
                Quaternion.LookRotation(hitNormal, Vector3.up) * bulletImpactPrefab.transform.rotation
            );

            bulletImpactObject.transform.SetParent(colliders[0].transform);

            Destroy(bulletImpactObject, 10f);
        }
    }
}
