using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOperation : MonoBehaviour
{
    Vector3 p1;
    Vector3 p2;
    Vector2[] corners;
    Vector3[] verts;
    Vector3[] vecs;
    bool dragSlection;
    MeshCollider selectionBox;
    SelectionMap map;
    RaycastHit hit;

    void Start()
    {
        dragSlection = false;
        map = GetComponent<SelectionMap>();
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse is clicked down
        if (Input.GetMouseButtonDown(0))
        {
           p1 = Input.mousePosition;
        }
        // Mouse is being clicked.
        // If two points are far away from each other, it should be the drag selection. Otherwise it is a single selection.
        else if (Input.GetMouseButton(0))
        {
           p2 = Input.mousePosition;
            if ((p2 - p1).magnitude > 40)
            {
                dragSlection = true;
            }
        }
        //Mouse is released. 
        else if (Input.GetMouseButtonUp(0))
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                map.removeAll();
            }
            if (!dragSlection)
            {
                Ray ray = Camera.main.ScreenPointToRay(p1);
                if (Physics.Raycast(ray, out hit, 50000.0f))
                {
                    if (Input.GetKey(KeyCode.LeftShift)) //inclusive select
                    {
                        map.add(hit.transform.gameObject);
                    }
                    else //exclusive selected
                    {
                        map.removeAll();
                        map.add(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                //p2 = Input.mousePosition;
                //verts = new Vector3[4];
                //vecs = new Vector3[4];
                //var count = 0;
                //corners= getBoundingBox(p1, p2);
                //foreach(Vector3 corner in corners)
                //{
                //    var ray = Camera.main.ScreenPointToRay(corner);
                //    if (Physics.Raycast(ray,out hit, 50000.0f))
                //    {
                //        vecs[count] = ray.origin - hit.point;
                //        verts[count] = hit.point;
                //        Debug.DrawLine(Camera.main.ScreenToWorldPoint(corner), hit.point, Color.red, 1.0f);     
                //    }
                //    count++;

                //}
                //var selectionMesh = generateSelectionMesh(verts, vecs);
                //selectionBox = gameObject.AddComponent<MeshCollider>();
                //selectionBox.sharedMesh = selectionMesh;
                //selectionBox.convex = true;
                //selectionBox.isTrigger = true;

                //First do a raycast from p1 and p2 in Selection layer.
                //After getting p1w and p2w, calculate the center and halfExtent, using OverlapBox to get All the collider within the box.
                //Note that we need to set selectable unit to layer"selectable".

                int mask = 1 << 6;
                Ray p1Ray = Camera.main.ScreenPointToRay(p1);
                Ray p2Ray = Camera.main.ScreenPointToRay(p2);
                RaycastHit p1Hit;
                RaycastHit p2Hit;
                if (Physics.Raycast(p1Ray, out p1Hit, 200f, mask)&& Physics.Raycast(p2Ray, out p2Hit, 200f, mask))
                {
                    Vector3 p1w = p1Hit.point;
                    Vector3 p2w = p2Hit.point;
                    Debug.DrawLine(p1w, p2w);
                    Debug.LogWarning("p1w:" + p1w + ", p2w:" + p2w);
                    if (p1w.y > p2w.y)
                    {
                        p1w.y = p2w.y;
                    }
                    if(p2w.y > p1w.y)
                    {
                        p2w.y = p1w.y;
                    }
                    
                    float halfHeight = 50f;
                    Vector3 center = (p1w + p2w) / 2f + Vector3.up * halfHeight;

                    Vector3 halfExtent = new Vector3(Mathf.Abs(p1w.x - p2w.x) / 2f, halfHeight, Mathf.Abs(p1w.z - p2w.z) / 2f);

                    Collider[] colliders = Physics.OverlapBox(center, halfExtent);
                    
                    foreach(Collider collder in colliders)
                    {
                        Debug.LogWarning(collder.gameObject.layer);
                        //Selectable layer
                        if (collder.gameObject.layer==7) map.add(collder.gameObject);
                    }
                }
                //Destroy(selectionBox, 0.02f);
                dragSlection = false;
            }
        }
    }

    private void OnGUI()
    {
        if (dragSlection == true)
        {
            var rect = Utils.GetScreenRect(p1, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    /// <summary>
    /// create a bounding box (4 corners in order) from the start and end mouse position
    /// </summary>
    /// <param name="p1">one of the two diagonal vertex</param>
    /// <param name="p2">the other one of the two diagonal vertex</param>
    /// <returns>the four vertices including p1 and p2 that forms a rectangle</returns>
    Vector2[] getBoundingBox(Vector2 p1, Vector2 p2)
    {
        Vector2 newP1;
        Vector2 newP2;
        Vector2 newP3;
        Vector2 newP4;

        if (p1.x < p2.x) //if p1 is to the left of p2
        {
            if (p1.y > p2.y) // if p1 is above p2
            {
                newP1 = p1;
                newP2 = new Vector2(p2.x, p1.y);
                newP3 = new Vector2(p1.x, p2.y);
                newP4 = p2;
            }
            else //if p1 is below p2
            {
                newP1 = new Vector2(p1.x, p2.y);
                newP2 = p2;
                newP3 = p1;
                newP4 = new Vector2(p2.x, p1.y);
            }
        }
        else //if p1 is to the right of p2
        {
            if (p1.y > p2.y) // if p1 is above p2
            {
                newP1 = new Vector2(p2.x, p1.y);
                newP2 = p1;
                newP3 = p2;
                newP4 = new Vector2(p1.x, p2.y);
            }
            else //if p1 is below p2
            {
                newP1 = p2;
                newP2 = new Vector2(p1.x, p2.y);
                newP3 = new Vector2(p2.x, p1.y);
                newP4 = p1;
            }

        }

        Vector2[] corners = { newP1, newP2, newP3, newP4 };
        return corners;

    }
    //generate a mesh from the 4 bottom points
    Mesh generateSelectionMesh(Vector3[] corners, Vector3[] vecs)
    {
        Vector3[] verts = new Vector3[8];
        int[] tris = { 0, 1, 2, 2, 1, 3, 4, 6, 0, 0, 6, 2, 6, 7, 2, 2, 7, 3, 7, 5, 3, 3, 5, 1, 5, 0, 1, 1, 4, 0, 4, 5, 6, 6, 5, 7 }; //map the tris of our cube

        for (int i = 0; i < 4; i++)
        {
            verts[i] = corners[i];
        }

        for (int j = 4; j < 8; j++)
        {
            verts[j] = corners[j - 4] + vecs[j - 4];
        }

        Mesh selectionMesh = new Mesh();
        selectionMesh.vertices = verts;
        selectionMesh.triangles = tris;

        return selectionMesh;
    }
    private void OnTriggerEnter(Collider other)
    {
        map.add(other.gameObject);
    }
}
