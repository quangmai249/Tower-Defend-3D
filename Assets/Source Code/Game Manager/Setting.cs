using UnityEngine;
public class Setting
{
    public bool isToggleFullScreen;
    public float volumeMusic;
    public float volumeFXSound;
    public int resolutionScreenWidth;
    public int resolutionScreenHeight;
    public Setting(bool isToggleFullScreen, float volumeMusic, float volumeFXSound, int resolutionScreenWidth, int resolutionScreenHeight)
    {
        this.isToggleFullScreen = isToggleFullScreen;
        this.volumeMusic = volumeMusic;
        this.volumeFXSound = volumeFXSound;
        this.resolutionScreenWidth = resolutionScreenWidth;
        this.resolutionScreenHeight = resolutionScreenHeight;
    }
}
