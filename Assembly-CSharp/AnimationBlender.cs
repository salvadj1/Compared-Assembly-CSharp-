using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000272 RID: 626
public static class AnimationBlender
{
	// Token: 0x060016FB RID: 5883 RVA: 0x00054E30 File Offset: 0x00053030
	private static void ZeroWeight(ref global::AnimationBlender.WeightUnit weight)
	{
		weight.raw = (weight.scaled = (weight.normalized = 0f));
	}

	// Token: 0x060016FC RID: 5884 RVA: 0x00054E5C File Offset: 0x0005305C
	private static void OneWeight(ref global::AnimationBlender.WeightUnit weight)
	{
		weight.raw = (weight.scaled = (weight.normalized = 1f));
	}

	// Token: 0x060016FD RID: 5885 RVA: 0x00054E88 File Offset: 0x00053088
	private static void OneWeightScale(ref global::AnimationBlender.WeightUnit weight)
	{
		weight.scaled = (weight.normalized = 1f);
	}

	// Token: 0x060016FE RID: 5886 RVA: 0x00054EAC File Offset: 0x000530AC
	private static void SetWeight(ref global::AnimationBlender.WeightUnit weight)
	{
		weight.scaled = weight.raw;
		weight.normalized = 1f;
	}

	// Token: 0x060016FF RID: 5887 RVA: 0x00054EC8 File Offset: 0x000530C8
	private static global::AnimationBlender.Weighted<T>[] WeightArray<T>(int size)
	{
		return new global::AnimationBlender.Weighted<T>[size];
	}

	// Token: 0x06001700 RID: 5888 RVA: 0x00054EE0 File Offset: 0x000530E0
	private static global::AnimationBlender.Weighted<T>[] WeightArray<T>(T[] source)
	{
		if (object.ReferenceEquals(source, null))
		{
			return null;
		}
		int num = source.Length;
		global::AnimationBlender.Weighted<T>[] array = global::AnimationBlender.WeightArray<T>(num);
		for (int i = 0; i < num; i++)
		{
			array[i].value = source[i];
		}
		return array;
	}

	// Token: 0x06001701 RID: 5889 RVA: 0x00054F2C File Offset: 0x0005312C
	private static bool WeightOf<T>(global::AnimationBlender.Weighted<T>[] items, int[] index, out global::AnimationBlender.WeightResult result)
	{
		float num = 0f;
		float num2 = 0f;
		int num3 = -1;
		int num4 = 0;
		int i = 0;
		int num5 = index.Length - 1;
		while (i <= num5)
		{
			float num6;
			if ((num6 = items[index[i]].weight.raw) <= 0f)
			{
				global::AnimationBlender.ZeroWeight(ref items[index[i]].weight);
				int num7 = index[num5];
				index[num5--] = index[i];
				index[i] = num7;
			}
			else
			{
				num4++;
				if (num6 >= 1f)
				{
					num6 = 1f;
					global::AnimationBlender.OneWeight(ref items[index[i]].weight);
				}
				else
				{
					global::AnimationBlender.SetWeight(ref items[index[i]].weight);
				}
				num += num6;
				if (num6 > num2)
				{
					num2 = num6;
					num3 = i;
				}
				i++;
			}
		}
		float num8;
		float num9;
		bool result2;
		if (num3 == -1)
		{
			num8 = 0f;
			num9 = 0f;
			result2 = false;
		}
		else
		{
			result2 = true;
			if (num4 == 1)
			{
				num8 = 1f;
				num9 = 1f;
				global::AnimationBlender.OneWeightScale(ref items[index[0]].weight);
			}
			else
			{
				if (num2 < 1f)
				{
					num8 = 0f;
					float num10 = 1f / num2;
					for (int j = 0; j < num4; j++)
					{
						num8 += items[index[j]].weight.SetScaledRecip(num10);
					}
				}
				else
				{
					num8 = num;
				}
				if (num8 == 1f)
				{
					num9 = 1f;
				}
				else
				{
					num9 = 0f;
					float num10 = 1f / num8;
					for (int k = 0; k < num4; k++)
					{
						num9 += items[index[k]].weight.SetNormalizedRecip(num10);
					}
				}
			}
		}
		result.count = num4;
		result.winner = num3;
		result.sum.raw = num;
		result.sum.scaled = num8;
		result.sum.normalized = num9;
		return result2;
	}

	// Token: 0x06001702 RID: 5890 RVA: 0x00055140 File Offset: 0x00053340
	private static int GetClear(ref int value)
	{
		int result = value;
		value = 0;
		return result;
	}

	// Token: 0x06001703 RID: 5891 RVA: 0x00055154 File Offset: 0x00053354
	private static void ArrayResize<T>(ref T[] array, int size)
	{
		Array.Resize<T>(ref array, size);
	}

	// Token: 0x06001704 RID: 5892 RVA: 0x00055160 File Offset: 0x00053360
	private static global::AnimationBlender.opt<T> to_opt<T>(T? nullable) where T : struct
	{
		return (nullable != null) ? nullable.Value : global::AnimationBlender.opt<T>.none;
	}

	// Token: 0x06001705 RID: 5893 RVA: 0x00055190 File Offset: 0x00053390
	public static global::AnimationBlender.ChannelConfig Alias(this global::AnimationBlender.ChannelField Field, string Name)
	{
		return new global::AnimationBlender.ChannelConfig(Name, Field);
	}

	// Token: 0x06001706 RID: 5894 RVA: 0x0005519C File Offset: 0x0005339C
	public static global::AnimationBlender.ChannelConfig[] Alias(this global::AnimationBlender.ChannelField Field, global::AnimationBlender.ChannelConfig[] Array, int Index, string Name)
	{
		Array[Index] = Field.Alias(Name);
		return Array;
	}

	// Token: 0x06001707 RID: 5895 RVA: 0x000551B4 File Offset: 0x000533B4
	public static global::AnimationBlender.ChannelConfig[] Define(this global::AnimationBlender.ChannelConfig[] Array, int Index, string Name, global::AnimationBlender.ChannelField Field)
	{
		return Field.Alias(Array, Index, Name);
	}

	// Token: 0x06001708 RID: 5896 RVA: 0x000551C0 File Offset: 0x000533C0
	public static global::AnimationBlender.MixerConfig Alias(this global::AnimationBlender.ResidualField ResidualField, Animation Animation, params global::AnimationBlender.ChannelConfig[] ChannelAliases)
	{
		return new global::AnimationBlender.MixerConfig(Animation, ResidualField, ChannelAliases);
	}

	// Token: 0x06001709 RID: 5897 RVA: 0x000551CC File Offset: 0x000533CC
	public static global::AnimationBlender.MixerConfig Alias(this global::AnimationBlender.ResidualField ResidualField, Animation Animation, int ChannelCount)
	{
		return new global::AnimationBlender.MixerConfig(Animation, ResidualField, new global::AnimationBlender.ChannelConfig[ChannelCount]);
	}

	// Token: 0x0600170A RID: 5898 RVA: 0x000551DC File Offset: 0x000533DC
	public static global::AnimationBlender.Mixer Create(this global::AnimationBlender.MixerConfig Config)
	{
		return new global::AnimationBlender.Mixer(Config);
	}

	// Token: 0x02000273 RID: 627
	private struct Channel
	{
		// Token: 0x0600170B RID: 5899 RVA: 0x000551E4 File Offset: 0x000533E4
		public Channel(int index, int animationIndex, string name, global::AnimationBlender.ChannelField field)
		{
			this.index = index;
			this.animationIndex = animationIndex;
			this.name = name;
			this.field = field;
			this.induce = new global::AnimationBlender.ChannelCurve(field.inCurveInfo, default(global::AnimationBlender.State), default(global::AnimationBlender.Influence), field, true);
			this.reduce = new global::AnimationBlender.ChannelCurve(field.outCurveInfo, default(global::AnimationBlender.State), default(global::AnimationBlender.Influence), field, false);
			this.active = false;
			this.wasActive = false;
			this.startedTransition = false;
			this.valid = (animationIndex != -1);
			this.maxBlend = ((field.residualBlend > 0f) ? ((field.residualBlend < 1f) ? (1f - field.residualBlend) : 0f) : 1f);
			this.startTime = field.startFrame;
			this.playbackRate = field.playbackRate;
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x000552E4 File Offset: 0x000534E4
		private bool StartTransition(ref global::AnimationBlender.ChannelCurve from, ref global::AnimationBlender.ChannelCurve to, ref float dt, bool startNow)
		{
			if (to.state.delay == 0f && startNow)
			{
				to.state.delay = to.delayDuration;
			}
			if (to.state.delay > dt)
			{
				to.state.delay = to.state.delay - dt;
				return false;
			}
			dt -= to.state.delay;
			to.state.delay = 0f;
			to.influence.percent = 0f;
			to.influence.duration = from.state.percent * to.info.duration;
			to.influence.value = from.state.value;
			to.influence.active = (from.state.percent > 0f);
			to.influence.timeleft = to.influence.duration;
			from.state.delay = to.delayDuration;
			from.state.active = false;
			to.state.active = true;
			if (to.induces)
			{
				from.state.time = from.info.start;
				from.state.percent = 0f;
			}
			return true;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0005543C File Offset: 0x0005363C
		private float Step(bool transitioning, ref global::AnimationBlender.ChannelCurve from, ref global::AnimationBlender.ChannelCurve to, ref float dt)
		{
			if (transitioning && to.state.delay > 0f)
			{
				return from.state.value;
			}
			float num = dt;
			float num2 = dt;
			float time = to.state.time;
			if (to.induces)
			{
				to.state.time = to.state.time + dt;
				if (to.state.time >= to.info.end)
				{
					num = to.state.time - to.info.end;
					to.state.time = to.info.end;
					to.state.percent = 1f;
					from.state.delay = from.delayDuration;
				}
				else
				{
					num = 0f;
					to.state.percent = to.info.TimeToPercent(to.state.time);
				}
			}
			else if (to.influence.duration == 0f)
			{
				num = dt;
				to.state.percent = 1f;
				to.state.time = to.info.end;
			}
			else
			{
				float num3 = from.info.duration / to.influence.duration;
				to.state.time = to.state.time + dt * num3;
				if (to.state.time >= to.info.end)
				{
					num = (to.state.time - to.info.end) / num3;
					to.state.percent = 1f;
					to.state.time = to.info.end;
				}
				else
				{
					num = 0f;
					to.state.percent = to.info.TimeToPercent(to.state.time);
				}
			}
			float num4 = to.info.SampleTime(to.state.time);
			if (to.influence.active)
			{
				if (to.induces)
				{
					if (to.influence.timeleft > dt)
					{
						to.influence.timeleft = to.influence.timeleft - dt;
						num2 = 0f;
						to.influence.percent = to.influence.timeleft / to.influence.duration;
					}
					else
					{
						num2 = dt - to.influence.timeleft;
						to.influence.timeleft = 0f;
						to.influence.percent = 0f;
						to.influence.active = false;
					}
				}
				else if (to.state.percent >= 1f && to.influence.active)
				{
					to.influence.active = false;
					from.state = default(global::AnimationBlender.State);
				}
			}
			if (to.induces)
			{
				to.state.value = ((!to.influence.active) ? num4 : (num4 + (to.influence.value - num4) * to.influence.percent));
			}
			else
			{
				to.state.value = to.influence.value * num4;
			}
			if (num2 < num)
			{
				dt = num2;
			}
			else
			{
				dt = num;
			}
			return to.state.value;
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x000557CC File Offset: 0x000539CC
		public float Update(float dt)
		{
			bool flag = this.active != this.wasActive;
			if (flag)
			{
				bool startNow = this.startedTransition != this.active;
				this.startedTransition = this.active;
				bool flag2;
				if (this.active)
				{
					flag2 = this.StartTransition(ref this.reduce, ref this.induce, ref dt, startNow);
				}
				else
				{
					flag2 = this.StartTransition(ref this.induce, ref this.reduce, ref dt, startNow);
				}
				if (flag2)
				{
					flag = false;
					this.wasActive = this.active;
				}
			}
			if (this.wasActive)
			{
				return this.Step(flag, ref this.reduce, ref this.induce, ref dt);
			}
			return this.Step(flag, ref this.induce, ref this.reduce, ref dt);
		}

		// Token: 0x04000BBF RID: 3007
		[NonSerialized]
		public global::AnimationBlender.ChannelField field;

		// Token: 0x04000BC0 RID: 3008
		[NonSerialized]
		public string name;

		// Token: 0x04000BC1 RID: 3009
		[NonSerialized]
		public bool active;

		// Token: 0x04000BC2 RID: 3010
		[NonSerialized]
		public bool valid;

		// Token: 0x04000BC3 RID: 3011
		[NonSerialized]
		public bool wasActive;

		// Token: 0x04000BC4 RID: 3012
		[NonSerialized]
		public bool startedTransition;

		// Token: 0x04000BC5 RID: 3013
		[NonSerialized]
		public global::AnimationBlender.ChannelCurve induce;

		// Token: 0x04000BC6 RID: 3014
		[NonSerialized]
		public global::AnimationBlender.ChannelCurve reduce;

		// Token: 0x04000BC7 RID: 3015
		[NonSerialized]
		public int index;

		// Token: 0x04000BC8 RID: 3016
		[NonSerialized]
		public int animationIndex;

		// Token: 0x04000BC9 RID: 3017
		[NonSerialized]
		public float maxBlend;

		// Token: 0x04000BCA RID: 3018
		[NonSerialized]
		public float playbackRate;

		// Token: 0x04000BCB RID: 3019
		[NonSerialized]
		public float startTime;
	}

	// Token: 0x02000274 RID: 628
	private struct ChannelCurve
	{
		// Token: 0x0600170F RID: 5903 RVA: 0x00055898 File Offset: 0x00053A98
		public ChannelCurve(global::AnimationBlender.CurveInfo info, global::AnimationBlender.State state, global::AnimationBlender.Influence influence, global::AnimationBlender.ChannelField field, bool induces)
		{
			this.info = info;
			this.state = state;
			this.influence = influence;
			this.induces = induces;
			this.delayDuration = ((!induces) ? field.outDelay : field.inDelay);
		}

		// Token: 0x04000BCC RID: 3020
		[NonSerialized]
		public global::AnimationBlender.CurveInfo info;

		// Token: 0x04000BCD RID: 3021
		[NonSerialized]
		public global::AnimationBlender.State state;

		// Token: 0x04000BCE RID: 3022
		[NonSerialized]
		public global::AnimationBlender.Influence influence;

		// Token: 0x04000BCF RID: 3023
		[NonSerialized]
		public float delayDuration;

		// Token: 0x04000BD0 RID: 3024
		[NonSerialized]
		public bool induces;
	}

	// Token: 0x02000275 RID: 629
	private struct Influence
	{
		// Token: 0x04000BD1 RID: 3025
		[NonSerialized]
		public bool active;

		// Token: 0x04000BD2 RID: 3026
		[NonSerialized]
		public float value;

		// Token: 0x04000BD3 RID: 3027
		[NonSerialized]
		public float percent;

		// Token: 0x04000BD4 RID: 3028
		[NonSerialized]
		public float timeleft;

		// Token: 0x04000BD5 RID: 3029
		[NonSerialized]
		public float duration;
	}

	// Token: 0x02000276 RID: 630
	private struct State
	{
		// Token: 0x04000BD6 RID: 3030
		[NonSerialized]
		public bool active;

		// Token: 0x04000BD7 RID: 3031
		[NonSerialized]
		public float time;

		// Token: 0x04000BD8 RID: 3032
		[NonSerialized]
		public float percent;

		// Token: 0x04000BD9 RID: 3033
		[NonSerialized]
		public float delay;

		// Token: 0x04000BDA RID: 3034
		[NonSerialized]
		public float value;
	}

	// Token: 0x02000277 RID: 631
	private struct Tracker
	{
		// Token: 0x04000BDB RID: 3035
		[NonSerialized]
		public AnimationClip clip;

		// Token: 0x04000BDC RID: 3036
		[NonSerialized]
		public AnimationState state;

		// Token: 0x04000BDD RID: 3037
		[NonSerialized]
		public int[] channels;

		// Token: 0x04000BDE RID: 3038
		[NonSerialized]
		public int channelCount;

		// Token: 0x04000BDF RID: 3039
		[NonSerialized]
		public global::AnimationBlender.WeightResult channelWeight;

		// Token: 0x04000BE0 RID: 3040
		[NonSerialized]
		public float playbackRate;

		// Token: 0x04000BE1 RID: 3041
		[NonSerialized]
		public float blendFraction;

		// Token: 0x04000BE2 RID: 3042
		[NonSerialized]
		public float startTime;

		// Token: 0x04000BE3 RID: 3043
		[NonSerialized]
		public bool enabled;

		// Token: 0x04000BE4 RID: 3044
		[NonSerialized]
		public bool wasEnabled;
	}

	// Token: 0x02000278 RID: 632
	private struct TrackerBlender
	{
		// Token: 0x06001710 RID: 5904 RVA: 0x000558D8 File Offset: 0x00053AD8
		public TrackerBlender(int count)
		{
			this.trackers = new int[count];
			this.trackerCount = count;
			this.trackerWeight = default(global::AnimationBlender.WeightResult);
			for (int i = 0; i < count; i++)
			{
				this.trackers[i] = i;
			}
		}

		// Token: 0x04000BE5 RID: 3045
		[NonSerialized]
		public int[] trackers;

		// Token: 0x04000BE6 RID: 3046
		[NonSerialized]
		public int trackerCount;

		// Token: 0x04000BE7 RID: 3047
		[NonSerialized]
		public global::AnimationBlender.WeightResult trackerWeight;
	}

	// Token: 0x02000279 RID: 633
	private struct opt<T>
	{
		// Token: 0x06001711 RID: 5905 RVA: 0x00055924 File Offset: 0x00053B24
		private opt(T value, bool defined)
		{
			this.value = value;
			this.defined = defined;
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x00055950 File Offset: 0x00053B50
		public bool check(out T value)
		{
			value = this.value;
			return this.defined;
		}

		// Token: 0x17000688 RID: 1672
		public T this[T fallback]
		{
			get
			{
				return (!this.defined) ? fallback : this.value;
			}
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00055980 File Offset: 0x00053B80
		public static implicit operator global::AnimationBlender.opt<T>(T value)
		{
			return new global::AnimationBlender.opt<T>(value, true);
		}

		// Token: 0x04000BE8 RID: 3048
		[NonSerialized]
		public readonly T value;

		// Token: 0x04000BE9 RID: 3049
		[NonSerialized]
		public readonly bool defined;

		// Token: 0x04000BEA RID: 3050
		public static readonly global::AnimationBlender.opt<T> none = default(global::AnimationBlender.opt<T>);
	}

	// Token: 0x0200027A RID: 634
	private struct WeightUnit
	{
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001716 RID: 5910 RVA: 0x0005598C File Offset: 0x00053B8C
		public bool any
		{
			get
			{
				return this.raw > 0f;
			}
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0005599C File Offset: 0x00053B9C
		public float SetScaledRecip(float recip)
		{
			return this.normalized = (this.scaled = this.raw * recip);
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x000559C4 File Offset: 0x00053BC4
		public float SetNormalizedRecip(float recip)
		{
			return this.normalized = this.scaled * recip;
		}

		// Token: 0x04000BEB RID: 3051
		[NonSerialized]
		public float raw;

		// Token: 0x04000BEC RID: 3052
		[NonSerialized]
		public float scaled;

		// Token: 0x04000BED RID: 3053
		[NonSerialized]
		public float normalized;
	}

	// Token: 0x0200027B RID: 635
	private struct WeightResult
	{
		// Token: 0x04000BEE RID: 3054
		[NonSerialized]
		public int count;

		// Token: 0x04000BEF RID: 3055
		[NonSerialized]
		public int winner;

		// Token: 0x04000BF0 RID: 3056
		public global::AnimationBlender.WeightUnit sum;
	}

	// Token: 0x0200027C RID: 636
	private struct Weighted<T>
	{
		// Token: 0x04000BF1 RID: 3057
		[NonSerialized]
		public global::AnimationBlender.WeightUnit weight;

		// Token: 0x04000BF2 RID: 3058
		[NonSerialized]
		public T value;
	}

	// Token: 0x0200027D RID: 637
	public struct CurveInfo
	{
		// Token: 0x06001719 RID: 5913 RVA: 0x000559E4 File Offset: 0x00053BE4
		public CurveInfo(AnimationCurve curve)
		{
			this.curve = curve;
			if ((this.length = curve.length) == 0)
			{
				this.start = (this.firstTime = (this.end = (this.lastTime = (this.duration = 0f))));
				this.percentRate = float.PositiveInfinity;
			}
			else
			{
				this.firstTime = curve[0].time;
				this.end = (this.lastTime = ((this.length != 1) ? curve[this.length - 1].time : this.firstTime));
				this.start = ((this.firstTime >= 0f) ? 0f : this.firstTime);
				if (this.end < this.start)
				{
					this.end = this.start;
					this.start = this.lastTime;
				}
				this.duration = this.end - this.start;
				this.percentRate = 1f / this.duration;
			}
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00055B10 File Offset: 0x00053D10
		public float TimeToPercentClamped(float time)
		{
			return (time < this.end) ? ((time > this.start) ? ((time - this.start) / this.duration) : 1f) : 1f;
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00055B50 File Offset: 0x00053D50
		public float TimeToPercent(float time)
		{
			return (time - this.start) / this.duration;
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00055B64 File Offset: 0x00053D64
		public float PercentToTimeClamped(float percent)
		{
			return (percent > 0f) ? ((percent < 1f) ? (this.start + this.duration * percent) : this.end) : this.start;
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x00055BA4 File Offset: 0x00053DA4
		public float PercentToTime(float percent)
		{
			return this.start + this.duration * percent;
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x00055BB8 File Offset: 0x00053DB8
		public float TimeClamp(float time)
		{
			return (time < this.end) ? ((time > this.start) ? time : this.start) : this.end;
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x00055BEC File Offset: 0x00053DEC
		public float PercentClamp(float percent)
		{
			return (percent > 0f) ? ((percent < 1f) ? percent : 1f) : 0f;
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x00055C1C File Offset: 0x00053E1C
		public float SampleTime(float time)
		{
			return this.curve.Evaluate(time);
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x00055C2C File Offset: 0x00053E2C
		public float SamplePercent(float percent)
		{
			return this.SampleTime(this.PercentToTime(percent));
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00055C3C File Offset: 0x00053E3C
		public float SampleTimeClamped(float time)
		{
			return this.SampleTime(this.TimeClamp(time));
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x00055C4C File Offset: 0x00053E4C
		public float SamplePercentClamped(float percent)
		{
			return this.SamplePercent(this.PercentToTimeClamped(percent));
		}

		// Token: 0x04000BF3 RID: 3059
		[NonSerialized]
		public AnimationCurve curve;

		// Token: 0x04000BF4 RID: 3060
		[NonSerialized]
		public int length;

		// Token: 0x04000BF5 RID: 3061
		[NonSerialized]
		public float start;

		// Token: 0x04000BF6 RID: 3062
		[NonSerialized]
		public float firstTime;

		// Token: 0x04000BF7 RID: 3063
		[NonSerialized]
		public float end;

		// Token: 0x04000BF8 RID: 3064
		[NonSerialized]
		public float lastTime;

		// Token: 0x04000BF9 RID: 3065
		[NonSerialized]
		public float duration;

		// Token: 0x04000BFA RID: 3066
		[NonSerialized]
		public float percentRate;
	}

	// Token: 0x0200027E RID: 638
	public struct MixerConfig
	{
		// Token: 0x06001724 RID: 5924 RVA: 0x00055C5C File Offset: 0x00053E5C
		public MixerConfig(Animation animation, global::AnimationBlender.ResidualField residual, params global::AnimationBlender.ChannelConfig[] channels)
		{
			this.animation = animation;
			this.residual = residual;
			this.channels = channels;
		}

		// Token: 0x04000BFB RID: 3067
		[NonSerialized]
		public readonly Animation animation;

		// Token: 0x04000BFC RID: 3068
		[NonSerialized]
		public readonly global::AnimationBlender.ResidualField residual;

		// Token: 0x04000BFD RID: 3069
		[NonSerialized]
		public readonly global::AnimationBlender.ChannelConfig[] channels;
	}

	// Token: 0x0200027F RID: 639
	public struct ChannelConfig
	{
		// Token: 0x06001725 RID: 5925 RVA: 0x00055C74 File Offset: 0x00053E74
		public ChannelConfig(string name, global::AnimationBlender.ChannelField field)
		{
			this.name = name;
			this.field = field;
		}

		// Token: 0x04000BFE RID: 3070
		[NonSerialized]
		public readonly string name;

		// Token: 0x04000BFF RID: 3071
		[NonSerialized]
		public readonly global::AnimationBlender.ChannelField field;
	}

	// Token: 0x02000280 RID: 640
	[Serializable]
	public class Field
	{
		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x00055C8C File Offset: 0x00053E8C
		public bool defined
		{
			get
			{
				return !string.IsNullOrEmpty(this.clipName);
			}
		}

		// Token: 0x04000C00 RID: 3072
		[SerializeField]
		public string clipName;

		// Token: 0x04000C01 RID: 3073
		[SerializeField]
		public float startFrame;

		// Token: 0x04000C02 RID: 3074
		[SerializeField]
		public float playbackRate;
	}

	// Token: 0x02000281 RID: 641
	[Serializable]
	public sealed class ResidualField : global::AnimationBlender.Field
	{
		// Token: 0x06001728 RID: 5928 RVA: 0x00055C9C File Offset: 0x00053E9C
		public ResidualField()
		{
			this.clipName = string.Empty;
			this.playbackRate = 1f;
			this.changeAnimLayer = false;
			this.channelBlend = (this.residualBlend = 0);
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x00055CDC File Offset: 0x00053EDC
		public global::AnimationBlender.CurveInfo introCurveInfo
		{
			get
			{
				return new global::AnimationBlender.CurveInfo(this.introCurve);
			}
		}

		// Token: 0x04000C03 RID: 3075
		[SerializeField]
		public AnimationCurve introCurve;

		// Token: 0x04000C04 RID: 3076
		[SerializeField]
		public AnimationBlendMode residualBlend;

		// Token: 0x04000C05 RID: 3077
		[SerializeField]
		public AnimationBlendMode channelBlend;

		// Token: 0x04000C06 RID: 3078
		[SerializeField]
		public int animLayer;

		// Token: 0x04000C07 RID: 3079
		[SerializeField]
		public bool changeAnimLayer;
	}

	// Token: 0x02000282 RID: 642
	[Serializable]
	public sealed class ChannelField : global::AnimationBlender.Field
	{
		// Token: 0x0600172A RID: 5930 RVA: 0x00055CEC File Offset: 0x00053EEC
		public ChannelField()
		{
			this.clipName = string.Empty;
			this.playbackRate = 1f;
			this.inCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
			this.outCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x00055D54 File Offset: 0x00053F54
		public global::AnimationBlender.CurveInfo inCurveInfo
		{
			get
			{
				return new global::AnimationBlender.CurveInfo(this.inCurve);
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x00055D64 File Offset: 0x00053F64
		public global::AnimationBlender.CurveInfo outCurveInfo
		{
			get
			{
				return new global::AnimationBlender.CurveInfo(this.outCurve);
			}
		}

		// Token: 0x04000C08 RID: 3080
		[SerializeField]
		public float inDelay;

		// Token: 0x04000C09 RID: 3081
		[SerializeField]
		public float outDelay;

		// Token: 0x04000C0A RID: 3082
		[SerializeField]
		public float residualBlend;

		// Token: 0x04000C0B RID: 3083
		[SerializeField]
		public bool blockedByAnimation;

		// Token: 0x04000C0C RID: 3084
		[SerializeField]
		public AnimationCurve inCurve;

		// Token: 0x04000C0D RID: 3085
		[SerializeField]
		public AnimationCurve outCurve;
	}

	// Token: 0x02000283 RID: 643
	public sealed class Mixer
	{
		// Token: 0x0600172D RID: 5933 RVA: 0x00055D74 File Offset: 0x00053F74
		public Mixer(global::AnimationBlender.MixerConfig config)
		{
			if (!config.animation)
			{
				throw new ArgumentException("null or missing", "config.animation");
			}
			this.animation = config.animation;
			this.residualField = config.residual;
			this.hasResidual = (!object.ReferenceEquals(config.residual, null) && config.residual.defined && this.animation.GetClip(config.residual.clipName));
			this.oneShotBlendIn = ((!this.hasResidual || object.ReferenceEquals(config.residual.introCurve, null)) ? default(global::AnimationBlender.CurveInfo) : config.residual.introCurveInfo);
			this.hasOneShotBlendIn = (this.oneShotBlendIn.duration > 0f);
			this.residualState = ((!this.hasResidual) ? null : this.animation[config.residual.clipName]);
			this.channelCount = config.channels.Length;
			this.channels = new global::AnimationBlender.Weighted<global::AnimationBlender.Channel>[this.channelCount];
			this.trackers = new global::AnimationBlender.Weighted<global::AnimationBlender.Tracker>[this.channelCount];
			this.trackerCount = 0;
			this.nameToChannel = new Dictionary<string, int>(this.channelCount);
			for (int i = 0; i < this.channelCount; i++)
			{
				global::AnimationBlender.ChannelField field = config.channels[i].field;
				string name = config.channels[i].name;
				this.nameToChannel.Add(name, i);
				int num = -1;
				AnimationClip clip;
				if (field.defined && (clip = this.animation.GetClip(field.clipName)))
				{
					bool flag = false;
					while (!flag)
					{
						if (flag = (++num == this.trackerCount))
						{
							this.trackers[num].value.clip = clip;
							this.trackers[num].value.state = this.animation[field.clipName];
							this.trackers[num].value.channelCount = 1;
							this.trackerCount++;
						}
						else if (flag = (this.trackers[num].value.clip == clip))
						{
							global::AnimationBlender.Weighted<global::AnimationBlender.Tracker>[] array = this.trackers;
							int num2 = num;
							array[num2].value.channelCount = array[num2].value.channelCount + 1;
						}
					}
					this.definedChannelCount++;
				}
				this.channels[i].value = new global::AnimationBlender.Channel(i, num, name, field);
			}
			for (int j = 0; j < this.trackerCount; j++)
			{
				this.trackers[j].value.channels = new int[global::AnimationBlender.GetClear(ref this.trackers[j].value.channelCount)];
			}
			this.definedChannels = new int[global::AnimationBlender.GetClear(ref this.definedChannelCount)];
			for (int k = 0; k < this.channelCount; k++)
			{
				if (this.channels[k].value.animationIndex != -1)
				{
					int[] array2 = this.trackers[this.channels[k].value.animationIndex].value.channels;
					global::AnimationBlender.Weighted<global::AnimationBlender.Tracker>[] array3 = this.trackers;
					int animationIndex = this.channels[k].value.animationIndex;
					int num3;
					array3[animationIndex].value.channelCount = (num3 = array3[animationIndex].value.channelCount) + 1;
					array2[num3] = (this.definedChannels[this.definedChannelCount++] = k);
				}
			}
			global::AnimationBlender.ArrayResize<global::AnimationBlender.Weighted<global::AnimationBlender.Tracker>>(ref this.trackers, this.trackerCount);
			global::AnimationBlender.ArrayResize<global::AnimationBlender.Weighted<global::AnimationBlender.Channel>>(ref this.channels, this.channelCount);
			global::AnimationBlender.ArrayResize<int>(ref this.definedChannels, this.definedChannelCount);
			for (int l = 0; l < this.trackerCount; l++)
			{
				global::AnimationBlender.ArrayResize<int>(ref this.trackers[l].value.channels, this.trackers[l].value.channelCount);
			}
			this.blender = new global::AnimationBlender.TrackerBlender(this.trackerCount);
			if (this.hasResidual)
			{
				if (this.residualField.changeAnimLayer)
				{
					this.residualState.layer = this.residualField.animLayer;
					for (int m = 0; m < this.trackerCount; m++)
					{
						this.trackers[m].value.state.layer = this.residualField.animLayer;
					}
				}
				this.residualState.blendMode = (this.residualBlendMode = this.residualField.residualBlend);
				this.channelBlendMode = this.residualField.channelBlend;
				for (int n = 0; n < this.trackerCount; n++)
				{
					this.trackers[n].value.state.blendMode = this.channelBlendMode;
				}
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x000562F0 File Offset: 0x000544F0
		public bool isPlayingManualAnimation
		{
			get
			{
				return this.ManualAnimationsPlaying(false);
			}
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x000562FC File Offset: 0x000544FC
		private bool PlayQueuedDirect(string animationName, global::AnimationBlender.opt<QueueMode> queueMode, global::AnimationBlender.opt<PlayMode> playMode)
		{
			PlayMode playMode2;
			if (playMode.check(out playMode2))
			{
				return this.animation.PlayQueued(animationName, queueMode[0], playMode2);
			}
			QueueMode queueMode2;
			if (queueMode.check(out queueMode2))
			{
				return this.animation.PlayQueued(animationName, queueMode2);
			}
			return this.animation.PlayQueued(animationName);
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x00056364 File Offset: 0x00054564
		private bool PlayDirect(string animationName, global::AnimationBlender.opt<PlayMode> playMode)
		{
			PlayMode playMode2;
			if (playMode.check(out playMode2))
			{
				return this.animation.Play(animationName, playMode2);
			}
			return this.animation.Play(animationName);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0005639C File Offset: 0x0005459C
		private void CrossFadeDirect(string animationName, global::AnimationBlender.opt<float> fadeLength, global::AnimationBlender.opt<PlayMode> playMode)
		{
			if (playMode.defined)
			{
				this.animation.CrossFade(animationName, fadeLength[0.3f], playMode.value);
			}
			else if (fadeLength.defined)
			{
				this.animation.CrossFade(animationName, fadeLength.value);
			}
			else
			{
				this.animation.CrossFade(animationName);
			}
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x0005640C File Offset: 0x0005460C
		private void StopBlendingNow()
		{
			for (int i = 0; i < this.trackerCount; i++)
			{
				this.trackers[i].value.state.enabled = false;
			}
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x0005644C File Offset: 0x0005464C
		private bool PlayQueuedOpt(string animationName, global::AnimationBlender.opt<QueueMode> queueMode, global::AnimationBlender.opt<PlayMode> playMode)
		{
			if (string.IsNullOrEmpty(animationName) || !this.PlayQueuedDirect(animationName, queueMode, playMode))
			{
				return false;
			}
			this.StopBlendingNow();
			if (queueMode.defined && queueMode.value == 2)
			{
				this.queuedAnimations.Clear();
				this.playingOneShot = false;
				this.oneShotAnimation = null;
				this.SetOneShotAnimation(animationName);
			}
			else if (this.playingOneShot)
			{
				this.queuedAnimations.Enqueue(animationName);
			}
			else
			{
				this.SetOneShotAnimation(animationName);
			}
			return true;
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x000564E0 File Offset: 0x000546E0
		private bool PlayOpt(string animationName, global::AnimationBlender.opt<PlayMode> playMode, global::AnimationBlender.opt<float> speed, global::AnimationBlender.opt<float> startTime)
		{
			AnimationState animationState;
			if (string.IsNullOrEmpty(animationName) || (animationState = this.animation[animationName]) == null)
			{
				return false;
			}
			if (!playMode.defined)
			{
				this.animation.Stop();
			}
			float speed2;
			if (speed.defined)
			{
				speed2 = animationState.speed;
				animationState.speed = speed.value;
			}
			else
			{
				speed2 = 0f;
			}
			if (!this.PlayDirect(animationName, playMode))
			{
				if (speed.defined)
				{
					animationState.speed = speed2;
				}
				return false;
			}
			this.queuedAnimations.Clear();
			this.playingOneShot = true;
			this.oneShotAnimation = animationState;
			if (startTime.defined)
			{
				animationState.time = startTime.value;
			}
			return true;
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x000565AC File Offset: 0x000547AC
		private bool CrossFadeOpt(string animationName, global::AnimationBlender.opt<float> fadeLength, global::AnimationBlender.opt<PlayMode> playMode, global::AnimationBlender.opt<float> speed, global::AnimationBlender.opt<float> startTime)
		{
			AnimationState animationState;
			if (string.IsNullOrEmpty(animationName) || (animationState = this.animation[animationName]) == null)
			{
				return false;
			}
			if (speed.defined)
			{
				animationState.speed = speed.value;
			}
			this.CrossFadeDirect(animationName, fadeLength, playMode);
			this.queuedAnimations.Clear();
			this.playingOneShot = true;
			this.oneShotAnimation = animationState;
			if (startTime.defined)
			{
				animationState.time = startTime.value;
			}
			return true;
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00056634 File Offset: 0x00054834
		private bool ManualAnimationsPlaying(bool ClearWhenNone)
		{
			if (!this.playingOneShot)
			{
				return false;
			}
			while (object.ReferenceEquals(this.oneShotAnimation, null) || !this.oneShotAnimation.enabled)
			{
				if (this.queuedAnimations.Count == 0)
				{
					if (ClearWhenNone)
					{
						this.oneShotAnimation = null;
						this.playingOneShot = false;
					}
					return false;
				}
				this.SetOneShotAnimation(this.queuedAnimations.Dequeue());
			}
			return true;
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x000566B0 File Offset: 0x000548B0
		private void UpdateChannel(ref global::AnimationBlender.Weighted<global::AnimationBlender.Channel> channel, float dt)
		{
			bool flag;
			if (flag = (channel.value.field.blockedByAnimation && this.animationBlocking && channel.value.active))
			{
				channel.value.active = false;
			}
			channel.weight.raw = channel.value.Update(dt);
			if (flag)
			{
				channel.value.active = true;
			}
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x00056728 File Offset: 0x00054928
		private void UpdateTracker(ref global::AnimationBlender.Weighted<global::AnimationBlender.Tracker> tracker, float dt)
		{
			for (int i = 0; i < tracker.value.channelCount; i++)
			{
				this.UpdateChannel(ref this.channels[tracker.value.channels[i]], dt);
			}
			if (tracker.value.enabled = global::AnimationBlender.WeightOf<global::AnimationBlender.Channel>(this.channels, tracker.value.channels, out tracker.value.channelWeight))
			{
				tracker.value.startTime = this.channels[tracker.value.channels[tracker.value.channelWeight.winner]].value.startTime;
				float num = 0f;
				float num2 = 0f;
				for (int j = 0; j < tracker.value.channelWeight.count; j++)
				{
					float normalized;
					num += this.channels[tracker.value.channels[j]].value.playbackRate * (normalized = this.channels[tracker.value.channels[j]].weight.normalized);
					num2 += this.channels[tracker.value.channels[j]].value.maxBlend * normalized;
				}
				tracker.value.playbackRate = num;
				tracker.value.blendFraction = num2;
			}
			tracker.weight.raw = tracker.value.channelWeight.sum.raw;
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x000568C0 File Offset: 0x00054AC0
		private bool UpdateBlender(ref global::AnimationBlender.TrackerBlender blender, float dt, float externalBlend, out float residualBlend)
		{
			for (int i = 0; i < this.trackerCount; i++)
			{
				this.UpdateTracker(ref this.trackers[blender.trackers[i]], dt);
			}
			bool flag = global::AnimationBlender.WeightOf<global::AnimationBlender.Tracker>(this.trackers, blender.trackers, out blender.trackerWeight);
			for (int j = blender.trackerWeight.count; j < blender.trackerCount; j++)
			{
				this.DisableTracker(ref this.trackers[blender.trackers[j]].value);
			}
			float num = 0f;
			for (int k = 0; k < blender.trackerWeight.count; k++)
			{
				num += this.BindTracker(ref this.trackers[blender.trackers[k]], externalBlend);
			}
			return (residualBlend = (1f - num) * externalBlend) > 0f;
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x000569B4 File Offset: 0x00054BB4
		private void DisableTracker(ref global::AnimationBlender.Tracker tracker)
		{
			if (!tracker.wasEnabled)
			{
				return;
			}
			tracker.state.enabled = (tracker.wasEnabled = false);
			tracker.state.weight = 0f;
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x000569F4 File Offset: 0x00054BF4
		private float BindTracker(ref global::AnimationBlender.Weighted<global::AnimationBlender.Tracker> tracker, float externalBlend)
		{
			float num = tracker.weight.normalized;
			if (this.hasResidual)
			{
				num *= tracker.value.blendFraction;
			}
			if (this.blender.trackerWeight.sum.raw < 1f)
			{
				num *= this.blender.trackerWeight.sum.raw;
			}
			if (num > 0f)
			{
				if (!tracker.value.wasEnabled)
				{
					tracker.value.state.enabled = (tracker.value.wasEnabled = true);
					tracker.value.state.time = tracker.value.startTime;
				}
				tracker.value.state.weight = num * externalBlend;
				tracker.value.state.speed = tracker.value.playbackRate;
			}
			else if (tracker.value.wasEnabled)
			{
				this.DisableTracker(ref tracker.value);
			}
			return num;
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00056B04 File Offset: 0x00054D04
		public void SetActive(int channel, bool value)
		{
			this.channels[channel].value.active = value;
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00056B20 File Offset: 0x00054D20
		public void SetActive(string channel, bool value)
		{
			this.SetActive(this.nameToChannel[channel], value);
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x00056B38 File Offset: 0x00054D38
		public void SetSolo(int channel)
		{
			for (int i = 0; i < this.channelCount; i++)
			{
				this.SetActive(i, i == channel);
			}
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00056B68 File Offset: 0x00054D68
		public void SetSolo(string channel)
		{
			this.SetSolo(this.nameToChannel[channel]);
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x00056B7C File Offset: 0x00054D7C
		public void SetSolo(int channel, bool muteall)
		{
			if (muteall)
			{
				for (int i = 0; i < this.channelCount; i++)
				{
					this.SetActive(i, false);
				}
			}
			else
			{
				this.SetSolo(channel);
			}
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00056BBC File Offset: 0x00054DBC
		public void SetSolo(string channel, bool muteall)
		{
			this.SetSolo(this.nameToChannel[channel], muteall);
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00056BD4 File Offset: 0x00054DD4
		public bool SetOneShotAnimation(AnimationState animationState)
		{
			if (animationState == null)
			{
				return false;
			}
			this.oneShotAnimation = animationState;
			return this.playingOneShot = true;
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x00056C00 File Offset: 0x00054E00
		public bool SetOneShotAnimation(string animationName)
		{
			return !string.IsNullOrEmpty(animationName) && this.SetOneShotAnimation(this.animation[animationName]);
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x00056C30 File Offset: 0x00054E30
		public bool Play(string animationName)
		{
			return this.PlayOpt(animationName, global::AnimationBlender.opt<PlayMode>.none, global::AnimationBlender.opt<float>.none, global::AnimationBlender.opt<float>.none);
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x00056C48 File Offset: 0x00054E48
		public bool Play(string animationName, PlayMode playMode)
		{
			return this.PlayOpt(animationName, playMode, global::AnimationBlender.opt<float>.none, global::AnimationBlender.opt<float>.none);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00056C64 File Offset: 0x00054E64
		public bool Play(string animationName, PlayMode playMode, float speed)
		{
			return this.PlayOpt(animationName, playMode, speed, global::AnimationBlender.opt<float>.none);
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x00056C80 File Offset: 0x00054E80
		public bool Play(string animationName, PlayMode playMode, float speed, float startTime)
		{
			return this.PlayOpt(animationName, playMode, speed, startTime);
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00056CA8 File Offset: 0x00054EA8
		public bool Play(string animationName, PlayMode playMode, float? speed, float startTime)
		{
			return this.PlayOpt(animationName, playMode, global::AnimationBlender.to_opt<float>(speed), startTime);
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x00056CD0 File Offset: 0x00054ED0
		public bool Play(string animationName, float speed)
		{
			return this.PlayOpt(animationName, global::AnimationBlender.opt<PlayMode>.none, speed, global::AnimationBlender.opt<float>.none);
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00056CEC File Offset: 0x00054EEC
		public bool Play(string animationName, float speed, float startTime)
		{
			return this.PlayOpt(animationName, global::AnimationBlender.opt<PlayMode>.none, speed, startTime);
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x00056D08 File Offset: 0x00054F08
		public bool Play(string animationName, float? speed, float startTime)
		{
			return this.PlayOpt(animationName, global::AnimationBlender.opt<PlayMode>.none, global::AnimationBlender.to_opt<float>(speed), startTime);
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00056D24 File Offset: 0x00054F24
		public bool PlayQueued(string animationName)
		{
			return this.PlayQueuedOpt(animationName, global::AnimationBlender.opt<QueueMode>.none, global::AnimationBlender.opt<PlayMode>.none);
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00056D38 File Offset: 0x00054F38
		public bool PlayQueued(string animationName, QueueMode queueMode)
		{
			return this.PlayQueuedOpt(animationName, queueMode, global::AnimationBlender.opt<PlayMode>.none);
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00056D4C File Offset: 0x00054F4C
		public bool PlayQueued(string animationName, QueueMode queueMode, PlayMode playMode)
		{
			return this.PlayQueuedOpt(animationName, queueMode, playMode);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00056D64 File Offset: 0x00054F64
		public bool CrossFade(string animationName)
		{
			return this.CrossFadeOpt(animationName, global::AnimationBlender.opt<float>.none, global::AnimationBlender.opt<PlayMode>.none, global::AnimationBlender.opt<float>.none, global::AnimationBlender.opt<float>.none);
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x00056D84 File Offset: 0x00054F84
		public bool CrossFade(string animationName, float fadeLen)
		{
			return this.CrossFadeOpt(animationName, fadeLen, global::AnimationBlender.opt<PlayMode>.none, global::AnimationBlender.opt<float>.none, global::AnimationBlender.opt<float>.none);
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x00056DA4 File Offset: 0x00054FA4
		public bool CrossFade(string animationName, float fadeLen, PlayMode playMode)
		{
			return this.CrossFadeOpt(animationName, fadeLen, playMode, global::AnimationBlender.opt<float>.none, global::AnimationBlender.opt<float>.none);
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x00056DD0 File Offset: 0x00054FD0
		public bool CrossFade(string animationName, float fadeLen, PlayMode playMode, float speed)
		{
			return this.CrossFadeOpt(animationName, fadeLen, playMode, speed, global::AnimationBlender.opt<float>.none);
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x00056DFC File Offset: 0x00054FFC
		public bool CrossFade(string animationName, float fadeLen, PlayMode playMode, float speed, float startTime)
		{
			return this.CrossFadeOpt(animationName, fadeLen, playMode, speed, startTime);
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00056E2C File Offset: 0x0005502C
		public bool CrossFade(string animationName, float fadeLen, PlayMode playMode, float? speed, float startTime)
		{
			return this.CrossFadeOpt(animationName, fadeLen, playMode, global::AnimationBlender.to_opt<float>(speed), startTime);
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x00056E5C File Offset: 0x0005505C
		public void Update(float blend, float dt)
		{
			if (this.playingOneShot)
			{
				if (!this.ManualAnimationsPlaying(true))
				{
					this.oneShotBlendInTime = this.oneShotBlendIn.start + dt;
					if (this.oneShotBlendInTime > this.oneShotBlendIn.end)
					{
						this.oneShotBlendInTime = this.oneShotBlendIn.end;
					}
					this.animationBlocking = false;
					for (int i = 0; i < this.trackerCount; i++)
					{
						this.trackers[i].value.wasEnabled = false;
					}
				}
				else
				{
					this.oneShotBlendInTime = this.oneShotBlendIn.start;
					if (!this.hasOneShotBlendIn)
					{
						blend = 0f;
					}
					this.animationBlocking = true;
				}
			}
			else
			{
				this.animationBlocking = false;
				if (this.oneShotBlendInTime < this.oneShotBlendIn.end && (this.oneShotBlendInTime += dt) > this.oneShotBlendIn.end)
				{
					this.oneShotBlendInTime = this.oneShotBlendIn.end;
				}
			}
			if (this.hasOneShotBlendIn)
			{
				blend *= this.oneShotBlendIn.SampleTime(this.oneShotBlendInTime);
			}
			if (blend > 1f)
			{
				blend = 1f;
			}
			else if (blend < 0f)
			{
				blend = 0f;
			}
			float weight;
			if (this.UpdateBlender(ref this.blender, dt, blend, out weight))
			{
				if (this.hasResidual)
				{
					bool flag = !this.residualState.enabled;
					this.residualState.enabled = true;
					this.residualState.weight = weight;
					if (flag)
					{
						this.residualState.time = this.residualField.startFrame;
						this.residualState.speed = this.residualField.playbackRate;
					}
				}
			}
			else if (this.hasResidual && this.residualState.enabled)
			{
				this.residualState.enabled = false;
				this.residualState.weight = 0f;
			}
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x00057070 File Offset: 0x00055270
		public void Debug(Rect rect, string name)
		{
			global::AnimationBlender.Mixer.DbgGUI.TableStart(rect);
			for (int i = 0; i < this.channels.Length; i++)
			{
				if (this.channels[i].weight.any)
				{
					global::AnimationBlender.Mixer.DbgGUI.Label(this.channels[i].value.name);
				}
			}
			for (int j = 0; j < this.trackers.Length; j++)
			{
				if (this.trackers[j].value.enabled)
				{
					global::AnimationBlender.Mixer.DbgGUI.Label(this.trackers[j].value.state.name);
				}
			}
			if (this.hasResidual)
			{
				global::AnimationBlender.Mixer.DbgGUI.Label(this.residualState.name);
			}
			global::AnimationBlender.Mixer.DbgGUI.ColumnNext();
			for (int k = 0; k < this.channels.Length; k++)
			{
				if (this.channels[k].weight.any)
				{
					global::AnimationBlender.Mixer.DbgGUI.Fract(this.channels[k].weight.normalized);
				}
			}
			for (int l = 0; l < this.trackers.Length; l++)
			{
				if (this.trackers[l].value.enabled)
				{
					global::AnimationBlender.Mixer.DbgGUI.Fract(this.trackers[l].weight.normalized);
				}
			}
			if (this.hasResidual)
			{
				global::AnimationBlender.Mixer.DbgGUI.Fract(this.residualState.weight);
			}
			global::AnimationBlender.Mixer.DbgGUI.TableEnd();
		}

		// Token: 0x04000C0E RID: 3086
		[NonSerialized]
		private Animation animation;

		// Token: 0x04000C0F RID: 3087
		[NonSerialized]
		private global::AnimationBlender.ResidualField residualField;

		// Token: 0x04000C10 RID: 3088
		[NonSerialized]
		private AnimationState residualState;

		// Token: 0x04000C11 RID: 3089
		[NonSerialized]
		private AnimationBlendMode residualBlendMode;

		// Token: 0x04000C12 RID: 3090
		[NonSerialized]
		private AnimationBlendMode channelBlendMode;

		// Token: 0x04000C13 RID: 3091
		[NonSerialized]
		private AnimationState oneShotAnimation;

		// Token: 0x04000C14 RID: 3092
		[NonSerialized]
		private bool playingOneShot;

		// Token: 0x04000C15 RID: 3093
		[NonSerialized]
		private bool animationBlocking;

		// Token: 0x04000C16 RID: 3094
		[NonSerialized]
		private int trackerCount;

		// Token: 0x04000C17 RID: 3095
		[NonSerialized]
		private int channelCount;

		// Token: 0x04000C18 RID: 3096
		[NonSerialized]
		private int definedChannelCount;

		// Token: 0x04000C19 RID: 3097
		[NonSerialized]
		private int[] definedChannels;

		// Token: 0x04000C1A RID: 3098
		[NonSerialized]
		private global::AnimationBlender.TrackerBlender blender;

		// Token: 0x04000C1B RID: 3099
		[NonSerialized]
		private global::AnimationBlender.Weighted<global::AnimationBlender.Channel>[] channels;

		// Token: 0x04000C1C RID: 3100
		[NonSerialized]
		private global::AnimationBlender.Weighted<global::AnimationBlender.Tracker>[] trackers;

		// Token: 0x04000C1D RID: 3101
		[NonSerialized]
		private Dictionary<string, int> nameToChannel;

		// Token: 0x04000C1E RID: 3102
		[NonSerialized]
		private global::AnimationBlender.CurveInfo oneShotBlendIn;

		// Token: 0x04000C1F RID: 3103
		[NonSerialized]
		private bool hasOneShotBlendIn;

		// Token: 0x04000C20 RID: 3104
		[NonSerialized]
		private Queue<string> queuedAnimations = new Queue<string>();

		// Token: 0x04000C21 RID: 3105
		[NonSerialized]
		private float oneShotBlendInTime;

		// Token: 0x04000C22 RID: 3106
		[NonSerialized]
		private float sumWeight;

		// Token: 0x04000C23 RID: 3107
		[NonSerialized]
		private bool hasResidual;

		// Token: 0x02000284 RID: 644
		private static class DbgGUI
		{
			// Token: 0x06001758 RID: 5976 RVA: 0x00057258 File Offset: 0x00055458
			public static void Label(string str)
			{
				GUILayout.Label(str, global::AnimationBlender.Mixer.DbgGUI.Cell);
			}

			// Token: 0x06001759 RID: 5977 RVA: 0x00057268 File Offset: 0x00055468
			public static void Fract(float frac)
			{
				GUILayout.HorizontalSlider(frac, 0f, 1f, global::AnimationBlender.Mixer.DbgGUI.Cell);
			}

			// Token: 0x0600175A RID: 5978 RVA: 0x00057280 File Offset: 0x00055480
			public static void ColumnNext()
			{
				GUILayout.EndVertical();
				GUILayout.BeginVertical(global::AnimationBlender.Mixer.DbgGUI.OtherColumn);
			}

			// Token: 0x0600175B RID: 5979 RVA: 0x00057294 File Offset: 0x00055494
			public static void TableStart(Rect rect)
			{
				GUILayout.BeginArea(rect);
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.BeginVertical(global::AnimationBlender.Mixer.DbgGUI.FirstColumn);
			}

			// Token: 0x0600175C RID: 5980 RVA: 0x000572B4 File Offset: 0x000554B4
			public static void TableEnd()
			{
				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
				GUILayout.EndArea();
			}

			// Token: 0x04000C24 RID: 3108
			private static readonly GUILayoutOption[] Cell = new GUILayoutOption[]
			{
				GUILayout.Height(18f)
			};

			// Token: 0x04000C25 RID: 3109
			private static readonly GUILayoutOption[] FirstColumn = new GUILayoutOption[]
			{
				GUILayout.Width(128f)
			};

			// Token: 0x04000C26 RID: 3110
			private static readonly GUILayoutOption[] OtherColumn = new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(true)
			};
		}
	}
}
