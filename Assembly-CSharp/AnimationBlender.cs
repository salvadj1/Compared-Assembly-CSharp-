using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200023F RID: 575
public static class AnimationBlender
{
	// Token: 0x060015A7 RID: 5543 RVA: 0x00050A88 File Offset: 0x0004EC88
	private static void ZeroWeight(ref AnimationBlender.WeightUnit weight)
	{
		weight.raw = (weight.scaled = (weight.normalized = 0f));
	}

	// Token: 0x060015A8 RID: 5544 RVA: 0x00050AB4 File Offset: 0x0004ECB4
	private static void OneWeight(ref AnimationBlender.WeightUnit weight)
	{
		weight.raw = (weight.scaled = (weight.normalized = 1f));
	}

	// Token: 0x060015A9 RID: 5545 RVA: 0x00050AE0 File Offset: 0x0004ECE0
	private static void OneWeightScale(ref AnimationBlender.WeightUnit weight)
	{
		weight.scaled = (weight.normalized = 1f);
	}

	// Token: 0x060015AA RID: 5546 RVA: 0x00050B04 File Offset: 0x0004ED04
	private static void SetWeight(ref AnimationBlender.WeightUnit weight)
	{
		weight.scaled = weight.raw;
		weight.normalized = 1f;
	}

	// Token: 0x060015AB RID: 5547 RVA: 0x00050B20 File Offset: 0x0004ED20
	private static AnimationBlender.Weighted<T>[] WeightArray<T>(int size)
	{
		return new AnimationBlender.Weighted<T>[size];
	}

	// Token: 0x060015AC RID: 5548 RVA: 0x00050B38 File Offset: 0x0004ED38
	private static AnimationBlender.Weighted<T>[] WeightArray<T>(T[] source)
	{
		if (object.ReferenceEquals(source, null))
		{
			return null;
		}
		int num = source.Length;
		AnimationBlender.Weighted<T>[] array = AnimationBlender.WeightArray<T>(num);
		for (int i = 0; i < num; i++)
		{
			array[i].value = source[i];
		}
		return array;
	}

	// Token: 0x060015AD RID: 5549 RVA: 0x00050B84 File Offset: 0x0004ED84
	private static bool WeightOf<T>(AnimationBlender.Weighted<T>[] items, int[] index, out AnimationBlender.WeightResult result)
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
				AnimationBlender.ZeroWeight(ref items[index[i]].weight);
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
					AnimationBlender.OneWeight(ref items[index[i]].weight);
				}
				else
				{
					AnimationBlender.SetWeight(ref items[index[i]].weight);
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
				AnimationBlender.OneWeightScale(ref items[index[0]].weight);
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

	// Token: 0x060015AE RID: 5550 RVA: 0x00050D98 File Offset: 0x0004EF98
	private static int GetClear(ref int value)
	{
		int result = value;
		value = 0;
		return result;
	}

	// Token: 0x060015AF RID: 5551 RVA: 0x00050DAC File Offset: 0x0004EFAC
	private static void ArrayResize<T>(ref T[] array, int size)
	{
		Array.Resize<T>(ref array, size);
	}

	// Token: 0x060015B0 RID: 5552 RVA: 0x00050DB8 File Offset: 0x0004EFB8
	private static AnimationBlender.opt<T> to_opt<T>(T? nullable) where T : struct
	{
		return (nullable != null) ? nullable.Value : AnimationBlender.opt<T>.none;
	}

	// Token: 0x060015B1 RID: 5553 RVA: 0x00050DE8 File Offset: 0x0004EFE8
	public static AnimationBlender.ChannelConfig Alias(this AnimationBlender.ChannelField Field, string Name)
	{
		return new AnimationBlender.ChannelConfig(Name, Field);
	}

	// Token: 0x060015B2 RID: 5554 RVA: 0x00050DF4 File Offset: 0x0004EFF4
	public static AnimationBlender.ChannelConfig[] Alias(this AnimationBlender.ChannelField Field, AnimationBlender.ChannelConfig[] Array, int Index, string Name)
	{
		Array[Index] = Field.Alias(Name);
		return Array;
	}

	// Token: 0x060015B3 RID: 5555 RVA: 0x00050E0C File Offset: 0x0004F00C
	public static AnimationBlender.ChannelConfig[] Define(this AnimationBlender.ChannelConfig[] Array, int Index, string Name, AnimationBlender.ChannelField Field)
	{
		return Field.Alias(Array, Index, Name);
	}

	// Token: 0x060015B4 RID: 5556 RVA: 0x00050E18 File Offset: 0x0004F018
	public static AnimationBlender.MixerConfig Alias(this AnimationBlender.ResidualField ResidualField, Animation Animation, params AnimationBlender.ChannelConfig[] ChannelAliases)
	{
		return new AnimationBlender.MixerConfig(Animation, ResidualField, ChannelAliases);
	}

	// Token: 0x060015B5 RID: 5557 RVA: 0x00050E24 File Offset: 0x0004F024
	public static AnimationBlender.MixerConfig Alias(this AnimationBlender.ResidualField ResidualField, Animation Animation, int ChannelCount)
	{
		return new AnimationBlender.MixerConfig(Animation, ResidualField, new AnimationBlender.ChannelConfig[ChannelCount]);
	}

	// Token: 0x060015B6 RID: 5558 RVA: 0x00050E34 File Offset: 0x0004F034
	public static AnimationBlender.Mixer Create(this AnimationBlender.MixerConfig Config)
	{
		return new AnimationBlender.Mixer(Config);
	}

	// Token: 0x02000240 RID: 576
	private struct Channel
	{
		// Token: 0x060015B7 RID: 5559 RVA: 0x00050E3C File Offset: 0x0004F03C
		public Channel(int index, int animationIndex, string name, AnimationBlender.ChannelField field)
		{
			this.index = index;
			this.animationIndex = animationIndex;
			this.name = name;
			this.field = field;
			this.induce = new AnimationBlender.ChannelCurve(field.inCurveInfo, default(AnimationBlender.State), default(AnimationBlender.Influence), field, true);
			this.reduce = new AnimationBlender.ChannelCurve(field.outCurveInfo, default(AnimationBlender.State), default(AnimationBlender.Influence), field, false);
			this.active = false;
			this.wasActive = false;
			this.startedTransition = false;
			this.valid = (animationIndex != -1);
			this.maxBlend = ((field.residualBlend > 0f) ? ((field.residualBlend < 1f) ? (1f - field.residualBlend) : 0f) : 1f);
			this.startTime = field.startFrame;
			this.playbackRate = field.playbackRate;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00050F3C File Offset: 0x0004F13C
		private bool StartTransition(ref AnimationBlender.ChannelCurve from, ref AnimationBlender.ChannelCurve to, ref float dt, bool startNow)
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

		// Token: 0x060015B9 RID: 5561 RVA: 0x00051094 File Offset: 0x0004F294
		private float Step(bool transitioning, ref AnimationBlender.ChannelCurve from, ref AnimationBlender.ChannelCurve to, ref float dt)
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
					from.state = default(AnimationBlender.State);
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

		// Token: 0x060015BA RID: 5562 RVA: 0x00051424 File Offset: 0x0004F624
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

		// Token: 0x04000A9C RID: 2716
		[NonSerialized]
		public AnimationBlender.ChannelField field;

		// Token: 0x04000A9D RID: 2717
		[NonSerialized]
		public string name;

		// Token: 0x04000A9E RID: 2718
		[NonSerialized]
		public bool active;

		// Token: 0x04000A9F RID: 2719
		[NonSerialized]
		public bool valid;

		// Token: 0x04000AA0 RID: 2720
		[NonSerialized]
		public bool wasActive;

		// Token: 0x04000AA1 RID: 2721
		[NonSerialized]
		public bool startedTransition;

		// Token: 0x04000AA2 RID: 2722
		[NonSerialized]
		public AnimationBlender.ChannelCurve induce;

		// Token: 0x04000AA3 RID: 2723
		[NonSerialized]
		public AnimationBlender.ChannelCurve reduce;

		// Token: 0x04000AA4 RID: 2724
		[NonSerialized]
		public int index;

		// Token: 0x04000AA5 RID: 2725
		[NonSerialized]
		public int animationIndex;

		// Token: 0x04000AA6 RID: 2726
		[NonSerialized]
		public float maxBlend;

		// Token: 0x04000AA7 RID: 2727
		[NonSerialized]
		public float playbackRate;

		// Token: 0x04000AA8 RID: 2728
		[NonSerialized]
		public float startTime;
	}

	// Token: 0x02000241 RID: 577
	private struct ChannelCurve
	{
		// Token: 0x060015BB RID: 5563 RVA: 0x000514F0 File Offset: 0x0004F6F0
		public ChannelCurve(AnimationBlender.CurveInfo info, AnimationBlender.State state, AnimationBlender.Influence influence, AnimationBlender.ChannelField field, bool induces)
		{
			this.info = info;
			this.state = state;
			this.influence = influence;
			this.induces = induces;
			this.delayDuration = ((!induces) ? field.outDelay : field.inDelay);
		}

		// Token: 0x04000AA9 RID: 2729
		[NonSerialized]
		public AnimationBlender.CurveInfo info;

		// Token: 0x04000AAA RID: 2730
		[NonSerialized]
		public AnimationBlender.State state;

		// Token: 0x04000AAB RID: 2731
		[NonSerialized]
		public AnimationBlender.Influence influence;

		// Token: 0x04000AAC RID: 2732
		[NonSerialized]
		public float delayDuration;

		// Token: 0x04000AAD RID: 2733
		[NonSerialized]
		public bool induces;
	}

	// Token: 0x02000242 RID: 578
	private struct Influence
	{
		// Token: 0x04000AAE RID: 2734
		[NonSerialized]
		public bool active;

		// Token: 0x04000AAF RID: 2735
		[NonSerialized]
		public float value;

		// Token: 0x04000AB0 RID: 2736
		[NonSerialized]
		public float percent;

		// Token: 0x04000AB1 RID: 2737
		[NonSerialized]
		public float timeleft;

		// Token: 0x04000AB2 RID: 2738
		[NonSerialized]
		public float duration;
	}

	// Token: 0x02000243 RID: 579
	private struct State
	{
		// Token: 0x04000AB3 RID: 2739
		[NonSerialized]
		public bool active;

		// Token: 0x04000AB4 RID: 2740
		[NonSerialized]
		public float time;

		// Token: 0x04000AB5 RID: 2741
		[NonSerialized]
		public float percent;

		// Token: 0x04000AB6 RID: 2742
		[NonSerialized]
		public float delay;

		// Token: 0x04000AB7 RID: 2743
		[NonSerialized]
		public float value;
	}

	// Token: 0x02000244 RID: 580
	private struct Tracker
	{
		// Token: 0x04000AB8 RID: 2744
		[NonSerialized]
		public AnimationClip clip;

		// Token: 0x04000AB9 RID: 2745
		[NonSerialized]
		public AnimationState state;

		// Token: 0x04000ABA RID: 2746
		[NonSerialized]
		public int[] channels;

		// Token: 0x04000ABB RID: 2747
		[NonSerialized]
		public int channelCount;

		// Token: 0x04000ABC RID: 2748
		[NonSerialized]
		public AnimationBlender.WeightResult channelWeight;

		// Token: 0x04000ABD RID: 2749
		[NonSerialized]
		public float playbackRate;

		// Token: 0x04000ABE RID: 2750
		[NonSerialized]
		public float blendFraction;

		// Token: 0x04000ABF RID: 2751
		[NonSerialized]
		public float startTime;

		// Token: 0x04000AC0 RID: 2752
		[NonSerialized]
		public bool enabled;

		// Token: 0x04000AC1 RID: 2753
		[NonSerialized]
		public bool wasEnabled;
	}

	// Token: 0x02000245 RID: 581
	private struct TrackerBlender
	{
		// Token: 0x060015BC RID: 5564 RVA: 0x00051530 File Offset: 0x0004F730
		public TrackerBlender(int count)
		{
			this.trackers = new int[count];
			this.trackerCount = count;
			this.trackerWeight = default(AnimationBlender.WeightResult);
			for (int i = 0; i < count; i++)
			{
				this.trackers[i] = i;
			}
		}

		// Token: 0x04000AC2 RID: 2754
		[NonSerialized]
		public int[] trackers;

		// Token: 0x04000AC3 RID: 2755
		[NonSerialized]
		public int trackerCount;

		// Token: 0x04000AC4 RID: 2756
		[NonSerialized]
		public AnimationBlender.WeightResult trackerWeight;
	}

	// Token: 0x02000246 RID: 582
	private struct opt<T>
	{
		// Token: 0x060015BD RID: 5565 RVA: 0x0005157C File Offset: 0x0004F77C
		private opt(T value, bool defined)
		{
			this.value = value;
			this.defined = defined;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x000515A8 File Offset: 0x0004F7A8
		public bool check(out T value)
		{
			value = this.value;
			return this.defined;
		}

		// Token: 0x17000640 RID: 1600
		public T this[T fallback]
		{
			get
			{
				return (!this.defined) ? fallback : this.value;
			}
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x000515D8 File Offset: 0x0004F7D8
		public static implicit operator AnimationBlender.opt<T>(T value)
		{
			return new AnimationBlender.opt<T>(value, true);
		}

		// Token: 0x04000AC5 RID: 2757
		[NonSerialized]
		public readonly T value;

		// Token: 0x04000AC6 RID: 2758
		[NonSerialized]
		public readonly bool defined;

		// Token: 0x04000AC7 RID: 2759
		public static readonly AnimationBlender.opt<T> none = default(AnimationBlender.opt<T>);
	}

	// Token: 0x02000247 RID: 583
	private struct WeightUnit
	{
		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x000515E4 File Offset: 0x0004F7E4
		public bool any
		{
			get
			{
				return this.raw > 0f;
			}
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x000515F4 File Offset: 0x0004F7F4
		public float SetScaledRecip(float recip)
		{
			return this.normalized = (this.scaled = this.raw * recip);
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0005161C File Offset: 0x0004F81C
		public float SetNormalizedRecip(float recip)
		{
			return this.normalized = this.scaled * recip;
		}

		// Token: 0x04000AC8 RID: 2760
		[NonSerialized]
		public float raw;

		// Token: 0x04000AC9 RID: 2761
		[NonSerialized]
		public float scaled;

		// Token: 0x04000ACA RID: 2762
		[NonSerialized]
		public float normalized;
	}

	// Token: 0x02000248 RID: 584
	private struct WeightResult
	{
		// Token: 0x04000ACB RID: 2763
		[NonSerialized]
		public int count;

		// Token: 0x04000ACC RID: 2764
		[NonSerialized]
		public int winner;

		// Token: 0x04000ACD RID: 2765
		public AnimationBlender.WeightUnit sum;
	}

	// Token: 0x02000249 RID: 585
	private struct Weighted<T>
	{
		// Token: 0x04000ACE RID: 2766
		[NonSerialized]
		public AnimationBlender.WeightUnit weight;

		// Token: 0x04000ACF RID: 2767
		[NonSerialized]
		public T value;
	}

	// Token: 0x0200024A RID: 586
	public struct CurveInfo
	{
		// Token: 0x060015C5 RID: 5573 RVA: 0x0005163C File Offset: 0x0004F83C
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

		// Token: 0x060015C6 RID: 5574 RVA: 0x00051768 File Offset: 0x0004F968
		public float TimeToPercentClamped(float time)
		{
			return (time < this.end) ? ((time > this.start) ? ((time - this.start) / this.duration) : 1f) : 1f;
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x000517A8 File Offset: 0x0004F9A8
		public float TimeToPercent(float time)
		{
			return (time - this.start) / this.duration;
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x000517BC File Offset: 0x0004F9BC
		public float PercentToTimeClamped(float percent)
		{
			return (percent > 0f) ? ((percent < 1f) ? (this.start + this.duration * percent) : this.end) : this.start;
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x000517FC File Offset: 0x0004F9FC
		public float PercentToTime(float percent)
		{
			return this.start + this.duration * percent;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00051810 File Offset: 0x0004FA10
		public float TimeClamp(float time)
		{
			return (time < this.end) ? ((time > this.start) ? time : this.start) : this.end;
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x00051844 File Offset: 0x0004FA44
		public float PercentClamp(float percent)
		{
			return (percent > 0f) ? ((percent < 1f) ? percent : 1f) : 0f;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00051874 File Offset: 0x0004FA74
		public float SampleTime(float time)
		{
			return this.curve.Evaluate(time);
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x00051884 File Offset: 0x0004FA84
		public float SamplePercent(float percent)
		{
			return this.SampleTime(this.PercentToTime(percent));
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00051894 File Offset: 0x0004FA94
		public float SampleTimeClamped(float time)
		{
			return this.SampleTime(this.TimeClamp(time));
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x000518A4 File Offset: 0x0004FAA4
		public float SamplePercentClamped(float percent)
		{
			return this.SamplePercent(this.PercentToTimeClamped(percent));
		}

		// Token: 0x04000AD0 RID: 2768
		[NonSerialized]
		public AnimationCurve curve;

		// Token: 0x04000AD1 RID: 2769
		[NonSerialized]
		public int length;

		// Token: 0x04000AD2 RID: 2770
		[NonSerialized]
		public float start;

		// Token: 0x04000AD3 RID: 2771
		[NonSerialized]
		public float firstTime;

		// Token: 0x04000AD4 RID: 2772
		[NonSerialized]
		public float end;

		// Token: 0x04000AD5 RID: 2773
		[NonSerialized]
		public float lastTime;

		// Token: 0x04000AD6 RID: 2774
		[NonSerialized]
		public float duration;

		// Token: 0x04000AD7 RID: 2775
		[NonSerialized]
		public float percentRate;
	}

	// Token: 0x0200024B RID: 587
	public struct MixerConfig
	{
		// Token: 0x060015D0 RID: 5584 RVA: 0x000518B4 File Offset: 0x0004FAB4
		public MixerConfig(Animation animation, AnimationBlender.ResidualField residual, params AnimationBlender.ChannelConfig[] channels)
		{
			this.animation = animation;
			this.residual = residual;
			this.channels = channels;
		}

		// Token: 0x04000AD8 RID: 2776
		[NonSerialized]
		public readonly Animation animation;

		// Token: 0x04000AD9 RID: 2777
		[NonSerialized]
		public readonly AnimationBlender.ResidualField residual;

		// Token: 0x04000ADA RID: 2778
		[NonSerialized]
		public readonly AnimationBlender.ChannelConfig[] channels;
	}

	// Token: 0x0200024C RID: 588
	public struct ChannelConfig
	{
		// Token: 0x060015D1 RID: 5585 RVA: 0x000518CC File Offset: 0x0004FACC
		public ChannelConfig(string name, AnimationBlender.ChannelField field)
		{
			this.name = name;
			this.field = field;
		}

		// Token: 0x04000ADB RID: 2779
		[NonSerialized]
		public readonly string name;

		// Token: 0x04000ADC RID: 2780
		[NonSerialized]
		public readonly AnimationBlender.ChannelField field;
	}

	// Token: 0x0200024D RID: 589
	[Serializable]
	public class Field
	{
		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x000518E4 File Offset: 0x0004FAE4
		public bool defined
		{
			get
			{
				return !string.IsNullOrEmpty(this.clipName);
			}
		}

		// Token: 0x04000ADD RID: 2781
		[SerializeField]
		public string clipName;

		// Token: 0x04000ADE RID: 2782
		[SerializeField]
		public float startFrame;

		// Token: 0x04000ADF RID: 2783
		[SerializeField]
		public float playbackRate;
	}

	// Token: 0x0200024E RID: 590
	[Serializable]
	public sealed class ResidualField : AnimationBlender.Field
	{
		// Token: 0x060015D4 RID: 5588 RVA: 0x000518F4 File Offset: 0x0004FAF4
		public ResidualField()
		{
			this.clipName = string.Empty;
			this.playbackRate = 1f;
			this.changeAnimLayer = false;
			this.channelBlend = (this.residualBlend = 0);
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x00051934 File Offset: 0x0004FB34
		public AnimationBlender.CurveInfo introCurveInfo
		{
			get
			{
				return new AnimationBlender.CurveInfo(this.introCurve);
			}
		}

		// Token: 0x04000AE0 RID: 2784
		[SerializeField]
		public AnimationCurve introCurve;

		// Token: 0x04000AE1 RID: 2785
		[SerializeField]
		public AnimationBlendMode residualBlend;

		// Token: 0x04000AE2 RID: 2786
		[SerializeField]
		public AnimationBlendMode channelBlend;

		// Token: 0x04000AE3 RID: 2787
		[SerializeField]
		public int animLayer;

		// Token: 0x04000AE4 RID: 2788
		[SerializeField]
		public bool changeAnimLayer;
	}

	// Token: 0x0200024F RID: 591
	[Serializable]
	public sealed class ChannelField : AnimationBlender.Field
	{
		// Token: 0x060015D6 RID: 5590 RVA: 0x00051944 File Offset: 0x0004FB44
		public ChannelField()
		{
			this.clipName = string.Empty;
			this.playbackRate = 1f;
			this.inCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
			this.outCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x000519AC File Offset: 0x0004FBAC
		public AnimationBlender.CurveInfo inCurveInfo
		{
			get
			{
				return new AnimationBlender.CurveInfo(this.inCurve);
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x000519BC File Offset: 0x0004FBBC
		public AnimationBlender.CurveInfo outCurveInfo
		{
			get
			{
				return new AnimationBlender.CurveInfo(this.outCurve);
			}
		}

		// Token: 0x04000AE5 RID: 2789
		[SerializeField]
		public float inDelay;

		// Token: 0x04000AE6 RID: 2790
		[SerializeField]
		public float outDelay;

		// Token: 0x04000AE7 RID: 2791
		[SerializeField]
		public float residualBlend;

		// Token: 0x04000AE8 RID: 2792
		[SerializeField]
		public bool blockedByAnimation;

		// Token: 0x04000AE9 RID: 2793
		[SerializeField]
		public AnimationCurve inCurve;

		// Token: 0x04000AEA RID: 2794
		[SerializeField]
		public AnimationCurve outCurve;
	}

	// Token: 0x02000250 RID: 592
	public sealed class Mixer
	{
		// Token: 0x060015D9 RID: 5593 RVA: 0x000519CC File Offset: 0x0004FBCC
		public Mixer(AnimationBlender.MixerConfig config)
		{
			if (!config.animation)
			{
				throw new ArgumentException("null or missing", "config.animation");
			}
			this.animation = config.animation;
			this.residualField = config.residual;
			this.hasResidual = (!object.ReferenceEquals(config.residual, null) && config.residual.defined && this.animation.GetClip(config.residual.clipName));
			this.oneShotBlendIn = ((!this.hasResidual || object.ReferenceEquals(config.residual.introCurve, null)) ? default(AnimationBlender.CurveInfo) : config.residual.introCurveInfo);
			this.hasOneShotBlendIn = (this.oneShotBlendIn.duration > 0f);
			this.residualState = ((!this.hasResidual) ? null : this.animation[config.residual.clipName]);
			this.channelCount = config.channels.Length;
			this.channels = new AnimationBlender.Weighted<AnimationBlender.Channel>[this.channelCount];
			this.trackers = new AnimationBlender.Weighted<AnimationBlender.Tracker>[this.channelCount];
			this.trackerCount = 0;
			this.nameToChannel = new Dictionary<string, int>(this.channelCount);
			for (int i = 0; i < this.channelCount; i++)
			{
				AnimationBlender.ChannelField field = config.channels[i].field;
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
							AnimationBlender.Weighted<AnimationBlender.Tracker>[] array = this.trackers;
							int num2 = num;
							array[num2].value.channelCount = array[num2].value.channelCount + 1;
						}
					}
					this.definedChannelCount++;
				}
				this.channels[i].value = new AnimationBlender.Channel(i, num, name, field);
			}
			for (int j = 0; j < this.trackerCount; j++)
			{
				this.trackers[j].value.channels = new int[AnimationBlender.GetClear(ref this.trackers[j].value.channelCount)];
			}
			this.definedChannels = new int[AnimationBlender.GetClear(ref this.definedChannelCount)];
			for (int k = 0; k < this.channelCount; k++)
			{
				if (this.channels[k].value.animationIndex != -1)
				{
					int[] array2 = this.trackers[this.channels[k].value.animationIndex].value.channels;
					AnimationBlender.Weighted<AnimationBlender.Tracker>[] array3 = this.trackers;
					int animationIndex = this.channels[k].value.animationIndex;
					int num3;
					array3[animationIndex].value.channelCount = (num3 = array3[animationIndex].value.channelCount) + 1;
					array2[num3] = (this.definedChannels[this.definedChannelCount++] = k);
				}
			}
			AnimationBlender.ArrayResize<AnimationBlender.Weighted<AnimationBlender.Tracker>>(ref this.trackers, this.trackerCount);
			AnimationBlender.ArrayResize<AnimationBlender.Weighted<AnimationBlender.Channel>>(ref this.channels, this.channelCount);
			AnimationBlender.ArrayResize<int>(ref this.definedChannels, this.definedChannelCount);
			for (int l = 0; l < this.trackerCount; l++)
			{
				AnimationBlender.ArrayResize<int>(ref this.trackers[l].value.channels, this.trackers[l].value.channelCount);
			}
			this.blender = new AnimationBlender.TrackerBlender(this.trackerCount);
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

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x00051F48 File Offset: 0x00050148
		public bool isPlayingManualAnimation
		{
			get
			{
				return this.ManualAnimationsPlaying(false);
			}
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00051F54 File Offset: 0x00050154
		private bool PlayQueuedDirect(string animationName, AnimationBlender.opt<QueueMode> queueMode, AnimationBlender.opt<PlayMode> playMode)
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

		// Token: 0x060015DC RID: 5596 RVA: 0x00051FBC File Offset: 0x000501BC
		private bool PlayDirect(string animationName, AnimationBlender.opt<PlayMode> playMode)
		{
			PlayMode playMode2;
			if (playMode.check(out playMode2))
			{
				return this.animation.Play(animationName, playMode2);
			}
			return this.animation.Play(animationName);
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00051FF4 File Offset: 0x000501F4
		private void CrossFadeDirect(string animationName, AnimationBlender.opt<float> fadeLength, AnimationBlender.opt<PlayMode> playMode)
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

		// Token: 0x060015DE RID: 5598 RVA: 0x00052064 File Offset: 0x00050264
		private void StopBlendingNow()
		{
			for (int i = 0; i < this.trackerCount; i++)
			{
				this.trackers[i].value.state.enabled = false;
			}
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x000520A4 File Offset: 0x000502A4
		private bool PlayQueuedOpt(string animationName, AnimationBlender.opt<QueueMode> queueMode, AnimationBlender.opt<PlayMode> playMode)
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

		// Token: 0x060015E0 RID: 5600 RVA: 0x00052138 File Offset: 0x00050338
		private bool PlayOpt(string animationName, AnimationBlender.opt<PlayMode> playMode, AnimationBlender.opt<float> speed, AnimationBlender.opt<float> startTime)
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

		// Token: 0x060015E1 RID: 5601 RVA: 0x00052204 File Offset: 0x00050404
		private bool CrossFadeOpt(string animationName, AnimationBlender.opt<float> fadeLength, AnimationBlender.opt<PlayMode> playMode, AnimationBlender.opt<float> speed, AnimationBlender.opt<float> startTime)
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

		// Token: 0x060015E2 RID: 5602 RVA: 0x0005228C File Offset: 0x0005048C
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

		// Token: 0x060015E3 RID: 5603 RVA: 0x00052308 File Offset: 0x00050508
		private void UpdateChannel(ref AnimationBlender.Weighted<AnimationBlender.Channel> channel, float dt)
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

		// Token: 0x060015E4 RID: 5604 RVA: 0x00052380 File Offset: 0x00050580
		private void UpdateTracker(ref AnimationBlender.Weighted<AnimationBlender.Tracker> tracker, float dt)
		{
			for (int i = 0; i < tracker.value.channelCount; i++)
			{
				this.UpdateChannel(ref this.channels[tracker.value.channels[i]], dt);
			}
			if (tracker.value.enabled = AnimationBlender.WeightOf<AnimationBlender.Channel>(this.channels, tracker.value.channels, out tracker.value.channelWeight))
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

		// Token: 0x060015E5 RID: 5605 RVA: 0x00052518 File Offset: 0x00050718
		private bool UpdateBlender(ref AnimationBlender.TrackerBlender blender, float dt, float externalBlend, out float residualBlend)
		{
			for (int i = 0; i < this.trackerCount; i++)
			{
				this.UpdateTracker(ref this.trackers[blender.trackers[i]], dt);
			}
			bool flag = AnimationBlender.WeightOf<AnimationBlender.Tracker>(this.trackers, blender.trackers, out blender.trackerWeight);
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

		// Token: 0x060015E6 RID: 5606 RVA: 0x0005260C File Offset: 0x0005080C
		private void DisableTracker(ref AnimationBlender.Tracker tracker)
		{
			if (!tracker.wasEnabled)
			{
				return;
			}
			tracker.state.enabled = (tracker.wasEnabled = false);
			tracker.state.weight = 0f;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x0005264C File Offset: 0x0005084C
		private float BindTracker(ref AnimationBlender.Weighted<AnimationBlender.Tracker> tracker, float externalBlend)
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

		// Token: 0x060015E8 RID: 5608 RVA: 0x0005275C File Offset: 0x0005095C
		public void SetActive(int channel, bool value)
		{
			this.channels[channel].value.active = value;
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00052778 File Offset: 0x00050978
		public void SetActive(string channel, bool value)
		{
			this.SetActive(this.nameToChannel[channel], value);
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00052790 File Offset: 0x00050990
		public void SetSolo(int channel)
		{
			for (int i = 0; i < this.channelCount; i++)
			{
				this.SetActive(i, i == channel);
			}
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x000527C0 File Offset: 0x000509C0
		public void SetSolo(string channel)
		{
			this.SetSolo(this.nameToChannel[channel]);
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x000527D4 File Offset: 0x000509D4
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

		// Token: 0x060015ED RID: 5613 RVA: 0x00052814 File Offset: 0x00050A14
		public void SetSolo(string channel, bool muteall)
		{
			this.SetSolo(this.nameToChannel[channel], muteall);
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x0005282C File Offset: 0x00050A2C
		public bool SetOneShotAnimation(AnimationState animationState)
		{
			if (animationState == null)
			{
				return false;
			}
			this.oneShotAnimation = animationState;
			return this.playingOneShot = true;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00052858 File Offset: 0x00050A58
		public bool SetOneShotAnimation(string animationName)
		{
			return !string.IsNullOrEmpty(animationName) && this.SetOneShotAnimation(this.animation[animationName]);
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00052888 File Offset: 0x00050A88
		public bool Play(string animationName)
		{
			return this.PlayOpt(animationName, AnimationBlender.opt<PlayMode>.none, AnimationBlender.opt<float>.none, AnimationBlender.opt<float>.none);
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x000528A0 File Offset: 0x00050AA0
		public bool Play(string animationName, PlayMode playMode)
		{
			return this.PlayOpt(animationName, playMode, AnimationBlender.opt<float>.none, AnimationBlender.opt<float>.none);
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x000528BC File Offset: 0x00050ABC
		public bool Play(string animationName, PlayMode playMode, float speed)
		{
			return this.PlayOpt(animationName, playMode, speed, AnimationBlender.opt<float>.none);
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x000528D8 File Offset: 0x00050AD8
		public bool Play(string animationName, PlayMode playMode, float speed, float startTime)
		{
			return this.PlayOpt(animationName, playMode, speed, startTime);
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00052900 File Offset: 0x00050B00
		public bool Play(string animationName, PlayMode playMode, float? speed, float startTime)
		{
			return this.PlayOpt(animationName, playMode, AnimationBlender.to_opt<float>(speed), startTime);
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00052928 File Offset: 0x00050B28
		public bool Play(string animationName, float speed)
		{
			return this.PlayOpt(animationName, AnimationBlender.opt<PlayMode>.none, speed, AnimationBlender.opt<float>.none);
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00052944 File Offset: 0x00050B44
		public bool Play(string animationName, float speed, float startTime)
		{
			return this.PlayOpt(animationName, AnimationBlender.opt<PlayMode>.none, speed, startTime);
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00052960 File Offset: 0x00050B60
		public bool Play(string animationName, float? speed, float startTime)
		{
			return this.PlayOpt(animationName, AnimationBlender.opt<PlayMode>.none, AnimationBlender.to_opt<float>(speed), startTime);
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x0005297C File Offset: 0x00050B7C
		public bool PlayQueued(string animationName)
		{
			return this.PlayQueuedOpt(animationName, AnimationBlender.opt<QueueMode>.none, AnimationBlender.opt<PlayMode>.none);
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00052990 File Offset: 0x00050B90
		public bool PlayQueued(string animationName, QueueMode queueMode)
		{
			return this.PlayQueuedOpt(animationName, queueMode, AnimationBlender.opt<PlayMode>.none);
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x000529A4 File Offset: 0x00050BA4
		public bool PlayQueued(string animationName, QueueMode queueMode, PlayMode playMode)
		{
			return this.PlayQueuedOpt(animationName, queueMode, playMode);
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x000529BC File Offset: 0x00050BBC
		public bool CrossFade(string animationName)
		{
			return this.CrossFadeOpt(animationName, AnimationBlender.opt<float>.none, AnimationBlender.opt<PlayMode>.none, AnimationBlender.opt<float>.none, AnimationBlender.opt<float>.none);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x000529DC File Offset: 0x00050BDC
		public bool CrossFade(string animationName, float fadeLen)
		{
			return this.CrossFadeOpt(animationName, fadeLen, AnimationBlender.opt<PlayMode>.none, AnimationBlender.opt<float>.none, AnimationBlender.opt<float>.none);
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x000529FC File Offset: 0x00050BFC
		public bool CrossFade(string animationName, float fadeLen, PlayMode playMode)
		{
			return this.CrossFadeOpt(animationName, fadeLen, playMode, AnimationBlender.opt<float>.none, AnimationBlender.opt<float>.none);
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00052A28 File Offset: 0x00050C28
		public bool CrossFade(string animationName, float fadeLen, PlayMode playMode, float speed)
		{
			return this.CrossFadeOpt(animationName, fadeLen, playMode, speed, AnimationBlender.opt<float>.none);
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00052A54 File Offset: 0x00050C54
		public bool CrossFade(string animationName, float fadeLen, PlayMode playMode, float speed, float startTime)
		{
			return this.CrossFadeOpt(animationName, fadeLen, playMode, speed, startTime);
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00052A84 File Offset: 0x00050C84
		public bool CrossFade(string animationName, float fadeLen, PlayMode playMode, float? speed, float startTime)
		{
			return this.CrossFadeOpt(animationName, fadeLen, playMode, AnimationBlender.to_opt<float>(speed), startTime);
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00052AB4 File Offset: 0x00050CB4
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

		// Token: 0x06001602 RID: 5634 RVA: 0x00052CC8 File Offset: 0x00050EC8
		public void Debug(Rect rect, string name)
		{
			AnimationBlender.Mixer.DbgGUI.TableStart(rect);
			for (int i = 0; i < this.channels.Length; i++)
			{
				if (this.channels[i].weight.any)
				{
					AnimationBlender.Mixer.DbgGUI.Label(this.channels[i].value.name);
				}
			}
			for (int j = 0; j < this.trackers.Length; j++)
			{
				if (this.trackers[j].value.enabled)
				{
					AnimationBlender.Mixer.DbgGUI.Label(this.trackers[j].value.state.name);
				}
			}
			if (this.hasResidual)
			{
				AnimationBlender.Mixer.DbgGUI.Label(this.residualState.name);
			}
			AnimationBlender.Mixer.DbgGUI.ColumnNext();
			for (int k = 0; k < this.channels.Length; k++)
			{
				if (this.channels[k].weight.any)
				{
					AnimationBlender.Mixer.DbgGUI.Fract(this.channels[k].weight.normalized);
				}
			}
			for (int l = 0; l < this.trackers.Length; l++)
			{
				if (this.trackers[l].value.enabled)
				{
					AnimationBlender.Mixer.DbgGUI.Fract(this.trackers[l].weight.normalized);
				}
			}
			if (this.hasResidual)
			{
				AnimationBlender.Mixer.DbgGUI.Fract(this.residualState.weight);
			}
			AnimationBlender.Mixer.DbgGUI.TableEnd();
		}

		// Token: 0x04000AEB RID: 2795
		[NonSerialized]
		private Animation animation;

		// Token: 0x04000AEC RID: 2796
		[NonSerialized]
		private AnimationBlender.ResidualField residualField;

		// Token: 0x04000AED RID: 2797
		[NonSerialized]
		private AnimationState residualState;

		// Token: 0x04000AEE RID: 2798
		[NonSerialized]
		private AnimationBlendMode residualBlendMode;

		// Token: 0x04000AEF RID: 2799
		[NonSerialized]
		private AnimationBlendMode channelBlendMode;

		// Token: 0x04000AF0 RID: 2800
		[NonSerialized]
		private AnimationState oneShotAnimation;

		// Token: 0x04000AF1 RID: 2801
		[NonSerialized]
		private bool playingOneShot;

		// Token: 0x04000AF2 RID: 2802
		[NonSerialized]
		private bool animationBlocking;

		// Token: 0x04000AF3 RID: 2803
		[NonSerialized]
		private int trackerCount;

		// Token: 0x04000AF4 RID: 2804
		[NonSerialized]
		private int channelCount;

		// Token: 0x04000AF5 RID: 2805
		[NonSerialized]
		private int definedChannelCount;

		// Token: 0x04000AF6 RID: 2806
		[NonSerialized]
		private int[] definedChannels;

		// Token: 0x04000AF7 RID: 2807
		[NonSerialized]
		private AnimationBlender.TrackerBlender blender;

		// Token: 0x04000AF8 RID: 2808
		[NonSerialized]
		private AnimationBlender.Weighted<AnimationBlender.Channel>[] channels;

		// Token: 0x04000AF9 RID: 2809
		[NonSerialized]
		private AnimationBlender.Weighted<AnimationBlender.Tracker>[] trackers;

		// Token: 0x04000AFA RID: 2810
		[NonSerialized]
		private Dictionary<string, int> nameToChannel;

		// Token: 0x04000AFB RID: 2811
		[NonSerialized]
		private AnimationBlender.CurveInfo oneShotBlendIn;

		// Token: 0x04000AFC RID: 2812
		[NonSerialized]
		private bool hasOneShotBlendIn;

		// Token: 0x04000AFD RID: 2813
		[NonSerialized]
		private Queue<string> queuedAnimations = new Queue<string>();

		// Token: 0x04000AFE RID: 2814
		[NonSerialized]
		private float oneShotBlendInTime;

		// Token: 0x04000AFF RID: 2815
		[NonSerialized]
		private float sumWeight;

		// Token: 0x04000B00 RID: 2816
		[NonSerialized]
		private bool hasResidual;

		// Token: 0x02000251 RID: 593
		private static class DbgGUI
		{
			// Token: 0x06001604 RID: 5636 RVA: 0x00052EB0 File Offset: 0x000510B0
			public static void Label(string str)
			{
				GUILayout.Label(str, AnimationBlender.Mixer.DbgGUI.Cell);
			}

			// Token: 0x06001605 RID: 5637 RVA: 0x00052EC0 File Offset: 0x000510C0
			public static void Fract(float frac)
			{
				GUILayout.HorizontalSlider(frac, 0f, 1f, AnimationBlender.Mixer.DbgGUI.Cell);
			}

			// Token: 0x06001606 RID: 5638 RVA: 0x00052ED8 File Offset: 0x000510D8
			public static void ColumnNext()
			{
				GUILayout.EndVertical();
				GUILayout.BeginVertical(AnimationBlender.Mixer.DbgGUI.OtherColumn);
			}

			// Token: 0x06001607 RID: 5639 RVA: 0x00052EEC File Offset: 0x000510EC
			public static void TableStart(Rect rect)
			{
				GUILayout.BeginArea(rect);
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.BeginVertical(AnimationBlender.Mixer.DbgGUI.FirstColumn);
			}

			// Token: 0x06001608 RID: 5640 RVA: 0x00052F0C File Offset: 0x0005110C
			public static void TableEnd()
			{
				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
				GUILayout.EndArea();
			}

			// Token: 0x04000B01 RID: 2817
			private static readonly GUILayoutOption[] Cell = new GUILayoutOption[]
			{
				GUILayout.Height(18f)
			};

			// Token: 0x04000B02 RID: 2818
			private static readonly GUILayoutOption[] FirstColumn = new GUILayoutOption[]
			{
				GUILayout.Width(128f)
			};

			// Token: 0x04000B03 RID: 2819
			private static readonly GUILayoutOption[] OtherColumn = new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(true)
			};
		}
	}
}
