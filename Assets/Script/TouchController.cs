using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class TouchController : MonoBehaviour
{
    private RaycastHit hit;
    private BoolenOperation boolenOperation;
    void Start()
    {
        boolenOperation = GetComponent<BoolenOperation>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.tag == "Row")
                    sendTouchPosition(hit.transform.localPosition.y, positionNormalize(Input.mousePosition));
            }
        }
#endif
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(t.position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Row")
                    sendTouchPosition(hit.transform.localPosition.y, positionNormalize(Input.mousePosition));
            }
        }
    }

    void sendTouchPosition(float hitObjectPosY, Vector3 removerPos)
    {
        transform.position = new Vector3(removerPos.x, removerPos.y, transform.position.z);
        boolenOperation.searchObject(hitObjectPosY);
    }

    Vector3 positionNormalize(Vector3 mPos)
    {
        mPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mPos);
    }
}
