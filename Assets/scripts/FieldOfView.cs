using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    //Draw Mesh Variables--------------------------
    private Mesh mesh;
    public MeshFilter _meshFilter;
    //FOV variables-------------------------------

    [SerializeField]
    private float meshResolution;

    public float angle;

    public float viewDistance=10;
    public LayerMask obstacleLayer;
    Vector3 lp;

    public Guard _guard;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        lp=transform.position;
    }

    void Init()
    {
        //Draw Mesh
        mesh = new Mesh();
        _meshFilter.mesh = mesh;  
    }

    private void Update()
    {
        //if(lp!=transform.position)
            DrawFOV();
    }

    void DrawFOV()
    {
        lp=transform.position;
        //set fov values
        int stepCount = Mathf.RoundToInt(angle * meshResolution);
        float stepAnglesize = angle / stepCount;       
     
        List<Vector3> viewPoints = new List<Vector3>();

        for (int i = 0; i <= stepCount; i++)
        {
            float currentAngle = transform.eulerAngles.y - (angle / 2) + (stepAnglesize * i);
            viewCastInfo newViewCast = viewCast(currentAngle);
            viewPoints.Add(newViewCast.point);
            Debug.DrawLine(transform.position, newViewCast.point, Color.red);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }

        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    //Viewcast medthod
    viewCastInfo viewCast(float globalAngle)
    {
        Vector3 dir = dirFromAngle(globalAngle, true);
        RaycastHit2D hit=Physics2D.Raycast(transform.position, dir,viewDistance);

        if (hit && hit.point!=Vector2.zero)
        {
            //Debug.Log("HIT");
            return new viewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new viewCastInfo(false, transform.position + (dir * viewDistance), viewDistance, globalAngle);
        }
    }

    //Return direction from angle
    public Vector3 dirFromAngle(float angleInDeg, bool angleisGlobal)
    {
        if (!angleisGlobal)
        {
            angleInDeg += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDeg * Mathf.Deg2Rad), Mathf.Cos(angleInDeg * Mathf.Deg2Rad));
    }
    //Handle raycast and its information
    public struct viewCastInfo
    {
        //Did the ray hit anything?
        public bool hit;

        //Endpoint of the ray
        public Vector3 point;

        //length of the ray
        public float distance;

        //angle of the ray
        public float angle;

        //constructor
        public viewCastInfo(bool _hit, Vector3 _point, float _distance, float _angle)
        {
            hit = _hit;
            point = _point;
            distance = _distance;
            angle = _angle;
        }
    }
}
