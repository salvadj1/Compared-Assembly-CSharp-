using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000618 RID: 1560
public class LightStylist : MonoBehaviour
{
	// Token: 0x06003778 RID: 14200 RVA: 0x000C98B8 File Offset: 0x000C7AB8
	public void EnsureAwake()
	{
		this.Awake();
	}

	// Token: 0x17000AFB RID: 2811
	// (get) Token: 0x06003779 RID: 14201 RVA: 0x000C98C0 File Offset: 0x000C7AC0
	public LightStylist ensuredAwake
	{
		get
		{
			this.Awake();
			return this;
		}
	}

	// Token: 0x0600377A RID: 14202 RVA: 0x000C98CC File Offset: 0x000C7ACC
	private void Awake()
	{
		if (!this.awoke)
		{
			this.clips = new Dictionary<LightStyle, LightStylist.Clip>();
			this.awoke = true;
		}
	}

	// Token: 0x0600377B RID: 14203 RVA: 0x000C98EC File Offset: 0x000C7AEC
	private void Start()
	{
		if (!this._lightStyle)
		{
			this._lightStyle = LightStyleDefault.Singleton;
		}
		this.simulationIdle = this._lightStyle.CreateSimulation(LightStyle.time, this);
	}

	// Token: 0x0600377C RID: 14204 RVA: 0x000C992C File Offset: 0x000C7B2C
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
		LightStyle.Mod a;
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
					Array.Resize<LightStylist.Clip>(ref this.sortingArray, (count / 4 + ((count % 4 != 0) ? 1 : 2)) * 4);
				}
			}
			int num3 = 0;
			foreach (LightStylist.Clip clip in this.clips.Values)
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
			Array.Sort<LightStylist.Clip>(this.sortingArray, 0, this.clipsInSortingArray);
			float num4 = this.sortingArray[0].weight;
			a = this.sortingArray[0].simulation.BindMod(this._mask);
			for (int i = 1; i < this.clipsInSortingArray; i++)
			{
				LightStylist.Clip clip2 = this.sortingArray[i];
				num4 += clip2.weight;
				a = LightStyle.Mod.Lerp(a, clip2.simulation.BindMod(this._mask), clip2.weight / num4, this._mask);
			}
			if (this._lightStyle)
			{
				LightStyle.Mod b = this.simulationIdle.BindMod(this._mask);
				if (num < 1f)
				{
					a = LightStyle.Mod.Lerp(a, b, 1f - num, this._mask);
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

	// Token: 0x0600377D RID: 14205 RVA: 0x000C9C88 File Offset: 0x000C7E88
	private void CrossFadeDone()
	{
		LightStylist.Clip clip;
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

	// Token: 0x17000AFC RID: 2812
	// (get) Token: 0x0600377E RID: 14206 RVA: 0x000C9CFC File Offset: 0x000C7EFC
	// (set) Token: 0x0600377F RID: 14207 RVA: 0x000C9D04 File Offset: 0x000C7F04
	public LightStyle style
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
					this.simulationIdle = this._lightStyle.CreateSimulation(LightStyle.time, this);
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
					this.simulationIdle = this._lightStyle.CreateSimulation(LightStyle.time, this);
				}
			}
		}
	}

	// Token: 0x06003780 RID: 14208 RVA: 0x000C9DD8 File Offset: 0x000C7FD8
	protected void Reset()
	{
		this.lights = base.GetComponents<Light>();
	}

	// Token: 0x06003781 RID: 14209 RVA: 0x000C9DE8 File Offset: 0x000C7FE8
	private LightStylist.Clip GetOrMakeClip(LightStyle style)
	{
		LightStylist.Clip clip;
		if (this.clips.TryGetValue(style, out clip))
		{
			return clip;
		}
		clip = new LightStylist.Clip();
		clip.simulation = style.CreateSimulation(LightStyle.time, this);
		this.clips[style] = clip;
		return clip;
	}

	// Token: 0x06003782 RID: 14210 RVA: 0x000C9E30 File Offset: 0x000C8030
	public void Play(LightStyle style, double time)
	{
		if (style == this._lightStyle)
		{
			this.clips.Clear();
		}
		else
		{
			LightStylist.Clip orMakeClip = this.GetOrMakeClip(style);
			this.clips.Clear();
			this.clips[style] = orMakeClip;
			orMakeClip.weight = 1f;
			orMakeClip.simulation.ResetTime(time);
		}
	}

	// Token: 0x06003783 RID: 14211 RVA: 0x000C9E98 File Offset: 0x000C8098
	public void Play(LightStyle style)
	{
		if (style == this._lightStyle)
		{
			this.clips.Clear();
		}
		else
		{
			LightStylist.Clip orMakeClip = this.GetOrMakeClip(style);
			this.clips.Clear();
			this.clips[style] = orMakeClip;
			orMakeClip.weight = 1f;
			orMakeClip.simulation.ResetTime(LightStyle.time);
		}
	}

	// Token: 0x06003784 RID: 14212 RVA: 0x000C9F04 File Offset: 0x000C8104
	private float CalculateSumWeight(bool normalize, out float maxWeight)
	{
		float num = 0f;
		maxWeight = 0f;
		foreach (LightStylist.Clip clip in this.clips.Values)
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
			foreach (LightStylist.Clip clip2 in this.clips.Values)
			{
				clip2.weight /= num2;
			}
			num = 1f;
		}
		return num;
	}

	// Token: 0x06003785 RID: 14213 RVA: 0x000CA038 File Offset: 0x000C8238
	public bool Blend(LightStyle style, float targetWeight, float fadeLength)
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
				foreach (LightStylist.Clip clip in this.clips.Values)
				{
					clip.weight = 0f;
				}
			}
			else
			{
				float num4 = num3 / num;
				foreach (LightStylist.Clip clip2 in this.clips.Values)
				{
					clip2.weight *= num4;
				}
			}
		}
		else
		{
			LightStylist.Clip orMakeClip = this.GetOrMakeClip(style);
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
				foreach (LightStylist.Clip clip3 in this.clips.Values)
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

	// Token: 0x06003786 RID: 14214 RVA: 0x000CA288 File Offset: 0x000C8488
	public bool Blend(LightStyle style, float targetWeight)
	{
		return this.Blend(style, targetWeight, 0.3f);
	}

	// Token: 0x06003787 RID: 14215 RVA: 0x000CA298 File Offset: 0x000C8498
	public bool Blend(LightStyle style)
	{
		return this.Blend(style, 1f, 0.3f);
	}

	// Token: 0x06003788 RID: 14216 RVA: 0x000CA2AC File Offset: 0x000C84AC
	public bool CrossFade(LightStyle style, float fadeLength)
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

	// Token: 0x06003789 RID: 14217 RVA: 0x000CA2FC File Offset: 0x000C84FC
	public bool CrossFade(LightStyle style)
	{
		return this.CrossFade(style, 0.3f);
	}

	// Token: 0x17000AFD RID: 2813
	// (get) Token: 0x0600378A RID: 14218 RVA: 0x000CA30C File Offset: 0x000C850C
	public IEnumerable<float> Weights
	{
		get
		{
			foreach (LightStylist.Clip clip in this.clips.Values)
			{
				yield return clip.weight;
			}
			yield break;
		}
	}

	// Token: 0x04001B9E RID: 7070
	private const float kDefaultFadeLength = 0.3f;

	// Token: 0x04001B9F RID: 7071
	[SerializeField]
	protected Light[] lights;

	// Token: 0x04001BA0 RID: 7072
	[SerializeField]
	protected LightStyle _lightStyle;

	// Token: 0x04001BA1 RID: 7073
	private LightStyle crossfadeThisFrame;

	// Token: 0x04001BA2 RID: 7074
	private LightStyle crossfadeNextFrame;

	// Token: 0x04001BA3 RID: 7075
	private float crossfadeLength;

	// Token: 0x04001BA4 RID: 7076
	protected LightStyle.Simulation simulationIdle;

	// Token: 0x04001BA5 RID: 7077
	protected LightStyle.Simulation simulationPlaying;

	// Token: 0x04001BA6 RID: 7078
	[SerializeField]
	[HideInInspector]
	protected LightStyle.Mod.Mask _mask = LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha | LightStyle.Mod.Mask.Intensity | LightStyle.Mod.Mask.Range | LightStyle.Mod.Mask.SpotAngle;

	// Token: 0x04001BA7 RID: 7079
	private Dictionary<LightStyle, LightStylist.Clip> clips;

	// Token: 0x04001BA8 RID: 7080
	private LightStylist.Clip[] sortingArray;

	// Token: 0x04001BA9 RID: 7081
	private int clipsInSortingArray;

	// Token: 0x04001BAA RID: 7082
	private bool awoke;

	// Token: 0x02000619 RID: 1561
	protected sealed class Clip : IComparable<LightStylist.Clip>
	{
		// Token: 0x0600378C RID: 14220 RVA: 0x000CA338 File Offset: 0x000C8538
		public int CompareTo(LightStylist.Clip other)
		{
			return this.weight.CompareTo(other.weight);
		}

		// Token: 0x04001BAB RID: 7083
		public float weight;

		// Token: 0x04001BAC RID: 7084
		public LightStyle.Simulation simulation;
	}
}
