using System;
using UnityEngine;

// Token: 0x020004DF RID: 1247
[ExecuteInEditMode]
public class WaterColor : MonoBehaviour
{
	// Token: 0x06002B20 RID: 11040 RVA: 0x000A089C File Offset: 0x0009EA9C
	private void Start()
	{
		this.sky = (global::TOD_Sky)Object.FindObjectOfType(typeof(global::TOD_Sky));
		this.water = base.GetComponent<WaterBase>();
	}

	// Token: 0x06002B21 RID: 11041 RVA: 0x000A08D0 File Offset: 0x0009EAD0
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

	// Token: 0x040014FC RID: 5372
	public Color colorMain = Color.green;

	// Token: 0x040014FD RID: 5373
	public float colorLerp = 0.5f;

	// Token: 0x040014FE RID: 5374
	private global::TOD_Sky sky;

	// Token: 0x040014FF RID: 5375
	private WaterBase water;
}
