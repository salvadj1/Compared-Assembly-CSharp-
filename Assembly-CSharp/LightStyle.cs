using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x0200060D RID: 1549
public abstract class LightStyle : ScriptableObject
{
	// Token: 0x17000AF5 RID: 2805
	// (get) Token: 0x0600371D RID: 14109 RVA: 0x000C83F0 File Offset: 0x000C65F0
	public static double time
	{
		get
		{
			if (NetCull.isRunning)
			{
				return NetCull.time;
			}
			return (double)Time.time;
		}
	}

	// Token: 0x0600371E RID: 14110
	protected abstract LightStyle.Simulation ConstructSimulation(LightStylist stylist);

	// Token: 0x0600371F RID: 14111
	protected abstract bool DeconstructSimulation(LightStyle.Simulation simulation);

	// Token: 0x06003720 RID: 14112 RVA: 0x000C8408 File Offset: 0x000C6608
	public LightStyle.Simulation CreateSimulation(LightStylist stylist)
	{
		return this.CreateSimulation(LightStyle.time, stylist);
	}

	// Token: 0x06003721 RID: 14113 RVA: 0x000C8418 File Offset: 0x000C6618
	public LightStyle.Simulation CreateSimulation(double startTime, LightStylist stylist)
	{
		LightStyle.Simulation simulation = this.ConstructSimulation(stylist);
		if (simulation != null)
		{
			simulation.ResetTime(startTime);
		}
		return simulation;
	}

	// Token: 0x06003722 RID: 14114 RVA: 0x000C843C File Offset: 0x000C663C
	private static LightStyle MissingLightStyle(string name)
	{
		return LightStyleDefault.Singleton;
	}

	// Token: 0x06003723 RID: 14115 RVA: 0x000C8444 File Offset: 0x000C6644
	public static implicit operator LightStyle(string name)
	{
		if (!LightStyle.madeLoadedByString)
		{
			LightStyle lightStyle = (LightStyle)Resources.Load(name, typeof(LightStyle));
			if (lightStyle)
			{
				LightStyle.loadedByString = new Dictionary<string, WeakReference>(StringComparer.InvariantCultureIgnoreCase);
				LightStyle.loadedByString[name] = new WeakReference(lightStyle);
			}
			else
			{
				lightStyle = LightStyle.MissingLightStyle(name);
			}
			return lightStyle;
		}
		WeakReference weakReference;
		if (!LightStyle.loadedByString.TryGetValue(name, out weakReference))
		{
			LightStyle lightStyle2 = (LightStyle)Resources.Load(name, typeof(LightStyle));
			if (lightStyle2)
			{
				weakReference = new WeakReference(lightStyle2);
				LightStyle.loadedByString[name] = weakReference;
			}
			else
			{
				lightStyle2 = LightStyle.MissingLightStyle(name);
			}
			return lightStyle2;
		}
		object target = weakReference.Target;
		if (weakReference.IsAlive && (Object)target)
		{
			return (LightStyle)target;
		}
		LightStyle lightStyle3 = (LightStyle)Resources.Load(name, typeof(LightStyle));
		if (lightStyle3)
		{
			weakReference.Target = lightStyle3;
		}
		else
		{
			lightStyle3 = LightStyle.MissingLightStyle(name);
		}
		return lightStyle3;
	}

	// Token: 0x06003724 RID: 14116 RVA: 0x000C8564 File Offset: 0x000C6764
	public static implicit operator string(LightStyle lightStyle)
	{
		return (!lightStyle) ? null : lightStyle.name;
	}

	// Token: 0x04001B65 RID: 7013
	private static Dictionary<string, WeakReference> loadedByString;

	// Token: 0x04001B66 RID: 7014
	private static bool madeLoadedByString;

	// Token: 0x0200060E RID: 1550
	[StructLayout(LayoutKind.Explicit)]
	public struct Mod
	{
		// Token: 0x06003725 RID: 14117 RVA: 0x000C8580 File Offset: 0x000C6780
		public bool AnyOf(LightStyle.Mod.Mask mask)
		{
			return (this.mask & mask) != (LightStyle.Mod.Mask)0;
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x000C8590 File Offset: 0x000C6790
		public bool AllOf(LightStyle.Mod.Mask mask)
		{
			return (this.mask & mask) == mask;
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x000C85A0 File Offset: 0x000C67A0
		public bool Contains(LightStyle.Mod.Element element)
		{
			return this.AllOf(LightStyle.Mod.ElementToMask(element));
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x000C85B0 File Offset: 0x000C67B0
		public void SetModify(LightStyle.Mod.Element element)
		{
			this.mask |= LightStyle.Mod.ElementToMask(element);
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x000C85C8 File Offset: 0x000C67C8
		public void SetModify(LightStyle.Mod.Element element, float assignValue)
		{
			this.SetFaceValue(element, assignValue);
			this.mask |= LightStyle.Mod.ElementToMask(element);
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x000C85E8 File Offset: 0x000C67E8
		public void ClearModify(LightStyle.Mod.Element element)
		{
			this.mask &= LightStyle.Mod.ElementToMaskNot(element);
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x000C8600 File Offset: 0x000C6800
		public void ToggleModify(LightStyle.Mod.Element element)
		{
			this.mask ^= LightStyle.Mod.ElementToMask(element);
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000C8618 File Offset: 0x000C6818
		public float GetFaceValue(LightStyle.Mod.Element element)
		{
			switch (element)
			{
			case LightStyle.Mod.Element.Red:
				return this.r;
			case LightStyle.Mod.Element.Green:
				return this.g;
			case LightStyle.Mod.Element.Blue:
				return this.b;
			case LightStyle.Mod.Element.Alpha:
				return this.a;
			case LightStyle.Mod.Element.Intensity:
				return this.intensity;
			case LightStyle.Mod.Element.Range:
				return this.range;
			case LightStyle.Mod.Element.SpotAngle:
				return this.spotAngle;
			default:
				throw new ArgumentOutOfRangeException("element");
			}
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000C868C File Offset: 0x000C688C
		public void SetFaceValue(LightStyle.Mod.Element element, float value)
		{
			switch (element)
			{
			case LightStyle.Mod.Element.Red:
				this.r = value;
				break;
			case LightStyle.Mod.Element.Green:
				this.g = value;
				break;
			case LightStyle.Mod.Element.Blue:
				this.b = value;
				break;
			case LightStyle.Mod.Element.Alpha:
				this.a = value;
				break;
			case LightStyle.Mod.Element.Intensity:
				this.intensity = value;
				break;
			case LightStyle.Mod.Element.Range:
				this.range = value;
				break;
			case LightStyle.Mod.Element.SpotAngle:
				this.spotAngle = value;
				break;
			default:
				throw new ArgumentOutOfRangeException("element");
			}
		}

		// Token: 0x17000AF6 RID: 2806
		public float? this[LightStyle.Mod.Element element]
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

		// Token: 0x06003730 RID: 14128 RVA: 0x000C8790 File Offset: 0x000C6990
		public static LightStyle.Mod.Mask ElementToMask(LightStyle.Mod.Element element)
		{
			return (LightStyle.Mod.Mask)(1 << (int)element & 127);
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x000C879C File Offset: 0x000C699C
		public static LightStyle.Mod.Mask ElementToMaskNot(LightStyle.Mod.Element element)
		{
			return (LightStyle.Mod.Mask)(~(1 << (int)element) & 127);
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x000C87A8 File Offset: 0x000C69A8
		public void ApplyTo(Light light)
		{
			switch (light.type)
			{
			case 0:
				this.ApplyTo(light, LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha | LightStyle.Mod.Mask.Intensity | LightStyle.Mod.Mask.Range | LightStyle.Mod.Mask.SpotAngle);
				break;
			case 1:
				this.ApplyTo(light, LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha | LightStyle.Mod.Mask.Intensity);
				break;
			case 2:
				this.ApplyTo(light, LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha | LightStyle.Mod.Mask.Intensity | LightStyle.Mod.Mask.Range);
				break;
			}
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x000C8800 File Offset: 0x000C6A00
		public void ApplyTo(Light light, LightStyle.Mod.Mask applyMask)
		{
			LightStyle.Mod.Mask mask = this.mask & applyMask;
			if ((mask & (LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha)) != (LightStyle.Mod.Mask)0)
			{
				if ((mask & (LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha)) == (LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha))
				{
					light.color = this.color;
				}
				else
				{
					Color color = light.color;
					if ((mask & LightStyle.Mod.Mask.Red) == LightStyle.Mod.Mask.Red)
					{
						color.r = this.r;
					}
					if ((mask & LightStyle.Mod.Mask.Green) == LightStyle.Mod.Mask.Green)
					{
						color.g = this.g;
					}
					if ((mask & LightStyle.Mod.Mask.Blue) == LightStyle.Mod.Mask.Blue)
					{
						color.b = this.b;
					}
					if ((mask & LightStyle.Mod.Mask.Alpha) == LightStyle.Mod.Mask.Alpha)
					{
						color.a = this.a;
					}
					light.color = color;
				}
			}
			if ((mask & LightStyle.Mod.Mask.Intensity) == LightStyle.Mod.Mask.Intensity)
			{
				light.intensity = this.intensity;
			}
			if ((mask & LightStyle.Mod.Mask.Range) == LightStyle.Mod.Mask.Range)
			{
				light.range = this.range;
			}
			if ((mask & LightStyle.Mod.Mask.SpotAngle) == LightStyle.Mod.Mask.SpotAngle)
			{
				light.spotAngle = this.spotAngle;
			}
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x000C88E8 File Offset: 0x000C6AE8
		public static LightStyle.Mod Lerp(LightStyle.Mod a, LightStyle.Mod b, float t, LightStyle.Mod.Mask mask)
		{
			b.mask &= mask;
			if (b.mask == (LightStyle.Mod.Mask)0)
			{
				return a;
			}
			a.mask &= mask;
			if (a.mask == (LightStyle.Mod.Mask)0)
			{
				return b;
			}
			LightStyle.Mod.Mask mask2 = a.mask & b.mask;
			if (mask2 != (LightStyle.Mod.Mask)0)
			{
				float num = 1f - t;
				if (mask != (LightStyle.Mod.Mask)0)
				{
					for (LightStyle.Mod.Element element = LightStyle.Mod.Element.Red; element < (LightStyle.Mod.Element)7; element++)
					{
						if ((mask2 & LightStyle.Mod.ElementToMask(element)) == LightStyle.Mod.ElementToMask(element))
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

		// Token: 0x06003735 RID: 14133 RVA: 0x000C89B8 File Offset: 0x000C6BB8
		public static LightStyle.Mod operator +(LightStyle.Mod a, LightStyle.Mod b)
		{
			LightStyle.Mod result = a;
			LightStyle.Mod.Mask mask = a.mask & b.mask;
			for (LightStyle.Mod.Element element = LightStyle.Mod.Element.Red; element < (LightStyle.Mod.Element)7; element++)
			{
				if ((mask & LightStyle.Mod.ElementToMask(element)) == LightStyle.Mod.ElementToMask(element))
				{
					result.SetFaceValue(element, a.GetFaceValue(element) + b.GetFaceValue(element));
				}
			}
			return result;
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x000C8A18 File Offset: 0x000C6C18
		public static LightStyle.Mod operator -(LightStyle.Mod a, LightStyle.Mod b)
		{
			LightStyle.Mod.Mask mask = a.mask & b.mask;
			for (LightStyle.Mod.Element element = LightStyle.Mod.Element.Red; element < (LightStyle.Mod.Element)7; element++)
			{
				if ((mask & LightStyle.Mod.ElementToMask(element)) == LightStyle.Mod.ElementToMask(element))
				{
					a.SetFaceValue(element, a.GetFaceValue(element) - b.GetFaceValue(element));
				}
			}
			return a;
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x000C8A74 File Offset: 0x000C6C74
		public static LightStyle.Mod operator *(LightStyle.Mod a, LightStyle.Mod b)
		{
			LightStyle.Mod.Mask mask = a.mask & b.mask;
			for (LightStyle.Mod.Element element = LightStyle.Mod.Element.Red; element < (LightStyle.Mod.Element)7; element++)
			{
				if ((mask & LightStyle.Mod.ElementToMask(element)) == LightStyle.Mod.ElementToMask(element))
				{
					a.SetFaceValue(element, a.GetFaceValue(element) * b.GetFaceValue(element));
				}
			}
			return a;
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x000C8AD0 File Offset: 0x000C6CD0
		public static LightStyle.Mod operator /(LightStyle.Mod a, LightStyle.Mod b)
		{
			LightStyle.Mod.Mask mask = a.mask & b.mask;
			for (LightStyle.Mod.Element element = LightStyle.Mod.Element.Red; element < (LightStyle.Mod.Element)7; element++)
			{
				if ((mask & LightStyle.Mod.ElementToMask(element)) == LightStyle.Mod.ElementToMask(element))
				{
					a.SetFaceValue(element, a.GetFaceValue(element) / b.GetFaceValue(element));
				}
			}
			return a;
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x000C8B2C File Offset: 0x000C6D2C
		public static LightStyle.Mod operator |(LightStyle.Mod a, LightStyle.Mod b)
		{
			for (LightStyle.Mod.Element element = LightStyle.Mod.Element.Red; element < (LightStyle.Mod.Element)7; element++)
			{
				if ((a.mask & LightStyle.Mod.ElementToMask(element)) != LightStyle.Mod.ElementToMask(element) && (b.mask & LightStyle.Mod.ElementToMask(element)) == LightStyle.Mod.ElementToMask(element))
				{
					a.SetModify(element, b.GetFaceValue(element));
				}
			}
			return a;
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000C8B90 File Offset: 0x000C6D90
		public static LightStyle.Mod operator &(LightStyle.Mod a, LightStyle.Mod b)
		{
			for (LightStyle.Mod.Element element = LightStyle.Mod.Element.Red; element < (LightStyle.Mod.Element)7; element++)
			{
				if ((a.mask & LightStyle.Mod.ElementToMask(element)) == LightStyle.Mod.ElementToMask(element) && (b.mask & LightStyle.Mod.ElementToMask(element)) == LightStyle.Mod.ElementToMask(element))
				{
					a.SetFaceValue(element, b.GetFaceValue(element));
				}
			}
			return a;
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x000C8BF4 File Offset: 0x000C6DF4
		public static LightStyle.Mod operator ^(LightStyle.Mod a, LightStyle.Mod b)
		{
			for (LightStyle.Mod.Element element = LightStyle.Mod.Element.Red; element < (LightStyle.Mod.Element)7; element++)
			{
				if ((a.mask & LightStyle.Mod.ElementToMask(element)) == LightStyle.Mod.ElementToMask(element))
				{
					if ((b.mask & LightStyle.Mod.ElementToMask(element)) == LightStyle.Mod.ElementToMask(element))
					{
						a.SetFaceValue(element, b.GetFaceValue(element));
					}
				}
				else if ((b.mask & LightStyle.Mod.ElementToMask(element)) == LightStyle.Mod.ElementToMask(element))
				{
					a.SetModify(element, b.GetFaceValue(element));
				}
			}
			return a;
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x000C8C90 File Offset: 0x000C6E90
		public static LightStyle.Mod operator |(LightStyle.Mod a, LightStyle.Mod.Mask b)
		{
			a.mask |= b;
			return a;
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x000C8CA4 File Offset: 0x000C6EA4
		public static LightStyle.Mod operator &(LightStyle.Mod a, LightStyle.Mod.Mask b)
		{
			a.mask &= b;
			return a;
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x000C8CB8 File Offset: 0x000C6EB8
		public static LightStyle.Mod operator ^(LightStyle.Mod a, LightStyle.Mod.Mask b)
		{
			a.mask ^= b;
			return a;
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x000C8CCC File Offset: 0x000C6ECC
		public static LightStyle.Mod operator +(LightStyle.Mod a, LightStyle.Mod.Element b)
		{
			a.mask |= LightStyle.Mod.ElementToMask(b);
			return a;
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x000C8CE4 File Offset: 0x000C6EE4
		public static LightStyle.Mod operator -(LightStyle.Mod a, LightStyle.Mod.Element b)
		{
			a.mask &= LightStyle.Mod.ElementToMaskNot(b);
			return a;
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x000C8CFC File Offset: 0x000C6EFC
		public static float?operator |(LightStyle.Mod a, LightStyle.Mod.Element b)
		{
			return a[b];
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x000C8D08 File Offset: 0x000C6F08
		public static bool operator &(LightStyle.Mod a, LightStyle.Mod.Element b)
		{
			return a.Contains(b);
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x000C8D14 File Offset: 0x000C6F14
		public static float operator ^(LightStyle.Mod a, LightStyle.Mod.Element b)
		{
			return a.GetFaceValue(b);
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x000C8D20 File Offset: 0x000C6F20
		public static LightStyle.Mod operator +(LightStyle.Mod a, float b)
		{
			for (LightStyle.Mod.Element element = LightStyle.Mod.Element.Red; element < (LightStyle.Mod.Element)7; element++)
			{
				if ((a.mask & LightStyle.Mod.ElementToMask(element)) == LightStyle.Mod.ElementToMask(element))
				{
					a.SetFaceValue(element, a.GetFaceValue(element) + b);
				}
			}
			return a;
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x000C8D6C File Offset: 0x000C6F6C
		public static LightStyle.Mod operator -(LightStyle.Mod a, float b)
		{
			for (LightStyle.Mod.Element element = LightStyle.Mod.Element.Red; element < (LightStyle.Mod.Element)7; element++)
			{
				if ((a.mask & LightStyle.Mod.ElementToMask(element)) == LightStyle.Mod.ElementToMask(element))
				{
					a.SetFaceValue(element, a.GetFaceValue(element) - b);
				}
			}
			return a;
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x000C8DB8 File Offset: 0x000C6FB8
		public static LightStyle.Mod operator *(LightStyle.Mod a, float b)
		{
			LightStyle.Mod result = a;
			for (LightStyle.Mod.Element element = LightStyle.Mod.Element.Red; element < (LightStyle.Mod.Element)7; element++)
			{
				if ((a.mask & LightStyle.Mod.ElementToMask(element)) == LightStyle.Mod.ElementToMask(element))
				{
					result.SetFaceValue(element, a.GetFaceValue(element) * b);
				}
			}
			return result;
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x000C8E08 File Offset: 0x000C7008
		public static LightStyle.Mod operator /(LightStyle.Mod a, float b)
		{
			for (LightStyle.Mod.Element element = LightStyle.Mod.Element.Red; element < (LightStyle.Mod.Element)7; element++)
			{
				if ((a.mask & LightStyle.Mod.ElementToMask(element)) == LightStyle.Mod.ElementToMask(element))
				{
					a.SetFaceValue(element, a.GetFaceValue(element) / b);
				}
			}
			return a;
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x000C8E54 File Offset: 0x000C7054
		public static LightStyle.Mod operator +(LightStyle.Mod a, Color b)
		{
			for (int i = 0; i < 4; i++)
			{
				if ((a.mask & LightStyle.Mod.ElementToMask(LightStyle.Mod.Element.Red + i)) == LightStyle.Mod.ElementToMask(LightStyle.Mod.Element.Red + i))
				{
					a.SetFaceValue(LightStyle.Mod.Element.Red + i, a.GetFaceValue(LightStyle.Mod.Element.Red + i) + b[i]);
				}
			}
			return a;
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x000C8EB0 File Offset: 0x000C70B0
		public static LightStyle.Mod operator -(LightStyle.Mod a, Color b)
		{
			for (int i = 0; i < 4; i++)
			{
				if ((a.mask & LightStyle.Mod.ElementToMask(LightStyle.Mod.Element.Red + i)) == LightStyle.Mod.ElementToMask(LightStyle.Mod.Element.Red + i))
				{
					a.SetFaceValue(LightStyle.Mod.Element.Red + i, a.GetFaceValue(LightStyle.Mod.Element.Red + i) - b[i]);
				}
			}
			return a;
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x000C8F0C File Offset: 0x000C710C
		public static LightStyle.Mod operator *(LightStyle.Mod a, Color b)
		{
			for (int i = 0; i < 4; i++)
			{
				if ((a.mask & LightStyle.Mod.ElementToMask(LightStyle.Mod.Element.Red + i)) == LightStyle.Mod.ElementToMask(LightStyle.Mod.Element.Red + i))
				{
					a.SetFaceValue(LightStyle.Mod.Element.Red + i, a.GetFaceValue(LightStyle.Mod.Element.Red + i) * b[i]);
				}
			}
			return a;
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x000C8F68 File Offset: 0x000C7168
		public static LightStyle.Mod operator /(LightStyle.Mod a, Color b)
		{
			for (int i = 0; i < 4; i++)
			{
				if ((a.mask & LightStyle.Mod.ElementToMask(LightStyle.Mod.Element.Red + i)) == LightStyle.Mod.ElementToMask(LightStyle.Mod.Element.Red + i))
				{
					a.SetFaceValue(LightStyle.Mod.Element.Red + i, a.GetFaceValue(LightStyle.Mod.Element.Red + i) / b[i]);
				}
			}
			return a;
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x000C8FC4 File Offset: 0x000C71C4
		public static Color operator +(Color b, LightStyle.Mod a)
		{
			for (int i = 0; i < 4; i++)
			{
				if ((a.mask & LightStyle.Mod.ElementToMask(LightStyle.Mod.Element.Red + i)) == LightStyle.Mod.ElementToMask(LightStyle.Mod.Element.Red + i))
				{
					ref Color ptr = ref b;
					int num2;
					int num = num2 = i;
					float num3 = ptr[num2];
					b[num] = num3 + a.GetFaceValue(LightStyle.Mod.Element.Red + i);
				}
			}
			return b;
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x000C9024 File Offset: 0x000C7224
		public static Color operator -(Color b, LightStyle.Mod a)
		{
			for (int i = 0; i < 4; i++)
			{
				if ((a.mask & LightStyle.Mod.ElementToMask(LightStyle.Mod.Element.Red + i)) == LightStyle.Mod.ElementToMask(LightStyle.Mod.Element.Red + i))
				{
					ref Color ptr = ref b;
					int num2;
					int num = num2 = i;
					float num3 = ptr[num2];
					b[num] = num3 - a.GetFaceValue(LightStyle.Mod.Element.Red + i);
				}
			}
			return b;
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x000C9084 File Offset: 0x000C7284
		public static Color operator *(Color b, LightStyle.Mod a)
		{
			for (int i = 0; i < 4; i++)
			{
				if ((a.mask & LightStyle.Mod.ElementToMask(LightStyle.Mod.Element.Red + i)) == LightStyle.Mod.ElementToMask(LightStyle.Mod.Element.Red + i))
				{
					ref Color ptr = ref b;
					int num2;
					int num = num2 = i;
					float num3 = ptr[num2];
					b[num] = num3 * a.GetFaceValue(LightStyle.Mod.Element.Red + i);
				}
			}
			return b;
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x000C90E4 File Offset: 0x000C72E4
		public static Color operator /(Color b, LightStyle.Mod a)
		{
			for (int i = 0; i < 4; i++)
			{
				if ((a.mask & LightStyle.Mod.ElementToMask(LightStyle.Mod.Element.Red + i)) == LightStyle.Mod.ElementToMask(LightStyle.Mod.Element.Red + i))
				{
					ref Color ptr = ref b;
					int num2;
					int num = num2 = i;
					float num3 = ptr[num2];
					b[num] = num3 / a.GetFaceValue(LightStyle.Mod.Element.Red + i);
				}
			}
			return b;
		}

		// Token: 0x06003750 RID: 14160 RVA: 0x000C9144 File Offset: 0x000C7344
		public static LightStyle.Mod operator ~(LightStyle.Mod a)
		{
			a.mask = (~a.mask & (LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha | LightStyle.Mod.Mask.Intensity | LightStyle.Mod.Mask.Range | LightStyle.Mod.Mask.SpotAngle));
			return a;
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x000C915C File Offset: 0x000C735C
		public static LightStyle.Mod operator -(LightStyle.Mod a)
		{
			for (LightStyle.Mod.Element element = LightStyle.Mod.Element.Red; element < (LightStyle.Mod.Element)7; element++)
			{
				a.SetFaceValue(element, -a.GetFaceValue(element));
			}
			return a;
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x000C9190 File Offset: 0x000C7390
		public static bool operator true(LightStyle.Mod a)
		{
			return (a.mask & (LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha | LightStyle.Mod.Mask.Intensity | LightStyle.Mod.Mask.Range | LightStyle.Mod.Mask.SpotAngle)) != (LightStyle.Mod.Mask)0;
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x000C91A4 File Offset: 0x000C73A4
		public static bool operator false(LightStyle.Mod b)
		{
			return (b.mask & (LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha | LightStyle.Mod.Mask.Intensity | LightStyle.Mod.Mask.Range | LightStyle.Mod.Mask.SpotAngle)) == (LightStyle.Mod.Mask)0;
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x000C91B4 File Offset: 0x000C73B4
		public static explicit operator LightStyle.Mod(Light light)
		{
			LightStyle.Mod result = default(LightStyle.Mod);
			if (light)
			{
				result.color = light.color;
				result.intensity = light.intensity;
				result.range = light.range;
				result.spotAngle = light.spotAngle;
				switch (light.type)
				{
				case 0:
					result.mask = (LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha | LightStyle.Mod.Mask.Intensity | LightStyle.Mod.Mask.Range | LightStyle.Mod.Mask.SpotAngle);
					break;
				case 1:
					result.mask = (LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha | LightStyle.Mod.Mask.Intensity);
					break;
				case 2:
					result.mask = (LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha | LightStyle.Mod.Mask.Intensity | LightStyle.Mod.Mask.Range);
					break;
				}
			}
			return result;
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x000C9254 File Offset: 0x000C7454
		public static explicit operator LightStyle.Mod(Color color)
		{
			return new LightStyle.Mod
			{
				color = color,
				mask = (LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha)
			};
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x000C9280 File Offset: 0x000C7480
		public static explicit operator LightStyle.Mod(float intensity)
		{
			return new LightStyle.Mod
			{
				intensity = intensity,
				mask = LightStyle.Mod.Mask.Intensity
			};
		}

		// Token: 0x04001B67 RID: 7015
		public const LightStyle.Mod.Element kElementFirst = LightStyle.Mod.Element.Red;

		// Token: 0x04001B68 RID: 7016
		public const LightStyle.Mod.Element kElementLast = LightStyle.Mod.Element.SpotAngle;

		// Token: 0x04001B69 RID: 7017
		public const LightStyle.Mod.Element kElementBegin = LightStyle.Mod.Element.Red;

		// Token: 0x04001B6A RID: 7018
		public const LightStyle.Mod.Element kElementEnd = (LightStyle.Mod.Element)7;

		// Token: 0x04001B6B RID: 7019
		public const LightStyle.Mod.Element kElementEnumeratorBegin = (LightStyle.Mod.Element)(-1);

		// Token: 0x04001B6C RID: 7020
		public const LightStyle.Mod.Mask kMaskNone = (LightStyle.Mod.Mask)0;

		// Token: 0x04001B6D RID: 7021
		public const LightStyle.Mod.Mask kMaskRGB = LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue;

		// Token: 0x04001B6E RID: 7022
		public const LightStyle.Mod.Mask kMaskRGBA = LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha;

		// Token: 0x04001B6F RID: 7023
		public const LightStyle.Mod.Mask kMaskDirectionalLight = LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha | LightStyle.Mod.Mask.Intensity;

		// Token: 0x04001B70 RID: 7024
		public const LightStyle.Mod.Mask kMaskPointLight = LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha | LightStyle.Mod.Mask.Intensity | LightStyle.Mod.Mask.Range;

		// Token: 0x04001B71 RID: 7025
		public const LightStyle.Mod.Mask kMaskSpotLight = LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha | LightStyle.Mod.Mask.Intensity | LightStyle.Mod.Mask.Range | LightStyle.Mod.Mask.SpotAngle;

		// Token: 0x04001B72 RID: 7026
		public const LightStyle.Mod.Mask kMaskAll = LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha | LightStyle.Mod.Mask.Intensity | LightStyle.Mod.Mask.Range | LightStyle.Mod.Mask.SpotAngle;

		// Token: 0x04001B73 RID: 7027
		[FieldOffset(0)]
		public Color color;

		// Token: 0x04001B74 RID: 7028
		[FieldOffset(0)]
		public float r;

		// Token: 0x04001B75 RID: 7029
		[FieldOffset(4)]
		public float g;

		// Token: 0x04001B76 RID: 7030
		[FieldOffset(8)]
		public float b;

		// Token: 0x04001B77 RID: 7031
		[FieldOffset(12)]
		public float a;

		// Token: 0x04001B78 RID: 7032
		[FieldOffset(16)]
		public float intensity;

		// Token: 0x04001B79 RID: 7033
		[FieldOffset(20)]
		public float range;

		// Token: 0x04001B7A RID: 7034
		[FieldOffset(24)]
		public float spotAngle;

		// Token: 0x04001B7B RID: 7035
		[FieldOffset(28)]
		public LightStyle.Mod.Mask mask;

		// Token: 0x0200060F RID: 1551
		public enum Element
		{
			// Token: 0x04001B7D RID: 7037
			Red,
			// Token: 0x04001B7E RID: 7038
			Green,
			// Token: 0x04001B7F RID: 7039
			Blue,
			// Token: 0x04001B80 RID: 7040
			Alpha,
			// Token: 0x04001B81 RID: 7041
			Intensity,
			// Token: 0x04001B82 RID: 7042
			Range,
			// Token: 0x04001B83 RID: 7043
			SpotAngle
		}

		// Token: 0x02000610 RID: 1552
		[Flags]
		public enum Mask
		{
			// Token: 0x04001B85 RID: 7045
			Red = 1,
			// Token: 0x04001B86 RID: 7046
			Green = 2,
			// Token: 0x04001B87 RID: 7047
			Blue = 4,
			// Token: 0x04001B88 RID: 7048
			Alpha = 8,
			// Token: 0x04001B89 RID: 7049
			Intensity = 16,
			// Token: 0x04001B8A RID: 7050
			Range = 32,
			// Token: 0x04001B8B RID: 7051
			SpotAngle = 64
		}
	}

	// Token: 0x02000611 RID: 1553
	public abstract class Simulation : IDisposable
	{
		// Token: 0x06003757 RID: 14167 RVA: 0x000C92AC File Offset: 0x000C74AC
		protected Simulation(LightStyle creator)
		{
			this.creator = creator;
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06003758 RID: 14168 RVA: 0x000C92DC File Offset: 0x000C74DC
		public bool alive
		{
			get
			{
				return !this.destroyed;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06003759 RID: 14169 RVA: 0x000C92E8 File Offset: 0x000C74E8
		public bool disposed
		{
			get
			{
				return this.destroyed;
			}
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x000C92F0 File Offset: 0x000C74F0
		public void ResetTime(double time)
		{
			if (this.startTime != time)
			{
				this.startTime = time;
				this.OnTimeReset();
			}
		}

		// Token: 0x0600375B RID: 14171
		protected abstract void Simulate(double currentTime);

		// Token: 0x0600375C RID: 14172 RVA: 0x000C930C File Offset: 0x000C750C
		protected virtual void OnTimeReset()
		{
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x000C9310 File Offset: 0x000C7510
		private void UpdateBinding()
		{
			double time = LightStyle.time;
			if (time >= this.nextBindTime)
			{
				this.Simulate(time);
				this.lastSimulateTime = time;
			}
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x000C9340 File Offset: 0x000C7540
		public void BindToLight(Light light)
		{
			if (this.destroyed)
			{
				return;
			}
			this.UpdateBinding();
			this.mod.ApplyTo(light);
		}

		// Token: 0x0600375F RID: 14175 RVA: 0x000C9360 File Offset: 0x000C7560
		public LightStyle.Mod BindMod(LightStyle.Mod.Mask mask)
		{
			if (!this.destroyed)
			{
				this.UpdateBinding();
			}
			LightStyle.Mod result = this.mod;
			result.mask &= mask;
			return result;
		}

		// Token: 0x06003760 RID: 14176 RVA: 0x000C9398 File Offset: 0x000C7598
		public void BindToLight(Light light, LightStyle.Mod.Mask mask)
		{
			if (mask == (LightStyle.Mod.Mask.Red | LightStyle.Mod.Mask.Green | LightStyle.Mod.Mask.Blue | LightStyle.Mod.Mask.Alpha | LightStyle.Mod.Mask.Intensity | LightStyle.Mod.Mask.Range | LightStyle.Mod.Mask.SpotAngle))
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
					LightStyle.Mod mod = this.mod;
					mod.mask &= mask;
					mod.ApplyTo(light);
				}
				else
				{
					this.mod.ApplyTo(light);
				}
			}
		}

		// Token: 0x06003761 RID: 14177 RVA: 0x000C9418 File Offset: 0x000C7618
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

		// Token: 0x04001B8C RID: 7052
		protected LightStyle creator;

		// Token: 0x04001B8D RID: 7053
		protected LightStyle.Mod mod;

		// Token: 0x04001B8E RID: 7054
		protected double startTime;

		// Token: 0x04001B8F RID: 7055
		protected double nextBindTime = double.NegativeInfinity;

		// Token: 0x04001B90 RID: 7056
		protected double lastSimulateTime = double.NegativeInfinity;

		// Token: 0x04001B91 RID: 7057
		private bool isDisposing;

		// Token: 0x04001B92 RID: 7058
		private bool destroyed;
	}

	// Token: 0x02000612 RID: 1554
	public abstract class Simulation<Style> : LightStyle.Simulation where Style : LightStyle
	{
		// Token: 0x06003762 RID: 14178 RVA: 0x000C9484 File Offset: 0x000C7684
		protected Simulation(Style creator) : base(creator)
		{
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06003763 RID: 14179 RVA: 0x000C9494 File Offset: 0x000C7694
		// (set) Token: 0x06003764 RID: 14180 RVA: 0x000C94A4 File Offset: 0x000C76A4
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
