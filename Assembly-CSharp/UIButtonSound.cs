using System;
using UnityEngine;

// Token: 0x02000764 RID: 1892
[AddComponentMenu("NGUI/Interaction/Button Sound")]
public class UIButtonSound : MonoBehaviour
{
	// Token: 0x060044EC RID: 17644 RVA: 0x0010E1A8 File Offset: 0x0010C3A8
	private void OnHover(bool isOver)
	{
		if (base.enabled && ((isOver && this.trigger == UIButtonSound.Trigger.OnMouseOver) || (!isOver && this.trigger == UIButtonSound.Trigger.OnMouseOut)))
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x060044ED RID: 17645 RVA: 0x0010E1FC File Offset: 0x0010C3FC
	private void OnPress(bool isPressed)
	{
		if (base.enabled && ((isPressed && this.trigger == UIButtonSound.Trigger.OnPress) || (!isPressed && this.trigger == UIButtonSound.Trigger.OnRelease)))
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x060044EE RID: 17646 RVA: 0x0010E250 File Offset: 0x0010C450
	private void OnClick()
	{
		if (base.enabled && this.trigger == UIButtonSound.Trigger.OnClick)
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x04002527 RID: 9511
	public AudioClip audioClip;

	// Token: 0x04002528 RID: 9512
	public UIButtonSound.Trigger trigger;

	// Token: 0x04002529 RID: 9513
	public float volume = 1f;

	// Token: 0x0400252A RID: 9514
	public float pitch = 1f;

	// Token: 0x02000765 RID: 1893
	public enum Trigger
	{
		// Token: 0x0400252C RID: 9516
		OnClick,
		// Token: 0x0400252D RID: 9517
		OnMouseOver,
		// Token: 0x0400252E RID: 9518
		OnMouseOut,
		// Token: 0x0400252F RID: 9519
		OnPress,
		// Token: 0x04002530 RID: 9520
		OnRelease
	}
}
