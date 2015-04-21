using UnityEngine;
using System.Collections;

public class ChunkManager : MonoBehaviour {

	public float distanceBelowToSpawn = 7.0f;

	private Transform player;

	void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update() {
		// if there are no chunks left in the world the player is falling
		if (FindObjectsOfType(typeof(ChunkDestroyer)).Length < 1) {
			Vector2 newChunkPos = new Vector2(player.position.x, player.position.y);
			newChunkPos.y -= distanceBelowToSpawn;

			Instantiate((Object)Resources.Load("world-chunk-1"), newChunkPos, Quaternion.identity);
		}
	}
}
