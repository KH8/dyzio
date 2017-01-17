using UnityEngine;

public class ShootingController : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            Fire();
        }
    }

    void Fire() {
        var bullet = (GameObject) Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * 16.0f;
        Destroy(bullet, 10.0f);
    }
}