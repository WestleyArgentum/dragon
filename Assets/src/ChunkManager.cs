using UnityEngine;
using System.Collections;

public class ChunkManager : MonoBehaviour {

	public float distanceBelowToSpawn = 7.0f;

	private Transform player;

	private Object[] chunkPool;

	void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").transform;

		chunkPool = (Object[])Resources.LoadAll("world-chunks");
	}

	void Update() {
		// if there are no chunks left in the world the player is falling
		if (FindObjectsOfType(typeof(ChunkDestroyer)).Length < 1) {
			Vector2 newChunkPos = new Vector2(player.position.x, player.position.y);
			newChunkPos.y -= distanceBelowToSpawn;

			Instantiate(GetRandomChunk(), newChunkPos, Quaternion.identity);
		}
	}

	Object GetRandomChunk() {
		return chunkPool[Random.Range(0, chunkPool.Length)];
	}
}
