using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeBuilding : MonoBehaviour
{
    [SerializeField] GameObject shopTurretCanvas;
    private Renderer rend;
    private Color color;
    private GameManager gameManager;
    private GameObject shopCanvas;
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    private void Start()
    {
        this.shopCanvas = Instantiate(shopTurretCanvas);
        this.shopCanvas.transform.SetParent(this.gameObject.transform);
        this.shopCanvas.gameObject.SetActive(false);

        this.rend = GetComponent<Renderer>();
        this.color = this.rend.material.color;
    }
    private void OnMouseEnter()
    {
        if (gameManager.GetIsGameOver() == false)
            rend.material.color = Color.green;
    }
    private void OnMouseExit()
    {
        rend.material.color = this.color;
    }
    private void OnMouseDown()
    {
        if (gameManager.GetIsGameOver() == false)
        {
            this.shopCanvas.gameObject.SetActive(true);
            this.shopCanvas.gameObject.transform.position = this.gameObject.transform.position + (3 * Vector3.up);
        }
    }
}
