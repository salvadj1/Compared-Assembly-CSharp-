using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200079D RID: 1949
public class UIMaterial : ScriptableObject
{
	// Token: 0x06004674 RID: 18036 RVA: 0x001184EC File Offset: 0x001166EC
	public static UIMaterial Create(Material key)
	{
		if (!key)
		{
			return null;
		}
		UIMaterial uimaterial;
		if (UIMaterial.g.keyedMaterials.TryGetValue(key, out uimaterial))
		{
			return uimaterial;
		}
		if (UIMaterial.g.generatedMaterials.TryGetValue(key, out uimaterial))
		{
			return uimaterial;
		}
		uimaterial = ScriptableObject.CreateInstance<UIMaterial>();
		uimaterial.key = key;
		uimaterial.hashCode = ++UIMaterial.g.hashCodeIterator;
		if (uimaterial.hashCode == 2147483647)
		{
			UIMaterial.g.hashCodeIterator = int.MinValue;
		}
		UIMaterial.g.keyedMaterials.Add(key, uimaterial);
		return uimaterial;
	}

	// Token: 0x06004675 RID: 18037 RVA: 0x00118578 File Offset: 0x00116778
	public static UIMaterial Create(Material key, bool manageKeyDestruction, UIDrawCall.Clipping useAsClipping)
	{
		if (!manageKeyDestruction)
		{
			return UIMaterial.Create(key);
		}
		if (!key)
		{
			return null;
		}
		UIMaterial uimaterial;
		if (UIMaterial.g.keyedMaterials.TryGetValue(key, out uimaterial))
		{
			throw new InvalidOperationException("That material is registered and cannot be used with manageKeyDestruction");
		}
		if (UIMaterial.g.generatedMaterials.TryGetValue(key, out uimaterial))
		{
			return uimaterial;
		}
		uimaterial = ScriptableObject.CreateInstance<UIMaterial>();
		uimaterial.key = key;
		uimaterial.hashCode = ++UIMaterial.g.hashCodeIterator;
		if (uimaterial.hashCode == 2147483647)
		{
			UIMaterial.g.hashCodeIterator = int.MinValue;
		}
		UIMaterial.g.generatedMaterials.Add(key, uimaterial);
		uimaterial.matFirst = key;
		switch (useAsClipping)
		{
		case UIDrawCall.Clipping.None:
			uimaterial.matNone = key;
			break;
		case UIDrawCall.Clipping.HardClip:
			uimaterial.matHardClip = key;
			break;
		case UIDrawCall.Clipping.AlphaClip:
			uimaterial.matAlphaClip = key;
			break;
		case UIDrawCall.Clipping.SoftClip:
			uimaterial.matSoftClip = key;
			break;
		default:
			throw new NotImplementedException();
		}
		uimaterial.madeMats = (UIMaterial.ClippingFlags)(1 << (int)useAsClipping);
		return uimaterial;
	}

	// Token: 0x06004676 RID: 18038 RVA: 0x00118680 File Offset: 0x00116880
	public static UIMaterial Create(Material key, bool manageKeyDestruction)
	{
		return UIMaterial.Create(key, manageKeyDestruction, UIDrawCall.Clipping.None);
	}

	// Token: 0x06004677 RID: 18039 RVA: 0x0011868C File Offset: 0x0011688C
	public sealed override int GetHashCode()
	{
		return this.hashCode;
	}

	// Token: 0x06004678 RID: 18040 RVA: 0x00118694 File Offset: 0x00116894
	public override string ToString()
	{
		return (!this.key) ? "destroyed" : this.key.ToString();
	}

	// Token: 0x06004679 RID: 18041 RVA: 0x001186BC File Offset: 0x001168BC
	private Material FastGet(UIDrawCall.Clipping clipping)
	{
		switch (clipping)
		{
		case UIDrawCall.Clipping.None:
			return this.matNone;
		case UIDrawCall.Clipping.HardClip:
			return this.matHardClip;
		case UIDrawCall.Clipping.AlphaClip:
			return this.matAlphaClip;
		case UIDrawCall.Clipping.SoftClip:
			return this.matSoftClip;
		default:
			throw new NotImplementedException();
		}
	}

	// Token: 0x0600467A RID: 18042 RVA: 0x00118708 File Offset: 0x00116908
	private static bool ShaderNameDecor(ref string shaderName, string not1, string not2, string suffix)
	{
		string text = shaderName.Replace(not1, string.Empty).Replace(not2, string.Empty);
		if (text != shaderName)
		{
			if (!text.EndsWith(suffix))
			{
				shaderName = text + suffix;
			}
			return true;
		}
		if (!shaderName.EndsWith(suffix))
		{
			shaderName += suffix;
			return true;
		}
		return false;
	}

	// Token: 0x0600467B RID: 18043 RVA: 0x0011876C File Offset: 0x0011696C
	private static Shader GetClippingShader(Shader original, UIDrawCall.Clipping clipping)
	{
		if (!original)
		{
			return null;
		}
		string text = original.name;
		switch (clipping)
		{
		case UIDrawCall.Clipping.None:
		{
			string text2 = text.Replace(" (HardClip)", string.Empty).Replace(" (AlphaClip)", string.Empty).Replace(" (SoftClip)", string.Empty);
			if (text2 == text)
			{
				return original;
			}
			text = text2;
			break;
		}
		case UIDrawCall.Clipping.HardClip:
			if (!UIMaterial.ShaderNameDecor(ref text, " (AlphaClip)", " (SoftClip)", " (HardClip)"))
			{
				return original;
			}
			break;
		case UIDrawCall.Clipping.AlphaClip:
			if (!UIMaterial.ShaderNameDecor(ref text, " (SoftClip)", " (HardClip)", " (AlphaClip)"))
			{
				return original;
			}
			break;
		case UIDrawCall.Clipping.SoftClip:
			if (!UIMaterial.ShaderNameDecor(ref text, " (HardClip)", " (AlphaClip)", " (SoftClip)"))
			{
				return original;
			}
			break;
		default:
			throw new NotImplementedException();
		}
		Shader shader = Shader.Find(text);
		if (!shader)
		{
			throw new MissingReferenceException("Theres no shader named " + text);
		}
		return shader;
	}

	// Token: 0x0600467C RID: 18044 RVA: 0x00118880 File Offset: 0x00116A80
	private static Material CreateMaterial(Shader shader)
	{
		return new Material(shader)
		{
			hideFlags = 12
		};
	}

	// Token: 0x0600467D RID: 18045 RVA: 0x001188A0 File Offset: 0x00116AA0
	private static UIDrawCall.Clipping ShaderClipping(string shaderName)
	{
		if (shaderName.EndsWith(" (SoftClip)"))
		{
			return UIDrawCall.Clipping.SoftClip;
		}
		if (shaderName.EndsWith(" (HardClip)"))
		{
			return UIDrawCall.Clipping.HardClip;
		}
		if (shaderName.EndsWith(" (AlphaClip)"))
		{
			return UIDrawCall.Clipping.AlphaClip;
		}
		return UIDrawCall.Clipping.None;
	}

	// Token: 0x0600467E RID: 18046 RVA: 0x001188E4 File Offset: 0x00116AE4
	private void MakeDefaultMaterial()
	{
		this.MakeMaterial(UIMaterial.ShaderClipping(this.key.shader.name));
	}

	// Token: 0x0600467F RID: 18047 RVA: 0x00118904 File Offset: 0x00116B04
	public UIMaterial Clone()
	{
		Material material = new Material(this.key)
		{
			hideFlags = 4
		};
		return UIMaterial.Create(material, true);
	}

	// Token: 0x06004680 RID: 18048 RVA: 0x00118930 File Offset: 0x00116B30
	private Material MakeMaterial(UIDrawCall.Clipping clipping)
	{
		bool flag = this.madeMats == (UIMaterial.ClippingFlags)0;
		Material material;
		Material material2;
		switch (clipping)
		{
		case UIDrawCall.Clipping.None:
		{
			Shader shader = this.key.shader;
			material = this.matNone;
			material2 = (this.matNone = UIMaterial.CreateMaterial(shader));
			this.madeMats |= UIMaterial.ClippingFlags.None;
			break;
		}
		case UIDrawCall.Clipping.HardClip:
		{
			Shader shader = UIMaterial.GetClippingShader(this.key.shader, UIDrawCall.Clipping.HardClip);
			material = this.matHardClip;
			material2 = (this.matHardClip = UIMaterial.CreateMaterial(shader));
			this.madeMats |= UIMaterial.ClippingFlags.HardClip;
			break;
		}
		case UIDrawCall.Clipping.AlphaClip:
		{
			Shader shader = UIMaterial.GetClippingShader(this.key.shader, UIDrawCall.Clipping.AlphaClip);
			material = this.matAlphaClip;
			material2 = (this.matAlphaClip = UIMaterial.CreateMaterial(shader));
			this.madeMats |= UIMaterial.ClippingFlags.AlphaClip;
			break;
		}
		case UIDrawCall.Clipping.SoftClip:
		{
			Shader shader = UIMaterial.GetClippingShader(this.key.shader, UIDrawCall.Clipping.SoftClip);
			material = this.matSoftClip;
			material2 = (this.matSoftClip = UIMaterial.CreateMaterial(shader));
			this.madeMats |= UIMaterial.ClippingFlags.SoftClip;
			break;
		}
		default:
			throw new NotImplementedException();
		}
		UIMaterial.g.generatedMaterials.Add(material2, this);
		if (flag)
		{
			this.matFirst = material2;
			material2.CopyPropertiesFromMaterial(this.key);
		}
		else
		{
			material2.CopyPropertiesFromMaterial(this.matFirst);
		}
		if (material)
		{
			Object.DestroyImmediate(material);
		}
		return material2;
	}

	// Token: 0x17000DA2 RID: 3490
	public Material this[UIDrawCall.Clipping clipping]
	{
		get
		{
			UIMaterial.ClippingFlags clippingFlags = (UIMaterial.ClippingFlags)(1 << (int)clipping);
			if ((clippingFlags & this.madeMats) != clippingFlags)
			{
				return this.MakeMaterial(clipping);
			}
			switch (clipping)
			{
			case UIDrawCall.Clipping.None:
				return this.matNone;
			case UIDrawCall.Clipping.HardClip:
				return this.matHardClip;
			case UIDrawCall.Clipping.AlphaClip:
				return this.matAlphaClip;
			case UIDrawCall.Clipping.SoftClip:
				return this.matSoftClip;
			default:
				throw new NotImplementedException();
			}
		}
	}

	// Token: 0x06004682 RID: 18050 RVA: 0x00118B10 File Offset: 0x00116D10
	public void Set(string property, float value)
	{
		if (this.madeMats == (UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		for (UIDrawCall.Clipping clipping = UIDrawCall.Clipping.None; clipping < (UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (UIMaterial.ClippingFlags)(1 << (int)clipping)) != (UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetFloat(property, value);
			}
		}
	}

	// Token: 0x06004683 RID: 18051 RVA: 0x00118B60 File Offset: 0x00116D60
	public void Set(string property, Vector2 value)
	{
		if (this.madeMats == (UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		Vector4 vector;
		vector.x = value.x;
		vector.y = value.y;
		vector.z = (vector.w = 0f);
		for (UIDrawCall.Clipping clipping = UIDrawCall.Clipping.None; clipping < (UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (UIMaterial.ClippingFlags)(1 << (int)clipping)) != (UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetVector(property, vector);
			}
		}
	}

	// Token: 0x06004684 RID: 18052 RVA: 0x00118BE4 File Offset: 0x00116DE4
	public void Set(string property, Vector3 value)
	{
		if (this.madeMats == (UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		Vector4 vector;
		vector.x = value.x;
		vector.y = value.y;
		vector.z = (vector.w = 0f);
		for (UIDrawCall.Clipping clipping = UIDrawCall.Clipping.None; clipping < (UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (UIMaterial.ClippingFlags)(1 << (int)clipping)) != (UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetVector(property, vector);
			}
		}
	}

	// Token: 0x06004685 RID: 18053 RVA: 0x00118C68 File Offset: 0x00116E68
	public void Set(string property, Vector4 value)
	{
		if (this.madeMats == (UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		Vector4 vector;
		vector.x = value.x;
		vector.y = value.y;
		vector.z = (vector.w = 0f);
		for (UIDrawCall.Clipping clipping = UIDrawCall.Clipping.None; clipping < (UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (UIMaterial.ClippingFlags)(1 << (int)clipping)) != (UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetVector(property, vector);
			}
		}
	}

	// Token: 0x06004686 RID: 18054 RVA: 0x00118CEC File Offset: 0x00116EEC
	public void Set(string property, Color color)
	{
		if (this.madeMats == (UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		for (UIDrawCall.Clipping clipping = UIDrawCall.Clipping.None; clipping < (UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (UIMaterial.ClippingFlags)(1 << (int)clipping)) != (UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetColor(property, color);
			}
		}
	}

	// Token: 0x06004687 RID: 18055 RVA: 0x00118D3C File Offset: 0x00116F3C
	public void Set(string property, Matrix4x4 mat)
	{
		if (this.madeMats == (UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		for (UIDrawCall.Clipping clipping = UIDrawCall.Clipping.None; clipping < (UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (UIMaterial.ClippingFlags)(1 << (int)clipping)) != (UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetMatrix(property, mat);
			}
		}
	}

	// Token: 0x06004688 RID: 18056 RVA: 0x00118D8C File Offset: 0x00116F8C
	public void Set(string property, Texture texture)
	{
		if (this.madeMats == (UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		for (UIDrawCall.Clipping clipping = UIDrawCall.Clipping.None; clipping < (UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (UIMaterial.ClippingFlags)(1 << (int)clipping)) != (UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetTexture(property, texture);
			}
		}
	}

	// Token: 0x06004689 RID: 18057 RVA: 0x00118DDC File Offset: 0x00116FDC
	public void SetTextureScale(string property, Vector2 scale)
	{
		if (this.madeMats == (UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		for (UIDrawCall.Clipping clipping = UIDrawCall.Clipping.None; clipping < (UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (UIMaterial.ClippingFlags)(1 << (int)clipping)) != (UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetTextureScale(property, scale);
			}
		}
	}

	// Token: 0x0600468A RID: 18058 RVA: 0x00118E2C File Offset: 0x0011702C
	public void SetTextureOffset(string property, Vector2 offset)
	{
		if (this.madeMats == (UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		for (UIDrawCall.Clipping clipping = UIDrawCall.Clipping.None; clipping < (UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (UIMaterial.ClippingFlags)(1 << (int)clipping)) != (UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetTextureOffset(property, offset);
			}
		}
	}

	// Token: 0x0600468B RID: 18059 RVA: 0x00118E7C File Offset: 0x0011707C
	public void SetPass(int pass)
	{
		if (this.madeMats == (UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		for (UIDrawCall.Clipping clipping = UIDrawCall.Clipping.None; clipping < (UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (UIMaterial.ClippingFlags)(1 << (int)clipping)) != (UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetPass(pass);
			}
		}
	}

	// Token: 0x0600468C RID: 18060 RVA: 0x00118ECC File Offset: 0x001170CC
	public void CopyPropertiesFromMaterial(Material material)
	{
		if (this.madeMats == (UIMaterial.ClippingFlags)0)
		{
			if (material == this.key)
			{
				return;
			}
			this.MakeDefaultMaterial();
		}
		for (UIDrawCall.Clipping clipping = UIDrawCall.Clipping.None; clipping < (UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (UIMaterial.ClippingFlags)(1 << (int)clipping)) != (UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).CopyPropertiesFromMaterial(material);
			}
		}
	}

	// Token: 0x0600468D RID: 18061 RVA: 0x00118F30 File Offset: 0x00117130
	public bool HasProperty(string property)
	{
		if (this.madeMats == (UIMaterial.ClippingFlags)0)
		{
			return this.key.HasProperty(property);
		}
		return this.matFirst.HasProperty(property);
	}

	// Token: 0x0600468E RID: 18062 RVA: 0x00118F64 File Offset: 0x00117164
	public void CopyPropertiesFromOriginal()
	{
		if (this.madeMats != (UIMaterial.ClippingFlags)0)
		{
			this.CopyPropertiesFromMaterial(this.key);
		}
	}

	// Token: 0x0600468F RID: 18063 RVA: 0x00118F80 File Offset: 0x00117180
	private void OnDestroy()
	{
		if (this.madeMats != (UIMaterial.ClippingFlags)0)
		{
			for (UIDrawCall.Clipping clipping = UIDrawCall.Clipping.None; clipping < (UIDrawCall.Clipping)4; clipping++)
			{
				if ((this.madeMats & (UIMaterial.ClippingFlags)(1 << (int)clipping)) != (UIMaterial.ClippingFlags)0)
				{
					Material material = this.FastGet(clipping);
					UIMaterial.g.generatedMaterials.Remove(material);
					Object.DestroyImmediate(material);
				}
			}
		}
		UIMaterial.g.keyedMaterials.Remove(this.key);
		this.matNone = (this.matFirst = (this.matHardClip = (this.matSoftClip = (this.matAlphaClip = (this.key = null)))));
	}

	// Token: 0x17000DA3 RID: 3491
	// (get) Token: 0x06004690 RID: 18064 RVA: 0x0011901C File Offset: 0x0011721C
	// (set) Token: 0x06004691 RID: 18065 RVA: 0x00119050 File Offset: 0x00117250
	public Texture mainTexture
	{
		get
		{
			return (this.madeMats != (UIMaterial.ClippingFlags)0) ? this.matFirst.mainTexture : this.key.mainTexture;
		}
		set
		{
			if (this.madeMats == (UIMaterial.ClippingFlags)0)
			{
				this.MakeDefaultMaterial();
			}
			this.Set("_MainTex", value);
		}
	}

	// Token: 0x06004692 RID: 18066 RVA: 0x00119070 File Offset: 0x00117270
	public static explicit operator UIMaterial(Material key)
	{
		return UIMaterial.Create(key);
	}

	// Token: 0x06004693 RID: 18067 RVA: 0x00119078 File Offset: 0x00117278
	public static explicit operator Material(UIMaterial uimat)
	{
		return (!uimat) ? null : uimat.key;
	}

	// Token: 0x040026A5 RID: 9893
	private const UIDrawCall.Clipping kBeginClipping = UIDrawCall.Clipping.None;

	// Token: 0x040026A6 RID: 9894
	private const UIDrawCall.Clipping kEndClipping = (UIDrawCall.Clipping)4;

	// Token: 0x040026A7 RID: 9895
	private const string hard = " (HardClip)";

	// Token: 0x040026A8 RID: 9896
	private const string alpha = " (AlphaClip)";

	// Token: 0x040026A9 RID: 9897
	private const string soft = " (SoftClip)";

	// Token: 0x040026AA RID: 9898
	private Material key;

	// Token: 0x040026AB RID: 9899
	private Material matNone;

	// Token: 0x040026AC RID: 9900
	private Material matHardClip;

	// Token: 0x040026AD RID: 9901
	private Material matAlphaClip;

	// Token: 0x040026AE RID: 9902
	private Material matSoftClip;

	// Token: 0x040026AF RID: 9903
	private Material matFirst;

	// Token: 0x040026B0 RID: 9904
	private int hashCode;

	// Token: 0x040026B1 RID: 9905
	private UIMaterial.ClippingFlags madeMats;

	// Token: 0x0200079E RID: 1950
	private static class g
	{
		// Token: 0x040026B2 RID: 9906
		public static int hashCodeIterator = int.MinValue;

		// Token: 0x040026B3 RID: 9907
		public static readonly Dictionary<Material, UIMaterial> generatedMaterials = new Dictionary<Material, UIMaterial>();

		// Token: 0x040026B4 RID: 9908
		public static readonly Dictionary<Material, UIMaterial> keyedMaterials = new Dictionary<Material, UIMaterial>();
	}

	// Token: 0x0200079F RID: 1951
	private enum ClippingFlags
	{
		// Token: 0x040026B6 RID: 9910
		None = 1,
		// Token: 0x040026B7 RID: 9911
		HardClip,
		// Token: 0x040026B8 RID: 9912
		AlphaClip = 4,
		// Token: 0x040026B9 RID: 9913
		SoftClip = 8
	}
}
