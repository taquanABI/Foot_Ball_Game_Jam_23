using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw_Line_Control : MonoBehaviour
{
    public bool isCanDraw;
    public LineRenderer line_Renderer;
    private List<Vector3> linePoints = new List<Vector3>();
    public Vector3 vec_Poit_Start;
    public Transform tf_Start_Drawn;
    public Transform tf_Plan;
    public Transform tf_Target;
    public Vector3 vec_Offset_Target;
    // Start is called before the first frame update
    void Start()
    {
        On_Init();
    }
    public void On_Init()
    {
        vec_Poit_Start = tf_Start_Drawn.position;
        Set_Point_Start(tf_Start_Drawn);
    }
    // Update is called once per frame
    void Update()
    {
        if (!isCanDraw)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            this.GetMouseDrag();
        }
    }
    private void GetMouseDrag()
    {
        var output = Check_Ray_Cast.Ins.Get_Pos_Raycast_Hit_Plan();
        if (output.Item1)
        {
            UpdateTrajectory(output.Item2);//
        }
        else
        {
            // ko raycast vào plan
        }
    }

    public void UpdateTrajectory(Vector3 vec_mouse_point)
    {
        linePoints.Clear();

        linePoints.Add(vec_Poit_Start);
        linePoints.Add(vec_mouse_point);

        tf_Target.position = vec_mouse_point + vec_Offset_Target;

        line_Renderer.SetPositions(linePoints.ToArray());
    }

    public void Set_Point_Start(Transform tf_start)
    {
        vec_Poit_Start = new Vector3(tf_start.position.x, tf_Plan.position.y, tf_start.position.z);
    }
}
