using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000782 RID: 1922
[RequireComponent(typeof(Animation))]
[AddComponentMenu("NGUI/Internal/Active Animation")]
public class ActiveAnimation : IgnoreTimeScale
{
	// Token: 0x060045A2 RID: 17826 RVA: 0x00113A78 File Offset: 0x00111C78
	public void Reset()
	{
		if (this.mAnim != null)
		{
			foreach (object obj in this.mAnim)
			{
				AnimationState animationState = (AnimationState)obj;
				if (this.mLastDirection == Direction.Reverse)
				{
					animationState.time = animationState.length;
				}
				else if (this.mLastDirection == Direction.Forward)
				{
					animationState.time = 0f;
				}
			}
		}
	}

	// Token: 0x060045A3 RID: 17827 RVA: 0x00113B28 File Offset: 0x00111D28
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
				if (this.mDisableDirection != Direction.Toggle && this.mLastDirection == this.mDisableDirection)
				{
					NGUITools.SetActive(base.gameObject, false);
				}
			}
		}
		base.enabled = false;
	}

	// Token: 0x060045A4 RID: 17828 RVA: 0x00113CC8 File Offset: 0x00111EC8
	private void Play(string clipName, Direction playDirection)
	{
		if (this.mAnim != null)
		{
			this.mAnim.enabled = false;
			if (playDirection == Direction.Toggle)
			{
				playDirection = ((this.mLastDirection == Direction.Forward) ? Direction.Reverse : Direction.Forward);
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
					if (playDirection == Direction.Reverse && animationState.time == 0f)
					{
						animationState.time = animationState.length;
					}
					else if (playDirection == Direction.Forward && animationState.time == animationState.length)
					{
						animationState.time = 0f;
					}
				}
			}
			this.mLastDirection = playDirection;
			this.mNotify = true;
		}
	}

	// Token: 0x060045A5 RID: 17829 RVA: 0x00113E44 File Offset: 0x00112044
	public static ActiveAnimation Play(Animation anim, string clipName, Direction playDirection, EnableCondition enableBeforePlay, DisableCondition disableCondition)
	{
		if (!anim.gameObject.activeInHierarchy)
		{
			if (enableBeforePlay != EnableCondition.EnableThenPlay)
			{
				return null;
			}
			NGUITools.SetActive(anim.gameObject, true);
		}
		ActiveAnimation activeAnimation = anim.GetComponent<ActiveAnimation>();
		if (activeAnimation != null)
		{
			activeAnimation.enabled = true;
		}
		else
		{
			activeAnimation = anim.gameObject.AddComponent<ActiveAnimation>();
		}
		activeAnimation.mAnim = anim;
		activeAnimation.mDisableDirection = (Direction)disableCondition;
		activeAnimation.Play(clipName, playDirection);
		return activeAnimation;
	}

	// Token: 0x060045A6 RID: 17830 RVA: 0x00113EBC File Offset: 0x001120BC
	public static ActiveAnimation Play(Animation anim, string clipName, Direction playDirection)
	{
		return ActiveAnimation.Play(anim, clipName, playDirection, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
	}

	// Token: 0x060045A7 RID: 17831 RVA: 0x00113EC8 File Offset: 0x001120C8
	public static ActiveAnimation Play(Animation anim, Direction playDirection)
	{
		return ActiveAnimation.Play(anim, null, playDirection, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
	}

	// Token: 0x0400260A RID: 9738
	public GameObject eventReceiver;

	// Token: 0x0400260B RID: 9739
	public string callWhenFinished;

	// Token: 0x0400260C RID: 9740
	private Animation mAnim;

	// Token: 0x0400260D RID: 9741
	private Direction mLastDirection;

	// Token: 0x0400260E RID: 9742
	private Direction mDisableDirection;

	// Token: 0x0400260F RID: 9743
	private bool mNotify;
}
