using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000766 RID: 1894
[AddComponentMenu("NGUI/Interaction/Button Tween")]
public class UIButtonTween : MonoBehaviour
{
	// Token: 0x060044F0 RID: 17648 RVA: 0x0010E29C File Offset: 0x0010C49C
	private void Start()
	{
		this.mStarted = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.gameObject;
		}
	}

	// Token: 0x060044F1 RID: 17649 RVA: 0x0010E2D0 File Offset: 0x0010C4D0
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060044F2 RID: 17650 RVA: 0x0010E2FC File Offset: 0x0010C4FC
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

	// Token: 0x060044F3 RID: 17651 RVA: 0x0010E354 File Offset: 0x0010C554
	private void OnPress(bool isPressed)
	{
		if (base.enabled && (this.trigger == Trigger.OnPress || (this.trigger == Trigger.OnPressTrue && isPressed) || (this.trigger == Trigger.OnPressFalse && !isPressed)))
		{
			this.Play(isPressed);
		}
	}

	// Token: 0x060044F4 RID: 17652 RVA: 0x0010E3A4 File Offset: 0x0010C5A4
	private void OnClick()
	{
		if (base.enabled && this.trigger == Trigger.OnClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x060044F5 RID: 17653 RVA: 0x0010E3C4 File Offset: 0x0010C5C4
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == Trigger.OnDoubleClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x060044F6 RID: 17654 RVA: 0x0010E3E8 File Offset: 0x0010C5E8
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (this.trigger == Trigger.OnSelect || (this.trigger == Trigger.OnSelectTrue && isSelected) || (this.trigger == Trigger.OnSelectFalse && !isSelected)))
		{
			this.Play(true);
		}
	}

	// Token: 0x060044F7 RID: 17655 RVA: 0x0010E43C File Offset: 0x0010C63C
	private void OnActivate(bool isActive)
	{
		if (base.enabled && (this.trigger == Trigger.OnActivate || (this.trigger == Trigger.OnActivateTrue && isActive) || (this.trigger == Trigger.OnActivateFalse && !isActive)))
		{
			this.Play(isActive);
		}
	}

	// Token: 0x060044F8 RID: 17656 RVA: 0x0010E48C File Offset: 0x0010C68C
	private void Update()
	{
		if (this.disableWhenFinished != DisableCondition.DoNotDisable && this.mTweens != null)
		{
			bool flag = true;
			bool flag2 = true;
			int i = 0;
			int num = this.mTweens.Length;
			while (i < num)
			{
				UITweener uitweener = this.mTweens[i];
				if (uitweener.enabled)
				{
					flag = false;
					break;
				}
				if (uitweener.direction != (Direction)this.disableWhenFinished)
				{
					flag2 = false;
				}
				i++;
			}
			if (flag)
			{
				if (flag2)
				{
					NGUITools.SetActive(this.tweenTarget, false);
				}
				this.mTweens = null;
			}
		}
	}

	// Token: 0x060044F9 RID: 17657 RVA: 0x0010E520 File Offset: 0x0010C720
	public void Play(bool forward)
	{
		GameObject gameObject = (!(this.tweenTarget == null)) ? this.tweenTarget : base.gameObject;
		if (!gameObject.activeInHierarchy)
		{
			if (this.ifDisabledOnPlay != EnableCondition.EnableThenPlay)
			{
				return;
			}
			NGUITools.SetActive(gameObject, true);
		}
		this.mTweens = ((!this.includeChildren) ? gameObject.GetComponents<UITweener>() : gameObject.GetComponentsInChildren<UITweener>());
		if (this.mTweens.Length == 0)
		{
			if (this.disableWhenFinished != DisableCondition.DoNotDisable)
			{
				NGUITools.SetActive(this.tweenTarget, false);
			}
		}
		else
		{
			bool flag = false;
			if (this.playDirection == Direction.Reverse)
			{
				forward = !forward;
			}
			int i = 0;
			int num = this.mTweens.Length;
			while (i < num)
			{
				UITweener uitweener = this.mTweens[i];
				if (uitweener.tweenGroup == this.tweenGroup)
				{
					if (!flag && !gameObject.activeInHierarchy)
					{
						flag = true;
						NGUITools.SetActive(gameObject, true);
					}
					if (this.playDirection == Direction.Toggle)
					{
						uitweener.Toggle();
					}
					else
					{
						uitweener.Play(forward);
					}
					if (this.resetOnPlay)
					{
						uitweener.Reset();
					}
					if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
					{
						uitweener.eventReceiver = this.eventReceiver;
						uitweener.callWhenFinished = this.callWhenFinished;
					}
				}
				i++;
			}
		}
	}

	// Token: 0x04002531 RID: 9521
	public GameObject tweenTarget;

	// Token: 0x04002532 RID: 9522
	public int tweenGroup;

	// Token: 0x04002533 RID: 9523
	public Trigger trigger;

	// Token: 0x04002534 RID: 9524
	public Direction playDirection = Direction.Forward;

	// Token: 0x04002535 RID: 9525
	public bool resetOnPlay;

	// Token: 0x04002536 RID: 9526
	public EnableCondition ifDisabledOnPlay;

	// Token: 0x04002537 RID: 9527
	public DisableCondition disableWhenFinished;

	// Token: 0x04002538 RID: 9528
	public bool includeChildren;

	// Token: 0x04002539 RID: 9529
	public GameObject eventReceiver;

	// Token: 0x0400253A RID: 9530
	public string callWhenFinished;

	// Token: 0x0400253B RID: 9531
	private UITweener[] mTweens;

	// Token: 0x0400253C RID: 9532
	private bool mStarted;

	// Token: 0x0400253D RID: 9533
	private bool mHighlighted;
}
