using System.Dynamic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGuardian : MonoBehaviour
{ 
    Vector3[] guardianArea;
    Vector3[] playArea;
    Vector3 guardPoint;
    Vector3 playPoint;

    public LineRenderer galr;
    public LineRenderer palr;

    public TMPro.TMP_Text text;

    bool isDraw = false;
    // Start is called before the first frame update
    void Start()
    {
        LineRenderer galr = GameObject.Find("guardianArea").GetComponent<LineRenderer>();
        LineRenderer palr = GameObject.Find("playArea").GetComponent<LineRenderer>();

        OVRBoundary boundary = new OVRBoundary();

        guardianArea = boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary);

        playArea = boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea);
     
        guardPoint = boundary.GetDimensions(OVRBoundary.BoundaryType.OuterBoundary);
    
        playPoint = boundary.GetDimensions(OVRBoundary.BoundaryType.PlayArea);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!isDraw)
        {
            DrawDuardianArea();
            DrawPlayArea();
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = Vector3.one * 0.2f;
            cube.transform.position = guardPoint;

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = Vector3.one * 0.2f;
            sphere.transform.position = playPoint;

            text.text = "guardianAreaLength:" + guardianArea.Length.ToString() +
             "\npalyAreaLength:" + playArea.Length.ToString() +
             "\nplayPoint:" + playPoint.ToString(); 
            isDraw = true;
        }

    }

    private void DrawPlayArea()
    {
        if (playArea.Length != 0)
        {
            palr.positionCount = playArea.Length + 1;
            for (int i = 0; i < playArea.Length; i++)
            {
                palr.SetPosition(i, playArea[i]);
            }
            palr.SetPosition(playArea.Length, playArea[0]);
        }
    }

    private void DrawDuardianArea()
    {
        if (guardianArea.Length != 0)
        {
            galr.positionCount = guardianArea.Length;
            for (int i = 0; i < guardianArea.Length; i++)
            {
                galr.SetPosition(i, guardianArea[i]);
            }
        }
    }
}
