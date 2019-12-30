using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryptoFx : MonoBehaviour
{
    public List<GameObject> CryptoEffects;
    public CryptoEffect effect;
    public Color color;

    public enum CryptoEffect { Effect1, Effect2, Effect3, Effect4, Effect5, Effect6, Effect7, Effect8, Effect9, Effect10, Effect11, Effect12, Effect13, Effect14, Effect15, Effect16, Effect17, Effect18, Effect19, Effect20, Effect21}
    // Start is called before the first frame update
    void Start()
    {
        Transform parent = transform.parent;
        GameObject currentInstance = Instantiate(CryptoEffects[(int)effect], parent);
        PSMeshRendererUpdater psUpdater = currentInstance.GetComponent<PSMeshRendererUpdater>();
        psUpdater.Color = color;
        psUpdater.UpdateMeshEffect(parent.gameObject);
    }
}