using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ParticalControl : MonoBehaviour
{
    VisualEffect visualEffectTarget;
    [SerializeField] private string targetValueName;
    private int nameID;
    public float effectScale = 1f;
    // Start is called before the first frame update
    void Start()
    {
        if(visualEffectTarget == null)
        {
            visualEffectTarget = GetComponent<VisualEffect>();
        }
        nameID = Shader.PropertyToID(targetValueName);
    }



    public void SetValue(float value)
    {
        visualEffectTarget.SetFloat(nameID, value);
    }
}
