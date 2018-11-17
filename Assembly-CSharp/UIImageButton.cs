using System;
using UnityEngine;

// Token: 0x02000775 RID: 1909
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Image Button")]
public class UIImageButton : MonoBehaviour
{
	// Token: 0x06004552 RID: 17746 RVA: 0x001110D0 File Offset: 0x0010F2D0
	private void Start()
	{
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<UISprite>();
		}
	}

	// Token: 0x06004553 RID: 17747 RVA: 0x001110F0 File Offset: 0x0010F2F0
	private void OnHover(bool isOver)
	{
		if (base.enabled && this.target != null)
		{
			this.target.spriteName = ((!isOver) ? this.normalSprite : this.hoverSprite);
			this.target.MakePixelPerfect();
		}
	}

	// Token: 0x06004554 RID: 17748 RVA: 0x00111148 File Offset: 0x0010F348
	private void OnPress(bool pressed)
	{
		if (base.enabled && this.target != null)
		{
			this.target.spriteName = ((!pressed) ? this.normalSprite : this.pressedSprite);
			this.target.MakePixelPerfect();
		}
	}

	// Token: 0x040025AD RID: 9645
	public UISprite target;

	// Token: 0x040025AE RID: 9646
	public string normalSprite;

	// Token: 0x040025AF RID: 9647
	public string hoverSprite;

	// Token: 0x040025B0 RID: 9648
	public string pressedSprite;
}
