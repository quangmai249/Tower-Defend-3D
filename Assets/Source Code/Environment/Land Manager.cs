using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LandManager : MonoBehaviour
{
    [SerializeField] string canvasShopTag = "Canvas Shop Turrets";
    [SerializeField] string canvasUpgradeTag = "Canvas Upgrade Turrets";
    [SerializeField] string btnConfirmShopTag = "Button Confirm Shop Turret";
    [SerializeField] string btnConfirmUpgradeTag = "Button Confirm Upgrade Turret";
    [SerializeField] string textTurretStatsTag = "Text Turret Stats";
    [SerializeField] string imgTurretStatsTag = "Image Turret Stats";

    private GameObject textTurretStats;
    private GameObject imgTurretStats;
    private void Start()
    {
        this.textTurretStats = SelectTarget.SelectFirstGameObjectWithTag(this.textTurretStatsTag);
        this.imgTurretStats = SelectTarget.SelectFirstGameObjectWithTag(this.imgTurretStatsTag);

        if (this.textTurretStats != null && this.imgTurretStats != null)
        {
            SelectTarget.SelectFirstGameObjectWithTag(this.textTurretStatsTag).GetComponent<TextMeshProUGUI>().text = string.Empty;
            SelectTarget.SelectFirstGameObjectWithTag(this.imgTurretStatsTag).GetComponent<RawImage>().texture = null;
            SelectTarget.SelectFirstGameObjectWithTag(this.imgTurretStatsTag).GetComponent<RawImage>().color = Color.clear;
        }
    }
    public void ClickedOutSite()
    {
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmShopTag);
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmUpgradeTag);
        SelectTarget.SetActiveGameObjecstWithTag(false, this.canvasShopTag);
        SelectTarget.SetActiveGameObjecstWithTag(false, this.canvasUpgradeTag);

        if (this.textTurretStats != null && this.imgTurretStats != null)
        {
            SelectTarget.SelectFirstGameObjectWithTag(this.textTurretStatsTag).GetComponent<TextMeshProUGUI>().text = string.Empty;
            SelectTarget.SelectFirstGameObjectWithTag(this.imgTurretStatsTag).GetComponent<RawImage>().texture = null;
            SelectTarget.SelectFirstGameObjectWithTag(this.imgTurretStatsTag).GetComponent<RawImage>().color = Color.clear;
        }
    }
}
