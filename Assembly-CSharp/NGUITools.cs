using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000877 RID: 2167
public static class NGUITools
{
	// Token: 0x06004A6F RID: 19055 RVA: 0x0011F824 File Offset: 0x0011DA24
	public static bool ZeroAlpha(float alpha)
	{
		return (alpha >= 0f) ? (alpha < 0.00196078443f) : (alpha > -0.00196078443f);
	}

	// Token: 0x17000E25 RID: 3621
	// (get) Token: 0x06004A70 RID: 19056 RVA: 0x0011F854 File Offset: 0x0011DA54
	// (set) Token: 0x06004A71 RID: 19057 RVA: 0x0011F880 File Offset: 0x0011DA80
	public static float soundVolume
	{
		get
		{
			if (!global::NGUITools.mLoaded)
			{
				global::NGUITools.mLoaded = true;
				global::NGUITools.mGlobalVolume = PlayerPrefs.GetFloat("Sound", 1f);
			}
			return global::NGUITools.mGlobalVolume;
		}
		set
		{
			if (global::NGUITools.mGlobalVolume != value)
			{
				global::NGUITools.mLoaded = true;
				global::NGUITools.mGlobalVolume = value;
				PlayerPrefs.SetFloat("Sound", value);
			}
		}
	}

	// Token: 0x06004A72 RID: 19058 RVA: 0x0011F8B0 File Offset: 0x0011DAB0
	public static AudioSource PlaySound(AudioClip clip)
	{
		return global::NGUITools.PlaySound(clip, 1f, 1f);
	}

	// Token: 0x06004A73 RID: 19059 RVA: 0x0011F8C4 File Offset: 0x0011DAC4
	public static AudioSource PlaySound(AudioClip clip, float volume)
	{
		return global::NGUITools.PlaySound(clip, volume, 1f);
	}

	// Token: 0x06004A74 RID: 19060 RVA: 0x0011F8D4 File Offset: 0x0011DAD4
	public static AudioSource PlaySound(AudioClip clip, float volume, float pitch)
	{
		volume *= global::NGUITools.soundVolume;
		if (clip != null && volume > 0.01f)
		{
			if (global::NGUITools.mListener == null)
			{
				global::NGUITools.mListener = (Object.FindObjectOfType(typeof(AudioListener)) as AudioListener);
				if (global::NGUITools.mListener == null)
				{
					Camera camera = Camera.main;
					if (camera == null)
					{
						camera = (Object.FindObjectOfType(typeof(Camera)) as Camera);
					}
					if (camera != null)
					{
						global::NGUITools.mListener = camera.gameObject.AddComponent<AudioListener>();
					}
				}
			}
			if (global::NGUITools.mListener != null)
			{
				AudioSource audioSource = global::NGUITools.mListener.audio;
				if (audioSource == null)
				{
					audioSource = global::NGUITools.mListener.gameObject.AddComponent<AudioSource>();
				}
				audioSource.pitch = pitch;
				audioSource.PlayOneShot(clip, volume);
				return audioSource;
			}
		}
		return null;
	}

	// Token: 0x06004A75 RID: 19061 RVA: 0x0011F9C8 File Offset: 0x0011DBC8
	public static WWW OpenURL(string url)
	{
		WWW result = null;
		try
		{
			result = new WWW(url);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
		}
		return result;
	}

	// Token: 0x06004A76 RID: 19062 RVA: 0x0011FA14 File Offset: 0x0011DC14
	public static int RandomRange(int min, int max)
	{
		if (min == max)
		{
			return min;
		}
		return Random.Range(min, max + 1);
	}

	// Token: 0x06004A77 RID: 19063 RVA: 0x0011FA28 File Offset: 0x0011DC28
	public static string GetHierarchy(GameObject obj)
	{
		string text = obj.name;
		while (obj.transform.parent != null)
		{
			obj = obj.transform.parent.gameObject;
			text = obj.name + "/" + text;
		}
		return "\"" + text + "\"";
	}

	// Token: 0x06004A78 RID: 19064 RVA: 0x0011FA8C File Offset: 0x0011DC8C
	public static Color ParseColor(string text, int offset)
	{
		int num = global::NGUIMath.HexToDecimal(text[offset]) << 4 | global::NGUIMath.HexToDecimal(text[offset + 1]);
		int num2 = global::NGUIMath.HexToDecimal(text[offset + 2]) << 4 | global::NGUIMath.HexToDecimal(text[offset + 3]);
		int num3 = global::NGUIMath.HexToDecimal(text[offset + 4]) << 4 | global::NGUIMath.HexToDecimal(text[offset + 5]);
		float num4 = 0.003921569f;
		return new Color(num4 * (float)num, num4 * (float)num2, num4 * (float)num3);
	}

	// Token: 0x06004A79 RID: 19065 RVA: 0x0011FB10 File Offset: 0x0011DD10
	public static string EncodeColor(Color c)
	{
		return (16777215 & global::NGUIMath.ColorToInt(c) >> 8).ToString("X6");
	}

	// Token: 0x06004A7A RID: 19066 RVA: 0x0011FB38 File Offset: 0x0011DD38
	public static string UnformattedString(string str)
	{
		int num = str.IndexOf("[»]");
		int num2 = str.IndexOf("[«]");
		if (num == -1)
		{
			if (num2 == -1)
			{
				return "[»]" + str + "[«]";
			}
			int num3 = 1;
			while (++num2 < str.Length)
			{
				num2 = str.IndexOf("[«]", num2);
				if (num2 == -1)
				{
					break;
				}
				num3++;
			}
			if (num3 == 1)
			{
				return "[»]" + "[»]" + str + "[«]";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[»]");
			while (num3-- > 0)
			{
				stringBuilder.Append("[»]");
			}
			stringBuilder.Append(str);
			stringBuilder.Append("[«]");
			return stringBuilder.ToString();
		}
		else
		{
			if (num2 != -1)
			{
				List<int> list = new List<int>();
				List<bool> list2 = new List<bool>();
				list.Add(num);
				list.Add(num2);
				list2.Add(true);
				list2.Add(false);
				while (++num < str.Length)
				{
					num = str.IndexOf("[«]", num);
					if (num == -1)
					{
						break;
					}
					list.Add(num);
					list2.Add(true);
				}
				while (++num2 < str.Length)
				{
					num2 = str.IndexOf("[«]", num2);
					if (num2 == -1)
					{
						break;
					}
					list.Add(num2);
					list2.Add(false);
				}
				bool[] array = list2.ToArray();
				Array.Sort<int, bool>(list.ToArray(), array);
				int num4 = 0;
				int num5 = 0;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i])
					{
						num4++;
						while (++i < array.Length)
						{
							if (array[i])
							{
								num4++;
							}
							else if (--num4 == 0)
							{
								break;
							}
						}
					}
					else
					{
						num5++;
						while (++i < array.Length)
						{
							if (array[i])
							{
								if (--num5 == 0)
								{
									break;
								}
							}
							else
							{
								num5++;
							}
						}
					}
				}
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("[»]");
				for (int j = 0; j < num5; j++)
				{
					stringBuilder2.Append("[»]");
				}
				stringBuilder2.Append(str);
				for (int k = 0; k < num4; k++)
				{
					stringBuilder2.Append("[«]");
				}
				stringBuilder2.Append("[«]");
				return stringBuilder2.ToString();
			}
			int num6 = 1;
			while (++num < str.Length)
			{
				num = str.IndexOf("[«]", num);
				if (num == -1)
				{
					break;
				}
				num6++;
			}
			if (num6 == 1)
			{
				return "[»]" + str + "[«]" + "[«]";
			}
			StringBuilder stringBuilder3 = new StringBuilder();
			stringBuilder3.Append("[»]");
			stringBuilder3.Append(str);
			while (num6-- > 0)
			{
				stringBuilder3.Append("[«]");
			}
			stringBuilder3.Append("[«]");
			return stringBuilder3.ToString();
		}
	}

	// Token: 0x06004A7B RID: 19067 RVA: 0x0011FEB0 File Offset: 0x0011E0B0
	public static int ParseSymbol(string text, int index, List<Color> colors, ref int symbolSkipCount)
	{
		int length = text.Length;
		if (index + 2 < length)
		{
			if (text[index + 2] == ']')
			{
				if (text[index + 1] == '-')
				{
					if (symbolSkipCount == 0)
					{
						if (colors != null && colors.Count > 1)
						{
							colors.RemoveAt(colors.Count - 1);
						}
						return 3;
					}
				}
				else if (text[index + 1] == '»')
				{
					if (symbolSkipCount++ == 0)
					{
						return 3;
					}
				}
				else if (text[index + 1] == '«' && --symbolSkipCount == 0)
				{
					return 3;
				}
			}
			else if (index + 7 < length && text[index + 7] == ']' && symbolSkipCount == 0)
			{
				if (colors != null)
				{
					Color item = global::NGUITools.ParseColor(text, index + 1);
					item.a = colors[colors.Count - 1].a;
					colors.Add(item);
				}
				return 8;
			}
		}
		return 0;
	}

	// Token: 0x06004A7C RID: 19068 RVA: 0x0011FFC4 File Offset: 0x0011E1C4
	public static string StripSymbols(string text)
	{
		if (text != null)
		{
			text = text.Replace("\\n", "\n");
			int num = 0;
			int i = 0;
			int length = text.Length;
			while (i < length)
			{
				char c = text[i];
				if (c == '[')
				{
					int num2 = global::NGUITools.ParseSymbol(text, i, null, ref num);
					if (num2 > 0)
					{
						text = text.Remove(i, num2);
						length = text.Length;
						continue;
					}
				}
				i++;
			}
		}
		return text;
	}

	// Token: 0x06004A7D RID: 19069 RVA: 0x00120040 File Offset: 0x0011E240
	public static T[] FindActive<T>() where T : Component
	{
		return Object.FindObjectsOfType(typeof(T)) as T[];
	}

	// Token: 0x06004A7E RID: 19070 RVA: 0x00120058 File Offset: 0x0011E258
	public static Camera FindCameraForLayer(int layer)
	{
		int num = 1 << layer;
		Camera[] array = global::NGUITools.FindActive<Camera>();
		int i = 0;
		int num2 = array.Length;
		while (i < num2)
		{
			Camera camera = array[i];
			if ((camera.cullingMask & num) != 0)
			{
				return camera;
			}
			i++;
		}
		return null;
	}

	// Token: 0x06004A7F RID: 19071 RVA: 0x001200A0 File Offset: 0x0011E2A0
	[Obsolete("Use AddWidgetHotSpot")]
	public static BoxCollider AddWidgetCollider(GameObject go)
	{
		if (go != null)
		{
			Collider component = go.GetComponent<Collider>();
			BoxCollider boxCollider = component as BoxCollider;
			if (boxCollider == null)
			{
				if (component != null)
				{
					if (Application.isPlaying)
					{
						Object.Destroy(component);
					}
					else
					{
						Object.DestroyImmediate(component);
					}
				}
				boxCollider = go.AddComponent<BoxCollider>();
			}
			int num = global::NGUITools.CalculateNextDepth(go);
			global::AABBox aabbox = global::NGUIMath.CalculateRelativeWidgetBounds(go.transform);
			boxCollider.isTrigger = true;
			boxCollider.center = aabbox.center + Vector3.back * ((float)num * 0.25f);
			boxCollider.size = new Vector3(aabbox.size.x, aabbox.size.y, 0f);
			return boxCollider;
		}
		return null;
	}

	// Token: 0x06004A80 RID: 19072 RVA: 0x00120174 File Offset: 0x0011E374
	private static void ColliderDestroy(Collider component)
	{
		if (Application.isPlaying)
		{
			Object.Destroy(component);
		}
		else
		{
			Object.DestroyImmediate(component);
		}
	}

	// Token: 0x06004A81 RID: 19073 RVA: 0x00120194 File Offset: 0x0011E394
	private static global::UIBoxHotSpot ColliderToHotSpotBox(BoxCollider collider, bool nullChecked)
	{
		if (nullChecked || collider)
		{
			Vector3 center = collider.center;
			Vector3 size = collider.size;
			GameObject gameObject = collider.gameObject;
			bool enabled = collider.enabled;
			global::NGUITools.ColliderDestroy(collider);
			global::UIBoxHotSpot uiboxHotSpot = gameObject.AddComponent<global::UIBoxHotSpot>();
			uiboxHotSpot.center = center;
			uiboxHotSpot.size = size;
			uiboxHotSpot.enabled = enabled;
			return uiboxHotSpot;
		}
		return null;
	}

	// Token: 0x06004A82 RID: 19074 RVA: 0x001201F8 File Offset: 0x0011E3F8
	public static global::UIBoxHotSpot ColliderToHotSpotBox(BoxCollider collider)
	{
		return global::NGUITools.ColliderToHotSpotBox(collider, false);
	}

	// Token: 0x06004A83 RID: 19075 RVA: 0x00120204 File Offset: 0x0011E404
	private static global::UIRectHotSpot ColliderToHotSpotRect(BoxCollider collider, bool nullChecked)
	{
		if (nullChecked || collider)
		{
			Vector3 center = collider.center;
			Vector2 size = collider.size;
			GameObject gameObject = collider.gameObject;
			bool enabled = collider.enabled;
			global::NGUITools.ColliderDestroy(collider);
			global::UIRectHotSpot uirectHotSpot = gameObject.AddComponent<global::UIRectHotSpot>();
			uirectHotSpot.center = center;
			uirectHotSpot.size = size;
			uirectHotSpot.enabled = enabled;
			return uirectHotSpot;
		}
		return null;
	}

	// Token: 0x06004A84 RID: 19076 RVA: 0x00120270 File Offset: 0x0011E470
	public static global::UIRectHotSpot ColliderToHotSpotRect(BoxCollider collider)
	{
		return global::NGUITools.ColliderToHotSpotRect(collider, false);
	}

	// Token: 0x06004A85 RID: 19077 RVA: 0x0012027C File Offset: 0x0011E47C
	private static global::UIHotSpot ColliderToHotSpot(BoxCollider collider, bool nullChecked)
	{
		if (!nullChecked && !collider)
		{
			return null;
		}
		Vector3 center = collider.center;
		Vector3 size = collider.size;
		GameObject gameObject = collider.gameObject;
		bool enabled = collider.enabled;
		global::NGUITools.ColliderDestroy(collider);
		if (size.z <= 0.001f)
		{
			global::UIRectHotSpot uirectHotSpot = gameObject.AddComponent<global::UIRectHotSpot>();
			uirectHotSpot.center = center;
			uirectHotSpot.size = size;
			uirectHotSpot.enabled = enabled;
			return uirectHotSpot;
		}
		global::UIBoxHotSpot uiboxHotSpot = gameObject.AddComponent<global::UIBoxHotSpot>();
		uiboxHotSpot.center = center;
		uiboxHotSpot.size = size;
		uiboxHotSpot.enabled = enabled;
		return uiboxHotSpot;
	}

	// Token: 0x06004A86 RID: 19078 RVA: 0x0012031C File Offset: 0x0011E51C
	public static global::UIHotSpot ColliderToHotSpot(BoxCollider collider)
	{
		return global::NGUITools.ColliderToHotSpot(collider, false);
	}

	// Token: 0x06004A87 RID: 19079 RVA: 0x00120328 File Offset: 0x0011E528
	private static global::UIHotSpot ColliderToHotSpot(Collider collider, bool nullChecked)
	{
		if (!nullChecked && !collider)
		{
			return null;
		}
		if (collider is BoxCollider)
		{
			return global::NGUITools.ColliderToHotSpot((BoxCollider)collider);
		}
		if (collider is SphereCollider)
		{
			return global::NGUITools.ColliderToHotSpot((SphereCollider)collider);
		}
		if (collider is TerrainCollider)
		{
			Debug.Log("Sorry not going to convert a terrain collider.. that sounds destructive.", collider);
			return null;
		}
		Bounds bounds = collider.bounds;
		Matrix4x4 worldToLocalMatrix = collider.transform.worldToLocalMatrix;
		Bounds bounds2;
		global::AABBox.Transform3x4(ref bounds, ref worldToLocalMatrix, out bounds2);
		bool enabled = collider.enabled;
		GameObject gameObject = collider.gameObject;
		global::NGUITools.ColliderDestroy(collider);
		Vector3 size = bounds2.size;
		if (size.z <= 0.001f)
		{
			global::UIRectHotSpot uirectHotSpot = gameObject.AddComponent<global::UIRectHotSpot>();
			uirectHotSpot.size = size;
			uirectHotSpot.center = bounds2.center;
			uirectHotSpot.enabled = enabled;
			return uirectHotSpot;
		}
		global::UIBoxHotSpot uiboxHotSpot = gameObject.AddComponent<global::UIBoxHotSpot>();
		uiboxHotSpot.size = size;
		uiboxHotSpot.center = bounds2.center;
		uiboxHotSpot.enabled = enabled;
		return uiboxHotSpot;
	}

	// Token: 0x06004A88 RID: 19080 RVA: 0x00120438 File Offset: 0x0011E638
	public static global::UIHotSpot ColliderToHotSpot(Collider collider)
	{
		return global::NGUITools.ColliderToHotSpot(collider, false);
	}

	// Token: 0x06004A89 RID: 19081 RVA: 0x00120444 File Offset: 0x0011E644
	public static global::UIHotSpot AddWidgetHotSpot(GameObject go)
	{
		if (!(go != null))
		{
			return null;
		}
		Collider collider = go.collider;
		if (!collider)
		{
			global::UIHotSpot component = go.GetComponent<global::UIHotSpot>();
			int num;
			global::AABBox aabbox;
			if (component)
			{
				if (component.isRect)
				{
					global::UIRectHotSpot asRect = component.asRect;
					num = global::NGUITools.CalculateNextDepth(go);
					aabbox = global::NGUIMath.CalculateRelativeWidgetBounds(go.transform);
					asRect.size = aabbox.size;
					asRect.center = aabbox.center + Vector3.back * ((float)num * 0.25f);
					return asRect;
				}
				if (Application.isPlaying)
				{
					Object.Destroy(component);
				}
				else
				{
					Object.DestroyImmediate(component);
				}
			}
			num = global::NGUITools.CalculateNextDepth(go);
			aabbox = global::NGUIMath.CalculateRelativeWidgetBounds(go.transform);
			global::UIRectHotSpot uirectHotSpot = go.AddComponent<global::UIRectHotSpot>();
			uirectHotSpot.size = aabbox.size;
			uirectHotSpot.center = aabbox.center + Vector3.back * ((float)num * 0.25f);
			return uirectHotSpot;
		}
		global::UIHotSpot uihotSpot = global::NGUITools.ColliderToHotSpot(collider, true);
		if (!uihotSpot)
		{
			return null;
		}
		return uihotSpot;
	}

	// Token: 0x06004A8A RID: 19082 RVA: 0x00120578 File Offset: 0x0011E778
	[Obsolete("Use UIAtlas.replacement instead")]
	public static void ReplaceAtlas(global::UIAtlas before, global::UIAtlas after)
	{
		global::UISprite[] array = global::NGUITools.FindActive<global::UISprite>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			global::UISprite uisprite = array[i];
			if (uisprite.atlas == before)
			{
				uisprite.atlas = after;
			}
			i++;
		}
		global::UILabel[] array2 = global::NGUITools.FindActive<global::UILabel>();
		int j = 0;
		int num2 = array2.Length;
		while (j < num2)
		{
			global::UILabel uilabel = array2[j];
			if (uilabel.font != null && uilabel.font.atlas == before)
			{
				uilabel.font.atlas = after;
			}
			j++;
		}
	}

	// Token: 0x06004A8B RID: 19083 RVA: 0x00120620 File Offset: 0x0011E820
	[Obsolete("Use UIFont.replacement instead")]
	public static void ReplaceFont(global::UIFont before, global::UIFont after)
	{
		global::UILabel[] array = global::NGUITools.FindActive<global::UILabel>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			global::UILabel uilabel = array[i];
			if (uilabel.font == before)
			{
				uilabel.font = after;
			}
			i++;
		}
	}

	// Token: 0x06004A8C RID: 19084 RVA: 0x00120668 File Offset: 0x0011E868
	public static string GetName<T>() where T : Component
	{
		string text = typeof(T).ToString();
		if (text.StartsWith("UI"))
		{
			text = text.Substring(2);
		}
		else if (text.StartsWith("UnityEngine."))
		{
			text = text.Substring(12);
		}
		return text;
	}

	// Token: 0x06004A8D RID: 19085 RVA: 0x001206BC File Offset: 0x0011E8BC
	public static GameObject AddChild(GameObject parent)
	{
		GameObject gameObject = new GameObject();
		if (parent != null)
		{
			Transform transform = gameObject.transform;
			transform.parent = parent.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			gameObject.layer = parent.layer;
		}
		return gameObject;
	}

	// Token: 0x06004A8E RID: 19086 RVA: 0x0012071C File Offset: 0x0011E91C
	public static GameObject AddChild(GameObject parent, GameObject prefab)
	{
		GameObject gameObject = Object.Instantiate(prefab) as GameObject;
		if (gameObject != null && parent != null)
		{
			Transform transform = gameObject.transform;
			transform.parent = parent.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			gameObject.layer = parent.layer;
		}
		return gameObject;
	}

	// Token: 0x06004A8F RID: 19087 RVA: 0x00120790 File Offset: 0x0011E990
	public static int CalculateNextDepth(GameObject go)
	{
		int num = -1;
		global::UIWidget[] componentsInChildren = go.GetComponentsInChildren<global::UIWidget>();
		int i = 0;
		int num2 = componentsInChildren.Length;
		while (i < num2)
		{
			num = Mathf.Max(num, componentsInChildren[i].depth);
			i++;
		}
		return num + 1;
	}

	// Token: 0x06004A90 RID: 19088 RVA: 0x001207D0 File Offset: 0x0011E9D0
	public static T AddChild<T>(GameObject parent) where T : Component
	{
		GameObject gameObject = global::NGUITools.AddChild(parent);
		gameObject.name = global::NGUITools.GetName<T>();
		return gameObject.AddComponent<T>();
	}

	// Token: 0x06004A91 RID: 19089 RVA: 0x001207F8 File Offset: 0x0011E9F8
	public static T AddWidget<T>(GameObject go) where T : global::UIWidget
	{
		int depth = global::NGUITools.CalculateNextDepth(go);
		T result = global::NGUITools.AddChild<T>(go);
		result.depth = depth;
		Transform transform = result.transform;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		transform.localScale = new Vector3(100f, 100f, 1f);
		result.gameObject.layer = go.layer;
		return result;
	}

	// Token: 0x06004A92 RID: 19090 RVA: 0x00120878 File Offset: 0x0011EA78
	public static global::UISprite AddSprite(GameObject go, global::UIAtlas atlas, string spriteName)
	{
		global::UIAtlas.Sprite sprite = (!(atlas != null)) ? null : atlas.GetSprite(spriteName);
		global::UISprite uisprite = (sprite != null && !(sprite.inner == sprite.outer)) ? global::NGUITools.AddWidget<global::UISlicedSprite>(go) : global::NGUITools.AddWidget<global::UISprite>(go);
		uisprite.atlas = atlas;
		uisprite.spriteName = spriteName;
		return uisprite;
	}

	// Token: 0x06004A93 RID: 19091 RVA: 0x001208DC File Offset: 0x0011EADC
	public static T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null)
		{
			return (T)((object)null);
		}
		object obj = go.GetComponent<T>();
		if (obj == null)
		{
			Transform parent = go.transform.parent;
			while (parent != null && obj == null)
			{
				obj = parent.gameObject.GetComponent<T>();
				parent = parent.parent;
			}
		}
		return (T)((object)obj);
	}

	// Token: 0x06004A94 RID: 19092 RVA: 0x00120950 File Offset: 0x0011EB50
	public static void Destroy(Object obj)
	{
		if (obj != null)
		{
			if (Application.isPlaying)
			{
				Object.Destroy(obj);
			}
			else
			{
				Object.DestroyImmediate(obj);
			}
		}
	}

	// Token: 0x06004A95 RID: 19093 RVA: 0x0012097C File Offset: 0x0011EB7C
	public static void DestroyImmediate(Object obj)
	{
		if (obj != null)
		{
			if (Application.isEditor)
			{
				Object.DestroyImmediate(obj);
			}
			else
			{
				Object.Destroy(obj);
			}
		}
	}

	// Token: 0x06004A96 RID: 19094 RVA: 0x001209A8 File Offset: 0x0011EBA8
	public static void Broadcast(string funcName)
	{
		GameObject[] array = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			array[i].SendMessage(funcName, 1);
			i++;
		}
	}

	// Token: 0x06004A97 RID: 19095 RVA: 0x001209EC File Offset: 0x0011EBEC
	public static void Broadcast(string funcName, object param)
	{
		GameObject[] array = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			array[i].SendMessage(funcName, param, 1);
			i++;
		}
	}

	// Token: 0x06004A98 RID: 19096 RVA: 0x00120A30 File Offset: 0x0011EC30
	public static bool IsChild(Transform parent, Transform child)
	{
		if (parent == null || child == null)
		{
			return false;
		}
		while (child != null)
		{
			if (child == parent)
			{
				return true;
			}
			child = child.parent;
		}
		return false;
	}

	// Token: 0x06004A99 RID: 19097 RVA: 0x00120A80 File Offset: 0x0011EC80
	private static void Activate(Transform t)
	{
		t.gameObject.SetActive(true);
	}

	// Token: 0x06004A9A RID: 19098 RVA: 0x00120A90 File Offset: 0x0011EC90
	private static void Deactivate(Transform t)
	{
		t.gameObject.SetActive(false);
	}

	// Token: 0x06004A9B RID: 19099 RVA: 0x00120AA0 File Offset: 0x0011ECA0
	public static void SetActive(GameObject go, bool state)
	{
		if (state)
		{
			global::NGUITools.Activate(go.transform);
		}
		else
		{
			global::NGUITools.Deactivate(go.transform);
		}
	}

	// Token: 0x06004A9C RID: 19100 RVA: 0x00120AC4 File Offset: 0x0011ECC4
	public static Vector3 Round(Vector3 v)
	{
		v.x = Mathf.Round(v.x);
		v.y = Mathf.Round(v.y);
		v.z = Mathf.Round(v.z);
		return v;
	}

	// Token: 0x06004A9D RID: 19101 RVA: 0x00120B0C File Offset: 0x0011ED0C
	public static void MakePixelPerfect(Transform t)
	{
		global::UIWidget component = t.GetComponent<global::UIWidget>();
		if (component != null)
		{
			component.MakePixelPerfect();
		}
		else
		{
			t.localPosition = global::NGUITools.Round(t.localPosition);
			t.localScale = global::NGUITools.Round(t.localScale);
			int i = 0;
			int childCount = t.childCount;
			while (i < childCount)
			{
				global::NGUITools.MakePixelPerfect(t.GetChild(i));
				i++;
			}
		}
	}

	// Token: 0x06004A9E RID: 19102 RVA: 0x00120B80 File Offset: 0x0011ED80
	public static bool SetAllowClick(Component self, bool allow)
	{
		Collider collider = self.collider;
		if (collider)
		{
			collider.enabled = allow;
			return true;
		}
		global::UIHotSpot component = self.GetComponent<global::UIHotSpot>();
		if (component)
		{
			component.enabled = allow;
			return true;
		}
		return false;
	}

	// Token: 0x06004A9F RID: 19103 RVA: 0x00120BC4 File Offset: 0x0011EDC4
	public static bool GetAllowClick(MonoBehaviour self, out bool possible)
	{
		Collider collider = self.collider;
		if (collider)
		{
			possible = true;
			return collider.enabled;
		}
		global::UIHotSpot component = self.GetComponent<global::UIHotSpot>();
		if (component)
		{
			possible = true;
			return component.enabled;
		}
		possible = false;
		return false;
	}

	// Token: 0x06004AA0 RID: 19104 RVA: 0x00120C10 File Offset: 0x0011EE10
	public static bool GetAllowClick(MonoBehaviour self)
	{
		bool flag;
		return global::NGUITools.GetAllowClick(self, out flag);
	}

	// Token: 0x06004AA1 RID: 19105 RVA: 0x00120C28 File Offset: 0x0011EE28
	public static void SetAllowClickChildren(GameObject mChild, bool par1)
	{
		Collider[] componentsInChildren = mChild.GetComponentsInChildren<Collider>();
		int i = 0;
		int num = componentsInChildren.Length;
		while (i < num)
		{
			componentsInChildren[i].enabled = false;
			i++;
		}
		global::UIHotSpot[] componentsInChildren2 = mChild.GetComponentsInChildren<global::UIHotSpot>();
		int j = 0;
		int num2 = componentsInChildren2.Length;
		while (j < num2)
		{
			componentsInChildren2[j].enabled = false;
			j++;
		}
	}

	// Token: 0x06004AA2 RID: 19106 RVA: 0x00120C88 File Offset: 0x0011EE88
	public static bool HasMeansOfClicking(Component self)
	{
		return self.collider || self.GetComponent<global::UIHotSpot>();
	}

	// Token: 0x06004AA3 RID: 19107 RVA: 0x00120CA8 File Offset: 0x0011EEA8
	public static bool GetCentroid(Component cell, out Vector3 centroid)
	{
		if (cell is Collider)
		{
			centroid = ((Collider)cell).bounds.center;
		}
		else if (cell is global::UIHotSpot)
		{
			centroid = ((global::UIHotSpot)cell).worldCenter;
		}
		else
		{
			global::UIHotSpot component = cell.GetComponent<global::UIHotSpot>();
			if (component)
			{
				centroid = component.worldCenter;
				return true;
			}
			Collider collider = cell.collider;
			if (collider)
			{
				centroid = collider.bounds.center;
				return true;
			}
			centroid = Vector3.zero;
			return false;
		}
		return true;
	}

	// Token: 0x06004AA4 RID: 19108 RVA: 0x00120D58 File Offset: 0x0011EF58
	public static TComponent QuickGet<TComponent>(GameObject gameObject) where TComponent : Component
	{
		switch (global::NGUITools.SG<TComponent>.V)
		{
		case global::NGUITools.SlipGate.Renderer:
			return gameObject.renderer as TComponent;
		case global::NGUITools.SlipGate.Collider:
			return gameObject.collider as TComponent;
		case global::NGUITools.SlipGate.Transform:
			return gameObject.transform as TComponent;
		}
		return gameObject.GetComponent<TComponent>();
	}

	// Token: 0x06004AA5 RID: 19109 RVA: 0x00120DD0 File Offset: 0x0011EFD0
	public static TComponent GetOrAddComponent<TComponent>(GameObject gameObject) where TComponent : Component
	{
		TComponent tcomponent = global::NGUITools.QuickGet<TComponent>(gameObject);
		return (!tcomponent) ? gameObject.AddComponent<TComponent>() : tcomponent;
	}

	// Token: 0x06004AA6 RID: 19110 RVA: 0x00120E00 File Offset: 0x0011F000
	public static TComponent GetOrAddComponent<TComponent>(Component component) where TComponent : Component
	{
		if (component is TComponent)
		{
			return (TComponent)((object)component);
		}
		return global::NGUITools.GetOrAddComponent<TComponent>(component.gameObject);
	}

	// Token: 0x06004AA7 RID: 19111 RVA: 0x00120E20 File Offset: 0x0011F020
	public static bool GetOrAddComponent<TComponent>(GameObject gameObject, ref TComponent value) where TComponent : Component
	{
		return (!value) ? (value = global::NGUITools.GetOrAddComponent<TComponent>(gameObject)) : value;
	}

	// Token: 0x06004AA8 RID: 19112 RVA: 0x00120E68 File Offset: 0x0011F068
	public static bool GetOrAddComponent<TComponent>(Component component, ref TComponent value) where TComponent : Component
	{
		return (!value) ? (value = global::NGUITools.GetOrAddComponent<TComponent>(component)) : value;
	}

	// Token: 0x04002893 RID: 10387
	public const float kMinimumAlpha = 0.00196078443f;

	// Token: 0x04002894 RID: 10388
	public const float kMaximumNegativeAlpha = -0.00196078443f;

	// Token: 0x04002895 RID: 10389
	public const string kFormattingOffDisableSymbol = "[«]";

	// Token: 0x04002896 RID: 10390
	public const string kFormattingOffEnableSymbol = "[»]";

	// Token: 0x04002897 RID: 10391
	public const char kFormattingOffDisableCharacter = '«';

	// Token: 0x04002898 RID: 10392
	public const char kFormattingOffEnableCharacter = '»';

	// Token: 0x04002899 RID: 10393
	private static AudioListener mListener;

	// Token: 0x0400289A RID: 10394
	private static bool mLoaded = false;

	// Token: 0x0400289B RID: 10395
	private static float mGlobalVolume = 1f;

	// Token: 0x0400289C RID: 10396
	private static readonly string[] kFormattingOffSymbols = new string[]
	{
		"[»]",
		"[«]"
	};

	// Token: 0x02000878 RID: 2168
	private enum SlipGate
	{
		// Token: 0x0400289E RID: 10398
		Renderer,
		// Token: 0x0400289F RID: 10399
		Collider,
		// Token: 0x040028A0 RID: 10400
		Behaviour,
		// Token: 0x040028A1 RID: 10401
		Transform,
		// Token: 0x040028A2 RID: 10402
		Component
	}

	// Token: 0x02000879 RID: 2169
	private static class SG<T> where T : Component
	{
		// Token: 0x06004AA9 RID: 19113 RVA: 0x00120EB0 File Offset: 0x0011F0B0
		static SG()
		{
			if (typeof(Renderer).IsAssignableFrom(typeof(T)))
			{
				global::NGUITools.SG<T>.V = global::NGUITools.SlipGate.Renderer;
			}
			else if (typeof(Collider).IsAssignableFrom(typeof(T)))
			{
				global::NGUITools.SG<T>.V = global::NGUITools.SlipGate.Collider;
			}
			else if (typeof(Behaviour).IsAssignableFrom(typeof(T)))
			{
				global::NGUITools.SG<T>.V = global::NGUITools.SlipGate.Behaviour;
			}
			else if (typeof(Transform).IsAssignableFrom(typeof(T)))
			{
				global::NGUITools.SG<T>.V = global::NGUITools.SlipGate.Transform;
			}
			else
			{
				global::NGUITools.SG<T>.V = global::NGUITools.SlipGate.Component;
			}
		}

		// Token: 0x040028A3 RID: 10403
		public static readonly global::NGUITools.SlipGate V;
	}
}
