using UnityEngine;

public class UITweenManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject gameOverPanelContent;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject gameOverPanel;
    private RectTransform rectTransSettingsPanel;
    private Vector2 openPosition;
    private Vector2 closedPosition;

    void Start()
    {
        if (settingsPanel != null)
        {
            rectTransSettingsPanel = settingsPanel.GetComponent<RectTransform>();

            // Save the current anchored position as the open (visible) position
            openPosition = rectTransSettingsPanel.anchoredPosition;

            // Compute closed position (e.g. off-screen to the left)
            closedPosition = new Vector2(-rectTransSettingsPanel.rect.width, openPosition.y);

            // Start hidden
            rectTransSettingsPanel.anchoredPosition = closedPosition;
        }
    }

    //Scales button down when pressed, and scales button up when released
    public void buttonPressed(GameObject pressedBtn)
    {
        LeanTween.scale(pressedBtn.gameObject, Vector3.one * 0.9f, 0.1f).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
        {
            LeanTween.scale(pressedBtn.gameObject, Vector3.one, 0.1f).setEase(LeanTweenType.easeOutBounce);
        });
    }

    //Slides settings panel to the right
    public void OpenSettings(GameObject settingsPanel)
    {
          LeanTween.move(rectTransSettingsPanel, openPosition, 0.2f)
            .setDelay(0.1f)
            .setEase(LeanTweenType.easeInOutCirc);
    }

    //Slides settings panel to the left/Return to irignal position
    public void HideSettings(GameObject settingsPanel)
    {
        LeanTween.move(rectTransSettingsPanel, closedPosition, 0.2f)
            .setDelay(0.1f)
            .setEase(LeanTweenType.easeInOutCirc);
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
    public void settingsModalScaleUp(GameObject modalToPopUp)
    {
        modalToPopUp.SetActive(true);
        //LeanTween.scale(modalToPopUp, new Vector3(1f, 1f, 0f), .6f).setDelay(.3f).setEase(LeanTweenType.easeOutElastic);
        // Assuming the first child is the modal to animate
        GameObject modal = modalToPopUp.transform.GetChild(0).gameObject;
        LeanTween.scale(modal, new Vector3(1f, 1f, 0f), .6f).setDelay(.3f).setEase(LeanTweenType.easeOutElastic);
    }

    //Modal scales down to 0f when deactivated
    public void settingsModalScaleDown(GameObject modalWrapper)
    {
        // Assuming the first child is the modal to animate
        GameObject modal = modalWrapper.transform.GetChild(0).gameObject;

        LeanTween.scale(modal, Vector3.zero, 0.3f).setEase(LeanTweenType.easeInBack).setOnComplete(() => { modalWrapper.SetActive(false); });
    }
}
