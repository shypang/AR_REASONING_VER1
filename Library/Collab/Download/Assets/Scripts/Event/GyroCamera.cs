﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroCamera : MonoBehaviour {

    public GameObject camParent;

    private Gyroscope gyro;
    private bool gyroSupported;
    private Quaternion rotFix;

    [SerializeField]
    private Transform worldObj;
    private float startY;

	// Use this for initialization
	void Start () {
        gyroSupported = SystemInfo.supportsGyroscope;

        //GameObject camParent = new GameObject("CamParent");
        //camParent.transform.position = transform.position;
        //transform.parent = camParent.transform;

        if (gyroSupported)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            camParent.transform.rotation = Quaternion.Euler(90f, 180f, 0f);
            rotFix = new Quaternion(0, 0, 1, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(gyroSupported && startY == 0)
        {
            ResetGyroRotation();
        }
        //#if UNITY_EDITOR

        //#endif
        if(Application.platform == RuntimePlatform.Android)
        {
            transform.localRotation = gyro.attitude * rotFix;
        }
	}

    void ResetGyroRotation()
    {
        startY = transform.eulerAngles.y;
        worldObj.rotation = Quaternion.Euler(0f, startY, 0f);
    }
}
