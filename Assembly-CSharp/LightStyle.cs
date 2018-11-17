using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x020006CD RID: 1741
public abstract class LightStyle : ScriptableObject
{
	// Token: 0x17000B6F RID: 2927
	// (get) Token: 0x06003AF5 RID: 15093 RVA: 0x000D0920 File Offset: 0x000CEB20
	public static double time
	{
		get
		{
			if (global::NetCull.isRunning)
			{
				return global::NetCull.time;
			}
			return (double)Time.time;
		}
	}

	// Token: 0x06003AF6 RID: 15094
	protected abstract global::LightStyle.Simulation ConstructSimulation(global::LightStylist stylist);

	// Token: 0x06003AF7 RID: 15095
	protected abstract bool DeconstructSimulation(global::LightStyle.Simulation simulation);

	// Token: 0x06003AF8 RID: 15096 RVA: 0x000D0938 File Offset: 0x000CEB38
	public global::LightStyle.Simulation CreateSimulation(global::LightStylist stylist)
	{
		return this.CreateSimulation(global::LightStyle.time, stylist);
	}

	// Token: 0x06003AF9 RID: 15097 RVA: 0x000D0948 File Offset: 0x000CEB48
	public global::LightStyle.Simulation CreateSimulation(double startTime, global::LightStylist stylist)
	{
		global::LightStyle.Simulation simulation = this.ConstructSimulation(stylist);
		if (simulation != null)
		{
			simulation.ResetTime(startTime);
		}
		return simulation;
	}

	// Token: 0x06003AFA RID: 15098 RVA: 0x000D096C File Offset: 0x000CEB6C
	private static global::LightStyle MissingLightStyle(string name)
	{
		return global::LightStyleDefault.Singleton;
	}

	// Token: 0x06003AFB RID: 15099 RVA: 0x000D0974 File Offset: 0x000CEB74
	public static implicit operator global::LightStyle(string name)
	{
		if (!global::LightStyle.madeLoadedByString)
		{
			global::LightStyle lightStyle = (global::LightStyle)UnityEngine.Resources.Load(name, typeof(global::LightStyle));
			if (lightStyle)
			{
				global::LightStyle.loadedByString = new Dictionary<string, WeakReference>(StringComparer.InvariantCultureIgnoreCase);
				global::LightStyle.loadedByString[name] = new WeakReference(lightStyle);
			}
			else
			{
				lightStyle = global::LightStyle.MissingLightStyle(name);
			}
			return lightStyle;
		}
		WeakReference weakReference;
		if (!global::LightStyle.loadedByString.TryGetValue(name, out weakReference))
		{
			global::LightStyle lightStyle2 = (global::LightStyle)UnityEngine.Resources.Load(name, typeof(global::LightStyle));
			if (lightStyle2)
			{
				weakReference = new WeakReference(lightStyle2);
				global::LightStyle.loadedByString[name] = weakReference;
			}
			else
			{
				lightStyle2 = global::LightStyle.MissingLightStyle(name);
			}
			return lightStyle2;
		}
		object target = weakReference.Target;
		if (weakReference.IsAlive && (Object)target)
		{
			return (global::LightStyle)target;
		}
		global::LightStyle lightStyle3 = (global::LightStyle)UnityEngine.Resources.Load(name, typeof(global::LightStyle));
		if (lightStyle3)
		{
			weakReference.Target = lightStyle3;
		}
		else
		{
			lightStyle3 = global::LightStyle.MissingLightStyle(name);
		}
		return lightStyle3;
	}

	// Token: 0x06003AFC RID: 15100 RVA: 0x000D0A94 File Offset: 0x000CEC94
	public static implicit operator string(global::LightStyle lightStyle)
	{
		return (!lightStyle) ? null : lightStyle.name;
	}

	// Token: 0x04001D4B RID: 7499
	private static Dictionary<string, WeakReference> loadedByString;

	// Token: 0x04001D4C RID: 7500
	private static bool madeLoadedByString;

	// Token: 0x020006CE RID: 1742
	[StructLayout(LayoutKind.Explicit)]
	public struct Mod
	{
		// Token: 0x06003AFD RID: 15101 RVA: 0x000D0AB0 File Offset: 0x000CECB0
		public bool AnyOf(global::LightStyle.Mod.Mask mask)
		{
			return (this.mask & mask) != (global::LightStyle.Mod.Mask)0;
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x000D0AC0 File Offset: 0x000CECC0
		public bool AllOf(global::LightStyle.Mod.Mask mask)
		{
			return (this.mask & mask) == mask;
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x000D0AD0 File Offset: 0x000CECD0
		public bool Contains(global::LightStyle.Mod.Element element)
		{
			return this.AllOf(global::LightStyle.Mod.ElementToMask(element));
		}

		// Token: 0x06003B00 RID: 15104 RVA: 0x000D0AE0 File Offset: 0x000CECE0
		public void SetModify(global::LightStyle.Mod.Element element)
		{
			this.mask |= global::LightStyle.Mod.ElementToMask(element);
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x000D0AF8 File Offset: 0x000CECF8
		public void SetModify(global::LightStyle.Mod.Element element, float assignValue)
		{
			this.SetFaceValue(element, assignValue);
			this.mask |= global::LightStyle.Mod.ElementToMask(element);
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x000D0B18 File Offset: 0x000CED18
		public void ClearModify(global::LightStyle.Mod.Element element)
		{
			this.mask &= global::LightStyle.Mod.ElementToMaskNot(element);
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x000D0B30 File Offset: 0x000CED30
		public void ToggleModify(global::LightStyle.Mod.Element element)
		{
			this.mask ^= global::LightStyle.Mod.ElementToMask(element);
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x000D0B48 File Offset: 0x000CED48
		public float GetFaceValue(global::LightStyle.Mod.Element element)
		{
			switch (element)
			{
			case global::LightStyle.Mod.Element.Red:
				return this.r;
			case global::LightStyle.Mod.Element.Green:
				return this.g;
			case global::LightStyle.Mod.Element.Blue:
				return this.b;
			case global::LightStyle.Mod.Element.Alpha:
				return this.a;
			case global::LightStyle.Mod.Element.Intensity:
				return this.intensity;
			case global::LightStyle.Mod.Element.Range:
				return this.range;
			case global::LightStyle.Mod.Element.SpotAngle:
				return this.spotAngle;
			default:
				throw new ArgumentOutOfRangeException("element");
			}
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x000D0BBC File Offset: 0x000CEDBC
		public void SetFaceValue(global::LightStyle.Mod.Element element, float value)
		{
			switch (element)
			{
			case global::LightStyle.Mod.Element.Red:
				this.r = value;
				break;
			case global::LightStyle.Mod.Element.Green:
				this.g = value;
				break;
			case global::LightStyle.Mod.Element.Blue:
				this.b = value;
				break;
			case global::LightStyle.Mod.Element.Alpha:
				this.a = value;
				break;
			case global::LightStyle.Mod.Element.Intensity:
				this.intensity = value;
				break;
			case global::LightStyle.Mod.Element.Range:
				this.range = value;
				break;
			case global::LightStyle.Mod.Element.SpotAngle:
				this.spotAngle = value;
				break;
			default:
				throw new ArgumentOutOfRangeException("element");
			}
		}

		// Token: 0x17000B70 RID: 2928
		public float? this[global::LightStyle.Mod.Element element]
		{
			get
			{
				if (this.Contains(element))
				{
					return new float?(this.GetFaceValue(element));
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.SetFaceValue(element, value.Value);
					this.SetModify(element);
				}
				else
				{
					this.ClearModify(element);
				}
			}
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x000D0CC0 File Offset: 0x000CEEC0
		public static global::LightStyle.Mod.Mask ElementToMask(global::LightStyle.Mod.Element element)
		{
			return (global::LightStyle.Mod.Mask)(1 << (int)element & 127);
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x000D0CCC File Offset: 0x000CEECC
		public static global::LightStyle.Mod.Mask ElementToMaskNot(global::LightStyle.Mod.Element element)
		{
			return (global::LightStyle.Mod.Mask)(~(1 << (int)element) & 127);
		}

		// Token: 0x06003B0A RID: 15114 RVA: 0x000D0CD8 File Offset: 0x000CEED8
		public void ApplyTo(Light light)
		{
			switch (light.type)
			{
			case 0:
				this.ApplyTo(light, global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha | global::LightStyle.Mod.Mask.Intensity | global::LightStyle.Mod.Mask.Range | global::LightStyle.Mod.Mask.SpotAngle);
				break;
			case 1:
				this.ApplyTo(light, global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha | global::LightStyle.Mod.Mask.Intensity);
				break;
			case 2:
				this.ApplyTo(light, global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha | global::LightStyle.Mod.Mask.Intensity | global::LightStyle.Mod.Mask.Range);
				break;
			}
		}

		// Token: 0x06003B0B RID: 15115 RVA: 0x000D0D30 File Offset: 0x000CEF30
		public void ApplyTo(Light light, global::LightStyle.Mod.Mask applyMask)
		{
			global::LightStyle.Mod.Mask mask = this.mask & applyMask;
			if ((mask & (global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha)) != (global::LightStyle.Mod.Mask)0)
			{
				if ((mask & (global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha)) == (global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha))
				{
					light.color = this.color;
				}
				else
				{
					Color color = light.color;
					if ((mask & global::LightStyle.Mod.Mask.Red) == global::LightStyle.Mod.Mask.Red)
					{
						color.r = this.r;
					}
					if ((mask & global::LightStyle.Mod.Mask.Green) == global::LightStyle.Mod.Mask.Green)
					{
						color.g = this.g;
					}
					if ((mask & global::LightStyle.Mod.Mask.Blue) == global::LightStyle.Mod.Mask.Blue)
					{
						color.b = this.b;
					}
					if ((mask & global::LightStyle.Mod.Mask.Alpha) == global::LightStyle.Mod.Mask.Alpha)
					{
						color.a = this.a;
					}
					light.color = color;
				}
			}
			if ((mask & global::LightStyle.Mod.Mask.Intensity) == global::LightStyle.Mod.Mask.Intensity)
			{
				light.intensity = this.intensity;
			}
			if ((mask & global::LightStyle.Mod.Mask.Range) == global::LightStyle.Mod.Mask.Range)
			{
				light.range = this.range;
			}
			if ((mask & global::LightStyle.Mod.Mask.SpotAngle) == global::LightStyle.Mod.Mask.SpotAngle)
			{
				light.spotAngle = this.spotAngle;
			}
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x000D0E18 File Offset: 0x000CF018
		public static global::LightStyle.Mod Lerp(global::LightStyle.Mod a, global::LightStyle.Mod b, float t, global::LightStyle.Mod.Mask mask)
		{
			b.mask &= mask;
			if (b.mask == (global::LightStyle.Mod.Mask)0)
			{
				return a;
			}
			a.mask &= mask;
			if (a.mask == (global::LightStyle.Mod.Mask)0)
			{
				return b;
			}
			global::LightStyle.Mod.Mask mask2 = a.mask & b.mask;
			if (mask2 != (global::LightStyle.Mod.Mask)0)
			{
				float num = 1f - t;
				if (mask != (global::LightStyle.Mod.Mask)0)
				{
					for (global::LightStyle.Mod.Element element = global::LightStyle.Mod.Element.Red; element < (global::LightStyle.Mod.Element)7; element++)
					{
						if ((mask2 & global::LightStyle.Mod.ElementToMask(element)) == global::LightStyle.Mod.ElementToMask(element))
						{
							float faceValue = a.GetFaceValue(element);
							float faceValue2 = b.GetFaceValue(element);
							float value = faceValue * num + faceValue2 * t;
							a.SetFaceValue(element, value);
						}
					}
				}
			}
			if (mask2 != a.mask)
			{
				a |= b;
			}
			return a;
		}

		// Token: 0x06003B0D RID: 15117 RVA: 0x000D0EE8 File Offset: 0x000CF0E8
		public static global::LightStyle.Mod operator +(global::LightStyle.Mod a, global::LightStyle.Mod b)
		{
			global::LightStyle.Mod result = a;
			global::LightStyle.Mod.Mask mask = a.mask & b.mask;
			for (global::LightStyle.Mod.Element element = global::LightStyle.Mod.Element.Red; element < (global::LightStyle.Mod.Element)7; element++)
			{
				if ((mask & global::LightStyle.Mod.ElementToMask(element)) == global::LightStyle.Mod.ElementToMask(element))
				{
					result.SetFaceValue(element, a.GetFaceValue(element) + b.GetFaceValue(element));
				}
			}
			return result;
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x000D0F48 File Offset: 0x000CF148
		public static global::LightStyle.Mod operator -(global::LightStyle.Mod a, global::LightStyle.Mod b)
		{
			global::LightStyle.Mod.Mask mask = a.mask & b.mask;
			for (global::LightStyle.Mod.Element element = global::LightStyle.Mod.Element.Red; element < (global::LightStyle.Mod.Element)7; element++)
			{
				if ((mask & global::LightStyle.Mod.ElementToMask(element)) == global::LightStyle.Mod.ElementToMask(element))
				{
					a.SetFaceValue(element, a.GetFaceValue(element) - b.GetFaceValue(element));
				}
			}
			return a;
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x000D0FA4 File Offset: 0x000CF1A4
		public static global::LightStyle.Mod operator *(global::LightStyle.Mod a, global::LightStyle.Mod b)
		{
			global::LightStyle.Mod.Mask mask = a.mask & b.mask;
			for (global::LightStyle.Mod.Element element = global::LightStyle.Mod.Element.Red; element < (global::LightStyle.Mod.Element)7; element++)
			{
				if ((mask & global::LightStyle.Mod.ElementToMask(element)) == global::LightStyle.Mod.ElementToMask(element))
				{
					a.SetFaceValue(element, a.GetFaceValue(element) * b.GetFaceValue(element));
				}
			}
			return a;
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x000D1000 File Offset: 0x000CF200
		public static global::LightStyle.Mod operator /(global::LightStyle.Mod a, global::LightStyle.Mod b)
		{
			global::LightStyle.Mod.Mask mask = a.mask & b.mask;
			for (global::LightStyle.Mod.Element element = global::LightStyle.Mod.Element.Red; element < (global::LightStyle.Mod.Element)7; element++)
			{
				if ((mask & global::LightStyle.Mod.ElementToMask(element)) == global::LightStyle.Mod.ElementToMask(element))
				{
					a.SetFaceValue(element, a.GetFaceValue(element) / b.GetFaceValue(element));
				}
			}
			return a;
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x000D105C File Offset: 0x000CF25C
		public static global::LightStyle.Mod operator |(global::LightStyle.Mod a, global::LightStyle.Mod b)
		{
			for (global::LightStyle.Mod.Element element = global::LightStyle.Mod.Element.Red; element < (global::LightStyle.Mod.Element)7; element++)
			{
				if ((a.mask & global::LightStyle.Mod.ElementToMask(element)) != global::LightStyle.Mod.ElementToMask(element) && (b.mask & global::LightStyle.Mod.ElementToMask(element)) == global::LightStyle.Mod.ElementToMask(element))
				{
					a.SetModify(element, b.GetFaceValue(element));
				}
			}
			return a;
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x000D10C0 File Offset: 0x000CF2C0
		public static global::LightStyle.Mod operator &(global::LightStyle.Mod a, global::LightStyle.Mod b)
		{
			for (global::LightStyle.Mod.Element element = global::LightStyle.Mod.Element.Red; element < (global::LightStyle.Mod.Element)7; element++)
			{
				if ((a.mask & global::LightStyle.Mod.ElementToMask(element)) == global::LightStyle.Mod.ElementToMask(element) && (b.mask & global::LightStyle.Mod.ElementToMask(element)) == global::LightStyle.Mod.ElementToMask(element))
				{
					a.SetFaceValue(element, b.GetFaceValue(element));
				}
			}
			return a;
		}

		// Token: 0x06003B13 RID: 15123 RVA: 0x000D1124 File Offset: 0x000CF324
		public static global::LightStyle.Mod operator ^(global::LightStyle.Mod a, global::LightStyle.Mod b)
		{
			for (global::LightStyle.Mod.Element element = global::LightStyle.Mod.Element.Red; element < (global::LightStyle.Mod.Element)7; element++)
			{
				if ((a.mask & global::LightStyle.Mod.ElementToMask(element)) == global::LightStyle.Mod.ElementToMask(element))
				{
					if ((b.mask & global::LightStyle.Mod.ElementToMask(element)) == global::LightStyle.Mod.ElementToMask(element))
					{
						a.SetFaceValue(element, b.GetFaceValue(element));
					}
				}
				else if ((b.mask & global::LightStyle.Mod.ElementToMask(element)) == global::LightStyle.Mod.ElementToMask(element))
				{
					a.SetModify(element, b.GetFaceValue(element));
				}
			}
			return a;
		}

		// Token: 0x06003B14 RID: 15124 RVA: 0x000D11C0 File Offset: 0x000CF3C0
		public static global::LightStyle.Mod operator |(global::LightStyle.Mod a, global::LightStyle.Mod.Mask b)
		{
			a.mask |= b;
			return a;
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x000D11D4 File Offset: 0x000CF3D4
		public static global::LightStyle.Mod operator &(global::LightStyle.Mod a, global::LightStyle.Mod.Mask b)
		{
			a.mask &= b;
			return a;
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x000D11E8 File Offset: 0x000CF3E8
		public static global::LightStyle.Mod operator ^(global::LightStyle.Mod a, global::LightStyle.Mod.Mask b)
		{
			a.mask ^= b;
			return a;
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x000D11FC File Offset: 0x000CF3FC
		public static global::LightStyle.Mod operator +(global::LightStyle.Mod a, global::LightStyle.Mod.Element b)
		{
			a.mask |= global::LightStyle.Mod.ElementToMask(b);
			return a;
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x000D1214 File Offset: 0x000CF414
		public static global::LightStyle.Mod operator -(global::LightStyle.Mod a, global::LightStyle.Mod.Element b)
		{
			a.mask &= global::LightStyle.Mod.ElementToMaskNot(b);
			return a;
		}

		// Token: 0x06003B19 RID: 15129 RVA: 0x000D122C File Offset: 0x000CF42C
		public static float?operator |(global::LightStyle.Mod a, global::LightStyle.Mod.Element b)
		{
			return a[b];
		}

		// Token: 0x06003B1A RID: 15130 RVA: 0x000D1238 File Offset: 0x000CF438
		public static bool operator &(global::LightStyle.Mod a, global::LightStyle.Mod.Element b)
		{
			return a.Contains(b);
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x000D1244 File Offset: 0x000CF444
		public static float operator ^(global::LightStyle.Mod a, global::LightStyle.Mod.Element b)
		{
			return a.GetFaceValue(b);
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x000D1250 File Offset: 0x000CF450
		public static global::LightStyle.Mod operator +(global::LightStyle.Mod a, float b)
		{
			for (global::LightStyle.Mod.Element element = global::LightStyle.Mod.Element.Red; element < (global::LightStyle.Mod.Element)7; element++)
			{
				if ((a.mask & global::LightStyle.Mod.ElementToMask(element)) == global::LightStyle.Mod.ElementToMask(element))
				{
					a.SetFaceValue(element, a.GetFaceValue(element) + b);
				}
			}
			return a;
		}

		// Token: 0x06003B1D RID: 15133 RVA: 0x000D129C File Offset: 0x000CF49C
		public static global::LightStyle.Mod operator -(global::LightStyle.Mod a, float b)
		{
			for (global::LightStyle.Mod.Element element = global::LightStyle.Mod.Element.Red; element < (global::LightStyle.Mod.Element)7; element++)
			{
				if ((a.mask & global::LightStyle.Mod.ElementToMask(element)) == global::LightStyle.Mod.ElementToMask(element))
				{
					a.SetFaceValue(element, a.GetFaceValue(element) - b);
				}
			}
			return a;
		}

		// Token: 0x06003B1E RID: 15134 RVA: 0x000D12E8 File Offset: 0x000CF4E8
		public static global::LightStyle.Mod operator *(global::LightStyle.Mod a, float b)
		{
			global::LightStyle.Mod result = a;
			for (global::LightStyle.Mod.Element element = global::LightStyle.Mod.Element.Red; element < (global::LightStyle.Mod.Element)7; element++)
			{
				if ((a.mask & global::LightStyle.Mod.ElementToMask(element)) == global::LightStyle.Mod.ElementToMask(element))
				{
					result.SetFaceValue(element, a.GetFaceValue(element) * b);
				}
			}
			return result;
		}

		// Token: 0x06003B1F RID: 15135 RVA: 0x000D1338 File Offset: 0x000CF538
		public static global::LightStyle.Mod operator /(global::LightStyle.Mod a, float b)
		{
			for (global::LightStyle.Mod.Element element = global::LightStyle.Mod.Element.Red; element < (global::LightStyle.Mod.Element)7; element++)
			{
				if ((a.mask & global::LightStyle.Mod.ElementToMask(element)) == global::LightStyle.Mod.ElementToMask(element))
				{
					a.SetFaceValue(element, a.GetFaceValue(element) / b);
				}
			}
			return a;
		}

		// Token: 0x06003B20 RID: 15136 RVA: 0x000D1384 File Offset: 0x000CF584
		public static global::LightStyle.Mod operator +(global::LightStyle.Mod a, Color b)
		{
			for (int i = 0; i < 4; i++)
			{
				if ((a.mask & global::LightStyle.Mod.ElementToMask(global::LightStyle.Mod.Element.Red + i)) == global::LightStyle.Mod.ElementToMask(global::LightStyle.Mod.Element.Red + i))
				{
					a.SetFaceValue(global::LightStyle.Mod.Element.Red + i, a.GetFaceValue(global::LightStyle.Mod.Element.Red + i) + b[i]);
				}
			}
			return a;
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x000D13E0 File Offset: 0x000CF5E0
		public static global::LightStyle.Mod operator -(global::LightStyle.Mod a, Color b)
		{
			for (int i = 0; i < 4; i++)
			{
				if ((a.mask & global::LightStyle.Mod.ElementToMask(global::LightStyle.Mod.Element.Red + i)) == global::LightStyle.Mod.ElementToMask(global::LightStyle.Mod.Element.Red + i))
				{
					a.SetFaceValue(global::LightStyle.Mod.Element.Red + i, a.GetFaceValue(global::LightStyle.Mod.Element.Red + i) - b[i]);
				}
			}
			return a;
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x000D143C File Offset: 0x000CF63C
		public static global::LightStyle.Mod operator *(global::LightStyle.Mod a, Color b)
		{
			for (int i = 0; i < 4; i++)
			{
				if ((a.mask & global::LightStyle.Mod.ElementToMask(global::LightStyle.Mod.Element.Red + i)) == global::LightStyle.Mod.ElementToMask(global::LightStyle.Mod.Element.Red + i))
				{
					a.SetFaceValue(global::LightStyle.Mod.Element.Red + i, a.GetFaceValue(global::LightStyle.Mod.Element.Red + i) * b[i]);
				}
			}
			return a;
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x000D1498 File Offset: 0x000CF698
		public static global::LightStyle.Mod operator /(global::LightStyle.Mod a, Color b)
		{
			for (int i = 0; i < 4; i++)
			{
				if ((a.mask & global::LightStyle.Mod.ElementToMask(global::LightStyle.Mod.Element.Red + i)) == global::LightStyle.Mod.ElementToMask(global::LightStyle.Mod.Element.Red + i))
				{
					a.SetFaceValue(global::LightStyle.Mod.Element.Red + i, a.GetFaceValue(global::LightStyle.Mod.Element.Red + i) / b[i]);
				}
			}
			return a;
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x000D14F4 File Offset: 0x000CF6F4
		public static Color operator +(Color b, global::LightStyle.Mod a)
		{
			for (int i = 0; i < 4; i++)
			{
				if ((a.mask & global::LightStyle.Mod.ElementToMask(global::LightStyle.Mod.Element.Red + i)) == global::LightStyle.Mod.ElementToMask(global::LightStyle.Mod.Element.Red + i))
				{
					ref Color ptr = ref b;
					int num2;
					int num = num2 = i;
					float num3 = ptr[num2];
					b[num] = num3 + a.GetFaceValue(global::LightStyle.Mod.Element.Red + i);
				}
			}
			return b;
		}

		// Token: 0x06003B25 RID: 15141 RVA: 0x000D1554 File Offset: 0x000CF754
		public static Color operator -(Color b, global::LightStyle.Mod a)
		{
			for (int i = 0; i < 4; i++)
			{
				if ((a.mask & global::LightStyle.Mod.ElementToMask(global::LightStyle.Mod.Element.Red + i)) == global::LightStyle.Mod.ElementToMask(global::LightStyle.Mod.Element.Red + i))
				{
					ref Color ptr = ref b;
					int num2;
					int num = num2 = i;
					float num3 = ptr[num2];
					b[num] = num3 - a.GetFaceValue(global::LightStyle.Mod.Element.Red + i);
				}
			}
			return b;
		}

		// Token: 0x06003B26 RID: 15142 RVA: 0x000D15B4 File Offset: 0x000CF7B4
		public static Color operator *(Color b, global::LightStyle.Mod a)
		{
			for (int i = 0; i < 4; i++)
			{
				if ((a.mask & global::LightStyle.Mod.ElementToMask(global::LightStyle.Mod.Element.Red + i)) == global::LightStyle.Mod.ElementToMask(global::LightStyle.Mod.Element.Red + i))
				{
					ref Color ptr = ref b;
					int num2;
					int num = num2 = i;
					float num3 = ptr[num2];
					b[num] = num3 * a.GetFaceValue(global::LightStyle.Mod.Element.Red + i);
				}
			}
			return b;
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x000D1614 File Offset: 0x000CF814
		public static Color operator /(Color b, global::LightStyle.Mod a)
		{
			for (int i = 0; i < 4; i++)
			{
				if ((a.mask & global::LightStyle.Mod.ElementToMask(global::LightStyle.Mod.Element.Red + i)) == global::LightStyle.Mod.ElementToMask(global::LightStyle.Mod.Element.Red + i))
				{
					ref Color ptr = ref b;
					int num2;
					int num = num2 = i;
					float num3 = ptr[num2];
					b[num] = num3 / a.GetFaceValue(global::LightStyle.Mod.Element.Red + i);
				}
			}
			return b;
		}

		// Token: 0x06003B28 RID: 15144 RVA: 0x000D1674 File Offset: 0x000CF874
		public static global::LightStyle.Mod operator ~(global::LightStyle.Mod a)
		{
			a.mask = (~a.mask & (global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha | global::LightStyle.Mod.Mask.Intensity | global::LightStyle.Mod.Mask.Range | global::LightStyle.Mod.Mask.SpotAngle));
			return a;
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x000D168C File Offset: 0x000CF88C
		public static global::LightStyle.Mod operator -(global::LightStyle.Mod a)
		{
			for (global::LightStyle.Mod.Element element = global::LightStyle.Mod.Element.Red; element < (global::LightStyle.Mod.Element)7; element++)
			{
				a.SetFaceValue(element, -a.GetFaceValue(element));
			}
			return a;
		}

		// Token: 0x06003B2A RID: 15146 RVA: 0x000D16C0 File Offset: 0x000CF8C0
		public static bool operator true(global::LightStyle.Mod a)
		{
			return (a.mask & (global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha | global::LightStyle.Mod.Mask.Intensity | global::LightStyle.Mod.Mask.Range | global::LightStyle.Mod.Mask.SpotAngle)) != (global::LightStyle.Mod.Mask)0;
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x000D16D4 File Offset: 0x000CF8D4
		public static bool operator false(global::LightStyle.Mod b)
		{
			return (b.mask & (global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha | global::LightStyle.Mod.Mask.Intensity | global::LightStyle.Mod.Mask.Range | global::LightStyle.Mod.Mask.SpotAngle)) == (global::LightStyle.Mod.Mask)0;
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x000D16E4 File Offset: 0x000CF8E4
		public static explicit operator global::LightStyle.Mod(Light light)
		{
			global::LightStyle.Mod result = default(global::LightStyle.Mod);
			if (light)
			{
				result.color = light.color;
				result.intensity = light.intensity;
				result.range = light.range;
				result.spotAngle = light.spotAngle;
				switch (light.type)
				{
				case 0:
					result.mask = (global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha | global::LightStyle.Mod.Mask.Intensity | global::LightStyle.Mod.Mask.Range | global::LightStyle.Mod.Mask.SpotAngle);
					break;
				case 1:
					result.mask = (global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha | global::LightStyle.Mod.Mask.Intensity);
					break;
				case 2:
					result.mask = (global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha | global::LightStyle.Mod.Mask.Intensity | global::LightStyle.Mod.Mask.Range);
					break;
				}
			}
			return result;
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x000D1784 File Offset: 0x000CF984
		public static explicit operator global::LightStyle.Mod(Color color)
		{
			return new global::LightStyle.Mod
			{
				color = color,
				mask = (global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha)
			};
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x000D17B0 File Offset: 0x000CF9B0
		public static explicit operator global::LightStyle.Mod(float intensity)
		{
			return new global::LightStyle.Mod
			{
				intensity = intensity,
				mask = global::LightStyle.Mod.Mask.Intensity
			};
		}

		// Token: 0x04001D4D RID: 7501
		public const global::LightStyle.Mod.Element kElementFirst = global::LightStyle.Mod.Element.Red;

		// Token: 0x04001D4E RID: 7502
		public const global::LightStyle.Mod.Element kElementLast = global::LightStyle.Mod.Element.SpotAngle;

		// Token: 0x04001D4F RID: 7503
		public const global::LightStyle.Mod.Element kElementBegin = global::LightStyle.Mod.Element.Red;

		// Token: 0x04001D50 RID: 7504
		public const global::LightStyle.Mod.Element kElementEnd = (global::LightStyle.Mod.Element)7;

		// Token: 0x04001D51 RID: 7505
		public const global::LightStyle.Mod.Element kElementEnumeratorBegin = (global::LightStyle.Mod.Element)(-1);

		// Token: 0x04001D52 RID: 7506
		public const global::LightStyle.Mod.Mask kMaskNone = (global::LightStyle.Mod.Mask)0;

		// Token: 0x04001D53 RID: 7507
		public const global::LightStyle.Mod.Mask kMaskRGB = global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue;

		// Token: 0x04001D54 RID: 7508
		public const global::LightStyle.Mod.Mask kMaskRGBA = global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha;

		// Token: 0x04001D55 RID: 7509
		public const global::LightStyle.Mod.Mask kMaskDirectionalLight = global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha | global::LightStyle.Mod.Mask.Intensity;

		// Token: 0x04001D56 RID: 7510
		public const global::LightStyle.Mod.Mask kMaskPointLight = global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha | global::LightStyle.Mod.Mask.Intensity | global::LightStyle.Mod.Mask.Range;

		// Token: 0x04001D57 RID: 7511
		public const global::LightStyle.Mod.Mask kMaskSpotLight = global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha | global::LightStyle.Mod.Mask.Intensity | global::LightStyle.Mod.Mask.Range | global::LightStyle.Mod.Mask.SpotAngle;

		// Token: 0x04001D58 RID: 7512
		public const global::LightStyle.Mod.Mask kMaskAll = global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha | global::LightStyle.Mod.Mask.Intensity | global::LightStyle.Mod.Mask.Range | global::LightStyle.Mod.Mask.SpotAngle;

		// Token: 0x04001D59 RID: 7513
		[FieldOffset(0)]
		public Color color;

		// Token: 0x04001D5A RID: 7514
		[FieldOffset(0)]
		public float r;

		// Token: 0x04001D5B RID: 7515
		[FieldOffset(4)]
		public float g;

		// Token: 0x04001D5C RID: 7516
		[FieldOffset(8)]
		public float b;

		// Token: 0x04001D5D RID: 7517
		[FieldOffset(12)]
		public float a;

		// Token: 0x04001D5E RID: 7518
		[FieldOffset(16)]
		public float intensity;

		// Token: 0x04001D5F RID: 7519
		[FieldOffset(20)]
		public float range;

		// Token: 0x04001D60 RID: 7520
		[FieldOffset(24)]
		public float spotAngle;

		// Token: 0x04001D61 RID: 7521
		[FieldOffset(28)]
		public global::LightStyle.Mod.Mask mask;

		// Token: 0x020006CF RID: 1743
		public enum Element
		{
			// Token: 0x04001D63 RID: 7523
			Red,
			// Token: 0x04001D64 RID: 7524
			Green,
			// Token: 0x04001D65 RID: 7525
			Blue,
			// Token: 0x04001D66 RID: 7526
			Alpha,
			// Token: 0x04001D67 RID: 7527
			Intensity,
			// Token: 0x04001D68 RID: 7528
			Range,
			// Token: 0x04001D69 RID: 7529
			SpotAngle
		}

		// Token: 0x020006D0 RID: 1744
		[Flags]
		public enum Mask
		{
			// Token: 0x04001D6B RID: 7531
			Red = 1,
			// Token: 0x04001D6C RID: 7532
			Green = 2,
			// Token: 0x04001D6D RID: 7533
			Blue = 4,
			// Token: 0x04001D6E RID: 7534
			Alpha = 8,
			// Token: 0x04001D6F RID: 7535
			Intensity = 16,
			// Token: 0x04001D70 RID: 7536
			Range = 32,
			// Token: 0x04001D71 RID: 7537
			SpotAngle = 64
		}
	}

	// Token: 0x020006D1 RID: 1745
	public abstract class Simulation : IDisposable
	{
		// Token: 0x06003B2F RID: 15151 RVA: 0x000D17DC File Offset: 0x000CF9DC
		protected Simulation(global::LightStyle creator)
		{
			this.creator = creator;
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06003B30 RID: 15152 RVA: 0x000D180C File Offset: 0x000CFA0C
		public bool alive
		{
			get
			{
				return !this.destroyed;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06003B31 RID: 15153 RVA: 0x000D1818 File Offset: 0x000CFA18
		public bool disposed
		{
			get
			{
				return this.destroyed;
			}
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x000D1820 File Offset: 0x000CFA20
		public void ResetTime(double time)
		{
			if (this.startTime != time)
			{
				this.startTime = time;
				this.OnTimeReset();
			}
		}

		// Token: 0x06003B33 RID: 15155
		protected abstract void Simulate(double currentTime);

		// Token: 0x06003B34 RID: 15156 RVA: 0x000D183C File Offset: 0x000CFA3C
		protected virtual void OnTimeReset()
		{
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x000D1840 File Offset: 0x000CFA40
		private void UpdateBinding()
		{
			double time = global::LightStyle.time;
			if (time >= this.nextBindTime)
			{
				this.Simulate(time);
				this.lastSimulateTime = time;
			}
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x000D1870 File Offset: 0x000CFA70
		public void BindToLight(Light light)
		{
			if (this.destroyed)
			{
				return;
			}
			this.UpdateBinding();
			this.mod.ApplyTo(light);
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x000D1890 File Offset: 0x000CFA90
		public global::LightStyle.Mod BindMod(global::LightStyle.Mod.Mask mask)
		{
			if (!this.destroyed)
			{
				this.UpdateBinding();
			}
			global::LightStyle.Mod result = this.mod;
			result.mask &= mask;
			return result;
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x000D18C8 File Offset: 0x000CFAC8
		public void BindToLight(Light light, global::LightStyle.Mod.Mask mask)
		{
			if (mask == (global::LightStyle.Mod.Mask.Red | global::LightStyle.Mod.Mask.Green | global::LightStyle.Mod.Mask.Blue | global::LightStyle.Mod.Mask.Alpha | global::LightStyle.Mod.Mask.Intensity | global::LightStyle.Mod.Mask.Range | global::LightStyle.Mod.Mask.SpotAngle))
			{
				this.BindToLight(light);
			}
			else
			{
				if (this.destroyed)
				{
					return;
				}
				this.UpdateBinding();
				if ((this.mod.mask & mask) != this.mod.mask)
				{
					global::LightStyle.Mod mod = this.mod;
					mod.mask &= mask;
					mod.ApplyTo(light);
				}
				else
				{
					this.mod.ApplyTo(light);
				}
			}
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x000D1948 File Offset: 0x000CFB48
		public void Dispose()
		{
			if (this.isDisposing || this.destroyed)
			{
				return;
			}
			this.isDisposing = true;
			bool flag = true;
			try
			{
				flag = this.creator.DeconstructSimulation(this);
			}
			finally
			{
				this.isDisposing = false;
				this.destroyed = flag;
			}
		}

		// Token: 0x04001D72 RID: 7538
		protected global::LightStyle creator;

		// Token: 0x04001D73 RID: 7539
		protected global::LightStyle.Mod mod;

		// Token: 0x04001D74 RID: 7540
		protected double startTime;

		// Token: 0x04001D75 RID: 7541
		protected double nextBindTime = double.NegativeInfinity;

		// Token: 0x04001D76 RID: 7542
		protected double lastSimulateTime = double.NegativeInfinity;

		// Token: 0x04001D77 RID: 7543
		private bool isDisposing;

		// Token: 0x04001D78 RID: 7544
		private bool destroyed;
	}

	// Token: 0x020006D2 RID: 1746
	public abstract class Simulation<Style> : global::LightStyle.Simulation where Style : global::LightStyle
	{
		// Token: 0x06003B3A RID: 15162 RVA: 0x000D19B4 File Offset: 0x000CFBB4
		protected Simulation(Style creator) : base(creator)
		{
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06003B3B RID: 15163 RVA: 0x000D19C4 File Offset: 0x000CFBC4
		// (set) Token: 0x06003B3C RID: 15164 RVA: 0x000D19D4 File Offset: 0x000CFBD4
		protected new Style creator
		{
			get
			{
				return (Style)((object)this.creator);
			}
			set
			{
				this.creator = value;
			}
		}
	}
}
