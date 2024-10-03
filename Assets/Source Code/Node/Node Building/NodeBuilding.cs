using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeBuilding : MonoBehaviour
{
    [SerializeField] float rangeGizmos = 6f;
    private Renderer rend;
    private Color color;
    private SingletonTurrets singletonTurrets;
    private SingletonShopTurrets singletonShopTurrets;
    private void Awake()
    {
        singletonTurrets = SingletonTurrets.Instance;
        singletonShopTurrets = SingletonShopTurrets.Instance;
        singletonShopTurrets.SetActiveShopTurrets(true, this.gameObject.transform.position);
    }
    private void Start()
    {
        singletonShopTurrets.SetActiveShopTurrets(false, this.gameObject.transform.position);
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
        singletonShopTurrets.SetActiveShopTurrets(true, this.gameObject.transform.position);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, rangeGizmos);
    }
}
