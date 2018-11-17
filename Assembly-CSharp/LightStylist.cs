using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006D8 RID: 1752
public class LightStylist : MonoBehaviour
{
	// Token: 0x06003B50 RID: 15184 RVA: 0x000D1DE8 File Offset: 0x000CFFE8
	public void EnsureAwake()
	{
		this.Awake();
	}

	// Token: 0x17000B75 RID: 2933
	// (get) Token: 0x06003B51 RID: 15185 RVA: 0x000D1DF0 File Offset: 0x000CFFF0
	public global::LightStylist ensuredAwake
	{
		get
		{
			this.Awake();
			return this;
		}
	}

	// Token: 0x06003B52 RID: 15186 RVA: 0x000D1DFC File Offset: 0x000CFFFC
	private void Awake()
	{
		if (!this.awoke)
		{
			this.clips = new Dictionary<global::LightStyle, global::LightStylist.Clip>();
			this.awoke = true;
		}
	}

	// Token: 0x06003B53 RID: 15187 RVA: 0x000D1E1C File Offset: 0x000D001C
	private void Start()
	{
		if (!this._lightStyle)
		{
			this._lightStyle = global::LightStyleDefault.Singleton;
		}
		this.simulationIdle = this._lightStyle.CreateSimulation(global::LightStyle.time, this);
	}

	// Token: 0x06003B54 RID: 15188 RVA: 0x000D1E5C File Offset: 0x000D005C
	protected void LateUpdate()
	{
		if (this.crossfadeThisFrame)
		{
			this.crossfadeNextFrame = this.crossfadeThisFrame;
			this.crossfadeThisFrame = null;
		}
		else if (this.crossfadeNextFrame && !this.CrossFade(this.crossfadeNextFrame, this.crossfadeLength))
		{
			this.crossfadeNextFrame = this.crossfadeThisFrame;
			this.crossfadeThisFrame = null;
		}
		float num2;
		float num = this.CalculateSumWeight(true, out num2);
		global::LightStyle.Mod a;
		if (num == 0f)
		{
			while (this.clipsInSortingArray > 0)
			{
				this.sortingArray[--this.clipsInSortingArray] = null;
			}
			if (!this._lightStyle)
			{
				return;
			}
			a = this.simulationIdle.BindMod(this._mask);
		}
		else
		{
			int count = this.clips.Count;
			if (this.clipsInSortingArray != count)
			{
				if (this.clipsInSortingArray > count)
				{
					while (this.clipsInSortingArray > count)
					{
						this.sortingArray[--this.clipsInSortingArray] = null;
					}
				}
				else if (this.sortingArray == null || this.sortingArray.Length < count)
				{
					Array.Resize<global::LightStylist.Clip>(ref this.sortingArray, (count / 4 + ((count % 4 != 0) ? 1 : 2)) * 4);
				}
			}
			int num3 = 0;
			foreach (global::LightStylist.Clip clip in this.clips.Values)
			{
				if (clip.weight > 0f)
				{
					this.sortingArray[num3++] = clip;
				}
			}
			if (this.clipsInSortingArray < num3)
			{
				this.clipsInSortingArray = num3;
			}
			else
			{
				while (this.clipsInSortingArray > num3)
				{
					this.sortingArray[--this.clipsInSortingArray] = null;
				}
			}
			Array.Sort<global::LightStylist.Clip>(this.sortingArray, 0, this.clipsInSortingArray);
			float num4 = this.sortingArray[0].weight;
			a = this.sortingArray[0].simulation.BindMod(this._mask);
			for (int i = 1; i < this.clipsInSortingArray; i++)
			{
				global::LightStylist.Clip clip2 = this.sortingArray[i];
				num4 += clip2.weight;
				a = global::LightStyle.Mod.Lerp(a, clip2.simulation.BindMod(this._mask), clip2.weight / num4, this._mask);
			}
			if (this._lightStyle)
			{
				global::LightStyle.Mod b = this.simulationIdle.BindMod(this._mask);
				if (num < 1f)
				{
					a = global::LightStyle.Mod.Lerp(a, b, 1f - num, this._mask);
				}
				else
				{
					a |= b;
				}
			}
		}
		foreach (Light light in this.lights)
		{
			if (light)
			{
				a.ApplyTo(light, this._mask);
			}
		}
	}

	// Token: 0x06003B55 RID: 15189 RVA: 0x000D21B8 File Offset: 0x000D03B8
	private void CrossFadeDone()
	{
		global::LightStylist.Clip clip;
		if (this.clips.TryGetValue(this.crossfadeThisFrame, out clip))
		{
			this.clips.Remove(this.style);
			this.GetOrMakeClip(this._lightStyle).weight = 0f;
			this._lightStyle = this.style;
			this.simulationIdle = clip.simulation;
		}
		this.crossfadeThisFrame = null;
		this.crossfadeNextFrame = null;
	}

	// Token: 0x17000B76 RID: 2934
	// (get) Token: 0x06003B56 RID: 15190 RVA: 0x000D222C File Offset: 0x000D042C
	// (set) Token: 0x06003B57 RID: 15191 RVA: 0x000D2234 File Offset: 0x000D0434
	public global::LightStyle style
	{
		get
		{
			return this._lightStyle;
		}
		set
		{
			if (this._lightStyle == value)
			{
				if (value && (this.simulationIdle == null || this.simulationIdle.disposed))
				{
					this.simulationIdle = this._lightStyle.CreateSimulation(global::LightStyle.time, this);
				}
			}
			else
			{
				if (this._lightStyle)
				{
					this.simulationIdle.Dispose();
					this.simulationIdle = null;
				}
				else if (this.simulationIdle != null)
				{
					this.simulationIdle.Dispose();
					this.simulationIdle = null;
				}
				this._lightStyle = value;
				if (this._lightStyle)
				{
					this.simulationIdle = this._lightStyle.CreateSimulation(global::LightStyle.time, this);
				}
			}
		}
	}

	// Token: 0x06003B58 RID: 15192 RVA: 0x000D2308 File Offset: 0x000D0508
	protected void Reset()
	{
		this.lights = base.GetComponents<Light>();
	}

	// Token: 0x06003B59 RID: 15193 RVA: 0x000D2318 File Offset: 0x000D0518
	private global::LightStylist.Clip GetOrMakeClip(global::LightStyle style)
	{
		global::LightStylist.Clip clip;
		if (this.clips.TryGetValue(style, out clip))
		{
			return clip;
		}
		clip = new global::LightStylist.Clip();
		clip.simulation = style.CreateSimulation(global::LightStyle.time, this);
		this.clips[style] = clip;
		return clip;
	}

	// Token: 0x06003B5A RID: 15194 RVA: 0x000D2360 File Offset: 0x000D0560
	public void Play(global::LightStyle style, double time)
	{
		if (style == this._lightStyle)
		{
			this.clips.Clear();
		}
		else
		{
			global::LightStylist.Clip orMakeClip = this.GetOrMakeClip(style);
			this.clips.Clear();
			this.clips[style] = orMakeClip;
			orMakeClip.weight = 1f;
			orMakeClip.simulation.ResetTime(time);
		}
	}

	// Token: 0x06003B5B RID: 15195 RVA: 0x000D23C8 File Offset: 0x000D05C8
	public void Play(global::LightStyle style)
	{
		if (style == this._lightStyle)
		{
			this.clips.Clear();
		}
		else
		{
			global::LightStylist.Clip orMakeClip = this.GetOrMakeClip(style);
			this.clips.Clear();
			this.clips[style] = orMakeClip;
			orMakeClip.weight = 1f;
			orMakeClip.simulation.ResetTime(global::LightStyle.time);
		}
	}

	// Token: 0x06003B5C RID: 15196 RVA: 0x000D2434 File Offset: 0x000D0634
	private float CalculateSumWeight(bool normalize, out float maxWeight)
	{
		float num = 0f;
		maxWeight = 0f;
		foreach (global::LightStylist.Clip clip in this.clips.Values)
		{
			if (clip.weight > maxWeight)
			{
				maxWeight = clip.weight;
			}
			else if (clip.weight < 0f)
			{
				clip.weight = 0f;
			}
			num += clip.weight;
		}
		if (normalize && num > 1f)
		{
			float num2 = num;
			maxWeight /= num2;
			foreach (global::LightStylist.Clip clip2 in this.clips.Values)
			{
				clip2.weight /= num2;
			}
			num = 1f;
		}
		return num;
	}

	// Token: 0x06003B5D RID: 15197 RVA: 0x000D2568 File Offset: 0x000D0768
	public bool Blend(global::LightStyle style, float targetWeight, float fadeLength)
	{
		if (fadeLength <= 0f)
		{
			this.Play(style);
			return true;
		}
		targetWeight = Mathf.Clamp01(targetWeight);
		if (style == this._lightStyle)
		{
			float num2;
			float num = this.CalculateSumWeight(true, out num2);
			if (Mathf.Approximately(1f - num, targetWeight))
			{
				return true;
			}
			float num3 = Mathf.MoveTowards(num, 1f - targetWeight, Time.deltaTime / fadeLength);
			if (num3 <= 0f)
			{
				foreach (global::LightStylist.Clip clip in this.clips.Values)
				{
					clip.weight = 0f;
				}
			}
			else
			{
				float num4 = num3 / num;
				foreach (global::LightStylist.Clip clip2 in this.clips.Values)
				{
					clip2.weight *= num4;
				}
			}
		}
		else
		{
			global::LightStylist.Clip orMakeClip = this.GetOrMakeClip(style);
			if (Mathf.Approximately(orMakeClip.weight, targetWeight))
			{
				return true;
			}
			orMakeClip.weight = Mathf.MoveTowards(orMakeClip.weight, targetWeight, Time.deltaTime / fadeLength);
			float num6;
			float num5 = this.CalculateSumWeight(false, out num6);
			if (num5 != orMakeClip.weight && num5 > 1f)
			{
				float num7 = num5 - orMakeClip.weight;
				foreach (global::LightStylist.Clip clip3 in this.clips.Values)
				{
					if (clip3 != orMakeClip)
					{
						clip3.weight /= num7;
						clip3.weight *= 1f - orMakeClip.weight;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06003B5E RID: 15198 RVA: 0x000D27B8 File Offset: 0x000D09B8
	public bool Blend(global::LightStyle style, float targetWeight)
	{
		return this.Blend(style, targetWeight, 0.3f);
	}

	// Token: 0x06003B5F RID: 15199 RVA: 0x000D27C8 File Offset: 0x000D09C8
	public bool Blend(global::LightStyle style)
	{
		return this.Blend(style, 1f, 0.3f);
	}

	// Token: 0x06003B60 RID: 15200 RVA: 0x000D27DC File Offset: 0x000D09DC
	public bool CrossFade(global::LightStyle style, float fadeLength)
	{
		if (this.crossfadeThisFrame != style)
		{
			this.crossfadeThisFrame = style;
			this.crossfadeNextFrame = null;
			this.crossfadeLength = fadeLength;
			if (this.Blend(style, 1f, fadeLength))
			{
				this.CrossFadeDone();
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003B61 RID: 15201 RVA: 0x000D282C File Offset: 0x000D0A2C
	public bool CrossFade(global::LightStyle style)
	{
		return this.CrossFade(style, 0.3f);
	}

	// Token: 0x17000B77 RID: 2935
	// (get) Token: 0x06003B62 RID: 15202 RVA: 0x000D283C File Offset: 0x000D0A3C
	public IEnumerable<float> Weights
	{
		get
		{
			foreach (global::LightStylist.Clip clip in this.clips.Values)
			{
				yield return clip.weight;
			}
			yield break;
		}
	}

	// Token: 0x04001D84 RID: 7556
	private const float kDefaultFadeLength = 0.3f;

	// Token: 0x04001D85 RID: 7557
	[SerializeField]
	protected Light[] lights;

	// Token: 0x04001D86 RID: 7558
	[SerializeField]
	protected global::LightStyle _lightStyle;

	// Token: 0x04001D87 RID: 7559
	private global::LightStyle crossfadeThisFrame;

	// Token: 0x04001D88 RID: 7560
	private global::LightStyle crossfadeNextFrame;

	// Token: 0x04001D89 RID: 7561
	private float crossfadeLength;

	// Token: 0x04001D8A RID: 7562
	protected global::LightStyle.Simulation simulationIdle;

	// Token: 0x04001D8B RID: 7563
	protected global::LightStyle.Simulation simulationPlaying;

	// Token: 0x04001D8C RID: 7564
	[HideInInspector]
	[SerializeField]
	protected global::LightStyle.Mod.Mask _mask = global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha | global::LightStyle.Mod.Mask.Intensity | global::LightStyle.Mod.Mask.Range | global::LightStyle.Mod.Mask.SpotAngle;

	// Token: 0x04001D8D RID: 7565
	private Dictionary<global::LightStyle, global::LightStylist.Clip> clips;

	// Token: 0x04001D8E RID: 7566
	private global::LightStylist.Clip[] sortingArray;

	// Token: 0x04001D8F RID: 7567
	private int clipsInSortingArray;

	// Token: 0x04001D90 RID: 7568
	private bool awoke;

	// Token: 0x020006D9 RID: 1753
	protected sealed class Clip : IComparable<global::LightStylist.Clip>
	{
		// Token: 0x06003B64 RID: 15204 RVA: 0x000D2868 File Offset: 0x000D0A68
		public int CompareTo(global::LightStylist.Clip other)
		{
			return this.weight.CompareTo(other.weight);
		}

		// Token: 0x04001D91 RID: 7569
		public float weight;

		// Token: 0x04001D92 RID: 7570
		public global::LightStyle.Simulation simulation;
	}
}
