using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000888 RID: 2184
public class UIMaterial : ScriptableObject
{
	// Token: 0x06004AF9 RID: 19193 RVA: 0x00121E6C File Offset: 0x0012006C
	public static global::UIMaterial Create(Material key)
	{
		if (!key)
		{
			return null;
		}
		global::UIMaterial uimaterial;
		if (global::UIMaterial.g.keyedMaterials.TryGetValue(key, out uimaterial))
		{
			return uimaterial;
		}
		if (global::UIMaterial.g.generatedMaterials.TryGetValue(key, out uimaterial))
		{
			return uimaterial;
		}
		uimaterial = ScriptableObject.CreateInstance<global::UIMaterial>();
		uimaterial.key = key;
		uimaterial.hashCode = ++global::UIMaterial.g.hashCodeIterator;
		if (uimaterial.hashCode == 2147483647)
		{
			global::UIMaterial.g.hashCodeIterator = int.MinValue;
		}
		global::UIMaterial.g.keyedMaterials.Add(key, uimaterial);
		return uimaterial;
	}

	// Token: 0x06004AFA RID: 19194 RVA: 0x00121EF8 File Offset: 0x001200F8
	public static global::UIMaterial Create(Material key, bool manageKeyDestruction, global::UIDrawCall.Clipping useAsClipping)
	{
		if (!manageKeyDestruction)
		{
			return global::UIMaterial.Create(key);
		}
		if (!key)
		{
			return null;
		}
		global::UIMaterial uimaterial;
		if (global::UIMaterial.g.keyedMaterials.TryGetValue(key, out uimaterial))
		{
			throw new InvalidOperationException("That material is registered and cannot be used with manageKeyDestruction");
		}
		if (global::UIMaterial.g.generatedMaterials.TryGetValue(key, out uimaterial))
		{
			return uimaterial;
		}
		uimaterial = ScriptableObject.CreateInstance<global::UIMaterial>();
		uimaterial.key = key;
		uimaterial.hashCode = ++global::UIMaterial.g.hashCodeIterator;
		if (uimaterial.hashCode == 2147483647)
		{
			global::UIMaterial.g.hashCodeIterator = int.MinValue;
		}
		global::UIMaterial.g.generatedMaterials.Add(key, uimaterial);
		uimaterial.matFirst = key;
		switch (useAsClipping)
		{
		case global::UIDrawCall.Clipping.None:
			uimaterial.matNone = key;
			break;
		case global::UIDrawCall.Clipping.HardClip:
			uimaterial.matHardClip = key;
			break;
		case global::UIDrawCall.Clipping.AlphaClip:
			uimaterial.matAlphaClip = key;
			break;
		case global::UIDrawCall.Clipping.SoftClip:
			uimaterial.matSoftClip = key;
			break;
		default:
			throw new NotImplementedException();
		}
		uimaterial.madeMats = (global::UIMaterial.ClippingFlags)(1 << (int)useAsClipping);
		return uimaterial;
	}

	// Token: 0x06004AFB RID: 19195 RVA: 0x00122000 File Offset: 0x00120200
	public static global::UIMaterial Create(Material key, bool manageKeyDestruction)
	{
		return global::UIMaterial.Create(key, manageKeyDestruction, global::UIDrawCall.Clipping.None);
	}

	// Token: 0x06004AFC RID: 19196 RVA: 0x0012200C File Offset: 0x0012020C
	public sealed override int GetHashCode()
	{
		return this.hashCode;
	}

	// Token: 0x06004AFD RID: 19197 RVA: 0x00122014 File Offset: 0x00120214
	public override string ToString()
	{
		return (!this.key) ? "destroyed" : this.key.ToString();
	}

	// Token: 0x06004AFE RID: 19198 RVA: 0x0012203C File Offset: 0x0012023C
	private Material FastGet(global::UIDrawCall.Clipping clipping)
	{
		switch (clipping)
		{
		case global::UIDrawCall.Clipping.None:
			return this.matNone;
		case global::UIDrawCall.Clipping.HardClip:
			return this.matHardClip;
		case global::UIDrawCall.Clipping.AlphaClip:
			return this.matAlphaClip;
		case global::UIDrawCall.Clipping.SoftClip:
			return this.matSoftClip;
		default:
			throw new NotImplementedException();
		}
	}

	// Token: 0x06004AFF RID: 19199 RVA: 0x00122088 File Offset: 0x00120288
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

	// Token: 0x06004B00 RID: 19200 RVA: 0x001220EC File Offset: 0x001202EC
	private static Shader GetClippingShader(Shader original, global::UIDrawCall.Clipping clipping)
	{
		if (!original)
		{
			return null;
		}
		string text = original.name;
		switch (clipping)
		{
		case global::UIDrawCall.Clipping.None:
		{
			string text2 = text.Replace(" (HardClip)", string.Empty).Replace(" (AlphaClip)", string.Empty).Replace(" (SoftClip)", string.Empty);
			if (text2 == text)
			{
				return original;
			}
			text = text2;
			break;
		}
		case global::UIDrawCall.Clipping.HardClip:
			if (!global::UIMaterial.ShaderNameDecor(ref text, " (AlphaClip)", " (SoftClip)", " (HardClip)"))
			{
				return original;
			}
			break;
		case global::UIDrawCall.Clipping.AlphaClip:
			if (!global::UIMaterial.ShaderNameDecor(ref text, " (SoftClip)", " (HardClip)", " (AlphaClip)"))
			{
				return original;
			}
			break;
		case global::UIDrawCall.Clipping.SoftClip:
			if (!global::UIMaterial.ShaderNameDecor(ref text, " (HardClip)", " (AlphaClip)", " (SoftClip)"))
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

	// Token: 0x06004B01 RID: 19201 RVA: 0x00122200 File Offset: 0x00120400
	private static Material CreateMaterial(Shader shader)
	{
		return new Material(shader)
		{
			hideFlags = 12
		};
	}

	// Token: 0x06004B02 RID: 19202 RVA: 0x00122220 File Offset: 0x00120420
	private static global::UIDrawCall.Clipping ShaderClipping(string shaderName)
	{
		if (shaderName.EndsWith(" (SoftClip)"))
		{
			return global::UIDrawCall.Clipping.SoftClip;
		}
		if (shaderName.EndsWith(" (HardClip)"))
		{
			return global::UIDrawCall.Clipping.HardClip;
		}
		if (shaderName.EndsWith(" (AlphaClip)"))
		{
			return global::UIDrawCall.Clipping.AlphaClip;
		}
		return global::UIDrawCall.Clipping.None;
	}

	// Token: 0x06004B03 RID: 19203 RVA: 0x00122264 File Offset: 0x00120464
	private void MakeDefaultMaterial()
	{
		this.MakeMaterial(global::UIMaterial.ShaderClipping(this.key.shader.name));
	}

	// Token: 0x06004B04 RID: 19204 RVA: 0x00122284 File Offset: 0x00120484
	public global::UIMaterial Clone()
	{
		Material material = new Material(this.key)
		{
			hideFlags = 4
		};
		return global::UIMaterial.Create(material, true);
	}

	// Token: 0x06004B05 RID: 19205 RVA: 0x001222B0 File Offset: 0x001204B0
	private Material MakeMaterial(global::UIDrawCall.Clipping clipping)
	{
		bool flag = this.madeMats == (global::UIMaterial.ClippingFlags)0;
		Material material;
		Material material2;
		switch (clipping)
		{
		case global::UIDrawCall.Clipping.None:
		{
			Shader shader = this.key.shader;
			material = this.matNone;
			material2 = (this.matNone = global::UIMaterial.CreateMaterial(shader));
			this.madeMats |= global::UIMaterial.ClippingFlags.None;
			break;
		}
		case global::UIDrawCall.Clipping.HardClip:
		{
			Shader shader = global::UIMaterial.GetClippingShader(this.key.shader, global::UIDrawCall.Clipping.HardClip);
			material = this.matHardClip;
			material2 = (this.matHardClip = global::UIMaterial.CreateMaterial(shader));
			this.madeMats |= global::UIMaterial.ClippingFlags.HardClip;
			break;
		}
		case global::UIDrawCall.Clipping.AlphaClip:
		{
			Shader shader = global::UIMaterial.GetClippingShader(this.key.shader, global::UIDrawCall.Clipping.AlphaClip);
			material = this.matAlphaClip;
			material2 = (this.matAlphaClip = global::UIMaterial.CreateMaterial(shader));
			this.madeMats |= global::UIMaterial.ClippingFlags.AlphaClip;
			break;
		}
		case global::UIDrawCall.Clipping.SoftClip:
		{
			Shader shader = global::UIMaterial.GetClippingShader(this.key.shader, global::UIDrawCall.Clipping.SoftClip);
			material = this.matSoftClip;
			material2 = (this.matSoftClip = global::UIMaterial.CreateMaterial(shader));
			this.madeMats |= global::UIMaterial.ClippingFlags.SoftClip;
			break;
		}
		default:
			throw new NotImplementedException();
		}
		global::UIMaterial.g.generatedMaterials.Add(material2, this);
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

	// Token: 0x17000E32 RID: 3634
	public Material this[global::UIDrawCall.Clipping clipping]
	{
		get
		{
			global::UIMaterial.ClippingFlags clippingFlags = (global::UIMaterial.ClippingFlags)(1 << (int)clipping);
			if ((clippingFlags & this.madeMats) != clippingFlags)
			{
				return this.MakeMaterial(clipping);
			}
			switch (clipping)
			{
			case global::UIDrawCall.Clipping.None:
				return this.matNone;
			case global::UIDrawCall.Clipping.HardClip:
				return this.matHardClip;
			case global::UIDrawCall.Clipping.AlphaClip:
				return this.matAlphaClip;
			case global::UIDrawCall.Clipping.SoftClip:
				return this.matSoftClip;
			default:
				throw new NotImplementedException();
			}
		}
	}

	// Token: 0x06004B07 RID: 19207 RVA: 0x00122490 File Offset: 0x00120690
	public void Set(string property, float value)
	{
		if (this.madeMats == (global::UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		for (global::UIDrawCall.Clipping clipping = global::UIDrawCall.Clipping.None; clipping < (global::UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (global::UIMaterial.ClippingFlags)(1 << (int)clipping)) != (global::UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetFloat(property, value);
			}
		}
	}

	// Token: 0x06004B08 RID: 19208 RVA: 0x001224E0 File Offset: 0x001206E0
	public void Set(string property, Vector2 value)
	{
		if (this.madeMats == (global::UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		Vector4 vector;
		vector.x = value.x;
		vector.y = value.y;
		vector.z = (vector.w = 0f);
		for (global::UIDrawCall.Clipping clipping = global::UIDrawCall.Clipping.None; clipping < (global::UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (global::UIMaterial.ClippingFlags)(1 << (int)clipping)) != (global::UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetVector(property, vector);
			}
		}
	}

	// Token: 0x06004B09 RID: 19209 RVA: 0x00122564 File Offset: 0x00120764
	public void Set(string property, Vector3 value)
	{
		if (this.madeMats == (global::UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		Vector4 vector;
		vector.x = value.x;
		vector.y = value.y;
		vector.z = (vector.w = 0f);
		for (global::UIDrawCall.Clipping clipping = global::UIDrawCall.Clipping.None; clipping < (global::UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (global::UIMaterial.ClippingFlags)(1 << (int)clipping)) != (global::UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetVector(property, vector);
			}
		}
	}

	// Token: 0x06004B0A RID: 19210 RVA: 0x001225E8 File Offset: 0x001207E8
	public void Set(string property, Vector4 value)
	{
		if (this.madeMats == (global::UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		Vector4 vector;
		vector.x = value.x;
		vector.y = value.y;
		vector.z = (vector.w = 0f);
		for (global::UIDrawCall.Clipping clipping = global::UIDrawCall.Clipping.None; clipping < (global::UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (global::UIMaterial.ClippingFlags)(1 << (int)clipping)) != (global::UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetVector(property, vector);
			}
		}
	}

	// Token: 0x06004B0B RID: 19211 RVA: 0x0012266C File Offset: 0x0012086C
	public void Set(string property, Color color)
	{
		if (this.madeMats == (global::UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		for (global::UIDrawCall.Clipping clipping = global::UIDrawCall.Clipping.None; clipping < (global::UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (global::UIMaterial.ClippingFlags)(1 << (int)clipping)) != (global::UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetColor(property, color);
			}
		}
	}

	// Token: 0x06004B0C RID: 19212 RVA: 0x001226BC File Offset: 0x001208BC
	public void Set(string property, Matrix4x4 mat)
	{
		if (this.madeMats == (global::UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		for (global::UIDrawCall.Clipping clipping = global::UIDrawCall.Clipping.None; clipping < (global::UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (global::UIMaterial.ClippingFlags)(1 << (int)clipping)) != (global::UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetMatrix(property, mat);
			}
		}
	}

	// Token: 0x06004B0D RID: 19213 RVA: 0x0012270C File Offset: 0x0012090C
	public void Set(string property, Texture texture)
	{
		if (this.madeMats == (global::UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		for (global::UIDrawCall.Clipping clipping = global::UIDrawCall.Clipping.None; clipping < (global::UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (global::UIMaterial.ClippingFlags)(1 << (int)clipping)) != (global::UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetTexture(property, texture);
			}
		}
	}

	// Token: 0x06004B0E RID: 19214 RVA: 0x0012275C File Offset: 0x0012095C
	public void SetTextureScale(string property, Vector2 scale)
	{
		if (this.madeMats == (global::UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		for (global::UIDrawCall.Clipping clipping = global::UIDrawCall.Clipping.None; clipping < (global::UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (global::UIMaterial.ClippingFlags)(1 << (int)clipping)) != (global::UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetTextureScale(property, scale);
			}
		}
	}

	// Token: 0x06004B0F RID: 19215 RVA: 0x001227AC File Offset: 0x001209AC
	public void SetTextureOffset(string property, Vector2 offset)
	{
		if (this.madeMats == (global::UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		for (global::UIDrawCall.Clipping clipping = global::UIDrawCall.Clipping.None; clipping < (global::UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (global::UIMaterial.ClippingFlags)(1 << (int)clipping)) != (global::UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetTextureOffset(property, offset);
			}
		}
	}

	// Token: 0x06004B10 RID: 19216 RVA: 0x001227FC File Offset: 0x001209FC
	public void SetPass(int pass)
	{
		if (this.madeMats == (global::UIMaterial.ClippingFlags)0)
		{
			this.MakeDefaultMaterial();
		}
		for (global::UIDrawCall.Clipping clipping = global::UIDrawCall.Clipping.None; clipping < (global::UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (global::UIMaterial.ClippingFlags)(1 << (int)clipping)) != (global::UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).SetPass(pass);
			}
		}
	}

	// Token: 0x06004B11 RID: 19217 RVA: 0x0012284C File Offset: 0x00120A4C
	public void CopyPropertiesFromMaterial(Material material)
	{
		if (this.madeMats == (global::UIMaterial.ClippingFlags)0)
		{
			if (material == this.key)
			{
				return;
			}
			this.MakeDefaultMaterial();
		}
		for (global::UIDrawCall.Clipping clipping = global::UIDrawCall.Clipping.None; clipping < (global::UIDrawCall.Clipping)4; clipping++)
		{
			if ((this.madeMats & (global::UIMaterial.ClippingFlags)(1 << (int)clipping)) != (global::UIMaterial.ClippingFlags)0)
			{
				this.FastGet(clipping).CopyPropertiesFromMaterial(material);
			}
		}
	}

	// Token: 0x06004B12 RID: 19218 RVA: 0x001228B0 File Offset: 0x00120AB0
	public bool HasProperty(string property)
	{
		if (this.madeMats == (global::UIMaterial.ClippingFlags)0)
		{
			return this.key.HasProperty(property);
		}
		return this.matFirst.HasProperty(property);
	}

	// Token: 0x06004B13 RID: 19219 RVA: 0x001228E4 File Offset: 0x00120AE4
	public void CopyPropertiesFromOriginal()
	{
		if (this.madeMats != (global::UIMaterial.ClippingFlags)0)
		{
			this.CopyPropertiesFromMaterial(this.key);
		}
	}

	// Token: 0x06004B14 RID: 19220 RVA: 0x00122900 File Offset: 0x00120B00
	private void OnDestroy()
	{
		if (this.madeMats != (global::UIMaterial.ClippingFlags)0)
		{
			for (global::UIDrawCall.Clipping clipping = global::UIDrawCall.Clipping.None; clipping < (global::UIDrawCall.Clipping)4; clipping++)
			{
				if ((this.madeMats & (global::UIMaterial.ClippingFlags)(1 << (int)clipping)) != (global::UIMaterial.ClippingFlags)0)
				{
					Material material = this.FastGet(clipping);
					global::UIMaterial.g.generatedMaterials.Remove(material);
					Object.DestroyImmediate(material);
				}
			}
		}
		global::UIMaterial.g.keyedMaterials.Remove(this.key);
		this.matNone = (this.matFirst = (this.matHardClip = (this.matSoftClip = (this.matAlphaClip = (this.key = null)))));
	}

	// Token: 0x17000E33 RID: 3635
	// (get) Token: 0x06004B15 RID: 19221 RVA: 0x0012299C File Offset: 0x00120B9C
	// (set) Token: 0x06004B16 RID: 19222 RVA: 0x001229D0 File Offset: 0x00120BD0
	public Texture mainTexture
	{
		get
		{
			return (this.madeMats != (global::UIMaterial.ClippingFlags)0) ? this.matFirst.mainTexture : this.key.mainTexture;
		}
		set
		{
			if (this.madeMats == (global::UIMaterial.ClippingFlags)0)
			{
				this.MakeDefaultMaterial();
			}
			this.Set("_MainTex", value);
		}
	}

	// Token: 0x06004B17 RID: 19223 RVA: 0x001229F0 File Offset: 0x00120BF0
	public static explicit operator global::UIMaterial(Material key)
	{
		return global::UIMaterial.Create(key);
	}

	// Token: 0x06004B18 RID: 19224 RVA: 0x001229F8 File Offset: 0x00120BF8
	public static explicit operator Material(global::UIMaterial uimat)
	{
		return (!uimat) ? null : uimat.key;
	}

	// Token: 0x040028DC RID: 10460
	private const global::UIDrawCall.Clipping kBeginClipping = global::UIDrawCall.Clipping.None;

	// Token: 0x040028DD RID: 10461
	private const global::UIDrawCall.Clipping kEndClipping = (global::UIDrawCall.Clipping)4;

	// Token: 0x040028DE RID: 10462
	private const string hard = " (HardClip)";

	// Token: 0x040028DF RID: 10463
	private const string alpha = " (AlphaClip)";

	// Token: 0x040028E0 RID: 10464
	private const string soft = " (SoftClip)";

	// Token: 0x040028E1 RID: 10465
	private Material key;

	// Token: 0x040028E2 RID: 10466
	private Material matNone;

	// Token: 0x040028E3 RID: 10467
	private Material matHardClip;

	// Token: 0x040028E4 RID: 10468
	private Material matAlphaClip;

	// Token: 0x040028E5 RID: 10469
	private Material matSoftClip;

	// Token: 0x040028E6 RID: 10470
	private Material matFirst;

	// Token: 0x040028E7 RID: 10471
	private int hashCode;

	// Token: 0x040028E8 RID: 10472
	private global::UIMaterial.ClippingFlags madeMats;

	// Token: 0x02000889 RID: 2185
	private static class g
	{
		// Token: 0x040028E9 RID: 10473
		public static int hashCodeIterator = int.MinValue;

		// Token: 0x040028EA RID: 10474
		public static readonly Dictionary<Material, global::UIMaterial> generatedMaterials = new Dictionary<Material, global::UIMaterial>();

		// Token: 0x040028EB RID: 10475
		public static readonly Dictionary<Material, global::UIMaterial> keyedMaterials = new Dictionary<Material, global::UIMaterial>();
	}

	// Token: 0x0200088A RID: 2186
	private enum ClippingFlags
	{
		// Token: 0x040028ED RID: 10477
		None = 1,
		// Token: 0x040028EE RID: 10478
		HardClip,
		// Token: 0x040028EF RID: 10479
		AlphaClip = 4,
		// Token: 0x040028F0 RID: 10480
		SoftClip = 8
	}
}
