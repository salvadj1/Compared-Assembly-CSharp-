using System;
using UnityEngine;

// Token: 0x0200074D RID: 1869
public class SurfaceScript : MonoBehaviour
{
	// Token: 0x0600444C RID: 17484 RVA: 0x0010A774 File Offset: 0x00108974
	private void Start()
	{
		Material material;
		if (base.transform.parent.GetComponent<MarkerScript>().objectScript.materialType == 0)
		{
			material = (Material)Object.Instantiate(Resources.Load("surfaceMaterial", typeof(Material)));
		}
		else
		{
			material = (Material)Object.Instantiate(Resources.Load("surfaceAlphaMaterial", typeof(Material)));
		}
		material.color.a = base.transform.parent.GetComponent<MarkerScript>().objectScript.surfaceOpacity;
		base.gameObject.renderer.sharedMaterial = material;
	}
}
