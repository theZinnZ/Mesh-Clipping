using theZinnZ.NClipper;
using UnityEngine;

public class ClipperTest : MonoBehaviour
{
    public CircleClipper clipper;
    public ClippableTerrain terrain;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            terrain.Clip(clipper);
        }

        clipper.segmentNum = Mathf.Clamp(clipper.segmentNum + (int)Input.mouseScrollDelta.y, 4, 25);
    }

    private void OnGUI()
    {
        string text = string.Format("Segment Count :  {0} \n Radius : {1} ", clipper.segmentNum , clipper.radius);
        var guiStyle = new GUIStyle();
        guiStyle.fontSize = 50;
        GUI.Label(new Rect(10, 10, 100, 20), text, guiStyle);
    }
}
