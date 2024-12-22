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
    [SerializeField] string gameManagerTag = "GameController";
    [SerializeField] float yPos = 2f;

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

        if (gameManager.IsGameOver == true || gameManager.IsGamePause == true || gameManager.IsGameWinLevel)
            return;

        SelectTarget.SetActiveGameObjecstWithTag(false, GameObjectTagManager.TagButtonConfirmShopTurret);
        SelectTarget.SetActiveGameObjecstWithTag(false, GameObjectTagManager.TagButtonConfirmShopTurret);

        SelectTarget.SetActiveGameObjecstWithTag(false, GameObjectTagManager.TagCanvasShopTurrets);
        SelectTarget.SetActiveGameObjecstWithTag(false, GameObjectTagManager.TagCanvasUpgradeTurrets);

        SelectTarget.SelectFirstGameObjectWithTag(GameObjectTagManager.TagTextTurretStats).GetComponent<TextMeshProUGUI>().text = string.Empty;
        SelectTarget.SelectFirstGameObjectWithTag(GameObjectTagManager.TagImageTurretStats).GetComponent<RawImage>().color = Color.clear;

        this.shopCanvas.gameObject.SetActive(true);
        this.shopCanvas.gameObject.transform.position
            = new Vector3(this.transform.position.x, this.yPos, this.transform.position.z);
    }
}
