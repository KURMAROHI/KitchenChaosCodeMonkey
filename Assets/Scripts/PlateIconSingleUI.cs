using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    [SerializeField] private Image _kitchenObjImage;
    public void SetKitchenObjectSO(KitchenObjects kitchenObjects)
    {
        _kitchenObjImage.sprite = kitchenObjects.Spritee;
    }
}
