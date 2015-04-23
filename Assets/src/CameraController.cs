using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float xMargin = 0f;
	public float yPositiveMargin = -3f;
	public float yNegativeMargin = 0.5f;
	public float xSmooth = 1f;
	public float ySmooth = 2f;
	public Vector2 maxXAndY;
	public Vector2 minXAndY;
	
	private Transform player;
	
	void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	bool CheckXMargin() {
		return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
	}
	
	bool CheckYMargin() {
		float y = transform.position.y - player.position.y;
		return y > yNegativeMargin || y < yPositiveMargin;
	}

	void FixedUpdate() {
		TrackPlayer();
	}

	void TrackPlayer() {
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		if (CheckXMargin ()) {
			float smooth = Mathf.Abs(player.position.x - transform.position.x) + xSmooth;
			targetX = Mathf.Lerp(transform.position.x, player.position.x, smooth * Time.deltaTime);
		}

		if (CheckYMargin ()) {
			targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);
		}

		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}

}
