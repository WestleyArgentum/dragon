using UnityEngine;
using System.Collections;

public class ChunkLinkController : MonoBehaviour {
	public enum linkTypes {
		Left,
		Right
	};

	public linkTypes linkType;
	public float triggerCreateDistance = 2.0f;

	private Transform player;
	private Object[] chunkPool;

	private Vector3 chunkOffset;
	private Object linked = null;

	void Awake() {
		chunkOffset = transform.parent.position - transform.position;

		player = GameObject.FindGameObjectWithTag("Player").transform;
		chunkPool = (Object[])Resources.LoadAll("world-chunks");
	}

	void Update() {
		if (linked == null && (transform.position - player.position).sqrMagnitude < triggerCreateDistance * triggerCreateDistance) {
			GameObject newChunk = (GameObject)Instantiate(GetRandomChunk(), transform.position, Quaternion.identity);

			// find a compatible link
			linkTypes lookingForLink = MatchingLinkType();
			ChunkLinkController[] newLinks = newChunk.GetComponentsInChildren<ChunkLinkController>();

			foreach (ChunkLinkController link in newLinks) {
				if (link.linkType == lookingForLink) {
					LinkNewChunk(link);
				}
			}
		}
	}

	void LinkNewChunk(ChunkLinkController newLink) {
		linked = newLink;
		newLink.linked = this;

		newLink.transform.parent.position += newLink.chunkOffset;
	}

	Object GetRandomChunk() {
		return chunkPool[Random.Range(0, chunkPool.Length)];
	}

	linkTypes MatchingLinkType() {
		return linkType == linkTypes.Left ? linkTypes.Right : linkTypes.Left;
	}
}
