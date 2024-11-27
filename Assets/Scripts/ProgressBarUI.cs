using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter _cuttingCounter;
    [SerializeField] private Image _barImage;

    private void Start()
    {
        _cuttingCounter.OnProgress_Changed += CuttingCounter_ProgressChanged;
        _barImage.fillAmount = 0;
        Hide();
    }

    // private void OnDisable()
    // {
    //     _cuttingCounter.OnProgress_Changed -= CuttingCounter_ProgressChanged;
    // }

    private void CuttingCounter_ProgressChanged(object Sender, CuttingCounter.OnProgreessChangedEventArgs e)
    {
        _barImage.fillAmount = e.ProgreessNormalized;
        if(e.ProgreessNormalized==0 || e.ProgreessNormalized==1)
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
