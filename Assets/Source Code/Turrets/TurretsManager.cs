using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretsManager : MonoBehaviour
{
    [SerializeField] GameObject nodeBuilding;
    private Renderer rend;
    private Color color;
    private void Start()
    {
        this.rend = GetComponent<Renderer>();
        this.color = this.rend.material.color;
    }
    private void OnMouseEnter()
    {
        rend.material.color = Color.yellow;
    }
    private void OnMouseExit()
    {
        rend.material.color = this.color;
    }
    private void OnMouseDown()
    {
        Instantiate(nodeBuilding, this.gameObject.transform.position, nodeBuilding.transform.rotation);
        DOTween.Kill(this.gameObject.transform);
        Destroy(this.gameObject);
    }
}
