using System;
using UnityEngine;

// Token: 0x0200077F RID: 1919
[AddComponentMenu("NGUI/Examples/Slider Colors")]
[RequireComponent(typeof(UISlider))]
[ExecuteInEditMode]
public class UISliderColors : MonoBehaviour
{
	// Token: 0x06004599 RID: 17817 RVA: 0x00113444 File Offset: 0x00111644
	private void Start()
	{
		this.mSlider = base.GetComponent<UISlider>();
		this.Update();
	}

	// Token: 0x0600459A RID: 17818 RVA: 0x00113458 File Offset: 0x00111658
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

	// Token: 0x040025F9 RID: 9721
	public UISprite sprite;

	// Token: 0x040025FA RID: 9722
	public Color[] colors = new Color[]
	{
		Color.red,
		Color.yellow,
		Color.green
	};

	// Token: 0x040025FB RID: 9723
	private UISlider mSlider;
}
