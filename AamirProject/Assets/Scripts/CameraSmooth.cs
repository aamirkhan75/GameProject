using UnityEngine;
using System.Collections;

public class CameraSmooth : MonoBehaviour {

    public float lerpRate = 5f;
    public Vector3 offset;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + offset, Time.deltaTime * lerpRate);
    }
}