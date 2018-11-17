using System;
using UnityEngine;

// Token: 0x02000863 RID: 2147
[RequireComponent(typeof(global::UISlider))]
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Examples/Slider Colors")]
public class UISliderColors : MonoBehaviour
{
	// Token: 0x06004A02 RID: 18946 RVA: 0x0011CDC4 File Offset: 0x0011AFC4
	private void Start()
	{
		this.mSlider = base.GetComponent<global::UISlider>();
		this.Update();
	}

	// Token: 0x06004A03 RID: 18947 RVA: 0x0011CDD8 File Offset: 0x0011AFD8
	private void Update()
	{
		if (this.sprite == null || this.colors.Length == 0)
		{
			return;
		}
		float num = this.mSlider.sliderValue;
		num *= (float)(this.colors.Length - 1);
		int num2 = Mathf.FloorToInt(num);
		Color color = this.colors[0];
		if (num2 >= 0)
		{
			if (num2 + 1 < this.colors.Length)
			{
				float num3 = num - (float)num2;
				color = Color.Lerp(this.colors[num2], this.colors[num2 + 1], num3);
			}
			else if (num2 < this.colors.Length)
			{
				color = this.colors[num2];
			}
			else
			{
				color = this.colors[this.colors.Length - 1];
			}
		}
		color.a = this.sprite.color.a;
		this.sprite.color = color;
	}

	// Token: 0x04002830 RID: 10288
	public global::UISprite sprite;

	// Token: 0x04002831 RID: 10289
	public Color[] colors = new Color[]
	{
		Color.red,
		Color.yellow,
		Color.green
	};

	// Token: 0x04002832 RID: 10290
	private global::UISlider mSlider;
}
