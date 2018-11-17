using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000849 RID: 2121
[AddComponentMenu("NGUI/Interaction/Checkbox")]
public class UICheckbox : MonoBehaviour
{
	// Token: 0x17000DFF RID: 3583
	// (get) Token: 0x0600495C RID: 18780 RVA: 0x00118030 File Offset: 0x00116230
	// (set) Token: 0x0600495D RID: 18781 RVA: 0x00118038 File Offset: 0x00116238
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

	// Token: 0x0600495E RID: 18782 RVA: 0x0011807C File Offset: 0x0011627C
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

	// Token: 0x0600495F RID: 18783 RVA: 0x00118100 File Offset: 0x00116300
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

	// Token: 0x06004960 RID: 18784 RVA: 0x0011814C File Offset: 0x0011634C
	private void OnClick()
	{
		if (base.enabled)
		{
			this.isChecked = !this.isChecked;
		}
	}

	// Token: 0x06004961 RID: 18785 RVA: 0x00118168 File Offset: 0x00116368
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
				global::UICheckbox[] componentsInChildren = this.radioButtonRoot.GetComponentsInChildren<global::UICheckbox>(true);
				int i = 0;
				int num = componentsInChildren.Length;
				while (i < num)
				{
					global::UICheckbox uicheckbox = componentsInChildren[i];
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
				global::TweenColor.Begin(this.checkSprite.gameObject, 0.2f, color);
			}
			if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName))
			{
				global::UICheckbox.current = this;
				this.eventReceiver.SendMessage(this.functionName, this.mChecked, 1);
			}
			if (this.checkAnimation != null)
			{
				global::ActiveAnimation.Play(this.checkAnimation, (!state) ? AnimationOrTween.Direction.Reverse : AnimationOrTween.Direction.Forward);
			}
		}
	}

	// Token: 0x04002775 RID: 10101
	public static global::UICheckbox current;

	// Token: 0x04002776 RID: 10102
	public global::UISprite checkSprite;

	// Token: 0x04002777 RID: 10103
	public Animation checkAnimation;

	// Token: 0x04002778 RID: 10104
	public GameObject eventReceiver;

	// Token: 0x04002779 RID: 10105
	public string functionName = "OnActivate";

	// Token: 0x0400277A RID: 10106
	public bool startsChecked = true;

	// Token: 0x0400277B RID: 10107
	public Transform radioButtonRoot;

	// Token: 0x0400277C RID: 10108
	public bool optionCanBeNone;

	// Token: 0x0400277D RID: 10109
	[HideInInspector]
	[SerializeField]
	private bool option;

	// Token: 0x0400277E RID: 10110
	private bool mChecked = true;

	// Token: 0x0400277F RID: 10111
	private bool mStarted;

	// Token: 0x04002780 RID: 10112
	private Transform mTrans;
}
