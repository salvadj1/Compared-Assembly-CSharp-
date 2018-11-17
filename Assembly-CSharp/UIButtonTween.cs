using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000848 RID: 2120
[AddComponentMenu("NGUI/Interaction/Button Tween")]
public class UIButtonTween : MonoBehaviour
{
	// Token: 0x06004951 RID: 18769 RVA: 0x00117C1C File Offset: 0x00115E1C
	private void Start()
	{
		this.mStarted = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.gameObject;
		}
	}

	// Token: 0x06004952 RID: 18770 RVA: 0x00117C50 File Offset: 0x00115E50
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(global::UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06004953 RID: 18771 RVA: 0x00117C7C File Offset: 0x00115E7C
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

	// Token: 0x06004954 RID: 18772 RVA: 0x00117CD4 File Offset: 0x00115ED4
	private void OnPress(bool isPressed)
	{
		if (base.enabled && (this.trigger == AnimationOrTween.Trigger.OnPress || (this.trigger == AnimationOrTween.Trigger.OnPressTrue && isPressed) || (this.trigger == AnimationOrTween.Trigger.OnPressFalse && !isPressed)))
		{
			this.Play(isPressed);
		}
	}

	// Token: 0x06004955 RID: 18773 RVA: 0x00117D24 File Offset: 0x00115F24
	private void OnClick()
	{
		if (base.enabled && this.trigger == AnimationOrTween.Trigger.OnClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x06004956 RID: 18774 RVA: 0x00117D44 File Offset: 0x00115F44
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == AnimationOrTween.Trigger.OnDoubleClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x06004957 RID: 18775 RVA: 0x00117D68 File Offset: 0x00115F68
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (this.trigger == AnimationOrTween.Trigger.OnSelect || (this.trigger == AnimationOrTween.Trigger.OnSelectTrue && isSelected) || (this.trigger == AnimationOrTween.Trigger.OnSelectFalse && !isSelected)))
		{
			this.Play(true);
		}
	}

	// Token: 0x06004958 RID: 18776 RVA: 0x00117DBC File Offset: 0x00115FBC
	private void OnActivate(bool isActive)
	{
		if (base.enabled && (this.trigger == AnimationOrTween.Trigger.OnActivate || (this.trigger == AnimationOrTween.Trigger.OnActivateTrue && isActive) || (this.trigger == AnimationOrTween.Trigger.OnActivateFalse && !isActive)))
		{
			this.Play(isActive);
		}
	}

	// Token: 0x06004959 RID: 18777 RVA: 0x00117E0C File Offset: 0x0011600C
	private void Update()
	{
		if (this.disableWhenFinished != AnimationOrTween.DisableCondition.DoNotDisable && this.mTweens != null)
		{
			bool flag = true;
			bool flag2 = true;
			int i = 0;
			int num = this.mTweens.Length;
			while (i < num)
			{
				global::UITweener uitweener = this.mTweens[i];
				if (uitweener.enabled)
				{
					flag = false;
					break;
				}
				if (uitweener.direction != (AnimationOrTween.Direction)this.disableWhenFinished)
				{
					flag2 = false;
				}
				i++;
			}
			if (flag)
			{
				if (flag2)
				{
					global::NGUITools.SetActive(this.tweenTarget, false);
				}
				this.mTweens = null;
			}
		}
	}

	// Token: 0x0600495A RID: 18778 RVA: 0x00117EA0 File Offset: 0x001160A0
	public void Play(bool forward)
	{
		GameObject gameObject = (!(this.tweenTarget == null)) ? this.tweenTarget : base.gameObject;
		if (!gameObject.activeInHierarchy)
		{
			if (this.ifDisabledOnPlay != AnimationOrTween.EnableCondition.EnableThenPlay)
			{
				return;
			}
			global::NGUITools.SetActive(gameObject, true);
		}
		this.mTweens = ((!this.includeChildren) ? gameObject.GetComponents<global::UITweener>() : gameObject.GetComponentsInChildren<global::UITweener>());
		if (this.mTweens.Length == 0)
		{
			if (this.disableWhenFinished != AnimationOrTween.DisableCondition.DoNotDisable)
			{
				global::NGUITools.SetActive(this.tweenTarget, false);
			}
		}
		else
		{
			bool flag = false;
			if (this.playDirection == AnimationOrTween.Direction.Reverse)
			{
				forward = !forward;
			}
			int i = 0;
			int num = this.mTweens.Length;
			while (i < num)
			{
				global::UITweener uitweener = this.mTweens[i];
				if (uitweener.tweenGroup == this.tweenGroup)
				{
					if (!flag && !gameObject.activeInHierarchy)
					{
						flag = true;
						global::NGUITools.SetActive(gameObject, true);
					}
					if (this.playDirection == AnimationOrTween.Direction.Toggle)
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

	// Token: 0x04002768 RID: 10088
	public GameObject tweenTarget;

	// Token: 0x04002769 RID: 10089
	public int tweenGroup;

	// Token: 0x0400276A RID: 10090
	public AnimationOrTween.Trigger trigger;

	// Token: 0x0400276B RID: 10091
	public AnimationOrTween.Direction playDirection = AnimationOrTween.Direction.Forward;

	// Token: 0x0400276C RID: 10092
	public bool resetOnPlay;

	// Token: 0x0400276D RID: 10093
	public AnimationOrTween.EnableCondition ifDisabledOnPlay;

	// Token: 0x0400276E RID: 10094
	public AnimationOrTween.DisableCondition disableWhenFinished;

	// Token: 0x0400276F RID: 10095
	public bool includeChildren;

	// Token: 0x04002770 RID: 10096
	public GameObject eventReceiver;

	// Token: 0x04002771 RID: 10097
	public string callWhenFinished;

	// Token: 0x04002772 RID: 10098
	private global::UITweener[] mTweens;

	// Token: 0x04002773 RID: 10099
	private bool mStarted;

	// Token: 0x04002774 RID: 10100
	private bool mHighlighted;
}
