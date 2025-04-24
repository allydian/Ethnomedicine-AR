using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITweenManager : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel, gameOverPanel, gameOverPanelContent;
    private GameObject pressedBtn, modalToPopUp;
    private RectTransform rectTransSettingsPanel;
    private Vector2 originalAnchoredPosSettingsPanel;

    //Scales button down when pressed, and scales button up when released
    public void buttonPressed(GameObject pressedBtn)
    {
        LeanTween.scale(pressedBtn.gameObject, Vector3.one * 0.9f, 0.1f).setEase(LeanTweenType.easeOutQuad).setOnComplete(() => {
        LeanTween.scale(pressedBtn.gameObject, Vector3.one, 0.1f).setEase(LeanTweenType.easeOutBounce);
        });
    }

    //Slides settings panel to the right
    public void settingsPanelSlideOut(GameObject settingsPanel)
    {
        LeanTween.moveX(settingsPanel, 1000f, 0.2f).setDelay(.5f).setEase(LeanTweenType.easeInOutCirc);
    }

    //Slides settings panel to the left/Return to irignal position
    public void settingsPanelSlideBackIn(GameObject settingsPanel)
    {
        LeanTween.moveX(settingsPanel, 0f, 0.2f).setDelay(.5f).setEase(LeanTweenType.easeInOutCirc);
    }

    //Modal scales to 1f when activated
    public void modalScaleUp(GameObject modalToPopUp)
    {

    }

    //Modal scales down to 0f when deactivated
    public void modalScaleDown(GameObject modalToPopUp)
    {
        
    }

    //Modal scales to 1f when activated
    public void quizDone(GameObject gameOverPanelContent)
    {

    }
}
