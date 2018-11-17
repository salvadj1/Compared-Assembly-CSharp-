using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000867 RID: 2151
[RequireComponent(typeof(Animation))]
[AddComponentMenu("NGUI/Internal/Active Animation")]
public class ActiveAnimation : global::IgnoreTimeScale
{
	// Token: 0x06004A0F RID: 18959 RVA: 0x0011D3F8 File Offset: 0x0011B5F8
	public void Reset()
	{
		if (this.mAnim != null)
		{
			foreach (object obj in this.mAnim)
			{
				AnimationState animationState = (AnimationState)obj;
				if (this.mLastDirection == AnimationOrTween.Direction.Reverse)
				{
					animationState.time = animationState.length;
				}
				else if (this.mLastDirection == AnimationOrTween.Direction.Forward)
				{
					animationState.time = 0f;
				}
			}
		}
	}

	// Token: 0x06004A10 RID: 18960 RVA: 0x0011D4A8 File Offset: 0x0011B6A8
	private void Update()
	{
		float num = base.UpdateRealTimeDelta();
		if (num == 0f)
		{
			return;
		}
		if (this.mAnim != null)
		{
			bool flag = false;
			foreach (object obj in this.mAnim)
			{
				AnimationState animationState = (AnimationState)obj;
				float num2 = animationState.speed * num;
				animationState.time += num2;
				if (num2 < 0f)
				{
					if (animationState.time > 0f)
					{
						flag = true;
					}
					else
					{
						animationState.time = 0f;
					}
				}
				else if (animationState.time < animationState.length)
				{
					flag = true;
				}
				else
				{
					animationState.time = animationState.length;
				}
			}
			this.mAnim.enabled = true;
			this.mAnim.Sample();
			this.mAnim.enabled = false;
			if (flag)
			{
				return;
			}
			if (this.mNotify)
			{
				this.mNotify = false;
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
				{
					this.eventReceiver.SendMessage(this.callWhenFinished, this, 1);
				}
				if (this.mDisableDirection != AnimationOrTween.Direction.Toggle && this.mLastDirection == this.mDisableDirection)
				{
					global::NGUITools.SetActive(base.gameObject, false);
				}
			}
		}
		base.enabled = false;
	}

	// Token: 0x06004A11 RID: 18961 RVA: 0x0011D648 File Offset: 0x0011B848
	private void Play(string clipName, AnimationOrTween.Direction playDirection)
	{
		if (this.mAnim != null)
		{
			this.mAnim.enabled = false;
			if (playDirection == AnimationOrTween.Direction.Toggle)
			{
				playDirection = ((this.mLastDirection == AnimationOrTween.Direction.Forward) ? AnimationOrTween.Direction.Reverse : AnimationOrTween.Direction.Forward);
			}
			bool flag = string.IsNullOrEmpty(clipName);
			if (flag)
			{
				if (!this.mAnim.isPlaying)
				{
					this.mAnim.Play();
				}
			}
			else if (!this.mAnim.IsPlaying(clipName))
			{
				this.mAnim.Play(clipName);
			}
			foreach (object obj in this.mAnim)
			{
				AnimationState animationState = (AnimationState)obj;
				if (string.IsNullOrEmpty(clipName) || animationState.name == clipName)
				{
					float num = Mathf.Abs(animationState.speed);
					animationState.speed = num * (float)playDirection;
					if (playDirection == AnimationOrTween.Direction.Reverse && animationState.time == 0f)
					{
						animationState.time = animationState.length;
					}
					else if (playDirection == AnimationOrTween.Direction.Forward && animationState.time == animationState.length)
					{
						animationState.time = 0f;
					}
				}
			}
			this.mLastDirection = playDirection;
			this.mNotify = true;
		}
	}

	// Token: 0x06004A12 RID: 18962 RVA: 0x0011D7C4 File Offset: 0x0011B9C4
	public static global::ActiveAnimation Play(Animation anim, string clipName, AnimationOrTween.Direction playDirection, AnimationOrTween.EnableCondition enableBeforePlay, AnimationOrTween.DisableCondition disableCondition)
	{
		if (!anim.gameObject.activeInHierarchy)
		{
			if (enableBeforePlay != AnimationOrTween.EnableCondition.EnableThenPlay)
			{
				return null;
			}
			global::NGUITools.SetActive(anim.gameObject, true);
		}
		global::ActiveAnimation activeAnimation = anim.GetComponent<global::ActiveAnimation>();
		if (activeAnimation != null)
		{
			activeAnimation.enabled = true;
		}
		else
		{
			activeAnimation = anim.gameObject.AddComponent<global::ActiveAnimation>();
		}
		activeAnimation.mAnim = anim;
		activeAnimation.mDisableDirection = (AnimationOrTween.Direction)disableCondition;
		activeAnimation.Play(clipName, playDirection);
		return activeAnimation;
	}

	// Token: 0x06004A13 RID: 18963 RVA: 0x0011D83C File Offset: 0x0011BA3C
	public static global::ActiveAnimation Play(Animation anim, string clipName, AnimationOrTween.Direction playDirection)
	{
		return global::ActiveAnimation.Play(anim, clipName, playDirection, AnimationOrTween.EnableCondition.DoNothing, AnimationOrTween.DisableCondition.DoNotDisable);
	}

	// Token: 0x06004A14 RID: 18964 RVA: 0x0011D848 File Offset: 0x0011BA48
	public static global::ActiveAnimation Play(Animation anim, AnimationOrTween.Direction playDirection)
	{
		return global::ActiveAnimation.Play(anim, null, playDirection, AnimationOrTween.EnableCondition.DoNothing, AnimationOrTween.DisableCondition.DoNotDisable);
	}

	// Token: 0x04002841 RID: 10305
	public GameObject eventReceiver;

	// Token: 0x04002842 RID: 10306
	public string callWhenFinished;

	// Token: 0x04002843 RID: 10307
	private Animation mAnim;

	// Token: 0x04002844 RID: 10308
	private AnimationOrTween.Direction mLastDirection;

	// Token: 0x04002845 RID: 10309
	private AnimationOrTween.Direction mDisableDirection;

	// Token: 0x04002846 RID: 10310
	private bool mNotify;
}
