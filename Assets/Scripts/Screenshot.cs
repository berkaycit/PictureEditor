using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screenshot : MonoBehaviour
{
    public Slider hueSlider;

    private Draw mDraw;
    private WebCam mWebCam;
    private int width, height;
    private GameObject ssButton, saveButton;


    private void Awake()
    {
        mDraw = FindObjectOfType<Draw>();
        mWebCam = FindObjectOfType<WebCam>();
        ssButton = GameObject.Find("SSbutton");
        saveButton = GameObject.Find("SaveButton");

        hueSlider.gameObject.SetActive(false);
        saveButton.gameObject.SetActive(false);
    }

    public static string ScreenshotName(){
        return string.Format("{0}.png",
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    private void MakeActive(){
        ssButton.SetActive(true);

        LineRenderer[] des = FindObjectsOfType<LineRenderer>();

        for (int i = 0; i < des.Length; i++){
            Destroy(des[i].gameObject);
        }

        mWebCam.StartCam();
    }

    private void MakeDisactive(bool isSave){
        ssButton.SetActive(false);
        if (isSave){
            hueSlider.gameObject.SetActive(false);
            saveButton.gameObject.SetActive(false);
        }else{
            hueSlider.gameObject.SetActive(true);
            saveButton.gameObject.SetActive(true);
        }

    }

    public void TakeScreenshot(){
        width = Screen.width;
        height = Screen.height;
        mWebCam.StopCam();
        MakeDisactive(false);
        mDraw.PenActive();
    }

    public void SaveScreenshot(){
        MakeDisactive(true);
        ScreenCapture.CaptureScreenshot(ScreenshotName());
        Invoke("MakeActive", 2f);
    }



}
