using UnityEngine;
using System.Collections;

public class ChunkDestroyer : MonoBehaviour {

	public float triggerDestroyDistance = 12.0f;

	private Transform player;

	void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update() {
		if ((transform.position - player.position).sqrMagnitude > triggerDestroyDistance * triggerDestroyDistance) {
			Destroy(transform.parent.gameObject);
		}
	}
}
