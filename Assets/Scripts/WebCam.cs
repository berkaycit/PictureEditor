using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class WebCam : MonoBehaviour
{
    private AspectRatioFitter fitter;
    private bool camAvailable = true;
    private WebCamTexture frontCam;

    void Start()
    {
        fitter = GetComponent<AspectRatioFitter>();
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length <= 0){
            camAvailable = false;
            return;
        }

        for(int i = 0; i< devices.Length; i++){
            if (devices[i].isFrontFacing){
                frontCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if (frontCam == null)
        {
            camAvailable = false;
            return;
        }

        if (!frontCam.isPlaying)
            frontCam.Play();

        gameObject.GetComponent<Image>().material.mainTexture = frontCam;

    }

    void Update(){

        if (!camAvailable)
            return;

        float ratio = (float)frontCam.width / (float)frontCam.height;
        fitter.aspectRatio = ratio;

        float scaleY = frontCam.videoVerticallyMirrored ? -1f : 1f;
        gameObject.GetComponent<Image>().rectTransform.localScale = new Vector3(1f, scaleY, 1f);

    }

    public void StopCam(){
        if (frontCam.isPlaying)
            frontCam.Stop();
    }

    public void StartCam(){
        if (!frontCam.isPlaying)
            frontCam.Play();
    }
}
