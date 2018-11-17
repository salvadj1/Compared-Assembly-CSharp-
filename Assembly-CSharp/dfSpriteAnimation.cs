using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x0200070D RID: 1805
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Daikon Forge/Tweens/Sprite Animator")]
[Serializable]
public class dfSpriteAnimation : dfTweenPlayableBase
{
	// Token: 0x1400005C RID: 92
	// (add) Token: 0x060041EB RID: 16875 RVA: 0x000FE660 File Offset: 0x000FC860
	// (remove) Token: 0x060041EC RID: 16876 RVA: 0x000FE67C File Offset: 0x000FC87C
	public event TweenNotification AnimationStarted;

	// Token: 0x1400005D RID: 93
	// (add) Token: 0x060041ED RID: 16877 RVA: 0x000FE698 File Offset: 0x000FC898
	// (remove) Token: 0x060041EE RID: 16878 RVA: 0x000FE6B4 File Offset: 0x000FC8B4
	public event TweenNotification AnimationStopped;

	// Token: 0x1400005E RID: 94
	// (add) Token: 0x060041EF RID: 16879 RVA: 0x000FE6D0 File Offset: 0x000FC8D0
	// (remove) Token: 0x060041F0 RID: 16880 RVA: 0x000FE6EC File Offset: 0x000FC8EC
	public event TweenNotification AnimationPaused;

	// Token: 0x1400005F RID: 95
	// (add) Token: 0x060041F1 RID: 16881 RVA: 0x000FE708 File Offset: 0x000FC908
	// (remove) Token: 0x060041F2 RID: 16882 RVA: 0x000FE724 File Offset: 0x000FC924
	public event TweenNotification AnimationResumed;

	// Token: 0x14000060 RID: 96
	// (add) Token: 0x060041F3 RID: 16883 RVA: 0x000FE740 File Offset: 0x000FC940
	// (remove) Token: 0x060041F4 RID: 16884 RVA: 0x000FE75C File Offset: 0x000FC95C
	public event TweenNotification AnimationReset;

	// Token: 0x14000061 RID: 97
	// (add) Token: 0x060041F5 RID: 16885 RVA: 0x000FE778 File Offset: 0x000FC978
	// (remove) Token: 0x060041F6 RID: 16886 RVA: 0x000FE794 File Offset: 0x000FC994
	public event TweenNotification AnimationCompleted;

	// Token: 0x17000CF4 RID: 3316
	// (get) Token: 0x060041F7 RID: 16887 RVA: 0x000FE7B0 File Offset: 0x000FC9B0
	// (set) Token: 0x060041F8 RID: 16888 RVA: 0x000FE7B8 File Offset: 0x000FC9B8
	public dfAnimationClip Clip
	{
		get
		{
			return this.clip;
		}
		set
		{
			this.clip = value;
		}
	}

	// Token: 0x17000CF5 RID: 3317
	// (get) Token: 0x060041F9 RID: 16889 RVA: 0x000FE7C4 File Offset: 0x000FC9C4
	// (set) Token: 0x060041FA RID: 16890 RVA: 0x000FE7CC File Offset: 0x000FC9CC
	public dfComponentMemberInfo Target
	{
		get
		{
			return this.memberInfo;
		}
		set
		{
			this.memberInfo = value;
		}
	}

	// Token: 0x17000CF6 RID: 3318
	// (get) Token: 0x060041FB RID: 16891 RVA: 0x000FE7D8 File Offset: 0x000FC9D8
	// (set) Token: 0x060041FC RID: 16892 RVA: 0x000FE7E0 File Offset: 0x000FC9E0
	public bool AutoRun
	{
		get
		{
			return this.autoStart;
		}
		set
		{
			this.autoStart = value;
		}
	}

	// Token: 0x17000CF7 RID: 3319
	// (get) Token: 0x060041FD RID: 16893 RVA: 0x000FE7EC File Offset: 0x000FC9EC
	// (set) Token: 0x060041FE RID: 16894 RVA: 0x000FE7F4 File Offset: 0x000FC9F4
	public float Length
	{
		get
		{
			return this.length;
		}
		set
		{
			this.length = Mathf.Max(value, 0.03f);
		}
	}

	// Token: 0x17000CF8 RID: 3320
	// (get) Token: 0x060041FF RID: 16895 RVA: 0x000FE808 File Offset: 0x000FCA08
	// (set) Token: 0x06004200 RID: 16896 RVA: 0x000FE810 File Offset: 0x000FCA10
	public dfTweenLoopType LoopType
	{
		get
		{
			return this.loopType;
		}
		set
		{
			this.loopType = value;
		}
	}

	// Token: 0x17000CF9 RID: 3321
	// (get) Token: 0x06004201 RID: 16897 RVA: 0x000FE81C File Offset: 0x000FCA1C
	// (set) Token: 0x06004202 RID: 16898 RVA: 0x000FE824 File Offset: 0x000FCA24
	public dfSpriteAnimation.PlayDirection Direction
	{
		get
		{
			return this.playDirection;
		}
		set
		{
			this.playDirection = value;
			if (this.IsPlaying)
			{
				this.Play();
			}
		}
	}

	// Token: 0x17000CFA RID: 3322
	// (get) Token: 0x06004203 RID: 16899 RVA: 0x000FE840 File Offset: 0x000FCA40
	// (set) Token: 0x06004204 RID: 16900 RVA: 0x000FE858 File Offset: 0x000FCA58
	public bool IsPaused
	{
		get
		{
			return this.isRunning && this.isPaused;
		}
		set
		{
			if (value != this.IsPaused)
			{
				if (value)
				{
					this.Pause();
				}
				else
				{
					this.Resume();
				}
			}
		}
	}

	// Token: 0x06004205 RID: 16901 RVA: 0x000FE880 File Offset: 0x000FCA80
	public void Awake()
	{
	}

	// Token: 0x06004206 RID: 16902 RVA: 0x000FE884 File Offset: 0x000FCA84
	public void Start()
	{
	}

	// Token: 0x06004207 RID: 16903 RVA: 0x000FE888 File Offset: 0x000FCA88
	public void LateUpdate()
	{
		if (this.AutoRun && !this.IsPlaying && !this.autoRunStarted)
		{
			this.autoRunStarted = true;
			this.Play();
		}
	}

	// Token: 0x06004208 RID: 16904 RVA: 0x000FE8C4 File Offset: 0x000FCAC4
	public void PlayForward()
	{
		this.playDirection = dfSpriteAnimation.PlayDirection.Forward;
		this.Play();
	}

	// Token: 0x06004209 RID: 16905 RVA: 0x000FE8D4 File Offset: 0x000FCAD4
	public void PlayReverse()
	{
		this.playDirection = dfSpriteAnimation.PlayDirection.Reverse;
		this.Play();
	}

	// Token: 0x0600420A RID: 16906 RVA: 0x000FE8E4 File Offset: 0x000FCAE4
	public void Pause()
	{
		if (this.isRunning)
		{
			this.isPaused = true;
			this.onPaused();
		}
	}

	// Token: 0x0600420B RID: 16907 RVA: 0x000FE900 File Offset: 0x000FCB00
	public void Resume()
	{
		if (this.isRunning && this.isPaused)
		{
			this.isPaused = false;
			this.onResumed();
		}
	}

	// Token: 0x17000CFB RID: 3323
	// (get) Token: 0x0600420C RID: 16908 RVA: 0x000FE928 File Offset: 0x000FCB28
	public override bool IsPlaying
	{
		get
		{
			return this.isRunning;
		}
	}

	// Token: 0x0600420D RID: 16909 RVA: 0x000FE930 File Offset: 0x000FCB30
	public override void Play()
	{
		if (this.IsPlaying)
		{
			this.Stop();
		}
		if (!base.enabled || !base.gameObject.activeSelf || !base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (this.memberInfo == null)
		{
			throw new NullReferenceException("Animation target is NULL");
		}
		if (!this.memberInfo.IsValid)
		{
			throw new InvalidOperationException(string.Concat(new object[]
			{
				"Invalid property binding configuration on ",
				this.getPath(base.gameObject.transform),
				" - ",
				this.target
			}));
		}
		this.target = this.memberInfo.GetProperty();
		base.StartCoroutine(this.Execute());
	}

	// Token: 0x0600420E RID: 16910 RVA: 0x000FE9FC File Offset: 0x000FCBFC
	public override void Reset()
	{
		List<string> list = (!(this.clip != null)) ? null : this.clip.Sprites;
		if (this.memberInfo.IsValid && list != null && list.Count > 0)
		{
			this.memberInfo.Component.SetProperty(this.memberInfo.MemberName, list[0]);
		}
		if (!this.isRunning)
		{
			return;
		}
		base.StopAllCoroutines();
		this.isRunning = false;
		this.isPaused = false;
		this.onReset();
		this.target = null;
	}

	// Token: 0x0600420F RID: 16911 RVA: 0x000FEAA0 File Offset: 0x000FCCA0
	public override void Stop()
	{
		if (!this.isRunning)
		{
			return;
		}
		List<string> list = (!(this.clip != null)) ? null : this.clip.Sprites;
		if (this.skipToEndOnStop && list != null)
		{
			this.setFrame(Mathf.Max(list.Count - 1, 0));
		}
		base.StopAllCoroutines();
		this.isRunning = false;
		this.isPaused = false;
		this.onStopped();
		this.target = null;
	}

	// Token: 0x17000CFC RID: 3324
	// (get) Token: 0x06004210 RID: 16912 RVA: 0x000FEB24 File Offset: 0x000FCD24
	// (set) Token: 0x06004211 RID: 16913 RVA: 0x000FEB2C File Offset: 0x000FCD2C
	public override string TweenName
	{
		get
		{
			return this.animationName;
		}
		set
		{
			this.animationName = value;
		}
	}

	// Token: 0x06004212 RID: 16914 RVA: 0x000FEB38 File Offset: 0x000FCD38
	protected void onPaused()
	{
		base.SendMessage("AnimationPaused", this, 1);
		if (this.AnimationPaused != null)
		{
			this.AnimationPaused();
		}
	}

	// Token: 0x06004213 RID: 16915 RVA: 0x000FEB60 File Offset: 0x000FCD60
	protected void onResumed()
	{
		base.SendMessage("AnimationResumed", this, 1);
		if (this.AnimationResumed != null)
		{
			this.AnimationResumed();
		}
	}

	// Token: 0x06004214 RID: 16916 RVA: 0x000FEB88 File Offset: 0x000FCD88
	protected void onStarted()
	{
		base.SendMessage("AnimationStarted", this, 1);
		if (this.AnimationStarted != null)
		{
			this.AnimationStarted();
		}
	}

	// Token: 0x06004215 RID: 16917 RVA: 0x000FEBB0 File Offset: 0x000FCDB0
	protected void onStopped()
	{
		base.SendMessage("AnimationStopped", this, 1);
		if (this.AnimationStopped != null)
		{
			this.AnimationStopped();
		}
	}

	// Token: 0x06004216 RID: 16918 RVA: 0x000FEBD8 File Offset: 0x000FCDD8
	protected void onReset()
	{
		base.SendMessage("AnimationReset", this, 1);
		if (this.AnimationReset != null)
		{
			this.AnimationReset();
		}
	}

	// Token: 0x06004217 RID: 16919 RVA: 0x000FEC00 File Offset: 0x000FCE00
	protected void onCompleted()
	{
		base.SendMessage("AnimationCompleted", this, 1);
		if (this.AnimationCompleted != null)
		{
			this.AnimationCompleted();
		}
	}

	// Token: 0x06004218 RID: 16920 RVA: 0x000FEC28 File Offset: 0x000FCE28
	private IEnumerator Execute()
	{
		if (this.clip == null || this.clip.Sprites == null || this.clip.Sprites.Count == 0)
		{
			yield break;
		}
		this.isRunning = true;
		this.isPaused = false;
		this.onStarted();
		float startTime = Time.realtimeSinceStartup;
		int direction = (this.playDirection != dfSpriteAnimation.PlayDirection.Forward) ? -1 : 1;
		int lastFrameIndex = (direction != 1) ? (this.clip.Sprites.Count - 1) : 0;
		this.setFrame(lastFrameIndex);
		for (;;)
		{
			yield return null;
			if (!this.IsPaused)
			{
				List<string> sprites = this.clip.Sprites;
				int maxFrameIndex = sprites.Count - 1;
				float timeNow = Time.realtimeSinceStartup;
				float elapsed = timeNow - startTime;
				int frameIndex = Mathf.RoundToInt(Mathf.Clamp01(elapsed / this.length) * (float)maxFrameIndex);
				if (elapsed >= this.length)
				{
					switch (this.loopType)
					{
					case dfTweenLoopType.Once:
						goto IL_1C8;
					case dfTweenLoopType.Loop:
						startTime = timeNow;
						frameIndex = 0;
						break;
					case dfTweenLoopType.PingPong:
						startTime = timeNow;
						direction *= -1;
						frameIndex = 0;
						break;
					}
				}
				if (direction == -1)
				{
					frameIndex = maxFrameIndex - frameIndex;
				}
				if (lastFrameIndex != frameIndex)
				{
					lastFrameIndex = frameIndex;
					this.setFrame(frameIndex);
				}
			}
		}
		IL_1C8:
		this.isRunning = false;
		this.onCompleted();
		yield break;
		yield break;
	}

	// Token: 0x06004219 RID: 16921 RVA: 0x000FEC44 File Offset: 0x000FCE44
	private string getPath(Transform obj)
	{
		StringBuilder stringBuilder = new StringBuilder();
		while (obj != null)
		{
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Insert(0, "\\");
				stringBuilder.Insert(0, obj.name);
			}
			else
			{
				stringBuilder.Append(obj.name);
			}
			obj = obj.parent;
		}
		return stringBuilder.ToString();
	}

	// Token: 0x0600421A RID: 16922 RVA: 0x000FECB0 File Offset: 0x000FCEB0
	private void setFrame(int frameIndex)
	{
		List<string> sprites = this.clip.Sprites;
		if (sprites.Count == 0)
		{
			return;
		}
		frameIndex = Mathf.Max(0, Mathf.Min(frameIndex, sprites.Count - 1));
		if (this.target != null)
		{
			this.target.Value = sprites[frameIndex];
		}
	}

	// Token: 0x040022B9 RID: 8889
	[SerializeField]
	private string animationName = "ANIMATION";

	// Token: 0x040022BA RID: 8890
	[SerializeField]
	private dfAnimationClip clip;

	// Token: 0x040022BB RID: 8891
	[SerializeField]
	private dfComponentMemberInfo memberInfo = new dfComponentMemberInfo();

	// Token: 0x040022BC RID: 8892
	[SerializeField]
	private dfTweenLoopType loopType = dfTweenLoopType.Loop;

	// Token: 0x040022BD RID: 8893
	[SerializeField]
	private float length = 1f;

	// Token: 0x040022BE RID: 8894
	[SerializeField]
	private bool autoStart;

	// Token: 0x040022BF RID: 8895
	[SerializeField]
	private bool skipToEndOnStop;

	// Token: 0x040022C0 RID: 8896
	[SerializeField]
	private dfSpriteAnimation.PlayDirection playDirection;

	// Token: 0x040022C1 RID: 8897
	private bool autoRunStarted;

	// Token: 0x040022C2 RID: 8898
	private bool isRunning;

	// Token: 0x040022C3 RID: 8899
	private bool isPaused;

	// Token: 0x040022C4 RID: 8900
	private dfObservableProperty target;

	// Token: 0x0200070E RID: 1806
	public enum PlayDirection
	{
		// Token: 0x040022CC RID: 8908
		Forward,
		// Token: 0x040022CD RID: 8909
		Reverse
	}
}
