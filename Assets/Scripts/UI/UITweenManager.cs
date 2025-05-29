using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITweenManager : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel, gameOverPanel, gameOverPanelContent, mainCanvas;
    private GameObject pressedBtn;
    //public GameObject modalToPopUp;
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
        LeanTween.moveX(settingsPanel, 540f, 0.2f).setDelay(.5f).setEase(LeanTweenType.easeInOutCirc);
        //mainCanvas.SetActive(false);
    }

    //Slides settings panel to the left/Return to irignal position
    public void settingsPanelSlideBackIn(GameObject settingsPanel)
    {
        //mainCanvas.SetActive(true);
        LeanTween.moveX(settingsPanel, -545f, 0.2f).setDelay(.5f).setEase(LeanTweenType.easeInOutCirc);
    }

    //Modal scales to 1f when activated
    public void modalScaleUp(GameObject modalToPopUp)
    {
        modalToPopUp.SetActive(true);
        GameObject modal = modalToPopUp.transform.GetChild(0).gameObject;
        LeanTween.scale(modal, new Vector3(1f, 1f, 0f), .6f).setDelay(.3f).setEase(LeanTweenType.easeOutElastic);
    }

    //Modal scales down to 0f when deactivated
    public void modalScaleDown(GameObject modalWrapper)
    {
        // Assuming the first child is the modal to animate
        GameObject modal = modalWrapper.transform.GetChild(0).gameObject;

        LeanTween.scale(modal, Vector3.zero, 0.3f).setEase(LeanTweenType.easeInBack).setOnComplete(() => { modalWrapper.SetActive(false); });
    }

    //Modal scales to 1f when activated
    public void settingsmodalScaleUp(GameObject modalToPopUp)
    {
        modalToPopUp.SetActive(true);
        LeanTween.scale(modalToPopUp, new Vector3(1f, 1f, 0f), .6f).setDelay(.3f).setEase(LeanTweenType.easeOutElastic);
    }

    //Modal scales down to 0f when deactivated
    public void settingsmodalScaleDown(GameObject modalWrapper)
    {
        // Assuming the first child is the modal to animate
        GameObject modal = modalWrapper.transform.GetChild(0).gameObject;

        LeanTween.scale(modal, Vector3.zero, 0.3f).setEase(LeanTweenType.easeInBack).setOnComplete(() => { modalWrapper.SetActive(false); });
    }

    //Panel moves up
    public void quizDone(GameObject gameOverPanelContent, GameObject gameOverPanel)
    {
        LeanTween.moveY(gameOverPanel, 448.5f, 0.6f).setEase(LeanTweenType.easeOutExpo);
    }
}
