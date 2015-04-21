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
	private Vector3 chunkOffset;
	private Object linked = null;

	void Awake() {
		chunkOffset = transform.parent.position - transform.position;

		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update() {
		if (linked == null && (transform.position - player.position).sqrMagnitude < triggerCreateDistance * triggerCreateDistance) {
			GameObject newChunk = (GameObject)Instantiate((Object)Resources.Load("world-chunk-2"), transform.position, Quaternion.identity);

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

	linkTypes MatchingLinkType() {
		return linkType == linkTypes.Left ? linkTypes.Right : linkTypes.Left;
	}
}
