using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000761 RID: 1889
[AddComponentMenu("NGUI/Interaction/Button Play Animation")]
public class UIButtonPlayAnimation : MonoBehaviour
{
	// Token: 0x060044D4 RID: 17620 RVA: 0x0010DAD4 File Offset: 0x0010BCD4
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x060044D5 RID: 17621 RVA: 0x0010DAE0 File Offset: 0x0010BCE0
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060044D6 RID: 17622 RVA: 0x0010DB0C File Offset: 0x0010BD0C
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (this.trigger == Trigger.OnHover || (this.trigger == Trigger.OnHoverTrue && isOver) || (this.trigger == Trigger.OnHoverFalse && !isOver))
			{
				this.Play(isOver);
			}
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x060044D7 RID: 17623 RVA: 0x0010DB64 File Offset: 0x0010BD64
	private void OnPress(bool isPressed)
	{
		if (base.enabled && (this.trigger == Trigger.OnPress || (this.trigger == Trigger.OnPressTrue && isPressed) || (this.trigger == Trigger.OnPressFalse && !isPressed)))
		{
			this.Play(isPressed);
		}
	}

	// Token: 0x060044D8 RID: 17624 RVA: 0x0010DBB4 File Offset: 0x0010BDB4
	private void OnClick()
	{
		if (base.enabled && this.trigger == Trigger.OnClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x060044D9 RID: 17625 RVA: 0x0010DBD4 File Offset: 0x0010BDD4
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == Trigger.OnDoubleClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x060044DA RID: 17626 RVA: 0x0010DBF8 File Offset: 0x0010BDF8
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (this.trigger == Trigger.OnSelect || (this.trigger == Trigger.OnSelectTrue && isSelected) || (this.trigger == Trigger.OnSelectFalse && !isSelected)))
		{
			this.Play(true);
		}
	}

	// Token: 0x060044DB RID: 17627 RVA: 0x0010DC4C File Offset: 0x0010BE4C
	private void OnActivate(bool isActive)
	{
		if (base.enabled && (this.trigger == Trigger.OnActivate || (this.trigger == Trigger.OnActivateTrue && isActive) || (this.trigger == Trigger.OnActivateFalse && !isActive)))
		{
			this.Play(isActive);
		}
	}

	// Token: 0x060044DC RID: 17628 RVA: 0x0010DC9C File Offset: 0x0010BE9C
	private void Play(bool forward)
	{
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<Animation>();
		}
		if (this.target != null)
		{
			if (this.clearSelection && UICamera.selectedObject == base.gameObject)
			{
				UICamera.selectedObject = null;
			}
			int num = (int)(-(int)this.playDirection);
			Direction direction = (Direction)((!forward) ? num : ((int)this.playDirection));
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.target, this.clipName, direction, this.ifDisabledOnPlay, this.disableWhenFinished);
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

	// Token: 0x0400250B RID: 9483
	public Animation target;

	// Token: 0x0400250C RID: 9484
	public string clipName;

	// Token: 0x0400250D RID: 9485
	public Trigger trigger;

	// Token: 0x0400250E RID: 9486
	public Direction playDirection = Direction.Forward;

	// Token: 0x0400250F RID: 9487
	public bool resetOnPlay;

	// Token: 0x04002510 RID: 9488
	public bool clearSelection;

	// Token: 0x04002511 RID: 9489
	public EnableCondition ifDisabledOnPlay;

	// Token: 0x04002512 RID: 9490
	public DisableCondition disableWhenFinished;

	// Token: 0x04002513 RID: 9491
	public GameObject eventReceiver;

	// Token: 0x04002514 RID: 9492
	public string callWhenFinished;

	// Token: 0x04002515 RID: 9493
	private bool mStarted;

	// Token: 0x04002516 RID: 9494
	private bool mHighlighted;
}
