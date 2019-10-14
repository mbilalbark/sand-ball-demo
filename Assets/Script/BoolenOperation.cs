
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using UnityEngine;
using Parabox.CSG;
using Vector3 = System.Numerics.Vector3;


public class BoolenOperation : MonoBehaviour
{
    private Transform rows;
    private float tempHitPosY = 1000;
    public GameConroller gameController;

    void Start()
    {
        rows = GameObject.Find("Rows").transform;
    }

    public void rowsAttach(Transform rows)
    {
        this.rows = rows;
    }

    public void searchObject(float hitPosY)
    {
        // Debug.Log("Hit pos "+ hitPosY);
        if (tempHitPosY != hitPosY)
        {
            foreach (Transform child in rows)
            {
                if (child.gameObject.active)
                {
                    float distance = System.Math.Abs(child.transform.localPosition.y - hitPosY);
                    if (distance <= 0.5f)
                    {
                        StartCoroutine(boolenObject(child.gameObject));
                        tempHitPosY = hitPosY;
                    }
                }
            }
        }
    }

    private IEnumerator boolenObject(GameObject objects)
    {
        objects.SetActive(false);
        CSG_Model result = Boolean.Subtract(objects.transform.GetChild(0).gameObject, gameObject);
        yield return new WaitForFixedUpdate();
        GameObject composite = new GameObject();
        GameObject parentObject = new GameObject();
        parentObject.transform.SetParent(rows);
        parentObject.transform.position = objects.transform.position;
        composite.AddComponent<MeshFilter>().sharedMesh = result.mesh;
        composite.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
        composite.AddComponent<MeshCollider>();
        parentObject.name = objects.name;
        parentObject.tag = objects.tag;
        composite.transform.SetParent(parentObject.transform);
        parentObject.AddComponent<BoxCollider>();
        parentObject.GetComponent<BoxCollider>().size = new UnityEngine.Vector3(4,0.5f,1);
        parentObject.GetComponent<BoxCollider>().isTrigger = true;
        Debug.Log("Run");
    }
}
