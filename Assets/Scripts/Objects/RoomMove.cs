using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour {
    private const string PLAYER_TAG = "Player";
    private const int SECONDS_TO_SHOW_ROOM_TEXT_FOR = 4;

    [Header("Distance to move Camera")]
    public Vector2 cameraChange;
    [Header("Distance to move Player")]
    public Vector3 playerChange;

    [Header("Text details")]
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    private CameraMovement cam;

    // Start is called before the first frame update
    void Start () {
        cam = Camera.main.GetComponent<CameraMovement> ();
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag (PLAYER_TAG) && !other.isTrigger) {
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
            if (needText) {
                StartCoroutine (placeNameCoroutine ());
            }
        }
    }

    private IEnumerator placeNameCoroutine () {
        text.SetActive (true);
        placeText.text = placeName;
        yield return new WaitForSeconds (SECONDS_TO_SHOW_ROOM_TEXT_FOR);
        text.SetActive (false);
    }
}