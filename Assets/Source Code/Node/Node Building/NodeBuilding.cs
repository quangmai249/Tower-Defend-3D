using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeBuilding : MonoBehaviour
{
    [SerializeField] GameObject shopTurretCanvas;
    [SerializeField] string canvasShopTag = "Canvas Shop Turrets";
    [SerializeField] string btnConfirmTag = "Button Confirm Shop Turret";

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
        if (gameManager.IsGameOver == true || gameManager.IsGamePause == true)
            return;

        rend.material.color = Color.green;
    }
    private void OnMouseExit()
    {
        rend.material.color = this.color;
    }
    private void OnMouseDown()
    {
        rend.material.color = Color.green;

        if (gameManager.IsGameOver == true || gameManager.IsGamePause == true)
            return;

        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmTag);
        SelectTarget.SetActiveGameObjecstWithTag(false, this.canvasShopTag);

        this.shopCanvas.gameObject.SetActive(true);
        this.shopCanvas.gameObject.transform.position = this.gameObject.transform.position;
    }
}
