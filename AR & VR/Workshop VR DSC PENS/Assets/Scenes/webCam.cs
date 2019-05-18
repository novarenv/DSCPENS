using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class webCam : MonoBehaviour
{
    public GameObject webCameraPlane;
    public Button fire;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.isMobilePlatform) //Jika aplikasi dalam platform mobile
        {
            GameObject cameraParent = new GameObject("Camera Parent");
            cameraParent.transform.position = this.transform.position;
            this.transform.parent = cameraParent.transform;
            cameraParent.transform.Rotate(Vector3.right, 90);
        }

        Input.gyro.enabled = true; //Mengaktifkan sensor gyroscope

        fire.onClick.AddListener(OnButtonDown);

        WebCamTexture webCamTexture = new WebCamTexture();
        webCameraPlane.GetComponent<MeshRenderer>().material.mainTexture = webCamTexture;
        webCamTexture.Play();
    }

    //Code ketika tombol fire ditekan
    private void OnButtonDown()
    {
        GameObject peluru = Instantiate(Resources.Load("Peluru", typeof(GameObject))) as GameObject;
        Rigidbody rb = peluru.GetComponent<Rigidbody>();
        peluru.transform.rotation = this.transform.rotation;
        peluru.transform.position= this.transform.position;
        rb.AddForce(Camera.main.transform.forward * 500f);
        Destroy(peluru, 3);

        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion cameraRotation = new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.y, Input.gyro.attitude.z, Input.gyro.attitude.w);
        this.transform.localRotation = cameraRotation; 
    }
}
