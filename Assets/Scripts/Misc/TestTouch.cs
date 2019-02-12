﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTouch : MonoBehaviour
{
    [SerializeField] private Vector2 touchPos, pos;
    [SerializeField] private float speed;
    [SerializeField] private bool cursorMode;
    [SerializeField] private float yOffset;
    private bool isInPlayerZone;

    private void OnDrawGizmos() {
    }

    // Update is called once per frame
    void Update()
    {
        if (!cursorMode) {
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                touchPos = new Vector2(touchPos.x, touchPos.y + yOffset);

                if (isInPlayerZone) {
                    transform.position = Vector2.Lerp(transform.position, touchPos, Time.deltaTime * speed);
                }

                RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector3.forward);
                if (hit.collider != null) {
                    if (hit.transform.tag == "Player") {
                        isInPlayerZone = true;
                    }
                }
            }
            else if(Input.touchCount == 0) {
                isInPlayerZone = false;
            }
        }
        else {

            if (Input.GetMouseButton(0)) {
                
                pos = Input.mousePosition;
                pos = Camera.main.ScreenToWorldPoint(pos);
                pos = new Vector2(pos.x, pos.y + yOffset);

                if (isInPlayerZone) {
                    transform.position = Vector2.Lerp(transform.position, pos, Time.deltaTime * speed);
                }
            }
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.forward);
                if (hit.collider != null) {
                    if(hit.transform.tag == "Player") {
                        isInPlayerZone = true;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0)) {
                isInPlayerZone = false;
            }

        }
    }
}