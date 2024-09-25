using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] GameObject turretsPrefab;
    private Renderer rend;
    private Color color;
    private void Start()
    {
        this.rend = GetComponent<Renderer>();
        this.color = this.rend.material.color;
    }
    private void OnMouseEnter()
    {
        rend.material.color = Color.green;
    }
    private void OnMouseExit()
    {
        rend.material.color = this.color;
    }
    private void OnMouseDown()
    {
        Instantiate(turretsPrefab, this.gameObject.transform.position, turretsPrefab.transform.rotation);
        Destroy(this.gameObject);
    }
}
