using System;
using UnityEngine;

// Token: 0x02000858 RID: 2136
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Image Button")]
public class UIImageButton : MonoBehaviour
{
	// Token: 0x060049B7 RID: 18871 RVA: 0x0011AA50 File Offset: 0x00118C50
	private void Start()
	{
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<global::UISprite>();
		}
	}

	// Token: 0x060049B8 RID: 18872 RVA: 0x0011AA70 File Offset: 0x00118C70
	private void OnHover(bool isOver)
	{
		if (base.enabled && this.target != null)
		{
			this.target.spriteName = ((!isOver) ? this.normalSprite : this.hoverSprite);
			this.target.MakePixelPerfect();
		}
	}

	// Token: 0x060049B9 RID: 18873 RVA: 0x0011AAC8 File Offset: 0x00118CC8
	private void OnPress(bool pressed)
	{
		if (base.enabled && this.target != null)
		{
			this.target.spriteName = ((!pressed) ? this.normalSprite : this.pressedSprite);
			this.target.MakePixelPerfect();
		}
	}

	// Token: 0x040027E4 RID: 10212
	public global::UISprite target;

	// Token: 0x040027E5 RID: 10213
	public string normalSprite;

	// Token: 0x040027E6 RID: 10214
	public string hoverSprite;

	// Token: 0x040027E7 RID: 10215
	public string pressedSprite;
}
