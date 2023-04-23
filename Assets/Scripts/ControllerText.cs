using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerText : MonoBehaviour
{
    [SerializeField] TMP_Text m_text;
    // Start is called before the first frame update
    void Start()
    {
        m_text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One)) m_text.text = "A";
        if (OVRInput.Get(OVRInput.Button.Two)) m_text.text = "B";
        if (OVRInput.Get(OVRInput.Button.Three)) m_text.text = "X";
        if (OVRInput.Get(OVRInput.Button.Four)) m_text.text = "Y";

        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick)) m_text.text = "Left Stick";
        Vector2 pt = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        if(pt != Vector2.zero)
        {
            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp)) m_text.text = "Left Stick Up:" + pt.ToString("0.000");
            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown)) m_text.text = "Left Stick Down:" + pt.ToString("0.000");
            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight)) m_text.text = "Left Stick Right:" + pt.ToString("0.000");
            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft)) m_text.text = "Left Stick Left:" + pt.ToString("0.000");
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstick)) m_text.text = "Right Stick";
        Vector2 pt2 = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        if(pt2 != Vector2.zero)
        {
            
            if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp)) m_text.text = "Right Stick Up:" + pt2.ToString("0.000");
            if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown)) m_text.text = "Right Stick Down:" + pt2.ToString("0.000");
            if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight)) m_text.text = "Right Stick Right:" + pt2.ToString("0.000");
            if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft)) m_text.text = "Right Stick Left:" + pt2.ToString("0.000");
        }
        float pit = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        if( pit > 0.0f) m_text.text = "Left Index Trigger:" + pit.ToString("0.000");
        float pht = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
        if( pht > 0.0f) m_text.text = "Left Hand Trigger:" + pht.ToString("0.000");

        float pit2 = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        if( pit2 > 0.0f) m_text.text = "Right Index Trigger:" + pit2.ToString("0.000");
        float pht2 = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
        if( pht2 > 0.0f) m_text.text = "Right Hand Trigger:" + pht2.ToString("0.000");

        // if (OVRInput.GetUp(OVRInput.Button.One))
        // {
            float a = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
            float b = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
            OVRInput.SetControllerVibration(a, b, OVRInput.Controller.RTouch);
        // }
    }
}
