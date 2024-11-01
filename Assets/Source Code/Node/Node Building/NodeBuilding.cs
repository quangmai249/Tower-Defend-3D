using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class NodeBuilding : MonoBehaviour
{
    [SerializeField] GameObject shopTurretCanvas;
    [SerializeField] string canvasShopTag = "Canvas Shop Turrets";
    [SerializeField] string canvasUpgradeTag = "Canvas Upgrade Turrets";
    [SerializeField] string btnConfirmTag = "Button Confirm Shop Turret";
    [SerializeField] string gameManagerTag = "GameController";
    [SerializeField] float yPos = 2f;

    [Header("Stats")]
    [SerializeField] string textTurretStatsTag = "Text Turret Stats";
    [SerializeField] string imgTurretStatsTag = "Image Turret Stats";

    private Renderer rend;
    private Color color;
    private GameManager gameManager;
    private GameObject shopCanvas;
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag(this.gameManagerTag)
            .GetComponent<GameManager>();
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
        SelectTarget.SetActiveGameObjecstWithTag(false, this.canvasUpgradeTag);

        SelectTarget.SelectFirstGameObjectWithTag(this.textTurretStatsTag).GetComponent<TextMeshProUGUI>().text = string.Empty;
        SelectTarget.SelectFirstGameObjectWithTag(this.imgTurretStatsTag).GetComponent<RawImage>().color = Color.clear;

        this.shopCanvas.gameObject.SetActive(true);
        this.shopCanvas.gameObject.transform.position
            = new Vector3(this.transform.position.x, this.yPos, this.transform.position.z);
    }
}
