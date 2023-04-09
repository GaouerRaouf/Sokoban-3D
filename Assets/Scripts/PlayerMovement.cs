using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 0.5f;
    [SerializeField]
    float rayLength = 1.4f;
    [SerializeField]
    float rayOffsetX = 0.5f;
    [SerializeField]
    float rayOffsetY = 0.5f;
    [SerializeField]
    float rayOffsetZ = 0.5f;


    [SerializeField] GameObject coloredObjects;
    [SerializeField] GameObject bottom;



   
    Vector3 targetPosition;
    Vector3 startPosition;

    public void PlayerMove(Vector3 destination)
    {
        targetPosition =  destination - transform.position + Vector3.up;
        transform.Translate(targetPosition);
        startPosition = transform.position;   
    }

    bool CanMove(Vector3 direction)
    {
        RaycastHit hit;

        if ((Vector3.Equals(Vector3.forward, direction) || Vector3.Equals(Vector3.back, direction)))
        {
            if ((Physics.Raycast(transform.position + Vector3.up * rayOffsetY + Vector3.right * rayOffsetX, direction, out hit, rayLength)) && ((hit.transform.gameObject.tag == "Wall") || (hit.transform.gameObject.tag == "Box" && !hit.transform.gameObject.GetComponent<BoxMovement>().CanMove(direction)))) return false;         
        }
        else if (Vector3.Equals(Vector3.left, direction) || Vector3.Equals(Vector3.right, direction))
        {
            if ((Physics.Raycast(transform.position + Vector3.up * rayOffsetY + Vector3.forward * rayOffsetZ, direction, out hit, rayLength)) && ((hit.transform.gameObject.tag == "Wall") || (hit.transform.gameObject.tag == "Box" && !hit.transform.gameObject.GetComponent<BoxMovement>().CanMove(direction)))) return false;
        }
        
        
        return true;
    }
    
    

    private void OnMouseDown()
    {
        bottom.SetActive(true);
        move = true;
        DetectBoxObjects();
    }

    
    public float detectDistance = 0.1f; 
    public string boxTag = "Box"; 
    public bool move = false;


    public Vector3[] DetectBoxObjects()
    {
        
        Vector3[] Directions = new Vector3[4];
        int i = 0;
        // Detect box objects on the left
        RaycastHit leftHit;
        if (Physics.Raycast(transform.position, -transform.right, out leftHit, detectDistance))
        {
            GameObject obj = leftHit.collider.gameObject;
            if (obj.CompareTag(boxTag) && obj.GetComponent<BoxMovement>().CanMove(Vector3.left) && Vector3.Distance(transform.position, obj.transform.position) <= 1f)
            {
                Renderer rend = obj.GetComponent<MeshRenderer>();
                if (rend != null)
                {
                    rend.material.color = Color.blue;
                    Directions[i] = Vector3.left;
                    i++;
                }
            }
        }

        // Detect box objects on the right
        RaycastHit rightHit;
        if (Physics.Raycast(transform.position, transform.right, out rightHit, detectDistance))
        {
            GameObject obj = rightHit.collider.gameObject;
            if (obj.CompareTag(boxTag) && obj.GetComponent<BoxMovement>().CanMove(Vector3.right) && Vector3.Distance(transform.position, obj.transform.position) <= 1f)
            {
                Renderer rend = obj.GetComponent<MeshRenderer>();
                if (rend != null)
                {
                    rend.material.color = Color.blue;
                    Directions[i] = Vector3.right;
                    i++;
                }
            }
        }

        // Detect box objects forward
        RaycastHit forwardHit;
        if (Physics.Raycast(transform.position, transform.forward, out forwardHit, detectDistance))
        {
            GameObject obj = forwardHit.collider.gameObject;
            if (obj.CompareTag(boxTag) && obj.GetComponent<BoxMovement>().CanMove(Vector3.forward) && Vector3.Distance(transform.position, obj.transform.position) <= 1f)
            {
                Renderer rend = obj.GetComponent<MeshRenderer>();
                if (rend != null)
                {
                    rend.material.color = Color.blue;
                    Directions[i] = Vector3.forward;
                    i++;
                }
            }
        }

        // Detect box objects backward
        RaycastHit backwardHit;
        if (Physics.Raycast(transform.position, -transform.forward, out backwardHit, detectDistance))
        {
            GameObject obj = backwardHit.collider.gameObject;
            if (obj.CompareTag(boxTag) && obj.GetComponent<BoxMovement>().CanMove(Vector3.back) && Vector3.Distance(transform.position, obj.transform.position) <= 1f)
            {
                Renderer rend = obj.GetComponent<MeshRenderer>();
                if (rend != null)
                {
                    rend.material.color = Color.blue;
                    Directions[i] = Vector3.back;
                    i++;
                }
            }
        }
        
    return Directions;
    }
}