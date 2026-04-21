using UnityEngine;

public class Sheep : MonoBehaviour
{

	public float runSpeed; // 1
	public float gotHayDestroyDelay; // 2
	private bool hitByHay; // 3

	public float dropDestroyDelay; // 1
	private Collider myCollider; // 2
	private Rigidbody myRigidbody; // 3

	public AudioClip hayHitSound;
	public AudioClip dropSound;

	private AudioSource audioSource;


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		myCollider = GetComponent<Collider>();
		myRigidbody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();

		if (audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
		}
	}

	// Update is called once per frame
	void Update()
    {
		transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
	}

	private void HitByHay()
	{
		hitByHay = true; // 1
		runSpeed = 0; // 2

		Destroy(gameObject, gotHayDestroyDelay); // 3
	}

	private void OnTriggerEnter(Collider other) // 1
	{

		if (other.CompareTag("Hay") && !hitByHay) // 2
		{
			Destroy(other.gameObject); // 3
			audioSource.PlayOneShot(hayHitSound);
			HitByHay(); // 4
		} else if (other.CompareTag("DropSheep"))
		{
			Drop();
		}
	}

	private void Drop()
	{
		audioSource.PlayOneShot(dropSound);
		myRigidbody.isKinematic = false; // 1
		myCollider.isTrigger = false; // 2
		Destroy(gameObject, dropDestroyDelay); // 3
	}
}
