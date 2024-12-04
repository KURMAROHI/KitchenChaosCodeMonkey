using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject _hasProgressGameObject;
    [SerializeField] private Image _barImage;
    private IHasProgress _hasProgress;

    private void Start()
    {
        _hasProgress = _hasProgressGameObject.GetComponent<IHasProgress>();
        if (_hasProgress == null)
        {
            Debug.LogError("gameObject:" + _hasProgressGameObject + "Does not have that implemented IProgress");
        }
        _hasProgress.OnProgress_Changed += CuttingCounter_ProgressChanged;
        _barImage.fillAmount = 0;
        Hide();
    }

    // private void OnDisable()
    // {
    //     _cuttingCounter.OnProgress_Changed -= CuttingCounter_ProgressChanged;
    // }

    private void CuttingCounter_ProgressChanged(object Sender, IHasProgress.OnProgreessChangedEventArgs e)
    {
        _barImage.fillAmount = e.ProgreessNormalized;
        if (e.ProgreessNormalized == 0 || e.ProgreessNormalized == 1)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);

    }
}
