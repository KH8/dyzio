using UnityEngine;

public class ShootingController : MonoBehaviour {
    public AudioSource audioSource;

    public GameObject bulletPrefab;
    public Transform bulletSpawnLeft;
    public Transform bulletSpawnRight;

    public float initialSpeed = 24.0f;
    public float lifeTime = 0.5f;

	public AudioClip[] laserSounds;

    private GameController _gc;

	private float _overheadAngle = 15.0f;
	private float _overheadAngleMax = 45.0f;
	private float _overheadAngleMin = -15.0f;

    private bool _spawnSideToggle = false;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        _gc = GameObject.Find("Game").GetComponent<GameController>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
		if (!GameMode.DubStep.Equals(_gc.GetMode())) {
			return;
		}
        CalculateOverheadAngle();
        CalculateBulletSpawnPosition();
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.F)) {
            Fire();
            PlayLaserSound();
        }
    }

    void Fire() {
        var bulletSpawn = GetNextBulletSpawn();
        var bullet = (GameObject) Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * initialSpeed;
        Destroy(bullet, lifeTime);
    }

    private Transform GetNextBulletSpawn() {
        _spawnSideToggle = !_spawnSideToggle;
        return _spawnSideToggle ? bulletSpawnLeft : bulletSpawnRight;
    }

	private void CalculateOverheadAngle() {
		_overheadAngle += Input.GetAxis("Mouse Y"); 
		_overheadAngle = Mathf.Max(_overheadAngle, _overheadAngleMin);
		_overheadAngle = Mathf.Min(_overheadAngle, _overheadAngleMax);
	}

    private void CalculateBulletSpawnPosition() {
		bulletSpawnLeft.rotation = GetSpawnAngles();
		bulletSpawnRight.rotation = GetSpawnAngles();
    }

    private Quaternion GetSpawnAngles() {
        return Quaternion.Euler(-1 * _overheadAngle + 105.0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void PlayLaserSound() {
        if (laserSounds != null && laserSounds.Length > 0 && audioSource != null) {
            var randomIndex = Random.Range(0, laserSounds.Length);
            audioSource.PlayOneShot(laserSounds[randomIndex]);
        }
    }
}