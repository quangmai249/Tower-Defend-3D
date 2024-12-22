using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LandManager : MonoBehaviour
{
    private GameObject textTurretStats;
    private GameObject imgTurretStats;
    private void Start()
    {
        this.textTurretStats = SelectTarget.SelectFirstGameObjectWithTag(GameObjectTagManager.TagTextTurretStats);
        this.imgTurretStats = SelectTarget.SelectFirstGameObjectWithTag(GameObjectTagManager.TagImageTurretStats);

        if (this.textTurretStats != null && this.imgTurretStats != null)
        {
            SelectTarget.SelectFirstGameObjectWithTag(GameObjectTagManager.TagTextTurretStats).GetComponent<TextMeshProUGUI>().text = string.Empty;
            SelectTarget.SelectFirstGameObjectWithTag(GameObjectTagManager.TagImageTurretStats).GetComponent<RawImage>().texture = null;
            SelectTarget.SelectFirstGameObjectWithTag(GameObjectTagManager.TagImageTurretStats).GetComponent<RawImage>().color = Color.clear;
        }
    }
    public void ClickedOutSite()
    {
        SelectTarget.SetActiveGameObjecstWithTag(false, GameObjectTagManager.TagButtonConfirmShopTurret);
        SelectTarget.SetActiveGameObjecstWithTag(false, GameObjectTagManager.TagButtonConfirmUpgradeTurret);
        SelectTarget.SetActiveGameObjecstWithTag(false, GameObjectTagManager.TagCanvasShopTurrets);
        SelectTarget.SetActiveGameObjecstWithTag(false, GameObjectTagManager.TagCanvasUpgradeTurrets);

        if (this.textTurretStats != null && this.imgTurretStats != null)
        {
            SelectTarget.SelectFirstGameObjectWithTag(GameObjectTagManager.TagTextTurretStats).GetComponent<TextMeshProUGUI>().text = string.Empty;
            SelectTarget.SelectFirstGameObjectWithTag(GameObjectTagManager.TagImageTurretStats).GetComponent<RawImage>().texture = null;
            SelectTarget.SelectFirstGameObjectWithTag(GameObjectTagManager.TagImageTurretStats).GetComponent<RawImage>().color = Color.clear;
        }
    }
}
