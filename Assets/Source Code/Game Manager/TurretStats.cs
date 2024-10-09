using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TurretStats
{
    private float priceTurret;
    private float priceUpgradeTurret;
    private float priceSellTurret;
    private float rangeTurret;
    private float damagedTurret;
    public TurretStats(float priceTurret, float priceUpgradeTurret, float priceSellTurret, float rangeTurret, float damagedTurret)
    {
        this.priceTurret = priceTurret;
        this.priceUpgradeTurret = priceUpgradeTurret;
        this.priceSellTurret = priceSellTurret;
        this.rangeTurret = rangeTurret;
        this.damagedTurret = damagedTurret;
    }
    public float PriceTurret { get => priceTurret; set => priceTurret = value; }
    public float PriceUpgradeTurret { get => priceUpgradeTurret; set => priceUpgradeTurret = value; }
    public float PriceSellTurret { get => priceSellTurret; set => priceSellTurret = value; }
    public float RangeTurret { get => rangeTurret; set => rangeTurret = value; }
    public float DamagedTurret { get => damagedTurret; set => damagedTurret = value; }
}
