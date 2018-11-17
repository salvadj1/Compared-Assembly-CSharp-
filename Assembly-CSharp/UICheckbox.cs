using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000767 RID: 1895
[AddComponentMenu("NGUI/Interaction/Checkbox")]
public class UICheckbox : MonoBehaviour
{
	// Token: 0x17000D6F RID: 3439
	// (get) Token: 0x060044FB RID: 17659 RVA: 0x0010E6B0 File Offset: 0x0010C8B0
	// (set) Token: 0x060044FC RID: 17660 RVA: 0x0010E6B8 File Offset: 0x0010C8B8
	public bool isChecked
	{
		get
		{
			return this.mChecked;
		}
		set
		{
			if (this.radioButtonRoot == null || value || this.optionCanBeNone || !this.mStarted)
			{
				this.Set(value);
			}
		}
	}

	// Token: 0x060044FD RID: 17661 RVA: 0x0010E6FC File Offset: 0x0010C8FC
	private void Awake()
	{
		this.mTrans = base.transform;
		if (this.checkSprite != null)
		{
			this.checkSprite.alpha = ((!this.startsChecked) ? 0f : 1f);
		}
		if (this.option)
		{
			this.option = false;
			if (this.radioButtonRoot == null)
			{
				this.radioButtonRoot = this.mTrans.parent;
			}
		}
	}

	// Token: 0x060044FE RID: 17662 RVA: 0x0010E780 File Offset: 0x0010C980
	private void Start()
	{
		if (this.eventReceiver == null)
		{
			this.eventReceiver = base.gameObject;
		}
		this.mChecked = !this.startsChecked;
		this.mStarted = true;
		this.Set(this.startsChecked);
	}

	// Token: 0x060044FF RID: 17663 RVA: 0x0010E7CC File Offset: 0x0010C9CC
	private void OnClick()
	{
		if (base.enabled)
		{
			this.isChecked = !this.isChecked;
		}
	}

	// Token: 0x06004500 RID: 17664 RVA: 0x0010E7E8 File Offset: 0x0010C9E8
	private void Set(bool state)
	{
		if (!this.mStarted)
		{
			this.mChecked = state;
			this.startsChecked = state;
			if (this.checkSprite != null)
			{
				this.checkSprite.alpha = ((!state) ? 0f : 1f);
			}
		}
		else if (this.mChecked != state)
		{
			if (this.radioButtonRoot != null && state)
			{
				UICheckbox[] componentsInChildren = this.radioButtonRoot.GetComponentsInChildren<UICheckbox>(true);
				int i = 0;
				int num = componentsInChildren.Length;
				while (i < num)
				{
					UICheckbox uicheckbox = componentsInChildren[i];
					if (uicheckbox != this && uicheckbox.radioButtonRoot == this.radioButtonRoot)
					{
						uicheckbox.Set(false);
					}
					i++;
				}
			}
			this.mChecked = state;
			if (this.checkSprite != null)
			{
				Color color = this.checkSprite.color;
				color.a = ((!this.mChecked) ? 0f : 1f);
				TweenColor.Begin(this.checkSprite.gameObject, 0.2f, color);
			}
			if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName))
			{
				UICheckbox.current = this;
				this.eventReceiver.SendMessage(this.functionName, this.mChecked, 1);
			}
			if (this.checkAnimation != null)
			{
				ActiveAnimation.Play(this.checkAnimation, (!state) ? Direction.Reverse : Direction.Forward);
			}
		}
	}

	// Token: 0x0400253E RID: 9534
	public static UICheckbox current;

	// Token: 0x0400253F RID: 9535
	public UISprite checkSprite;

	// Token: 0x04002540 RID: 9536
	public Animation checkAnimation;

	// Token: 0x04002541 RID: 9537
	public GameObject eventReceiver;

	// Token: 0x04002542 RID: 9538
	public string functionName = "OnActivate";

	// Token: 0x04002543 RID: 9539
	public bool startsChecked = true;

	// Token: 0x04002544 RID: 9540
	public Transform radioButtonRoot;

	// Token: 0x04002545 RID: 9541
	public bool optionCanBeNone;

	// Token: 0x04002546 RID: 9542
	[HideInInspector]
	[SerializeField]
	private bool option;

	// Token: 0x04002547 RID: 9543
	private bool mChecked = true;

	// Token: 0x04002548 RID: 9544
	private bool mStarted;

	// Token: 0x04002549 RID: 9545
	private Transform mTrans;
}
