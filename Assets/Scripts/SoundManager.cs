using System;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioClipsRefsSO _audioClipsRefsSO;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        DeliveryManager.InStance.OnRecipeSucess += DeliveryManager_OnRecipeSucess;
        DeliveryManager.InStance.OnRecipeFail += DeliveryManager_OnRecipeFail;
        CuttingCounter.OnAnyCut += CuttinCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrash += TrashCounter_OnAnyObjectTrash;
    }


    private void OnDisable()
    {
        DeliveryManager.InStance.OnRecipeSucess -= DeliveryManager_OnRecipeSucess;
        DeliveryManager.InStance.OnRecipeFail -= DeliveryManager_OnRecipeFail;
        CuttingCounter.OnAnyCut -= CuttinCounter_OnAnyCut;
        Player.Instance.OnPickedSomething -= Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere -= BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrash -= TrashCounter_OnAnyObjectTrash;
    }
    private void TrashCounter_OnAnyObjectTrash(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(_audioClipsRefsSO.Trash, trashCounter.transform.position);
    }
    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(_audioClipsRefsSO.ObjectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, EventArgs e)
    {
        PlaySound(_audioClipsRefsSO.ObjectPickUp, Player.Instance.transform.position);
    }

    private void CuttinCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(_audioClipsRefsSO.Chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSucess(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(_audioClipsRefsSO.DeliverySucess, deliveryCounter.transform.position);
    }
    private void DeliveryManager_OnRecipeFail(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(_audioClipsRefsSO.DeliveryFail, deliveryCounter.transform.position);
    }
    private void PlaySound(AudioClip[] audioCliparray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioCliparray[UnityEngine.Random.Range(0, audioCliparray.Length)], position, volume);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayFootStepSound(Vector3 position,float volume)
    {
        PlaySound(_audioClipsRefsSO.FootStep, position, volume);
    }
}
