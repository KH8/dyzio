using UnityEngine;

public class BulletCollider : MonoBehaviour {
    public GameObject bulletExposionPrefab;
    public float expolsionForce = 10.0f;
    public float expolsionRadius = 0.5f;

	public AudioClip[] exposionSounds;

    private AudioSource _audio;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        _audio = GetComponent<AudioSource>();
    }

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
            PlayExposionSound();
            AnimateExplosion();
            AddExplosionForce();
        }
    }

    private void PlayExposionSound() {
        if (exposionSounds != null && exposionSounds.Length > 0 && _audio != null) {
            var randomIndex = Random.Range(0, exposionSounds.Length);
            _audio.PlayOneShot(exposionSounds[randomIndex]);
        }
    }

    private void AnimateExplosion() {
        var expolsion = (GameObject) Instantiate(
            bulletExposionPrefab,
            transform.position,
            transform.rotation);
        Destroy(expolsion, 0.1f);
    }

    private void AddExplosionForce() {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, expolsionRadius);
        foreach (Collider hit in colliders) {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) {
                Debug.Log("BOOM!");
                rb.AddExplosionForce(expolsionForce, explosionPos, expolsionRadius, 3.0F);
            }   
        }
    }
}