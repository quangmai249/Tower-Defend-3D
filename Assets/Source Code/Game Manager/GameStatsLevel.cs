using UnityEngine;
public class GameStatsLevel : MonoBehaviour
{
    protected GameStats SetStatsLevel(string s)
    {
        switch (s)
        {
            case "LEVEL_1":
                return new GameStats(2000, 10, 1, 5);
            case "LEVEL_2":
                return new GameStats(2500, 15, 1, 6);
            case "LEVEL_3":
                return new GameStats(3500, 20, 1, 7);
            //case "LEVEL_4":
            //    return new GameStats(2000, 10, 1, 5);
            //case "LEVEL_5":
            //    return new GameStats(2000, 10, 1, 5);
            //case "LEVEL_6":
            //    return new GameStats(2000, 10, 1, 5);
            //case "LEVEL_7":
            //    return new GameStats(2000, 10, 1, 5);
            default:
                break;
        }
        return null;
    }
}
