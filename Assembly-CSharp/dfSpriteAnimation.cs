using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020007E7 RID: 2023
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Daikon Forge/Tweens/Sprite Animator")]
[Serializable]
public class dfSpriteAnimation : global::dfTweenPlayableBase
{
	// Token: 0x1400005C RID: 92
	// (add) Token: 0x06004625 RID: 17957 RVA: 0x00107594 File Offset: 0x00105794
	// (remove) Token: 0x06004626 RID: 17958 RVA: 0x001075B0 File Offset: 0x001057B0
	public event global::TweenNotification AnimationStarted;

	// Token: 0x1400005D RID: 93
	// (add) Token: 0x06004627 RID: 17959 RVA: 0x001075CC File Offset: 0x001057CC
	// (remove) Token: 0x06004628 RID: 17960 RVA: 0x001075E8 File Offset: 0x001057E8
	public event global::TweenNotification AnimationStopped;

	// Token: 0x1400005E RID: 94
	// (add) Token: 0x06004629 RID: 17961 RVA: 0x00107604 File Offset: 0x00105804
	// (remove) Token: 0x0600462A RID: 17962 RVA: 0x00107620 File Offset: 0x00105820
	public event global::TweenNotification AnimationPaused;

	// Token: 0x1400005F RID: 95
	// (add) Token: 0x0600462B RID: 17963 RVA: 0x0010763C File Offset: 0x0010583C
	// (remove) Token: 0x0600462C RID: 17964 RVA: 0x00107658 File Offset: 0x00105858
	public event global::TweenNotification AnimationResumed;

	// Token: 0x14000060 RID: 96
	// (add) Token: 0x0600462D RID: 17965 RVA: 0x00107674 File Offset: 0x00105874
	// (remove) Token: 0x0600462E RID: 17966 RVA: 0x00107690 File Offset: 0x00105890
	public event global::TweenNotification AnimationReset;

	// Token: 0x14000061 RID: 97
	// (add) Token: 0x0600462F RID: 17967 RVA: 0x001076AC File Offset: 0x001058AC
	// (remove) Token: 0x06004630 RID: 17968 RVA: 0x001076C8 File Offset: 0x001058C8
	public event global::TweenNotification AnimationCompleted;

	// Token: 0x17000D7C RID: 3452
	// (get) Token: 0x06004631 RID: 17969 RVA: 0x001076E4 File Offset: 0x001058E4
	// (set) Token: 0x06004632 RID: 17970 RVA: 0x001076EC File Offset: 0x001058EC
	public global::dfAnimationClip Clip
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

	// Token: 0x17000D7D RID: 3453
	// (get) Token: 0x06004633 RID: 17971 RVA: 0x001076F8 File Offset: 0x001058F8
	// (set) Token: 0x06004634 RID: 17972 RVA: 0x00107700 File Offset: 0x00105900
	public global::dfComponentMemberInfo Target
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

	// Token: 0x17000D7E RID: 3454
	// (get) Token: 0x06004635 RID: 17973 RVA: 0x0010770C File Offset: 0x0010590C
	// (set) Token: 0x06004636 RID: 17974 RVA: 0x00107714 File Offset: 0x00105914
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

	// Token: 0x17000D7F RID: 3455
	// (get) Token: 0x06004637 RID: 17975 RVA: 0x00107720 File Offset: 0x00105920
	// (set) Token: 0x06004638 RID: 17976 RVA: 0x00107728 File Offset: 0x00105928
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

	// Token: 0x17000D80 RID: 3456
	// (get) Token: 0x06004639 RID: 17977 RVA: 0x0010773C File Offset: 0x0010593C
	// (set) Token: 0x0600463A RID: 17978 RVA: 0x00107744 File Offset: 0x00105944
	public global::dfTweenLoopType LoopType
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

	// Token: 0x17000D81 RID: 3457
	// (get) Token: 0x0600463B RID: 17979 RVA: 0x00107750 File Offset: 0x00105950
	// (set) Token: 0x0600463C RID: 17980 RVA: 0x00107758 File Offset: 0x00105958
	public global::dfSpriteAnimation.PlayDirection Direction
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

	// Token: 0x17000D82 RID: 3458
	// (get) Token: 0x0600463D RID: 17981 RVA: 0x00107774 File Offset: 0x00105974
	// (set) Token: 0x0600463E RID: 17982 RVA: 0x0010778C File Offset: 0x0010598C
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

	// Token: 0x0600463F RID: 17983 RVA: 0x001077B4 File Offset: 0x001059B4
	public void Awake()
	{
	}

	// Token: 0x06004640 RID: 17984 RVA: 0x001077B8 File Offset: 0x001059B8
	public void Start()
	{
	}

	// Token: 0x06004641 RID: 17985 RVA: 0x001077BC File Offset: 0x001059BC
	public void LateUpdate()
	{
		if (this.AutoRun && !this.IsPlaying && !this.autoRunStarted)
		{
			this.autoRunStarted = true;
			this.Play();
		}
	}

	// Token: 0x06004642 RID: 17986 RVA: 0x001077F8 File Offset: 0x001059F8
	public void PlayForward()
	{
		this.playDirection = global::dfSpriteAnimation.PlayDirection.Forward;
		this.Play();
	}

	// Token: 0x06004643 RID: 17987 RVA: 0x00107808 File Offset: 0x00105A08
	public void PlayReverse()
	{
		this.playDirection = global::dfSpriteAnimation.PlayDirection.Reverse;
		this.Play();
	}

	// Token: 0x06004644 RID: 17988 RVA: 0x00107818 File Offset: 0x00105A18
	public void Pause()
	{
		if (this.isRunning)
		{
			this.isPaused = true;
			this.onPaused();
		}
	}

	// Token: 0x06004645 RID: 17989 RVA: 0x00107834 File Offset: 0x00105A34
	public void Resume()
	{
		if (this.isRunning && this.isPaused)
		{
			this.isPaused = false;
			this.onResumed();
		}
	}

	// Token: 0x17000D83 RID: 3459
	// (get) Token: 0x06004646 RID: 17990 RVA: 0x0010785C File Offset: 0x00105A5C
	public override bool IsPlaying
	{
		get
		{
			return this.isRunning;
		}
	}

	// Token: 0x06004647 RID: 17991 RVA: 0x00107864 File Offset: 0x00105A64
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

	// Token: 0x06004648 RID: 17992 RVA: 0x00107930 File Offset: 0x00105B30
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

	// Token: 0x06004649 RID: 17993 RVA: 0x001079D4 File Offset: 0x00105BD4
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

	// Token: 0x17000D84 RID: 3460
	// (get) Token: 0x0600464A RID: 17994 RVA: 0x00107A58 File Offset: 0x00105C58
	// (set) Token: 0x0600464B RID: 17995 RVA: 0x00107A60 File Offset: 0x00105C60
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

	// Token: 0x0600464C RID: 17996 RVA: 0x00107A6C File Offset: 0x00105C6C
	protected void onPaused()
	{
		base.SendMessage("AnimationPaused", this, 1);
		if (this.AnimationPaused != null)
		{
			this.AnimationPaused();
		}
	}

	// Token: 0x0600464D RID: 17997 RVA: 0x00107A94 File Offset: 0x00105C94
	protected void onResumed()
	{
		base.SendMessage("AnimationResumed", this, 1);
		if (this.AnimationResumed != null)
		{
			this.AnimationResumed();
		}
	}

	// Token: 0x0600464E RID: 17998 RVA: 0x00107ABC File Offset: 0x00105CBC
	protected void onStarted()
	{
		base.SendMessage("AnimationStarted", this, 1);
		if (this.AnimationStarted != null)
		{
			this.AnimationStarted();
		}
	}

	// Token: 0x0600464F RID: 17999 RVA: 0x00107AE4 File Offset: 0x00105CE4
	protected void onStopped()
	{
		base.SendMessage("AnimationStopped", this, 1);
		if (this.AnimationStopped != null)
		{
			this.AnimationStopped();
		}
	}

	// Token: 0x06004650 RID: 18000 RVA: 0x00107B0C File Offset: 0x00105D0C
	protected void onReset()
	{
		base.SendMessage("AnimationReset", this, 1);
		if (this.AnimationReset != null)
		{
			this.AnimationReset();
		}
	}

	// Token: 0x06004651 RID: 18001 RVA: 0x00107B34 File Offset: 0x00105D34
	protected void onCompleted()
	{
		base.SendMessage("AnimationCompleted", this, 1);
		if (this.AnimationCompleted != null)
		{
			this.AnimationCompleted();
		}
	}

	// Token: 0x06004652 RID: 18002 RVA: 0x00107B5C File Offset: 0x00105D5C
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
		int direction = (this.playDirection != global::dfSpriteAnimation.PlayDirection.Forward) ? -1 : 1;
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
					case global::dfTweenLoopType.Once:
						goto IL_1C8;
					case global::dfTweenLoopType.Loop:
						startTime = timeNow;
						frameIndex = 0;
						break;
					case global::dfTweenLoopType.PingPong:
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

	// Token: 0x06004653 RID: 18003 RVA: 0x00107B78 File Offset: 0x00105D78
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

	// Token: 0x06004654 RID: 18004 RVA: 0x00107BE4 File Offset: 0x00105DE4
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

	// Token: 0x040024CD RID: 9421
	[SerializeField]
	private string animationName = "ANIMATION";

	// Token: 0x040024CE RID: 9422
	[SerializeField]
	private global::dfAnimationClip clip;

	// Token: 0x040024CF RID: 9423
	[SerializeField]
	private global::dfComponentMemberInfo memberInfo = new global::dfComponentMemberInfo();

	// Token: 0x040024D0 RID: 9424
	[SerializeField]
	private global::dfTweenLoopType loopType = global::dfTweenLoopType.Loop;

	// Token: 0x040024D1 RID: 9425
	[SerializeField]
	private float length = 1f;

	// Token: 0x040024D2 RID: 9426
	[SerializeField]
	private bool autoStart;

	// Token: 0x040024D3 RID: 9427
	[SerializeField]
	private bool skipToEndOnStop;

	// Token: 0x040024D4 RID: 9428
	[SerializeField]
	private global::dfSpriteAnimation.PlayDirection playDirection;

	// Token: 0x040024D5 RID: 9429
	private bool autoRunStarted;

	// Token: 0x040024D6 RID: 9430
	private bool isRunning;

	// Token: 0x040024D7 RID: 9431
	private bool isPaused;

	// Token: 0x040024D8 RID: 9432
	private global::dfObservableProperty target;

	// Token: 0x020007E8 RID: 2024
	public enum PlayDirection
	{
		// Token: 0x040024E0 RID: 9440
		Forward,
		// Token: 0x040024E1 RID: 9441
		Reverse
	}
}
