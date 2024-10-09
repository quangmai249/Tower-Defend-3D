using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeBuilding : MonoBehaviour
{
    [SerializeField] GameObject panelShopTurrets;
    private Renderer rend;
    private Color color;
    private SingletonShopTurrets singletonShopTurrets;
    private GameManager gameManager;

    private void Awake()
    {
        singletonShopTurrets = SingletonShopTurrets.Instance;
        gameManager = GameManager.Instance;

        singletonShopTurrets.SetActiveShopTurrets(true, this.gameObject.transform.position);
        panelShopTurrets.SetActive(true);
    }
    private void Start()
    {
        singletonShopTurrets.SetActiveShopTurrets(false, this.gameObject.transform.position);
        panelShopTurrets.SetActive(false);
        this.rend = GetComponent<Renderer>();
        this.color = this.rend.material.color;
    }
    private void OnMouseEnter()
    {
        if (gameManager.GetIsGameOver() == true)
            return;
        rend.material.color = Color.green;
    }
    private void OnMouseExit()
    {
        rend.material.color = this.color;
    }
    private void OnMouseDown()
    {
        if (gameManager.GetIsGameOver() == true)
            return;
        singletonShopTurrets.SetActiveShopTurrets(true, this.gameObject.transform.position);
        panelShopTurrets.SetActive(true);
    }
}
