using UnityEngine;

public class BulletCollider : MonoBehaviour {
    public GameObject bulletExposionPrefab;

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other) {
        ExpoldeOnCollision(other);
        TouchableController tc = other.gameObject.GetComponent<TouchableController>();
        if (tc != null) {
            tc.TouchWithSound();
        }
    }

    void ExpoldeOnCollision(Collision other) {
        if (other.gameObject.name != "Bullet(Clone)" && other.gameObject.name != "BulletExplosion(Clone)") {
            Debug.Log(other.gameObject.name);
            Explode();
        }
    }

    void Explode() {
        var expolsion = (GameObject) Instantiate(
            bulletExposionPrefab,
            transform.position,
            transform.rotation);
        Destroy(expolsion, 0.1f);
    }
}