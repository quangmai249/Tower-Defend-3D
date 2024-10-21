using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] float maxScale = 30;
    [SerializeField] float minScale = 10;
    [SerializeField] float maxVerticalMoving = 0;
    [SerializeField] float minVerticalMoving = -15;
    [SerializeField] float rangeHorizontalMoving = 5;
    [SerializeField] float speedMoveCamera = 10;
    [SerializeField] float locationYCamera;

    [SerializeField] Vector3 pos;
    private void Start()
    {
        locationYCamera = this.gameObject.transform.position.y;
    }
    private void Update()
    {
        TranslateCamera();
    }
    private void OnGUI()
    {
        pos = transform.position;
        pos.y -= Input.mouseScrollDelta.y;

        if (pos.y >= maxScale)
            pos.y = maxScale;
        if (pos.y <= minScale)
            pos.y = minScale;

        this.gameObject.transform.position = pos;
    }
    private void TranslateCamera()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            this.gameObject.transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * speedMoveCamera * Time.deltaTime);
            if (this.gameObject.transform.position.x <= -rangeHorizontalMoving)
            {
                this.gameObject.transform.position = this.pos;
                return;
            }
            if (this.gameObject.transform.position.x >= rangeHorizontalMoving)
            {
                this.gameObject.transform.position = this.pos;
                return;
            }
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            this.gameObject.transform.Translate(Vector3.up * Input.GetAxis("Vertical") * speedMoveCamera * Time.deltaTime);
            if (this.gameObject.transform.position.z <= minVerticalMoving)
            {
                this.gameObject.transform.position = this.pos;
                return;
            }
            if (this.gameObject.transform.position.z >= maxVerticalMoving)
            {
                this.gameObject.transform.position = this.pos;
                return;
            }
        }

    }
}
