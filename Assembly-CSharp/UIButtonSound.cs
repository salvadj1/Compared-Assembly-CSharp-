using System;
using UnityEngine;

// Token: 0x02000846 RID: 2118
[AddComponentMenu("NGUI/Interaction/Button Sound")]
public class UIButtonSound : MonoBehaviour
{
	// Token: 0x0600494D RID: 18765 RVA: 0x00117B28 File Offset: 0x00115D28
	private void OnHover(bool isOver)
	{
		if (base.enabled && ((isOver && this.trigger == global::UIButtonSound.Trigger.OnMouseOver) || (!isOver && this.trigger == global::UIButtonSound.Trigger.OnMouseOut)))
		{
			global::NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x0600494E RID: 18766 RVA: 0x00117B7C File Offset: 0x00115D7C
	private void OnPress(bool isPressed)
	{
		if (base.enabled && ((isPressed && this.trigger == global::UIButtonSound.Trigger.OnPress) || (!isPressed && this.trigger == global::UIButtonSound.Trigger.OnRelease)))
		{
			global::NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x0600494F RID: 18767 RVA: 0x00117BD0 File Offset: 0x00115DD0
	private void OnClick()
	{
		if (base.enabled && this.trigger == global::UIButtonSound.Trigger.OnClick)
		{
			global::NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x0400275E RID: 10078
	public AudioClip audioClip;

	// Token: 0x0400275F RID: 10079
	public global::UIButtonSound.Trigger trigger;

	// Token: 0x04002760 RID: 10080
	public float volume = 1f;

	// Token: 0x04002761 RID: 10081
	public float pitch = 1f;

	// Token: 0x02000847 RID: 2119
	public enum Trigger
	{
		// Token: 0x04002763 RID: 10083
		OnClick,
		// Token: 0x04002764 RID: 10084
		OnMouseOver,
		// Token: 0x04002765 RID: 10085
		OnMouseOut,
		// Token: 0x04002766 RID: 10086
		OnPress,
		// Token: 0x04002767 RID: 10087
		OnRelease
	}
}
