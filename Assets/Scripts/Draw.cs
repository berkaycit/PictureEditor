using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Draw : MonoBehaviour
{
    public GameObject linePrefab;
    public Slider hueSlider;

    private GameObject currentLine;
    private LineRenderer lineRenderer;
    private List<Vector2> vecPositions;
    private bool isPenActive = false;

    void Start(){
        vecPositions = new List<Vector2>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && isPenActive)
            CreateLine();

        if (Input.GetButton("Fire1") && isPenActive){
            Vector2 tempVecPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempVecPos, vecPositions[vecPositions.Count - 1]) > .1f)
                UpdateLine(tempVecPos);
        }
    }

    public void PenActive() => isPenActive = true;
    public void PenNotActive() => isPenActive = false;

    public void ChangeColor(){
        Color chosenColor = Color.HSVToRGB(hueSlider.value, 1, 1);

        linePrefab.GetComponent<LineRenderer>().startColor = chosenColor;
        linePrefab.GetComponent<LineRenderer>().endColor = chosenColor;
    }

    void CreateLine(){
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
        lineRenderer = currentLine.GetComponent<LineRenderer>();

        vecPositions.Clear();
        vecPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        vecPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        lineRenderer.SetPosition(0, vecPositions[0]);
        lineRenderer.SetPosition(1, vecPositions[1]);

    }

    void UpdateLine(Vector2 newVecPos){
        vecPositions.Add(newVecPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newVecPos);

    }
}
