using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private Renderer rend;
    private Color color;
    private SingletonTurrets singletonTurrets;
    private void Start()
    {
        this.singletonTurrets = SingletonTurrets.Instance;
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
        singletonTurrets.InstantiateTurretsAt(this.gameObject);
        Destroy(this.gameObject);
    }
}
