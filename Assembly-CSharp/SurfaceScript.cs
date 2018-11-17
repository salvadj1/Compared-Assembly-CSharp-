using System;
using UnityEngine;

// Token: 0x0200082F RID: 2095
public class SurfaceScript : MonoBehaviour
{
	// Token: 0x060048AD RID: 18605 RVA: 0x001140F4 File Offset: 0x001122F4
	private void Start()
	{
		Material material;
		if (base.transform.parent.GetComponent<global::MarkerScript>().objectScript.materialType == 0)
		{
			material = (Material)Object.Instantiate(UnityEngine.Resources.Load("surfaceMaterial", typeof(Material)));
		}
		else
		{
			material = (Material)Object.Instantiate(UnityEngine.Resources.Load("surfaceAlphaMaterial", typeof(Material)));
		}
		material.color.a = base.transform.parent.GetComponent<global::MarkerScript>().objectScript.surfaceOpacity;
		base.gameObject.renderer.sharedMaterial = material;
	}
}
