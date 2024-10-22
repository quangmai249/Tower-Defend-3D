using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField] string canvasShopTag = "Canvas Shop Turrets";
    [SerializeField] string canvasUpgradeTag = "Canvas Upgrade Turrets";
    [SerializeField] string btnConfirmShopTag = "Button Confirm Shop Turret";
    [SerializeField] string btnConfirmUpgradeTag = "Button Confirm Upgrade Turret";
    public void ClickedOutSite()
    {
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmShopTag);
        SelectTarget.SetActiveGameObjecstWithTag(false, this.btnConfirmUpgradeTag);
        SelectTarget.SetActiveGameObjecstWithTag(false, this.canvasShopTag);
        SelectTarget.SetActiveGameObjecstWithTag(false, this.canvasUpgradeTag);
    }
}
