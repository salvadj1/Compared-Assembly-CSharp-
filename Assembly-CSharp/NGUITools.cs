using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000792 RID: 1938
public static class NGUITools
{
	// Token: 0x06004602 RID: 17922 RVA: 0x00115EA4 File Offset: 0x001140A4
	public static bool ZeroAlpha(float alpha)
	{
		return (alpha >= 0f) ? (alpha < 0.00196078443f) : (alpha > -0.00196078443f);
	}

	// Token: 0x17000D95 RID: 3477
	// (get) Token: 0x06004603 RID: 17923 RVA: 0x00115ED4 File Offset: 0x001140D4
	// (set) Token: 0x06004604 RID: 17924 RVA: 0x00115F00 File Offset: 0x00114100
	public static float soundVolume
	{
		get
		{
			if (!NGUITools.mLoaded)
			{
				NGUITools.mLoaded = true;
				NGUITools.mGlobalVolume = PlayerPrefs.GetFloat("Sound", 1f);
			}
			return NGUITools.mGlobalVolume;
		}
		set
		{
			if (NGUITools.mGlobalVolume != value)
			{
				NGUITools.mLoaded = true;
				NGUITools.mGlobalVolume = value;
				PlayerPrefs.SetFloat("Sound", value);
			}
		}
	}

	// Token: 0x06004605 RID: 17925 RVA: 0x00115F30 File Offset: 0x00114130
	public static AudioSource PlaySound(AudioClip clip)
	{
		return NGUITools.PlaySound(clip, 1f, 1f);
	}

	// Token: 0x06004606 RID: 17926 RVA: 0x00115F44 File Offset: 0x00114144
	public static AudioSource PlaySound(AudioClip clip, float volume)
	{
		return NGUITools.PlaySound(clip, volume, 1f);
	}

	// Token: 0x06004607 RID: 17927 RVA: 0x00115F54 File Offset: 0x00114154
	public static AudioSource PlaySound(AudioClip clip, float volume, float pitch)
	{
		volume *= NGUITools.soundVolume;
		if (clip != null && volume > 0.01f)
		{
			if (NGUITools.mListener == null)
			{
				NGUITools.mListener = (Object.FindObjectOfType(typeof(AudioListener)) as AudioListener);
				if (NGUITools.mListener == null)
				{
					Camera camera = Camera.main;
					if (camera == null)
					{
						camera = (Object.FindObjectOfType(typeof(Camera)) as Camera);
					}
					if (camera != null)
					{
						NGUITools.mListener = camera.gameObject.AddComponent<AudioListener>();
					}
				}
			}
			if (NGUITools.mListener != null)
			{
				AudioSource audioSource = NGUITools.mListener.audio;
				if (audioSource == null)
				{
					audioSource = NGUITools.mListener.gameObject.AddComponent<AudioSource>();
				}
				audioSource.pitch = pitch;
				audioSource.PlayOneShot(clip, volume);
				return audioSource;
			}
		}
		return null;
	}

	// Token: 0x06004608 RID: 17928 RVA: 0x00116048 File Offset: 0x00114248
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

	// Token: 0x06004609 RID: 17929 RVA: 0x00116094 File Offset: 0x00114294
	public static int RandomRange(int min, int max)
	{
		if (min == max)
		{
			return min;
		}
		return Random.Range(min, max + 1);
	}

	// Token: 0x0600460A RID: 17930 RVA: 0x001160A8 File Offset: 0x001142A8
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

	// Token: 0x0600460B RID: 17931 RVA: 0x0011610C File Offset: 0x0011430C
	public static Color ParseColor(string text, int offset)
	{
		int num = NGUIMath.HexToDecimal(text[offset]) << 4 | NGUIMath.HexToDecimal(text[offset + 1]);
		int num2 = NGUIMath.HexToDecimal(text[offset + 2]) << 4 | NGUIMath.HexToDecimal(text[offset + 3]);
		int num3 = NGUIMath.HexToDecimal(text[offset + 4]) << 4 | NGUIMath.HexToDecimal(text[offset + 5]);
		float num4 = 0.003921569f;
		return new Color(num4 * (float)num, num4 * (float)num2, num4 * (float)num3);
	}

	// Token: 0x0600460C RID: 17932 RVA: 0x00116190 File Offset: 0x00114390
	public static string EncodeColor(Color c)
	{
		return (16777215 & NGUIMath.ColorToInt(c) >> 8).ToString("X6");
	}

	// Token: 0x0600460D RID: 17933 RVA: 0x001161B8 File Offset: 0x001143B8
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

	// Token: 0x0600460E RID: 17934 RVA: 0x00116530 File Offset: 0x00114730
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
					Color item = NGUITools.ParseColor(text, index + 1);
					item.a = colors[colors.Count - 1].a;
					colors.Add(item);
				}
				return 8;
			}
		}
		return 0;
	}

	// Token: 0x0600460F RID: 17935 RVA: 0x00116644 File Offset: 0x00114844
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
					int num2 = NGUITools.ParseSymbol(text, i, null, ref num);
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

	// Token: 0x06004610 RID: 17936 RVA: 0x001166C0 File Offset: 0x001148C0
	public static T[] FindActive<T>() where T : Component
	{
		return Object.FindObjectsOfType(typeof(T)) as T[];
	}

	// Token: 0x06004611 RID: 17937 RVA: 0x001166D8 File Offset: 0x001148D8
	public static Camera FindCameraForLayer(int layer)
	{
		int num = 1 << layer;
		Camera[] array = NGUITools.FindActive<Camera>();
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

	// Token: 0x06004612 RID: 17938 RVA: 0x00116720 File Offset: 0x00114920
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
			int num = NGUITools.CalculateNextDepth(go);
			AABBox aabbox = NGUIMath.CalculateRelativeWidgetBounds(go.transform);
			boxCollider.isTrigger = true;
			boxCollider.center = aabbox.center + Vector3.back * ((float)num * 0.25f);
			boxCollider.size = new Vector3(aabbox.size.x, aabbox.size.y, 0f);
			return boxCollider;
		}
		return null;
	}

	// Token: 0x06004613 RID: 17939 RVA: 0x001167F4 File Offset: 0x001149F4
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

	// Token: 0x06004614 RID: 17940 RVA: 0x00116814 File Offset: 0x00114A14
	private static UIBoxHotSpot ColliderToHotSpotBox(BoxCollider collider, bool nullChecked)
	{
		if (nullChecked || collider)
		{
			Vector3 center = collider.center;
			Vector3 size = collider.size;
			GameObject gameObject = collider.gameObject;
			bool enabled = collider.enabled;
			NGUITools.ColliderDestroy(collider);
			UIBoxHotSpot uiboxHotSpot = gameObject.AddComponent<UIBoxHotSpot>();
			uiboxHotSpot.center = center;
			uiboxHotSpot.size = size;
			uiboxHotSpot.enabled = enabled;
			return uiboxHotSpot;
		}
		return null;
	}

	// Token: 0x06004615 RID: 17941 RVA: 0x00116878 File Offset: 0x00114A78
	public static UIBoxHotSpot ColliderToHotSpotBox(BoxCollider collider)
	{
		return NGUITools.ColliderToHotSpotBox(collider, false);
	}

	// Token: 0x06004616 RID: 17942 RVA: 0x00116884 File Offset: 0x00114A84
	private static UIRectHotSpot ColliderToHotSpotRect(BoxCollider collider, bool nullChecked)
	{
		if (nullChecked || collider)
		{
			Vector3 center = collider.center;
			Vector2 size = collider.size;
			GameObject gameObject = collider.gameObject;
			bool enabled = collider.enabled;
			NGUITools.ColliderDestroy(collider);
			UIRectHotSpot uirectHotSpot = gameObject.AddComponent<UIRectHotSpot>();
			uirectHotSpot.center = center;
			uirectHotSpot.size = size;
			uirectHotSpot.enabled = enabled;
			return uirectHotSpot;
		}
		return null;
	}

	// Token: 0x06004617 RID: 17943 RVA: 0x001168F0 File Offset: 0x00114AF0
	public static UIRectHotSpot ColliderToHotSpotRect(BoxCollider collider)
	{
		return NGUITools.ColliderToHotSpotRect(collider, false);
	}

	// Token: 0x06004618 RID: 17944 RVA: 0x001168FC File Offset: 0x00114AFC
	private static UIHotSpot ColliderToHotSpot(BoxCollider collider, bool nullChecked)
	{
		if (!nullChecked && !collider)
		{
			return null;
		}
		Vector3 center = collider.center;
		Vector3 size = collider.size;
		GameObject gameObject = collider.gameObject;
		bool enabled = collider.enabled;
		NGUITools.ColliderDestroy(collider);
		if (size.z <= 0.001f)
		{
			UIRectHotSpot uirectHotSpot = gameObject.AddComponent<UIRectHotSpot>();
			uirectHotSpot.center = center;
			uirectHotSpot.size = size;
			uirectHotSpot.enabled = enabled;
			return uirectHotSpot;
		}
		UIBoxHotSpot uiboxHotSpot = gameObject.AddComponent<UIBoxHotSpot>();
		uiboxHotSpot.center = center;
		uiboxHotSpot.size = size;
		uiboxHotSpot.enabled = enabled;
		return uiboxHotSpot;
	}

	// Token: 0x06004619 RID: 17945 RVA: 0x0011699C File Offset: 0x00114B9C
	public static UIHotSpot ColliderToHotSpot(BoxCollider collider)
	{
		return NGUITools.ColliderToHotSpot(collider, false);
	}

	// Token: 0x0600461A RID: 17946 RVA: 0x001169A8 File Offset: 0x00114BA8
	private static UIHotSpot ColliderToHotSpot(Collider collider, bool nullChecked)
	{
		if (!nullChecked && !collider)
		{
			return null;
		}
		if (collider is BoxCollider)
		{
			return NGUITools.ColliderToHotSpot((BoxCollider)collider);
		}
		if (collider is SphereCollider)
		{
			return NGUITools.ColliderToHotSpot((SphereCollider)collider);
		}
		if (collider is TerrainCollider)
		{
			Debug.Log("Sorry not going to convert a terrain collider.. that sounds destructive.", collider);
			return null;
		}
		Bounds bounds = collider.bounds;
		Matrix4x4 worldToLocalMatrix = collider.transform.worldToLocalMatrix;
		Bounds bounds2;
		AABBox.Transform3x4(ref bounds, ref worldToLocalMatrix, out bounds2);
		bool enabled = collider.enabled;
		GameObject gameObject = collider.gameObject;
		NGUITools.ColliderDestroy(collider);
		Vector3 size = bounds2.size;
		if (size.z <= 0.001f)
		{
			UIRectHotSpot uirectHotSpot = gameObject.AddComponent<UIRectHotSpot>();
			uirectHotSpot.size = size;
			uirectHotSpot.center = bounds2.center;
			uirectHotSpot.enabled = enabled;
			return uirectHotSpot;
		}
		UIBoxHotSpot uiboxHotSpot = gameObject.AddComponent<UIBoxHotSpot>();
		uiboxHotSpot.size = size;
		uiboxHotSpot.center = bounds2.center;
		uiboxHotSpot.enabled = enabled;
		return uiboxHotSpot;
	}

	// Token: 0x0600461B RID: 17947 RVA: 0x00116AB8 File Offset: 0x00114CB8
	public static UIHotSpot ColliderToHotSpot(Collider collider)
	{
		return NGUITools.ColliderToHotSpot(collider, false);
	}

	// Token: 0x0600461C RID: 17948 RVA: 0x00116AC4 File Offset: 0x00114CC4
	public static UIHotSpot AddWidgetHotSpot(GameObject go)
	{
		if (!(go != null))
		{
			return null;
		}
		Collider collider = go.collider;
		if (!collider)
		{
			UIHotSpot component = go.GetComponent<UIHotSpot>();
			int num;
			AABBox aabbox;
			if (component)
			{
				if (component.isRect)
				{
					UIRectHotSpot asRect = component.asRect;
					num = NGUITools.CalculateNextDepth(go);
					aabbox = NGUIMath.CalculateRelativeWidgetBounds(go.transform);
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
			num = NGUITools.CalculateNextDepth(go);
			aabbox = NGUIMath.CalculateRelativeWidgetBounds(go.transform);
			UIRectHotSpot uirectHotSpot = go.AddComponent<UIRectHotSpot>();
			uirectHotSpot.size = aabbox.size;
			uirectHotSpot.center = aabbox.center + Vector3.back * ((float)num * 0.25f);
			return uirectHotSpot;
		}
		UIHotSpot uihotSpot = NGUITools.ColliderToHotSpot(collider, true);
		if (!uihotSpot)
		{
			return null;
		}
		return uihotSpot;
	}

	// Token: 0x0600461D RID: 17949 RVA: 0x00116BF8 File Offset: 0x00114DF8
	[Obsolete("Use UIAtlas.replacement instead")]
	public static void ReplaceAtlas(UIAtlas before, UIAtlas after)
	{
		UISprite[] array = NGUITools.FindActive<UISprite>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			UISprite uisprite = array[i];
			if (uisprite.atlas == before)
			{
				uisprite.atlas = after;
			}
			i++;
		}
		UILabel[] array2 = NGUITools.FindActive<UILabel>();
		int j = 0;
		int num2 = array2.Length;
		while (j < num2)
		{
			UILabel uilabel = array2[j];
			if (uilabel.font != null && uilabel.font.atlas == before)
			{
				uilabel.font.atlas = after;
			}
			j++;
		}
	}

	// Token: 0x0600461E RID: 17950 RVA: 0x00116CA0 File Offset: 0x00114EA0
	[Obsolete("Use UIFont.replacement instead")]
	public static void ReplaceFont(UIFont before, UIFont after)
	{
		UILabel[] array = NGUITools.FindActive<UILabel>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			UILabel uilabel = array[i];
			if (uilabel.font == before)
			{
				uilabel.font = after;
			}
			i++;
		}
	}

	// Token: 0x0600461F RID: 17951 RVA: 0x00116CE8 File Offset: 0x00114EE8
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

	// Token: 0x06004620 RID: 17952 RVA: 0x00116D3C File Offset: 0x00114F3C
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

	// Token: 0x06004621 RID: 17953 RVA: 0x00116D9C File Offset: 0x00114F9C
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

	// Token: 0x06004622 RID: 17954 RVA: 0x00116E10 File Offset: 0x00115010
	public static int CalculateNextDepth(GameObject go)
	{
		int num = -1;
		UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>();
		int i = 0;
		int num2 = componentsInChildren.Length;
		while (i < num2)
		{
			num = Mathf.Max(num, componentsInChildren[i].depth);
			i++;
		}
		return num + 1;
	}

	// Token: 0x06004623 RID: 17955 RVA: 0x00116E50 File Offset: 0x00115050
	public static T AddChild<T>(GameObject parent) where T : Component
	{
		GameObject gameObject = NGUITools.AddChild(parent);
		gameObject.name = NGUITools.GetName<T>();
		return gameObject.AddComponent<T>();
	}

	// Token: 0x06004624 RID: 17956 RVA: 0x00116E78 File Offset: 0x00115078
	public static T AddWidget<T>(GameObject go) where T : UIWidget
	{
		int depth = NGUITools.CalculateNextDepth(go);
		T result = NGUITools.AddChild<T>(go);
		result.depth = depth;
		Transform transform = result.transform;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		transform.localScale = new Vector3(100f, 100f, 1f);
		result.gameObject.layer = go.layer;
		return result;
	}

	// Token: 0x06004625 RID: 17957 RVA: 0x00116EF8 File Offset: 0x001150F8
	public static UISprite AddSprite(GameObject go, UIAtlas atlas, string spriteName)
	{
		UIAtlas.Sprite sprite = (!(atlas != null)) ? null : atlas.GetSprite(spriteName);
		UISprite uisprite = (sprite != null && !(sprite.inner == sprite.outer)) ? NGUITools.AddWidget<UISlicedSprite>(go) : NGUITools.AddWidget<UISprite>(go);
		uisprite.atlas = atlas;
		uisprite.spriteName = spriteName;
		return uisprite;
	}

	// Token: 0x06004626 RID: 17958 RVA: 0x00116F5C File Offset: 0x0011515C
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

	// Token: 0x06004627 RID: 17959 RVA: 0x00116FD0 File Offset: 0x001151D0
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

	// Token: 0x06004628 RID: 17960 RVA: 0x00116FFC File Offset: 0x001151FC
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

	// Token: 0x06004629 RID: 17961 RVA: 0x00117028 File Offset: 0x00115228
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

	// Token: 0x0600462A RID: 17962 RVA: 0x0011706C File Offset: 0x0011526C
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

	// Token: 0x0600462B RID: 17963 RVA: 0x001170B0 File Offset: 0x001152B0
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

	// Token: 0x0600462C RID: 17964 RVA: 0x00117100 File Offset: 0x00115300
	private static void Activate(Transform t)
	{
		t.gameObject.SetActive(true);
	}

	// Token: 0x0600462D RID: 17965 RVA: 0x00117110 File Offset: 0x00115310
	private static void Deactivate(Transform t)
	{
		t.gameObject.SetActive(false);
	}

	// Token: 0x0600462E RID: 17966 RVA: 0x00117120 File Offset: 0x00115320
	public static void SetActive(GameObject go, bool state)
	{
		if (state)
		{
			NGUITools.Activate(go.transform);
		}
		else
		{
			NGUITools.Deactivate(go.transform);
		}
	}

	// Token: 0x0600462F RID: 17967 RVA: 0x00117144 File Offset: 0x00115344
	public static Vector3 Round(Vector3 v)
	{
		v.x = Mathf.Round(v.x);
		v.y = Mathf.Round(v.y);
		v.z = Mathf.Round(v.z);
		return v;
	}

	// Token: 0x06004630 RID: 17968 RVA: 0x0011718C File Offset: 0x0011538C
	public static void MakePixelPerfect(Transform t)
	{
		UIWidget component = t.GetComponent<UIWidget>();
		if (component != null)
		{
			component.MakePixelPerfect();
		}
		else
		{
			t.localPosition = NGUITools.Round(t.localPosition);
			t.localScale = NGUITools.Round(t.localScale);
			int i = 0;
			int childCount = t.childCount;
			while (i < childCount)
			{
				NGUITools.MakePixelPerfect(t.GetChild(i));
				i++;
			}
		}
	}

	// Token: 0x06004631 RID: 17969 RVA: 0x00117200 File Offset: 0x00115400
	public static bool SetAllowClick(Component self, bool allow)
	{
		Collider collider = self.collider;
		if (collider)
		{
			collider.enabled = allow;
			return true;
		}
		UIHotSpot component = self.GetComponent<UIHotSpot>();
		if (component)
		{
			component.enabled = allow;
			return true;
		}
		return false;
	}

	// Token: 0x06004632 RID: 17970 RVA: 0x00117244 File Offset: 0x00115444
	public static bool GetAllowClick(MonoBehaviour self, out bool possible)
	{
		Collider collider = self.collider;
		if (collider)
		{
			possible = true;
			return collider.enabled;
		}
		UIHotSpot component = self.GetComponent<UIHotSpot>();
		if (component)
		{
			possible = true;
			return component.enabled;
		}
		possible = false;
		return false;
	}

	// Token: 0x06004633 RID: 17971 RVA: 0x00117290 File Offset: 0x00115490
	public static bool GetAllowClick(MonoBehaviour self)
	{
		bool flag;
		return NGUITools.GetAllowClick(self, out flag);
	}

	// Token: 0x06004634 RID: 17972 RVA: 0x001172A8 File Offset: 0x001154A8
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
		UIHotSpot[] componentsInChildren2 = mChild.GetComponentsInChildren<UIHotSpot>();
		int j = 0;
		int num2 = componentsInChildren2.Length;
		while (j < num2)
		{
			componentsInChildren2[j].enabled = false;
			j++;
		}
	}

	// Token: 0x06004635 RID: 17973 RVA: 0x00117308 File Offset: 0x00115508
	public static bool HasMeansOfClicking(Component self)
	{
		return self.collider || self.GetComponent<UIHotSpot>();
	}

	// Token: 0x06004636 RID: 17974 RVA: 0x00117328 File Offset: 0x00115528
	public static bool GetCentroid(Component cell, out Vector3 centroid)
	{
		if (cell is Collider)
		{
			centroid = ((Collider)cell).bounds.center;
		}
		else if (cell is UIHotSpot)
		{
			centroid = ((UIHotSpot)cell).worldCenter;
		}
		else
		{
			UIHotSpot component = cell.GetComponent<UIHotSpot>();
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

	// Token: 0x06004637 RID: 17975 RVA: 0x001173D8 File Offset: 0x001155D8
	public static TComponent QuickGet<TComponent>(GameObject gameObject) where TComponent : Component
	{
		switch (NGUITools.SG<TComponent>.V)
		{
		case NGUITools.SlipGate.Renderer:
			return gameObject.renderer as TComponent;
		case NGUITools.SlipGate.Collider:
			return gameObject.collider as TComponent;
		case NGUITools.SlipGate.Transform:
			return gameObject.transform as TComponent;
		}
		return gameObject.GetComponent<TComponent>();
	}

	// Token: 0x06004638 RID: 17976 RVA: 0x00117450 File Offset: 0x00115650
	public static TComponent GetOrAddComponent<TComponent>(GameObject gameObject) where TComponent : Component
	{
		TComponent tcomponent = NGUITools.QuickGet<TComponent>(gameObject);
		return (!tcomponent) ? gameObject.AddComponent<TComponent>() : tcomponent;
	}

	// Token: 0x06004639 RID: 17977 RVA: 0x00117480 File Offset: 0x00115680
	public static TComponent GetOrAddComponent<TComponent>(Component component) where TComponent : Component
	{
		if (component is TComponent)
		{
			return (TComponent)((object)component);
		}
		return NGUITools.GetOrAddComponent<TComponent>(component.gameObject);
	}

	// Token: 0x0600463A RID: 17978 RVA: 0x001174A0 File Offset: 0x001156A0
	public static bool GetOrAddComponent<TComponent>(GameObject gameObject, ref TComponent value) where TComponent : Component
	{
		return (!value) ? (value = NGUITools.GetOrAddComponent<TComponent>(gameObject)) : value;
	}

	// Token: 0x0600463B RID: 17979 RVA: 0x001174E8 File Offset: 0x001156E8
	public static bool GetOrAddComponent<TComponent>(Component component, ref TComponent value) where TComponent : Component
	{
		return (!value) ? (value = NGUITools.GetOrAddComponent<TComponent>(component)) : value;
	}

	// Token: 0x0400265C RID: 9820
	public const float kMinimumAlpha = 0.00196078443f;

	// Token: 0x0400265D RID: 9821
	public const float kMaximumNegativeAlpha = -0.00196078443f;

	// Token: 0x0400265E RID: 9822
	public const string kFormattingOffDisableSymbol = "[«]";

	// Token: 0x0400265F RID: 9823
	public const string kFormattingOffEnableSymbol = "[»]";

	// Token: 0x04002660 RID: 9824
	public const char kFormattingOffDisableCharacter = '«';

	// Token: 0x04002661 RID: 9825
	public const char kFormattingOffEnableCharacter = '»';

	// Token: 0x04002662 RID: 9826
	private static AudioListener mListener;

	// Token: 0x04002663 RID: 9827
	private static bool mLoaded = false;

	// Token: 0x04002664 RID: 9828
	private static float mGlobalVolume = 1f;

	// Token: 0x04002665 RID: 9829
	private static readonly string[] kFormattingOffSymbols = new string[]
	{
		"[»]",
		"[«]"
	};

	// Token: 0x02000793 RID: 1939
	private enum SlipGate
	{
		// Token: 0x04002667 RID: 9831
		Renderer,
		// Token: 0x04002668 RID: 9832
		Collider,
		// Token: 0x04002669 RID: 9833
		Behaviour,
		// Token: 0x0400266A RID: 9834
		Transform,
		// Token: 0x0400266B RID: 9835
		Component
	}

	// Token: 0x02000794 RID: 1940
	private static class SG<T> where T : Component
	{
		// Token: 0x0600463C RID: 17980 RVA: 0x00117530 File Offset: 0x00115730
		static SG()
		{
			if (typeof(Renderer).IsAssignableFrom(typeof(T)))
			{
				NGUITools.SG<T>.V = NGUITools.SlipGate.Renderer;
			}
			else if (typeof(Collider).IsAssignableFrom(typeof(T)))
			{
				NGUITools.SG<T>.V = NGUITools.SlipGate.Collider;
			}
			else if (typeof(Behaviour).IsAssignableFrom(typeof(T)))
			{
				NGUITools.SG<T>.V = NGUITools.SlipGate.Behaviour;
			}
			else if (typeof(Transform).IsAssignableFrom(typeof(T)))
			{
				NGUITools.SG<T>.V = NGUITools.SlipGate.Transform;
			}
			else
			{
				NGUITools.SG<T>.V = NGUITools.SlipGate.Component;
			}
		}

		// Token: 0x0400266C RID: 9836
		public static readonly NGUITools.SlipGate V;
	}
}
