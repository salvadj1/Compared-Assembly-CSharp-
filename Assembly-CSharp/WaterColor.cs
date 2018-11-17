using System;
using UnityEngine;

// Token: 0x02000429 RID: 1065
[ExecuteInEditMode]
public class WaterColor : MonoBehaviour
{
	// Token: 0x06002790 RID: 10128 RVA: 0x0009A91C File Offset: 0x00098B1C
	private void Start()
	{
		this.sky = (TOD_Sky)Object.FindObjectOfType(typeof(TOD_Sky));
		this.water = base.GetComponent<WaterBase>();
	}

	// Token: 0x06002791 RID: 10129 RVA: 0x0009A950 File Offset: 0x00098B50
	private void Update()
	{
		if (!this.sky || !this.water)
		{
			return;
		}
		Color color = Color.Lerp(this.sky.FogColor, this.sky.AmbientColor, 0.5f);
		color.a = 1f;
		Color color2 = Color.Lerp(color, this.colorMain, this.colorLerp) * 0.8f;
		color2.a = 0.1f;
		Color color3 = color2 * 0.8f;
		color3.a = 0.75f;
		this.water.sharedMaterial.SetColor("_ReflectionColor", color2);
		this.water.sharedMaterial.SetColor("_BaseColor", color3);
	}

	// Token: 0x04001379 RID: 4985
	public Color colorMain = Color.green;

	// Token: 0x0400137A RID: 4986
	public float colorLerp = 0.5f;

	// Token: 0x0400137B RID: 4987
	private TOD_Sky sky;

	// Token: 0x0400137C RID: 4988
	private WaterBase water;
}
