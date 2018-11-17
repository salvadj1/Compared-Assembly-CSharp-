using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000843 RID: 2115
[AddComponentMenu("NGUI/Interaction/Button Play Animation")]
public class UIButtonPlayAnimation : MonoBehaviour
{
	// Token: 0x06004935 RID: 18741 RVA: 0x00117454 File Offset: 0x00115654
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x06004936 RID: 18742 RVA: 0x00117460 File Offset: 0x00115660
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(global::UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06004937 RID: 18743 RVA: 0x0011748C File Offset: 0x0011568C
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (this.trigger == AnimationOrTween.Trigger.OnHover || (this.trigger == AnimationOrTween.Trigger.OnHoverTrue && isOver) || (this.trigger == AnimationOrTween.Trigger.OnHoverFalse && !isOver))
			{
				this.Play(isOver);
			}
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x06004938 RID: 18744 RVA: 0x001174E4 File Offset: 0x001156E4
	private void OnPress(bool isPressed)
	{
		if (base.enabled && (this.trigger == AnimationOrTween.Trigger.OnPress || (this.trigger == AnimationOrTween.Trigger.OnPressTrue && isPressed) || (this.trigger == AnimationOrTween.Trigger.OnPressFalse && !isPressed)))
		{
			this.Play(isPressed);
		}
	}

	// Token: 0x06004939 RID: 18745 RVA: 0x00117534 File Offset: 0x00115734
	private void OnClick()
	{
		if (base.enabled && this.trigger == AnimationOrTween.Trigger.OnClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x0600493A RID: 18746 RVA: 0x00117554 File Offset: 0x00115754
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == AnimationOrTween.Trigger.OnDoubleClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x0600493B RID: 18747 RVA: 0x00117578 File Offset: 0x00115778
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (this.trigger == AnimationOrTween.Trigger.OnSelect || (this.trigger == AnimationOrTween.Trigger.OnSelectTrue && isSelected) || (this.trigger == AnimationOrTween.Trigger.OnSelectFalse && !isSelected)))
		{
			this.Play(true);
		}
	}

	// Token: 0x0600493C RID: 18748 RVA: 0x001175CC File Offset: 0x001157CC
	private void OnActivate(bool isActive)
	{
		if (base.enabled && (this.trigger == AnimationOrTween.Trigger.OnActivate || (this.trigger == AnimationOrTween.Trigger.OnActivateTrue && isActive) || (this.trigger == AnimationOrTween.Trigger.OnActivateFalse && !isActive)))
		{
			this.Play(isActive);
		}
	}

	// Token: 0x0600493D RID: 18749 RVA: 0x0011761C File Offset: 0x0011581C
	private void Play(bool forward)
	{
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<Animation>();
		}
		if (this.target != null)
		{
			if (this.clearSelection && global::UICamera.selectedObject == base.gameObject)
			{
				global::UICamera.selectedObject = null;
			}
			int num = (int)(-(int)this.playDirection);
			AnimationOrTween.Direction direction = (AnimationOrTween.Direction)((!forward) ? num : ((int)this.playDirection));
			global::ActiveAnimation activeAnimation = global::ActiveAnimation.Play(this.target, this.clipName, direction, this.ifDisabledOnPlay, this.disableWhenFinished);
			if (this.resetOnPlay)
			{
				activeAnimation.Reset();
			}
			if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
			{
				activeAnimation.eventReceiver = this.eventReceiver;
				activeAnimation.callWhenFinished = this.callWhenFinished;
			}
		}
	}

	// Token: 0x04002742 RID: 10050
	public Animation target;

	// Token: 0x04002743 RID: 10051
	public string clipName;

	// Token: 0x04002744 RID: 10052
	public AnimationOrTween.Trigger trigger;

	// Token: 0x04002745 RID: 10053
	public AnimationOrTween.Direction playDirection = AnimationOrTween.Direction.Forward;

	// Token: 0x04002746 RID: 10054
	public bool resetOnPlay;

	// Token: 0x04002747 RID: 10055
	public bool clearSelection;

	// Token: 0x04002748 RID: 10056
	public AnimationOrTween.EnableCondition ifDisabledOnPlay;

	// Token: 0x04002749 RID: 10057
	public AnimationOrTween.DisableCondition disableWhenFinished;

	// Token: 0x0400274A RID: 10058
	public GameObject eventReceiver;

	// Token: 0x0400274B RID: 10059
	public string callWhenFinished;

	// Token: 0x0400274C RID: 10060
	private bool mStarted;

	// Token: 0x0400274D RID: 10061
	private bool mHighlighted;
}
