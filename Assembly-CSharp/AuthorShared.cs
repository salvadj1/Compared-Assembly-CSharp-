using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x0200000F RID: 15
public abstract class AuthorShared : MonoBehaviour
{
	// Token: 0x0600006F RID: 111 RVA: 0x000032D4 File Offset: 0x000014D4
	public static AuthorShared.ObjectKind GetObjectKind(Object value)
	{
		return AuthorShared.ObjectKind.Null;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x000032D8 File Offset: 0x000014D8
	public static bool IsNonModelPrefabAssetOrInstance(AuthorShared.ObjectKind kind)
	{
		return kind == AuthorShared.ObjectKind.Prefab || kind == AuthorShared.ObjectKind.PrefabInstance || kind == AuthorShared.ObjectKind.DisconnectedPrefabInstance;
	}

	// Token: 0x06000071 RID: 113 RVA: 0x000032F0 File Offset: 0x000014F0
	public static bool IsModelAssetOrInstance(AuthorShared.ObjectKind kind)
	{
		return kind == AuthorShared.ObjectKind.Model || kind == AuthorShared.ObjectKind.ModelInstance || kind == AuthorShared.ObjectKind.DisconnectedModelInstance;
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00003308 File Offset: 0x00001508
	public static bool IsPrefabAssetOrInstance(AuthorShared.ObjectKind kind)
	{
		return kind == AuthorShared.ObjectKind.Prefab || kind == AuthorShared.ObjectKind.Model || kind == AuthorShared.ObjectKind.PrefabInstance || kind == AuthorShared.ObjectKind.ModelInstance || kind == AuthorShared.ObjectKind.DisconnectedPrefabInstance || kind == AuthorShared.ObjectKind.DisconnectedModelInstance;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00003340 File Offset: 0x00001540
	public static bool IsScriptableObjectAssetOrInstance(AuthorShared.ObjectKind kind)
	{
		return kind == AuthorShared.ObjectKind.ScriptableObject || kind == AuthorShared.ObjectKind.ScriptableObjectInstance;
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00003354 File Offset: 0x00001554
	public static bool IsInstance(AuthorShared.ObjectKind kind)
	{
		return kind >= AuthorShared.ObjectKind.LevelInstance && (kind & AuthorShared.ObjectKind.Prefab) == AuthorShared.ObjectKind.LevelInstance;
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00003374 File Offset: 0x00001574
	public static bool IsAsset(AuthorShared.ObjectKind kind)
	{
		return kind >= AuthorShared.ObjectKind.LevelInstance && (kind & AuthorShared.ObjectKind.Prefab) == AuthorShared.ObjectKind.Prefab;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00003394 File Offset: 0x00001594
	public static bool IsLevelInstance(AuthorShared.ObjectKind kind)
	{
		return kind == AuthorShared.ObjectKind.LevelInstance || kind == AuthorShared.ObjectKind.MissingPrefabInstance || kind == AuthorShared.ObjectKind.PrefabInstance || kind == AuthorShared.ObjectKind.ModelInstance || kind == AuthorShared.ObjectKind.DisconnectedPrefabInstance || kind == AuthorShared.ObjectKind.DisconnectedModelInstance;
	}

	// Token: 0x06000077 RID: 119 RVA: 0x000033C0 File Offset: 0x000015C0
	public static bool Exists(AuthorShared.ObjectKind kind)
	{
		return kind >= AuthorShared.ObjectKind.LevelInstance;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x000033CC File Offset: 0x000015CC
	public static void PingObject(Object o)
	{
	}

	// Token: 0x06000079 RID: 121 RVA: 0x000033D0 File Offset: 0x000015D0
	public static void PingObject(int instanceID)
	{
	}

	// Token: 0x0600007A RID: 122 RVA: 0x000033D4 File Offset: 0x000015D4
	private static AuthorShared.Content ObjectContentR(Object o, Type type)
	{
		return GUIContent.none;
	}

	// Token: 0x0600007B RID: 123 RVA: 0x000033E0 File Offset: 0x000015E0
	public static AuthorShared.Content ObjectContent(Object o, Type type)
	{
		return AuthorShared.ObjectContentR(o, type);
	}

	// Token: 0x0600007C RID: 124 RVA: 0x000033EC File Offset: 0x000015EC
	public static AuthorShared.Content ObjectContent(Type type)
	{
		return AuthorShared.ObjectContentR(null, type);
	}

	// Token: 0x0600007D RID: 125 RVA: 0x000033F8 File Offset: 0x000015F8
	public static AuthorShared.Content ObjectContent<T>(T o, Type type) where T : Object
	{
		return AuthorShared.ObjectContentR(o, type ?? typeof(T));
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00003418 File Offset: 0x00001618
	public static AuthorShared.Content ObjectContent<T>(T o) where T : Object
	{
		return AuthorShared.ObjectContentR(o, typeof(T));
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00003430 File Offset: 0x00001630
	public static AuthorShared.Content ObjectContent<T>() where T : Object
	{
		return AuthorShared.ObjectContentR(null, typeof(T));
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00003444 File Offset: 0x00001644
	public static Object ObjectField(AuthorShared.Content label, Object value, Type type, bool allowScene, params GUILayoutOption[] options)
	{
		return value;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00003448 File Offset: 0x00001648
	public static Object ObjectField(AuthorShared.Content label, Object value, Type type, AuthorShared.ObjectFieldFlags flags, params GUILayoutOption[] options)
	{
		return AuthorShared.ObjectField(label, value, type, (flags & AuthorShared.ObjectFieldFlags.AllowScene) == AuthorShared.ObjectFieldFlags.AllowScene, options);
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00003468 File Offset: 0x00001668
	public static Object ObjectField(Object obj, Type type, AuthorShared.ObjectFieldFlags flags, params GUILayoutOption[] options)
	{
		return AuthorShared.ObjectField(default(AuthorShared.Content), obj, type, flags, options);
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00003488 File Offset: 0x00001688
	public static Object ObjectField(Object obj, Type type, params GUILayoutOption[] options)
	{
		return AuthorShared.ObjectField(default(AuthorShared.Content), obj, type, false, options);
	}

	// Token: 0x06000084 RID: 132 RVA: 0x000034A8 File Offset: 0x000016A8
	public static bool ObjectField<T>(AuthorShared.Content content, ref T reference, AuthorShared.ObjectFieldFlags flags, params GUILayoutOption[] options) where T : Object
	{
		return AuthorShared.ObjectField<T>(content, ref reference, typeof(T), flags, options);
	}

	// Token: 0x06000085 RID: 133 RVA: 0x000034C0 File Offset: 0x000016C0
	public static bool ObjectField<T>(AuthorShared.Content content, ref T reference, Type type, AuthorShared.ObjectFieldFlags flags, params GUILayoutOption[] options) where T : Object
	{
		Object @object = AuthorShared.ObjectField(content, reference, type ?? typeof(T), flags, options);
		if (GUI.changed)
		{
			reference = (T)((object)@object);
			return true;
		}
		return false;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00003510 File Offset: 0x00001710
	public static Object[] GetAllSelectedObjects()
	{
		return new Object[0];
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00003518 File Offset: 0x00001718
	public static void SetAllSelectedObjects(params Object[] objects)
	{
	}

	// Token: 0x06000088 RID: 136 RVA: 0x0000351C File Offset: 0x0000171C
	public static void Label(AuthorShared.Content content, GUIStyle style, params GUILayoutOption[] options)
	{
		int type = content.type;
		if (type != 1)
		{
			if (type != 2)
			{
				GUILayout.Label(GUIContent.none, style, options);
			}
			else
			{
				GUILayout.Label(content.content, style, options);
			}
		}
		else
		{
			GUILayout.Label(content.text, style, options);
		}
	}

	// Token: 0x06000089 RID: 137 RVA: 0x0000357C File Offset: 0x0000177C
	public static void Label(Texture content, GUIStyle style, params GUILayoutOption[] options)
	{
		GUILayout.Label(content, style, options);
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00003588 File Offset: 0x00001788
	public static void Label(AuthorShared.Content content, params GUILayoutOption[] options)
	{
		int type = content.type;
		if (type != 1)
		{
			if (type != 2)
			{
				GUILayout.Label(GUIContent.none, options);
			}
			else
			{
				GUILayout.Label(content.content, options);
			}
		}
		else
		{
			GUILayout.Label(content.text, options);
		}
	}

	// Token: 0x0600008B RID: 139 RVA: 0x000035E4 File Offset: 0x000017E4
	public static void Label(Texture content, params GUILayoutOption[] options)
	{
		GUILayout.Label(content, options);
	}

	// Token: 0x0600008C RID: 140 RVA: 0x000035F0 File Offset: 0x000017F0
	public static Rect BeginSubSection(AuthorShared.Content title, params GUILayoutOption[] options)
	{
		Color backgroundColor = GUI.backgroundColor;
		GUI.backgroundColor = new Color(backgroundColor.r, backgroundColor.g, backgroundColor.b, backgroundColor.a * 0.4f);
		Rect result = AuthorShared.BeginVertical(AuthorShared.Styles.subSection, new GUILayoutOption[0]);
		AuthorShared.Label(title, AuthorShared.Styles.subSectionTitle, new GUILayoutOption[0]);
		GUI.backgroundColor = backgroundColor;
		return result;
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00003658 File Offset: 0x00001858
	public static Rect BeginSubSection(AuthorShared.Content title, AuthorShared.Content infoContent, GUIStyle infoStyle, params GUILayoutOption[] options)
	{
		Rect result = AuthorShared.BeginSubSection(title, options);
		if (infoContent.type != 0 && Event.current.type == 7)
		{
			if (infoContent.type == 1)
			{
				GUI.Label(GUILayoutUtility.GetLastRect(), infoContent.text, infoStyle);
			}
			else
			{
				GUI.Label(GUILayoutUtility.GetLastRect(), infoContent.content, infoStyle);
			}
		}
		return result;
	}

	// Token: 0x0600008E RID: 142 RVA: 0x000036C0 File Offset: 0x000018C0
	public static Rect BeginSubSection(AuthorShared.Content title, AuthorShared.Content infoContent, params GUILayoutOption[] options)
	{
		return AuthorShared.BeginSubSection(title, infoContent, AuthorShared.Styles.infoLabel, options);
	}

	// Token: 0x0600008F RID: 143 RVA: 0x000036D0 File Offset: 0x000018D0
	public static void EndSubSection()
	{
		AuthorShared.EndVertical();
	}

	// Token: 0x06000090 RID: 144 RVA: 0x000036D8 File Offset: 0x000018D8
	public static string StringField(AuthorShared.Content content, string value, GUIStyle style, params GUILayoutOption[] options)
	{
		return value;
	}

	// Token: 0x06000091 RID: 145 RVA: 0x000036DC File Offset: 0x000018DC
	public static bool StringField(AuthorShared.Content content, ref string value, GUIStyle style, params GUILayoutOption[] options)
	{
		return AuthorShared.Change(ref value, AuthorShared.StringField(content, value, style, options));
	}

	// Token: 0x06000092 RID: 146 RVA: 0x000036F0 File Offset: 0x000018F0
	public static TComponent AddComponent<TComponent>(GameObject target, string type) where TComponent : Component
	{
		Component component = target.AddComponent(type);
		if (!component)
		{
			Debug.LogWarning("The string type \"" + type + "\" evaluated to no component type. null returning", target);
			return (TComponent)((object)null);
		}
		if (component is TComponent)
		{
			return (TComponent)((object)component);
		}
		Debug.LogWarning(string.Concat(new string[]
		{
			"The string type \"",
			type,
			"\" is a component class but does not inherit \"",
			typeof(TComponent).AssemblyQualifiedName,
			"\""
		}), target);
		Object.DestroyImmediate(component);
		return (TComponent)((object)null);
	}

	// Token: 0x06000093 RID: 147 RVA: 0x0000378C File Offset: 0x0000198C
	public static string StringField(AuthorShared.Content content, string value, params GUILayoutOption[] options)
	{
		return value;
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00003790 File Offset: 0x00001990
	public static bool StringField(AuthorShared.Content content, ref string value, params GUILayoutOption[] options)
	{
		return AuthorShared.Change(ref value, AuthorShared.StringField(content, value, options));
	}

	// Token: 0x06000095 RID: 149 RVA: 0x000037A4 File Offset: 0x000019A4
	public static bool? Ask(string Question)
	{
		return null;
	}

	// Token: 0x06000096 RID: 150 RVA: 0x000037BC File Offset: 0x000019BC
	public static int IntField(AuthorShared.Content content, int value, GUIStyle style, params GUILayoutOption[] options)
	{
		return value;
	}

	// Token: 0x06000097 RID: 151 RVA: 0x000037C0 File Offset: 0x000019C0
	public static bool IntField(AuthorShared.Content content, ref int value, GUIStyle style, params GUILayoutOption[] options)
	{
		return AuthorShared.Change(ref value, AuthorShared.IntField(content, value, style, options));
	}

	// Token: 0x06000098 RID: 152 RVA: 0x000037D4 File Offset: 0x000019D4
	public static int IntField(AuthorShared.Content content, int value, params GUILayoutOption[] options)
	{
		return value;
	}

	// Token: 0x06000099 RID: 153 RVA: 0x000037D8 File Offset: 0x000019D8
	public static bool IntField(AuthorShared.Content content, ref int value, params GUILayoutOption[] options)
	{
		return AuthorShared.Change(ref value, AuthorShared.IntField(content, value, options));
	}

	// Token: 0x0600009A RID: 154 RVA: 0x000037EC File Offset: 0x000019EC
	public static float FloatField(AuthorShared.Content content, float value, GUIStyle style, params GUILayoutOption[] options)
	{
		return value;
	}

	// Token: 0x0600009B RID: 155 RVA: 0x000037F0 File Offset: 0x000019F0
	public static bool FloatField(AuthorShared.Content content, ref float value, params GUILayoutOption[] options)
	{
		return AuthorShared.Change(ref value, AuthorShared.FloatField(content, value, options));
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00003804 File Offset: 0x00001A04
	public static float FloatField(AuthorShared.Content content, float value, params GUILayoutOption[] options)
	{
		return value;
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00003808 File Offset: 0x00001A08
	public static bool FloatField(AuthorShared.Content content, ref float value, GUIStyle style, params GUILayoutOption[] options)
	{
		return AuthorShared.Change(ref value, AuthorShared.FloatField(content, value, style, options));
	}

	// Token: 0x0600009E RID: 158 RVA: 0x0000381C File Offset: 0x00001A1C
	public static Vector3 Vector3Field(AuthorShared.Content content, Vector3 value, params GUILayoutOption[] options)
	{
		return value;
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00003820 File Offset: 0x00001A20
	public static bool Vector3Field(AuthorShared.Content content, ref Vector3 value, params GUILayoutOption[] options)
	{
		return AuthorShared.Change(ref value, AuthorShared.Vector3Field(content, value, options));
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00003838 File Offset: 0x00001A38
	public static Enum EnumField(AuthorShared.Content content, Enum value, GUIStyle style, params GUILayoutOption[] options)
	{
		return value;
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x0000383C File Offset: 0x00001A3C
	public static bool EnumField<T>(AuthorShared.Content content, ref T value, GUIStyle style, params GUILayoutOption[] options) where T : struct
	{
		return AuthorShared.Change<T>(ref value, AuthorShared.EnumField(content, (Enum)Enum.ToObject(typeof(T), value), style, options));
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x0000386C File Offset: 0x00001A6C
	public static Enum EnumField(AuthorShared.Content content, Enum value, params GUILayoutOption[] options)
	{
		return value;
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x00003870 File Offset: 0x00001A70
	public static bool EnumField<T>(AuthorShared.Content content, ref T value, params GUILayoutOption[] options) where T : struct
	{
		return AuthorShared.Change<T>(ref value, AuthorShared.EnumField(content, (Enum)Enum.ToObject(typeof(T), value), options));
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x000038AC File Offset: 0x00001AAC
	public static void SetSerializedProperty(Object objSet, string propertyPath, Object value)
	{
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x000038B0 File Offset: 0x00001AB0
	public static bool SelectionContains(Object obj)
	{
		return false;
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x000038B4 File Offset: 0x00001AB4
	public static bool SelectionContains(int obj)
	{
		return false;
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x000038B8 File Offset: 0x00001AB8
	public static Rect BeginVertical(params GUILayoutOption[] options)
	{
		return new Rect(0f, 0f, 0f, 0f);
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x000038D4 File Offset: 0x00001AD4
	public static Rect BeginVertical(GUIStyle style, params GUILayoutOption[] options)
	{
		return new Rect(0f, 0f, 0f, 0f);
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x000038F0 File Offset: 0x00001AF0
	public static Rect BeginHorizontal(params GUILayoutOption[] options)
	{
		return new Rect(0f, 0f, 0f, 0f);
	}

	// Token: 0x060000AA RID: 170 RVA: 0x0000390C File Offset: 0x00001B0C
	public static Rect BeginHorizontal(GUIStyle style, params GUILayoutOption[] options)
	{
		return new Rect(0f, 0f, 0f, 0f);
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00003928 File Offset: 0x00001B28
	public static Vector2 BeginScrollView(Vector2 scroll, params GUILayoutOption[] options)
	{
		return scroll;
	}

	// Token: 0x060000AC RID: 172 RVA: 0x0000392C File Offset: 0x00001B2C
	public static void EndVertical()
	{
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00003930 File Offset: 0x00001B30
	public static void EndHorizontal()
	{
	}

	// Token: 0x060000AE RID: 174 RVA: 0x00003934 File Offset: 0x00001B34
	public static void EndScrollView()
	{
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00003938 File Offset: 0x00001B38
	public static void SetDirty(Object obj)
	{
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x0000393C File Offset: 0x00001B3C
	public static string GetAssetPath(Object obj)
	{
		return string.Empty;
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x00003944 File Offset: 0x00001B44
	public static string PathToProjectPath(string path)
	{
		return path;
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00003948 File Offset: 0x00001B48
	public static string TryPathToProjectPath(string path)
	{
		return path;
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x0000394C File Offset: 0x00001B4C
	public static string PathToGUID(string path)
	{
		return string.Empty;
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00003954 File Offset: 0x00001B54
	public static string GUIDToPath(string guid)
	{
		return string.Empty;
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x0000395C File Offset: 0x00001B5C
	public static void CustomMenu(Rect position, GUIContent[] options, int selected, AuthorShared.CustomMenuProc proc, object userData)
	{
		string[] array = new string[options.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = options[i].text;
		}
		proc(userData, array, selected);
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x0000399C File Offset: 0x00001B9C
	public static int Popup(AuthorShared.Content content, int index, GUIContent[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
	{
		return index;
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x000039A0 File Offset: 0x00001BA0
	public static bool Popup(AuthorShared.Content content, ref int index, GUIContent[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
	{
		return AuthorShared.Change(ref index, AuthorShared.Popup(content, index, displayedOptions, style, options));
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x000039B4 File Offset: 0x00001BB4
	public static int Popup(AuthorShared.Content content, int index, GUIContent[] displayedOptions, params GUILayoutOption[] options)
	{
		return index;
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x000039B8 File Offset: 0x00001BB8
	public static bool Popup(AuthorShared.Content content, ref int index, GUIContent[] displayedOptions, params GUILayoutOption[] options)
	{
		return AuthorShared.Change(ref index, AuthorShared.Popup(content, index, displayedOptions, options));
	}

	// Token: 0x060000BA RID: 186 RVA: 0x000039CC File Offset: 0x00001BCC
	public static int Popup(int index, GUIContent[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
	{
		return index;
	}

	// Token: 0x060000BB RID: 187 RVA: 0x000039D0 File Offset: 0x00001BD0
	public static bool Popup(ref int index, GUIContent[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
	{
		return AuthorShared.Change(ref index, AuthorShared.Popup(index, displayedOptions, style, options));
	}

	// Token: 0x060000BC RID: 188 RVA: 0x000039E4 File Offset: 0x00001BE4
	public static int Popup(int index, GUIContent[] displayedOptions, params GUILayoutOption[] options)
	{
		return index;
	}

	// Token: 0x060000BD RID: 189 RVA: 0x000039E8 File Offset: 0x00001BE8
	public static bool Popup(ref int index, GUIContent[] displayedOptions, params GUILayoutOption[] options)
	{
		return AuthorShared.Change(ref index, AuthorShared.Popup(index, displayedOptions, options));
	}

	// Token: 0x060000BE RID: 190 RVA: 0x000039FC File Offset: 0x00001BFC
	public static int Popup(AuthorShared.Content content, int index, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
	{
		return index;
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00003A00 File Offset: 0x00001C00
	public static bool Popup(AuthorShared.Content content, ref int index, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
	{
		return AuthorShared.Change(ref index, AuthorShared.Popup(content, index, displayedOptions, style, options));
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00003A14 File Offset: 0x00001C14
	public static int Popup(AuthorShared.Content content, int index, string[] displayedOptions, params GUILayoutOption[] options)
	{
		return index;
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00003A18 File Offset: 0x00001C18
	public static bool Popup(AuthorShared.Content content, ref int index, string[] displayedOptions, params GUILayoutOption[] options)
	{
		return AuthorShared.Change(ref index, AuthorShared.Popup(content, index, displayedOptions, options));
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x00003A2C File Offset: 0x00001C2C
	public static int Popup(int index, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
	{
		return index;
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00003A30 File Offset: 0x00001C30
	public static bool Popup(ref int index, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
	{
		return AuthorShared.Change(ref index, AuthorShared.Popup(index, displayedOptions, style, options));
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x00003A44 File Offset: 0x00001C44
	public static int Popup(int index, string[] displayedOptions, params GUILayoutOption[] options)
	{
		return index;
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00003A48 File Offset: 0x00001C48
	public static bool Popup(ref int index, string[] displayedOptions, params GUILayoutOption[] options)
	{
		return AuthorShared.Change(ref index, AuthorShared.Popup(index, displayedOptions, options));
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x00003A5C File Offset: 0x00001C5C
	public static bool Change(ref int current, int incoming)
	{
		if (current == incoming)
		{
			return false;
		}
		current = incoming;
		return true;
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00003A6C File Offset: 0x00001C6C
	public static bool Change(ref float current, float incoming)
	{
		if (current == incoming)
		{
			return false;
		}
		current = incoming;
		return true;
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00003A7C File Offset: 0x00001C7C
	public static bool Change(ref bool current, bool incoming)
	{
		if (current == incoming)
		{
			return false;
		}
		current = incoming;
		return true;
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00003A8C File Offset: 0x00001C8C
	public static bool Change(ref string current, string incoming)
	{
		if (current == incoming)
		{
			return false;
		}
		current = incoming;
		return true;
	}

	// Token: 0x060000CA RID: 202 RVA: 0x00003AA4 File Offset: 0x00001CA4
	public static bool Change(ref Vector2 current, Vector2 incoming)
	{
		if (current == incoming)
		{
			return false;
		}
		current = incoming;
		return true;
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00003AC4 File Offset: 0x00001CC4
	public static bool Change(ref Vector3 current, Vector3 incoming)
	{
		if (current == incoming)
		{
			return false;
		}
		current = incoming;
		return true;
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00003AE4 File Offset: 0x00001CE4
	public static bool Change(ref Vector4 current, Vector4 incoming)
	{
		if (current == incoming)
		{
			return false;
		}
		current = incoming;
		return true;
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00003B04 File Offset: 0x00001D04
	public static bool Change(ref Quaternion current, Quaternion incoming)
	{
		if (current == incoming)
		{
			return false;
		}
		current = incoming;
		return true;
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00003B24 File Offset: 0x00001D24
	public static bool Change<T>(ref T current, object incoming) where T : struct
	{
		if (current.Equals(incoming))
		{
			return false;
		}
		T t = current;
		bool result;
		try
		{
			current = (T)((object)incoming);
			result = true;
		}
		catch
		{
			current = t;
			result = false;
		}
		return result;
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00003B98 File Offset: 0x00001D98
	public static void PrefixLabel(AuthorShared.Content content)
	{
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00003B9C File Offset: 0x00001D9C
	public static void PrefixLabel(AuthorShared.Content content, GUIStyle followingStyle)
	{
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x00003BA0 File Offset: 0x00001DA0
	public static void PrefixLabel(AuthorShared.Content content, GUIStyle followingStyle, GUIStyle labelStyle)
	{
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00003BA4 File Offset: 0x00001DA4
	private static Rect GetControlRect(bool hasLabel, float height, GUIStyle style, params GUILayoutOption[] options)
	{
		return GUILayoutUtility.GetRect(0f, 100f, height, height, style, options);
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00003BBC File Offset: 0x00001DBC
	private static bool VerifyArgs(AuthorShared.GenerateOptions generateOptions, GUIContent[] options, Array array)
	{
		return options != null && array != null && options.Length == array.Length && options.Length != 0;
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00003BF0 File Offset: 0x00001DF0
	private static bool PopupImmediate<T>(AuthorShared.Content content, AuthorShared.GenerateOptions generateOptions, T args, GUIStyle style, GUILayoutOption[] options, out object value)
	{
		value = null;
		return false;
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00003BF8 File Offset: 0x00001DF8
	private static bool AuthorPopupGenerate(object arg, ref int selected, out GUIContent[] options, out Array array)
	{
		options = null;
		array = null;
		AuthorShared.AuthorOptionGenerate authorOptionGenerate = (AuthorShared.AuthorOptionGenerate)arg;
		List<AuthorPeice> list = new List<AuthorPeice>(authorOptionGenerate.creation.EnumeratePeices(authorOptionGenerate.selectedOnly));
		int num = list.Count;
		if (num == 0)
		{
			return false;
		}
		if (authorOptionGenerate.type != null)
		{
			for (int i = 0; i < num; i++)
			{
				AuthorPeice authorPeice;
				if (!(authorPeice = list[i]) || !authorOptionGenerate.type.IsAssignableFrom(authorPeice.GetType()))
				{
					list.RemoveAt(i--);
					num--;
				}
			}
		}
		else
		{
			for (int j = 0; j < num; j++)
			{
				if (!list[j])
				{
					list.RemoveAt(j--);
					num--;
				}
			}
		}
		if (num == 0)
		{
			return false;
		}
		if (!authorOptionGenerate.allowSelf && authorOptionGenerate.self)
		{
			if (authorOptionGenerate.peice)
			{
				for (int k = 0; k < num; k++)
				{
					AuthorPeice authorPeice;
					if ((authorPeice = list[k]) == authorOptionGenerate.self)
					{
						list.RemoveAt(k--);
						num--;
					}
					else if (authorPeice == authorOptionGenerate.peice)
					{
						selected = k++;
						while (k < num)
						{
							if (list[k] == authorOptionGenerate.self)
							{
								list.RemoveAt(k--);
								num--;
							}
							k++;
						}
						break;
					}
				}
			}
			else
			{
				for (int l = 0; l < num; l++)
				{
					if (list[l] == authorOptionGenerate.self)
					{
						list.RemoveAt(l--);
						num--;
					}
				}
			}
		}
		else if (authorOptionGenerate.peice)
		{
			for (int m = 0; m < num; m++)
			{
				if (list[m] == authorOptionGenerate.peice)
				{
					selected = m;
					break;
				}
			}
		}
		if (num == 0)
		{
			return false;
		}
		AuthorPeice[] array2 = list.ToArray();
		options = new GUIContent[array2.Length];
		for (int n = 0; n < array2.Length; n++)
		{
			options[n] = new GUIContent(string.Format("{0:00}. {1} ({2})", n, array2[n].peiceID, array2[n].GetType().Name), array2[n].ToString());
		}
		array = array2;
		return true;
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00003EC4 File Offset: 0x000020C4
	private static bool PeiceFieldBase<T>(AuthorShared.Content content, AuthorShared self, ref T peice, Type type, bool allowSelf, GUIStyle style, params GUILayoutOption[] options) where T : AuthorPeice
	{
		return false;
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00003EC8 File Offset: 0x000020C8
	public static bool PeiceField<T>(AuthorShared.Content content, AuthorCreation self, ref T peice, Type type, GUIStyle style, params GUILayoutOption[] options) where T : AuthorPeice
	{
		return AuthorShared.PeiceFieldBase<T>(content, self, ref peice, type, true, style, options);
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x00003ED8 File Offset: 0x000020D8
	public static bool PeiceField<T>(AuthorShared.Content content, AuthorPeice self, ref T peice, Type type, bool allowSelf, GUIStyle style, params GUILayoutOption[] options) where T : AuthorPeice
	{
		return AuthorShared.PeiceFieldBase<T>(content, self, ref peice, type, allowSelf, style, options);
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x00003EEC File Offset: 0x000020EC
	public static bool PeiceField<T>(AuthorShared.Content content, AuthorPeice self, ref T peice, Type type, GUIStyle style, params GUILayoutOption[] options) where T : AuthorPeice
	{
		return AuthorShared.PeiceFieldBase<T>(content, self, ref peice, type, false, style, options);
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00003EFC File Offset: 0x000020FC
	public static GameObject FindPrefabRoot(GameObject prefab)
	{
		return prefab.transform.root.gameObject;
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00003F10 File Offset: 0x00002110
	public static T InstantiatePrefab<T>(T prefab) where T : Component
	{
		Object @object = Object.Instantiate(prefab);
		return (T)((object)@object);
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00003F30 File Offset: 0x00002130
	public static GameObject InstantiatePrefab(GameObject prefab)
	{
		Object @object = Object.Instantiate(prefab);
		return (GameObject)@object;
	}

	// Token: 0x060000DD RID: 221 RVA: 0x00003F4C File Offset: 0x0000214C
	public static void SetActiveSelection(Object o)
	{
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00003F50 File Offset: 0x00002150
	public static bool InAnimationMode()
	{
		return false;
	}

	// Token: 0x060000DF RID: 223 RVA: 0x00003F54 File Offset: 0x00002154
	public static void StartAnimationMode(params Object[] objects)
	{
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x00003F58 File Offset: 0x00002158
	public static void StopAnimationMode()
	{
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x00003F5C File Offset: 0x0000215C
	public static string CalculatePath(Transform targetTransform, Transform root)
	{
		return targetTransform.name;
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x00003F64 File Offset: 0x00002164
	public static Transform GetRootBone(GameObject go)
	{
		SkinnedMeshRenderer skinnedMeshRenderer;
		return AuthorShared.GetRootBone(go, out skinnedMeshRenderer);
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x00003F7C File Offset: 0x0000217C
	public static Transform GetRootBone(GameObject go, out SkinnedMeshRenderer renderer)
	{
		if (go.renderer is SkinnedMeshRenderer)
		{
			renderer = (go.renderer as SkinnedMeshRenderer);
		}
		else
		{
			renderer = null;
			foreach (Transform transform in go.transform.ListDecendantsByDepth())
			{
				if (transform.renderer is SkinnedMeshRenderer)
				{
					renderer = (transform.renderer as SkinnedMeshRenderer);
					break;
				}
			}
			if (renderer == null)
			{
				return go.transform;
			}
		}
		return AuthorShared.GetRootBone(renderer);
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x00004044 File Offset: 0x00002244
	public static Transform GetRootBone(Component co, out SkinnedMeshRenderer renderer)
	{
		if (co is SkinnedMeshRenderer)
		{
			renderer = (co as SkinnedMeshRenderer);
			return AuthorShared.GetRootBone(renderer);
		}
		return AuthorShared.GetRootBone(co.gameObject, out renderer);
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00004070 File Offset: 0x00002270
	public static Transform GetRootBone(Component co)
	{
		if (co is SkinnedMeshRenderer)
		{
			return AuthorShared.GetRootBone(co as SkinnedMeshRenderer);
		}
		return AuthorShared.GetRootBone(co.gameObject);
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x000040A0 File Offset: 0x000022A0
	public static Transform GetRootBone(SkinnedMeshRenderer renderer)
	{
		if (!renderer)
		{
			throw new ArgumentNullException("renderer");
		}
		return renderer.transform;
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x000040C0 File Offset: 0x000022C0
	public static bool Button(AuthorShared.Content content, GUIStyle style, params GUILayoutOption[] options)
	{
		int type = content.type;
		if (type == 1)
		{
			return GUILayout.Button(content.text, style, options);
		}
		if (type != 2)
		{
			return GUILayout.Button(GUIContent.none, style, options);
		}
		return GUILayout.Button(content.content, style, options);
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x00004114 File Offset: 0x00002314
	public static bool Button(Texture image, GUIStyle style, params GUILayoutOption[] options)
	{
		return GUILayout.Button(image, style, options);
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x00004120 File Offset: 0x00002320
	public static bool Button(AuthorShared.Content content, params GUILayoutOption[] options)
	{
		int type = content.type;
		if (type == 1)
		{
			return GUILayout.Button(content.text, options);
		}
		if (type != 2)
		{
			return GUILayout.Button(GUIContent.none, options);
		}
		return GUILayout.Button(content.content, options);
	}

	// Token: 0x060000EA RID: 234 RVA: 0x00004170 File Offset: 0x00002370
	public static bool Button(Texture image, params GUILayoutOption[] options)
	{
		return GUILayout.Button(image, options);
	}

	// Token: 0x060000EB RID: 235 RVA: 0x0000417C File Offset: 0x0000237C
	public static bool Toggle(AuthorShared.Content content, bool state, GUIStyle style, params GUILayoutOption[] options)
	{
		int type = content.type;
		if (type == 1)
		{
			return GUILayout.Toggle(state, content.text, style, options);
		}
		if (type != 2)
		{
			return GUILayout.Toggle(state, GUIContent.none, style, options);
		}
		return GUILayout.Toggle(state, content.content, style, options);
	}

	// Token: 0x060000EC RID: 236 RVA: 0x000041D4 File Offset: 0x000023D4
	public static bool Toggle(Texture image, bool state, GUIStyle style, params GUILayoutOption[] options)
	{
		return GUILayout.Toggle(state, image, style, options);
	}

	// Token: 0x060000ED RID: 237 RVA: 0x000041E0 File Offset: 0x000023E0
	public static bool Toggle(AuthorShared.Content content, bool state, params GUILayoutOption[] options)
	{
		int type = content.type;
		if (type == 1)
		{
			return GUILayout.Toggle(state, content.text, options);
		}
		if (type != 2)
		{
			return GUILayout.Toggle(state, GUIContent.none, options);
		}
		return GUILayout.Toggle(state, content.content, options);
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00004234 File Offset: 0x00002434
	public static bool Toggle(Texture image, bool state, params GUILayoutOption[] options)
	{
		return GUILayout.Toggle(state, image, options);
	}

	// Token: 0x060000EF RID: 239 RVA: 0x00004240 File Offset: 0x00002440
	public static bool Toggle(AuthorShared.Content content, ref bool state, GUIStyle style, params GUILayoutOption[] options)
	{
		return AuthorShared.Change(ref state, AuthorShared.Toggle(content, state, style, options));
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x00004254 File Offset: 0x00002454
	public static bool Toggle(AuthorShared.Content content, ref bool state, params GUILayoutOption[] options)
	{
		return AuthorShared.Change(ref state, AuthorShared.Toggle(content, state, options));
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00004268 File Offset: 0x00002468
	public static bool ArrayField<T>(AuthorShared.Content content, ref T[] array, AuthorShared.ArrayFieldFunctor<T> functor)
	{
		AuthorShared.BeginHorizontal(new GUILayoutOption[0]);
		int num = (array != null) ? array.Length : 0;
		AuthorShared.PrefixLabel(content);
		AuthorShared.BeginVertical(new GUILayoutOption[0]);
		AuthorShared.BeginHorizontal(new GUILayoutOption[0]);
		AuthorShared.Label("Size", new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(false)
		});
		int num2 = Mathf.Max(0, AuthorShared.IntField(default(AuthorShared.Content), num, new GUILayoutOption[0]));
		AuthorShared.EndHorizontal();
		bool flag = num != num2;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				flag |= functor(ref array[i]);
			}
		}
		AuthorShared.EndVertical();
		AuthorShared.EndHorizontal();
		if (flag)
		{
			Array.Resize<T>(ref array, num2);
			return true;
		}
		return false;
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00004340 File Offset: 0x00002540
	public static bool MatchPrefab(Object a, Object b)
	{
		return (a == b || !a || !b) && false;
	}

	// Token: 0x04000032 RID: 50
	private static readonly GUIContent AuthorPeiceContent = new GUIContent();

	// Token: 0x04000033 RID: 51
	private static Rect lastRect_popup;

	// Token: 0x04000034 RID: 52
	private static readonly AuthorShared.GenerateOptions authorPopupGenerate = new AuthorShared.GenerateOptions(AuthorShared.AuthorPopupGenerate);

	// Token: 0x02000010 RID: 16
	public struct Content
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00004368 File Offset: 0x00002568
		private Content(GUIContent content)
		{
			this.content = content;
			this.text = (content ?? GUIContent.none).text;
			this.type = 2;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000439C File Offset: 0x0000259C
		private Content(string text)
		{
			this.content = null;
			this.text = text;
			this.type = ((text != null) ? 1 : 0);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000043C0 File Offset: 0x000025C0
		public Texture image
		{
			get
			{
				return (this.type != 2) ? GUIContent.none.image : this.content.image;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000043F4 File Offset: 0x000025F4
		public string tooltip
		{
			get
			{
				return (this.type != 2) ? GUIContent.none.tooltip : this.content.tooltip;
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004428 File Offset: 0x00002628
		public static implicit operator AuthorShared.Content(GUIContent content)
		{
			return new AuthorShared.Content(content);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004430 File Offset: 0x00002630
		public static implicit operator AuthorShared.Content(string content)
		{
			return new AuthorShared.Content(content);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004438 File Offset: 0x00002638
		public static implicit operator AuthorShared.Content(bool show)
		{
			if (show)
			{
				return new AuthorShared.Content(GUIContent.none);
			}
			return default(AuthorShared.Content);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004460 File Offset: 0x00002660
		public static bool operator true(AuthorShared.Content content)
		{
			return content.type != 0;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004470 File Offset: 0x00002670
		public static bool operator false(AuthorShared.Content content)
		{
			return content.type == 0;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000447C File Offset: 0x0000267C
		public static implicit operator GUIContent(AuthorShared.Content content)
		{
			return AuthorShared.Content.g.GetOrTemp(content);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004484 File Offset: 0x00002684
		public static explicit operator string(AuthorShared.Content content)
		{
			return content.text;
		}

		// Token: 0x04000035 RID: 53
		public readonly int type;

		// Token: 0x04000036 RID: 54
		public readonly string text;

		// Token: 0x04000037 RID: 55
		public readonly GUIContent content;

		// Token: 0x02000011 RID: 17
		private static class g
		{
			// Token: 0x060000FF RID: 255 RVA: 0x00004540 File Offset: 0x00002740
			public static GUIContent GetOrTemp(AuthorShared.Content content)
			{
				if (content.type == 2)
				{
					return content.content;
				}
				if (content.type == 1)
				{
					GUIContent guicontent = AuthorShared.Content.g.bufContents[AuthorShared.Content.g.bufPos];
					if (++AuthorShared.Content.g.bufPos == AuthorShared.Content.g.bufContents.Length)
					{
						AuthorShared.Content.g.bufPos = 0;
					}
					guicontent.text = content.text;
					guicontent.tooltip = AuthorShared.Content.g.noneCopy.tooltip;
					guicontent.image = AuthorShared.Content.g.noneCopy.image;
					return guicontent;
				}
				return AuthorShared.Content.g.noneCopy;
			}

			// Token: 0x04000038 RID: 56
			public static readonly GUIContent noneCopy = new GUIContent();

			// Token: 0x04000039 RID: 57
			public static readonly GUIContent[] bufContents = new GUIContent[]
			{
				new GUIContent(),
				new GUIContent(),
				new GUIContent(),
				new GUIContent(),
				new GUIContent(),
				new GUIContent(),
				new GUIContent(),
				new GUIContent(),
				new GUIContent(),
				new GUIContent(),
				new GUIContent(),
				new GUIContent(),
				new GUIContent(),
				new GUIContent(),
				new GUIContent(),
				new GUIContent()
			};

			// Token: 0x0400003A RID: 58
			private static int bufPos = 0;
		}
	}

	// Token: 0x02000012 RID: 18
	public enum ObjectKind
	{
		// Token: 0x0400003C RID: 60
		LevelInstance,
		// Token: 0x0400003D RID: 61
		Prefab,
		// Token: 0x0400003E RID: 62
		PrefabInstance = 3,
		// Token: 0x0400003F RID: 63
		Model = 2,
		// Token: 0x04000040 RID: 64
		ModelInstance = 4,
		// Token: 0x04000041 RID: 65
		MissingPrefabInstance,
		// Token: 0x04000042 RID: 66
		DisconnectedPrefabInstance,
		// Token: 0x04000043 RID: 67
		ScriptableObject,
		// Token: 0x04000044 RID: 68
		DisconnectedModelInstance,
		// Token: 0x04000045 RID: 69
		OtherAsset,
		// Token: 0x04000046 RID: 70
		OtherInstance,
		// Token: 0x04000047 RID: 71
		ScriptableObjectInstance,
		// Token: 0x04000048 RID: 72
		Null = -2
	}

	// Token: 0x02000013 RID: 19
	public static class Styles
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000046E4 File Offset: 0x000028E4
		public static GUIStyle miniBoldLabel
		{
			get
			{
				return AuthorShared.Styles.label;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000046EC File Offset: 0x000028EC
		public static GUIStyle boldLabel
		{
			get
			{
				return AuthorShared.Styles.label;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000046F4 File Offset: 0x000028F4
		public static GUIStyle largeLabel
		{
			get
			{
				return AuthorShared.Styles.label;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000046FC File Offset: 0x000028FC
		public static GUIStyle largeWhiteLabel
		{
			get
			{
				return AuthorShared.Styles.label;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004704 File Offset: 0x00002904
		public static GUIStyle miniButton
		{
			get
			{
				return AuthorShared.Styles.button;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000470C File Offset: 0x0000290C
		public static GUIStyle miniButtonLeft
		{
			get
			{
				return AuthorShared.Styles.button;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004714 File Offset: 0x00002914
		public static GUIStyle miniButtonMid
		{
			get
			{
				return AuthorShared.Styles.button;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000471C File Offset: 0x0000291C
		public static GUIStyle miniButtonRight
		{
			get
			{
				return AuthorShared.Styles.button;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00004724 File Offset: 0x00002924
		public static GUIStyle miniLabel
		{
			get
			{
				return AuthorShared.Styles.label;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000472C File Offset: 0x0000292C
		private static void RightAlignText(GUIStyle original, ref GUIStyle mod)
		{
			switch (original.alignment)
			{
			case 0:
			case 1:
				mod.alignment = 2;
				break;
			case 3:
			case 4:
				mod.alignment = 5;
				break;
			case 6:
			case 7:
				mod.alignment = 8;
				break;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004794 File Offset: 0x00002994
		private static void CenterAlignText(GUIStyle original, ref GUIStyle mod)
		{
			switch (original.alignment)
			{
			case 0:
			case 2:
				mod.alignment = 1;
				break;
			case 3:
			case 5:
				mod.alignment = 4;
				break;
			case 6:
			case 8:
				mod.alignment = 7;
				break;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004800 File Offset: 0x00002A00
		private static void LeftAlignText(GUIStyle original, ref GUIStyle mod)
		{
			switch (original.alignment)
			{
			case 1:
			case 2:
				mod.alignment = 0;
				break;
			case 4:
			case 5:
				mod.alignment = 3;
				break;
			case 7:
			case 8:
				mod.alignment = 6;
				break;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004868 File Offset: 0x00002A68
		private static void IconAbove(GUIStyle original, ref GUIStyle mod)
		{
			mod.imagePosition = 1;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004874 File Offset: 0x00002A74
		private static void CreateGradientOutline(GUIStyle original, ref GUIStyle mod)
		{
			mod.border = new RectOffset(1, 1, 1, 1);
			mod.normal = new GUIStyleState();
			mod.normal.background = (Texture2D)Resources.LoadAssetAtPath("Assets/AuthorSuite/Editor Resources/Icons/GradientOutline.png", typeof(Texture2D));
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000048C4 File Offset: 0x00002AC4
		private static void CreateGradientInline(GUIStyle original, ref GUIStyle mod)
		{
			mod.border = new RectOffset(1, 1, 1, 1);
			mod.normal = new GUIStyleState();
			mod.normal.background = (Texture2D)Resources.LoadAssetAtPath("Assets/AuthorSuite/Editor Resources/Icons/GradientInline.png", typeof(Texture2D));
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004914 File Offset: 0x00002B14
		private static void CreateGradientOutlineFill(GUIStyle original, ref GUIStyle mod)
		{
			mod.border = new RectOffset(1, 1, 1, 1);
			mod.normal = new GUIStyleState();
			mod.normal.background = (Texture2D)Resources.LoadAssetAtPath("Assets/AuthorSuite/Editor Resources/Icons/GradientOutlineFill.png", typeof(Texture2D));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004964 File Offset: 0x00002B64
		private static void CreateGradientInlineFill(GUIStyle original, ref GUIStyle mod)
		{
			mod.border = new RectOffset(1, 1, 1, 1);
			mod.normal = new GUIStyleState();
			mod.normal.background = (Texture2D)Resources.LoadAssetAtPath("Assets/AuthorSuite/Editor Resources/Icons/GradientInlineFill.png", typeof(Texture2D));
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000049B4 File Offset: 0x00002BB4
		private static void CreateSubSectionTitleFill(GUIStyle original, ref GUIStyle mod)
		{
			AuthorShared.Styles.CreateGradientOutlineFill(original, ref mod);
			mod.alignment = 2;
			mod.font = AuthorShared.Styles.boldLabel.font;
			mod.normal.textColor = new Color(0.03f, 0.03f, 0.03f, 1f);
			mod.stretchWidth = true;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004A10 File Offset: 0x00002C10
		private static void CreateInfoLabel(GUIStyle original, ref GUIStyle mod)
		{
			mod.alignment = 6;
			mod.normal.textColor = new Color(1f, 1f, 1f, 0.17f);
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00004A4C File Offset: 0x00002C4C
		public static GUIStyle peiceButtonLeft
		{
			get
			{
				return AuthorShared.Styles._peiceButtonLeft.GetStyle(AuthorShared.Styles.miniButtonLeft);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00004A60 File Offset: 0x00002C60
		public static GUIStyle peiceButtonMid
		{
			get
			{
				return AuthorShared.Styles._peiceButtonMid.GetStyle(AuthorShared.Styles.miniButtonMid);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00004A74 File Offset: 0x00002C74
		public static GUIStyle peiceButtonRight
		{
			get
			{
				return AuthorShared.Styles._peiceButtonRight.GetStyle(AuthorShared.Styles.miniButtonRight);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00004A88 File Offset: 0x00002C88
		public static GUIStyle palletButton
		{
			get
			{
				return AuthorShared.Styles._palletButton.GetStyle(AuthorShared.Styles.miniButton);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00004A9C File Offset: 0x00002C9C
		public static GUIStyle gradientOutline
		{
			get
			{
				return AuthorShared.Styles._gradientOutline.GetStyle(AuthorShared.Styles.box);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00004AB0 File Offset: 0x00002CB0
		public static GUIStyle gradientInline
		{
			get
			{
				return AuthorShared.Styles._gradientInline.GetStyle(AuthorShared.Styles.box);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00004AC4 File Offset: 0x00002CC4
		public static GUIStyle gradientOutlineFill
		{
			get
			{
				return AuthorShared.Styles._gradientOutlineFill.GetStyle(AuthorShared.Styles.box);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00004AD8 File Offset: 0x00002CD8
		public static GUIStyle gradientInlineFill
		{
			get
			{
				return AuthorShared.Styles._gradientInlineFill.GetStyle(AuthorShared.Styles.box);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00004AEC File Offset: 0x00002CEC
		public static GUIStyle button
		{
			get
			{
				return GUI.skin.button;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00004AF8 File Offset: 0x00002CF8
		public static GUIStyle label
		{
			get
			{
				return GUI.skin.label;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004B04 File Offset: 0x00002D04
		public static GUIStyle box
		{
			get
			{
				return GUI.skin.box;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00004B10 File Offset: 0x00002D10
		public static GUIStyle subSection
		{
			get
			{
				return AuthorShared.Styles.gradientOutline;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00004B18 File Offset: 0x00002D18
		public static GUIStyle subSectionTitle
		{
			get
			{
				return AuthorShared.Styles._subSectionTitle.GetStyle(AuthorShared.Styles.box);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00004B2C File Offset: 0x00002D2C
		public static GUIStyle infoLabel
		{
			get
			{
				return AuthorShared.Styles._infoLabel.GetStyle(AuthorShared.Styles.miniLabel);
			}
		}

		// Token: 0x04000049 RID: 73
		private static readonly AuthorShared.Styles.StyleModFunctor rightAlignText = new AuthorShared.Styles.StyleModFunctor(AuthorShared.Styles.RightAlignText);

		// Token: 0x0400004A RID: 74
		private static readonly AuthorShared.Styles.StyleModFunctor leftAlignText = new AuthorShared.Styles.StyleModFunctor(AuthorShared.Styles.LeftAlignText);

		// Token: 0x0400004B RID: 75
		private static readonly AuthorShared.Styles.StyleModFunctor centerAlignText = new AuthorShared.Styles.StyleModFunctor(AuthorShared.Styles.CenterAlignText);

		// Token: 0x0400004C RID: 76
		private static readonly AuthorShared.Styles.StyleModFunctor iconAbove = new AuthorShared.Styles.StyleModFunctor(AuthorShared.Styles.IconAbove);

		// Token: 0x0400004D RID: 77
		private static AuthorShared.Styles.StyleMod _peiceButtonLeft = new AuthorShared.Styles.StyleMod(AuthorShared.Styles.leftAlignText);

		// Token: 0x0400004E RID: 78
		private static AuthorShared.Styles.StyleMod _peiceButtonMid = new AuthorShared.Styles.StyleMod(AuthorShared.Styles.centerAlignText);

		// Token: 0x0400004F RID: 79
		private static AuthorShared.Styles.StyleMod _peiceButtonRight = new AuthorShared.Styles.StyleMod(AuthorShared.Styles.rightAlignText);

		// Token: 0x04000050 RID: 80
		private static AuthorShared.Styles.StyleMod _palletButton = new AuthorShared.Styles.StyleMod(AuthorShared.Styles.iconAbove);

		// Token: 0x04000051 RID: 81
		private static AuthorShared.Styles.StyleMod _gradientOutline = new AuthorShared.Styles.StyleMod(new AuthorShared.Styles.StyleModFunctor(AuthorShared.Styles.CreateGradientOutline));

		// Token: 0x04000052 RID: 82
		private static AuthorShared.Styles.StyleMod _gradientInline = new AuthorShared.Styles.StyleMod(new AuthorShared.Styles.StyleModFunctor(AuthorShared.Styles.CreateGradientInline));

		// Token: 0x04000053 RID: 83
		private static AuthorShared.Styles.StyleMod _gradientOutlineFill = new AuthorShared.Styles.StyleMod(new AuthorShared.Styles.StyleModFunctor(AuthorShared.Styles.CreateGradientOutlineFill));

		// Token: 0x04000054 RID: 84
		private static AuthorShared.Styles.StyleMod _gradientInlineFill = new AuthorShared.Styles.StyleMod(new AuthorShared.Styles.StyleModFunctor(AuthorShared.Styles.CreateGradientInlineFill));

		// Token: 0x04000055 RID: 85
		private static AuthorShared.Styles.StyleMod _subSectionTitle = new AuthorShared.Styles.StyleMod(new AuthorShared.Styles.StyleModFunctor(AuthorShared.Styles.CreateSubSectionTitleFill));

		// Token: 0x04000056 RID: 86
		private static AuthorShared.Styles.StyleMod _infoLabel = new AuthorShared.Styles.StyleMod(new AuthorShared.Styles.StyleModFunctor(AuthorShared.Styles.CreateInfoLabel));

		// Token: 0x02000014 RID: 20
		private struct StyleMod
		{
			// Token: 0x06000122 RID: 290 RVA: 0x00004B40 File Offset: 0x00002D40
			public StyleMod(AuthorShared.Styles.StyleModFunctor functor)
			{
				this.functor = functor;
				this.original = (this.modified = null);
			}

			// Token: 0x06000123 RID: 291 RVA: 0x00004B64 File Offset: 0x00002D64
			public GUIStyle GetStyle(GUIStyle original)
			{
				if (original == null)
				{
					return null;
				}
				if (this.original != original)
				{
					this.original = original;
					this.modified = new GUIStyle(original);
					try
					{
						this.functor(original, ref this.modified);
						this.modified = (this.modified ?? this.original);
					}
					catch (Exception ex)
					{
						Debug.LogError(ex);
					}
				}
				return this.modified;
			}

			// Token: 0x04000057 RID: 87
			public readonly AuthorShared.Styles.StyleModFunctor functor;

			// Token: 0x04000058 RID: 88
			private GUIStyle original;

			// Token: 0x04000059 RID: 89
			private GUIStyle modified;
		}

		// Token: 0x02000859 RID: 2137
		// (Invoke) Token: 0x06004B3C RID: 19260
		private delegate void StyleModFunctor(GUIStyle original, ref GUIStyle mod);
	}

	// Token: 0x02000015 RID: 21
	protected static class Icon
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00004BF8 File Offset: 0x00002DF8
		public static Texture texSolo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00004BFC File Offset: 0x00002DFC
		public static Texture texDelete
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00004C00 File Offset: 0x00002E00
		public static GUIContent solo
		{
			get
			{
				GUIContent result;
				if ((result = AuthorShared.Icon._solo) == null)
				{
					result = (AuthorShared.Icon._solo = new GUIContent(AuthorShared.Icon.texSolo, "Solo Select"));
				}
				return result;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00004C24 File Offset: 0x00002E24
		public static GUIContent delete
		{
			get
			{
				GUIContent result;
				if ((result = AuthorShared.Icon._delete) == null)
				{
					result = (AuthorShared.Icon._delete = new GUIContent(AuthorShared.Icon.texDelete, "Delete"));
				}
				return result;
			}
		}

		// Token: 0x0400005A RID: 90
		private static GUIContent _solo;

		// Token: 0x0400005B RID: 91
		private static GUIContent _delete;
	}

	// Token: 0x02000016 RID: 22
	public enum ObjectFieldFlags
	{
		// Token: 0x0400005D RID: 93
		AllowScene = 1,
		// Token: 0x0400005E RID: 94
		ForbidNull,
		// Token: 0x0400005F RID: 95
		Prefab = 4,
		// Token: 0x04000060 RID: 96
		Model = 8,
		// Token: 0x04000061 RID: 97
		Instance = 16,
		// Token: 0x04000062 RID: 98
		NotPrefab = 32,
		// Token: 0x04000063 RID: 99
		NotModel = 64,
		// Token: 0x04000064 RID: 100
		NotInstance = 128,
		// Token: 0x04000065 RID: 101
		Asset = 256,
		// Token: 0x04000066 RID: 102
		Root = 512
	}

	// Token: 0x02000017 RID: 23
	private static class Hash
	{
		// Token: 0x04000067 RID: 103
		public static readonly int s_PopupHash = "EditorPopup".GetHashCode();
	}

	// Token: 0x02000018 RID: 24
	private struct AuthorOptionGenerate
	{
		// Token: 0x04000068 RID: 104
		public AuthorCreation creation;

		// Token: 0x04000069 RID: 105
		public AuthorShared self;

		// Token: 0x0400006A RID: 106
		public AuthorPeice peice;

		// Token: 0x0400006B RID: 107
		public Type type;

		// Token: 0x0400006C RID: 108
		public bool allowSelf;

		// Token: 0x0400006D RID: 109
		public bool selectedOnly;
	}

	// Token: 0x02000019 RID: 25
	public struct PropMod
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00004C5C File Offset: 0x00002E5C
		public static AuthorShared.PropMod New()
		{
			return default(AuthorShared.PropMod);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004C74 File Offset: 0x00002E74
		public static AuthorShared.PropMod[] Get(Object o)
		{
			return new AuthorShared.PropMod[0];
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00004C7C File Offset: 0x00002E7C
		public string propertyPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00004C84 File Offset: 0x00002E84
		public string value
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00004C8C File Offset: 0x00002E8C
		public Object objectReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00004C90 File Offset: 0x00002E90
		public Object target
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004C94 File Offset: 0x00002E94
		public static void Set(Object o, AuthorShared.PropMod[] mod)
		{
		}
	}

	// Token: 0x0200001A RID: 26
	public static class Scene
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00004C98 File Offset: 0x00002E98
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00004CA0 File Offset: 0x00002EA0
		public static Matrix4x4 matrix
		{
			get
			{
				return Matrix4x4.identity;
			}
			set
			{
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00004CA4 File Offset: 0x00002EA4
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00004CAC File Offset: 0x00002EAC
		public static Color color
		{
			get
			{
				return Color.white;
			}
			set
			{
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00004CB0 File Offset: 0x00002EB0
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00004CB4 File Offset: 0x00002EB4
		public static bool lighting
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004CB8 File Offset: 0x00002EB8
		public static void GetUpAndRight(ref Vector3 forward, out Vector3 right, out Vector3 up)
		{
			forward.Normalize();
			float num = Vector3.Dot(forward, Vector3.up);
			if (num * num > 0.809999943f)
			{
				if (forward.x * forward.x <= forward.z * forward.z)
				{
					up = Vector3.Cross(forward, Vector3.right);
				}
				else
				{
					up = Vector3.Cross(forward, Vector3.forward);
				}
				up.Normalize();
				right = Vector3.Cross(forward, up);
				right.Normalize();
			}
			else
			{
				right = Vector3.Cross(forward, Vector3.up);
				right.Normalize();
				up = Vector3.Cross(forward, right);
				up.Normalize();
			}
			if (Vector3.Dot(Vector3.Cross(up, forward), right) < 0f)
			{
				right = -right;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004DD4 File Offset: 0x00002FD4
		private static void DrawSphereNow(Vector3 center, float radius)
		{
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004DD8 File Offset: 0x00002FD8
		private static void DrawCapsuleNow(Vector3 center, float radius, float height, int axis)
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004DDC File Offset: 0x00002FDC
		private static void DrawBoxNow(Vector3 center, Vector3 size)
		{
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004DE0 File Offset: 0x00002FE0
		private static void DrawBoneNow(Vector3 origin, Quaternion forward, float length, float backLength, Vector3 size)
		{
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public static void DrawSphere(Vector3 center, float radius)
		{
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004DE8 File Offset: 0x00002FE8
		public static void DrawCapsule(Vector3 center, float radius, float height, int axis)
		{
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004DEC File Offset: 0x00002FEC
		public static void DrawBox(Vector3 center, Vector3 size)
		{
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004DF0 File Offset: 0x00002FF0
		public static void DrawBone(Vector3 origin, Quaternion rot, float length, float backLength, Vector3 size)
		{
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004DF4 File Offset: 0x00002FF4
		public static bool SphereDrag(ref Vector3 center, ref float radius)
		{
			return false;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004DF8 File Offset: 0x00002FF8
		public static bool PointDrag(ref Vector3 anchor)
		{
			return false;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004DFC File Offset: 0x00002FFC
		public static bool PointDrag(ref Vector3 anchor, ref Vector3 axis)
		{
			return false;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004E00 File Offset: 0x00003000
		public static bool PivotDrag(ref Vector3 anchor, ref Vector3 axis)
		{
			return false;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004E04 File Offset: 0x00003004
		public static float? GetAxialAngleDifference(Quaternion a, Quaternion b)
		{
			float num;
			Vector3 vector;
			a.ToAngleAxis(ref num, ref vector);
			float num2;
			Vector3 vector2;
			b.ToAngleAxis(ref num2, ref vector2);
			float num3 = Vector3.Dot(vector, vector2);
			if (Mathf.Approximately(num3, 1f))
			{
				return new float?(Mathf.DeltaAngle(num, num2));
			}
			if (Mathf.Approximately(num3, -1f))
			{
				return new float?(Mathf.DeltaAngle(num, -num2));
			}
			return null;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004E78 File Offset: 0x00003078
		public static bool LimitDrag(Vector3 anchor, Vector3 axis, ref float min, ref float max)
		{
			float num = 0f;
			return AuthorShared.Scene.LimitDrag(anchor, axis, ref num, ref min, ref max) && num == 0f;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004EA8 File Offset: 0x000030A8
		public static bool LimitDrag(Vector3 anchor, Vector3 axis, ref float offset, ref float min, ref float max)
		{
			return false;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004EAC File Offset: 0x000030AC
		public static bool LimitDragBothWays(Vector3 anchor, Vector3 axis, ref float angle)
		{
			float num = 0f;
			return AuthorShared.Scene.LimitDragBothWays(anchor, axis, ref num, ref angle) && num == 0f;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004EDC File Offset: 0x000030DC
		public static bool LimitDragBothWays(Vector3 anchor, Vector3 axis, ref float offset, ref float angle)
		{
			return false;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004EE0 File Offset: 0x000030E0
		public static bool LimitDrag(Vector3 anchor, Vector3 axis, ref JointLimits limit)
		{
			float min = limit.min;
			float max = limit.max;
			if (AuthorShared.Scene.LimitDrag(anchor, axis, ref min, ref max))
			{
				limit.min = min;
				limit.max = max;
				limit.min = min;
				return true;
			}
			return false;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004F24 File Offset: 0x00003124
		public static bool LimitDrag(Vector3 anchor, Vector3 axis, ref float offset, ref JointLimits limit)
		{
			float min = limit.min;
			float max = limit.max;
			if (AuthorShared.Scene.LimitDrag(anchor, axis, ref offset, ref min, ref max))
			{
				limit.min = min;
				limit.max = max;
				return true;
			}
			return false;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004F64 File Offset: 0x00003164
		public static bool LimitDrag(Vector3 anchor, Vector3 axis, ref SoftJointLimit low, ref SoftJointLimit high)
		{
			float num = low.limit;
			float num2 = high.limit;
			if (AuthorShared.Scene.LimitDrag(anchor, axis, ref num, ref num2))
			{
				if (num != low.limit)
				{
					num = Mathf.Clamp(num, -180f, 180f);
					if (num != low.limit)
					{
						low.limit = num;
						return true;
					}
					return false;
				}
				else
				{
					if (num2 == high.limit)
					{
						return true;
					}
					num2 = Mathf.Clamp(num2, -180f, 180f);
					if (num2 != high.limit)
					{
						high.limit = num2;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005000 File Offset: 0x00003200
		public static bool LimitDrag(Vector3 anchor, Vector3 axis, ref SoftJointLimit bothWays)
		{
			float limit = bothWays.limit;
			if (AuthorShared.Scene.LimitDragBothWays(anchor, axis, ref limit))
			{
				bothWays.limit = limit;
				return true;
			}
			return false;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000502C File Offset: 0x0000322C
		public static bool LimitDrag(Vector3 anchor, Vector3 axis, ref float offset, ref SoftJointLimit low, ref SoftJointLimit high)
		{
			float num = low.limit;
			float num2 = high.limit;
			if (AuthorShared.Scene.LimitDrag(anchor, axis, ref offset, ref num, ref num2))
			{
				if (num != low.limit)
				{
					num = Mathf.Clamp(num, -180f, 180f);
					if (num != low.limit)
					{
						low.limit = num;
						return true;
					}
					return false;
				}
				else
				{
					if (num2 == high.limit)
					{
						return true;
					}
					num2 = Mathf.Clamp(num2, -180f, 180f);
					if (num2 != high.limit)
					{
						high.limit = num2;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000050CC File Offset: 0x000032CC
		public static bool LimitDrag(Vector3 anchor, Vector3 axis, ref float offset, ref SoftJointLimit bothWays)
		{
			float limit = bothWays.limit;
			if (AuthorShared.Scene.LimitDragBothWays(anchor, axis, ref offset, ref limit))
			{
				bothWays.limit = limit;
				return true;
			}
			return false;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000050FC File Offset: 0x000032FC
		private static float CapRadius(float radius, float height, int axis, int heightAxis)
		{
			if (heightAxis == axis)
			{
				return radius + height / 2f;
			}
			return radius;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005110 File Offset: 0x00003310
		private static Vector3 Direction(int i)
		{
			switch (i % 3)
			{
			default:
				return (i / 3 % 2 * (i / 3 % 2) != 1) ? Vector3.right : Vector3.left;
			case 1:
				return (i / 3 % 2 * (i / 3 % 2) != 1) ? Vector3.up : Vector3.down;
			case 2:
				return (i / 3 % 2 * (i / 3 % 2) != 1) ? Vector3.forward : Vector3.back;
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000519C File Offset: 0x0000339C
		public static bool CapsuleDrag(ref Vector3 center, ref float radius, ref float height, ref int heightAxis)
		{
			return false;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000051A0 File Offset: 0x000033A0
		public static bool BoxDrag(ref Vector3 center, ref Vector3 size)
		{
			return false;
		}

		// Token: 0x0400006E RID: 110
		private const int SHAPE_MESH = 0;

		// Token: 0x0400006F RID: 111
		private const int SHAPE_DISH = 1;

		// Token: 0x04000070 RID: 112
		private const int SHAPE_BONE = 2;

		// Token: 0x04000071 RID: 113
		private const int SHAPE_BOX = 3;

		// Token: 0x04000072 RID: 114
		private const int SHAPE_CAPSULE_X = 4;

		// Token: 0x04000073 RID: 115
		private const int SHAPE_CAPSULE_Y = 5;

		// Token: 0x04000074 RID: 116
		private const int SHAPE_CAPSULE_Z = 6;

		// Token: 0x04000075 RID: 117
		private const int SHAPE_SPHERE = 7;

		// Token: 0x04000076 RID: 118
		private const int kShapeCount = 8;

		// Token: 0x04000077 RID: 119
		private const string _ToolColor = "_Tc";

		// Token: 0x04000078 RID: 120
		private const string _Radius = "_Rv";

		// Token: 0x04000079 RID: 121
		private const string _Height = "_Hv";

		// Token: 0x0400007A RID: 122
		private const string _Sides = "_S3";

		// Token: 0x0400007B RID: 123
		private const string _LightScale = "_Lv";

		// Token: 0x0400007C RID: 124
		private const string _BoneParameters = "_B4";

		// Token: 0x0200001B RID: 27
		private static class Keyword
		{
			// Token: 0x06000152 RID: 338 RVA: 0x000051A4 File Offset: 0x000033A4
			static Keyword()
			{
				for (int i = 0; i < 8; i++)
				{
					int num = 0;
					for (int j = 0; j < 3; j++)
					{
						if ((i & 1 << j) == 1 << j)
						{
							num++;
						}
					}
					AuthorShared.Scene.Keyword.SHAPE[i] = new string[num];
					int num2 = 0;
					for (int k = 0; k < 3; k++)
					{
						if ((i & 1 << k) == 1 << k)
						{
							AuthorShared.Scene.Keyword.SHAPE[i][num2++] = AuthorShared.Scene.Keyword.BIT_STRINGS[k];
						}
					}
				}
			}

			// Token: 0x0400007D RID: 125
			private const int BIT_STRINGS_LENGTH = 3;

			// Token: 0x0400007E RID: 126
			private static readonly string[] BIT_STRINGS = new string[]
			{
				"SBA",
				"SBB",
				"SBC"
			};

			// Token: 0x0400007F RID: 127
			public static readonly string[][] SHAPE = new string[8][];
		}
	}

	// Token: 0x0200001C RID: 28
	public enum PeiceAction
	{
		// Token: 0x04000081 RID: 129
		None,
		// Token: 0x04000082 RID: 130
		AddToSelection,
		// Token: 0x04000083 RID: 131
		RemoveFromSelection,
		// Token: 0x04000084 RID: 132
		SelectSolo,
		// Token: 0x04000085 RID: 133
		Delete,
		// Token: 0x04000086 RID: 134
		Dirty,
		// Token: 0x04000087 RID: 135
		Ping
	}

	// Token: 0x0200001D RID: 29
	public struct PeiceCommand
	{
		// Token: 0x04000088 RID: 136
		public AuthorPeice peice;

		// Token: 0x04000089 RID: 137
		public AuthorShared.PeiceAction action;
	}

	// Token: 0x0200001E RID: 30
	protected class AttributeKeyValueList
	{
		// Token: 0x06000153 RID: 339 RVA: 0x0000526C File Offset: 0x0000346C
		public AttributeKeyValueList(params object[] keysThenValues) : this(keysThenValues)
		{
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005278 File Offset: 0x00003478
		public AttributeKeyValueList(IEnumerable keysThenValues)
		{
			this.dict = new Dictionary<AuthTarg, ArrayList>();
			AuthTarg? authTarg = null;
			IEnumerator enumerator = null;
			try
			{
				enumerator = keysThenValues.GetEnumerator();
				if (enumerator != null)
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						if (obj is AuthTarg)
						{
							authTarg = new AuthTarg?((AuthTarg)((int)obj));
						}
						else if (authTarg == null || object.ReferenceEquals(obj, null))
						{
							continue;
						}
						ArrayList arrayList;
						if (!this.dict.TryGetValue(authTarg.Value, out arrayList))
						{
							arrayList = (this.dict[authTarg.Value] = new ArrayList());
						}
						arrayList.Add(obj);
					}
				}
			}
			finally
			{
				if (enumerator is IDisposable)
				{
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005378 File Offset: 0x00003578
		private static void RunInstance(MonoBehaviour instance, AuthorShared.AttributeKeyValueList.AuthField attribute, ArrayList args)
		{
			object value = attribute.field.GetValue(instance);
			if ((!(value is Object)) ? (value != null) : ((Object)value))
			{
				return;
			}
			Type fieldType = attribute.field.FieldType;
			bool flag = typeof(Component).IsAssignableFrom(fieldType);
			bool flag2 = !flag && typeof(GameObject).IsAssignableFrom(fieldType);
			if (flag == flag2)
			{
				return;
			}
			if (AuthorShared.AttributeKeyValueList.Search(instance, attribute, args, flag, ref value))
			{
				attribute.field.SetValue(instance, value);
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005418 File Offset: 0x00003618
		private static bool Search(MonoBehaviour instance, AuthorShared.AttributeKeyValueList.AuthField attribute, ArrayList args, bool isComponent, ref object value)
		{
			AuthOptions authOptions = attribute.options & (AuthOptions.SearchDown | AuthOptions.SearchUp);
			bool flag = authOptions != (AuthOptions)0;
			bool flag2 = !flag || (attribute.options & AuthOptions.SearchInclusive) == AuthOptions.SearchInclusive;
			if (flag2 && AuthorShared.AttributeKeyValueList.SearchGameObject(instance.gameObject, attribute, args, isComponent, ref value))
			{
				return true;
			}
			if (flag)
			{
				if ((authOptions & AuthOptions.SearchDown) == AuthOptions.SearchDown)
				{
					if ((attribute.options & (AuthOptions.SearchUp | AuthOptions.SearchReverse)) == (AuthOptions.SearchUp | AuthOptions.SearchReverse))
					{
						if (AuthorShared.AttributeKeyValueList.SearchGameObjectUp(instance.gameObject, attribute, args, isComponent, ref value))
						{
							return true;
						}
						authOptions &= ~AuthOptions.SearchUp;
					}
					if (AuthorShared.AttributeKeyValueList.SearchGameObjectDown(instance.gameObject, attribute, args, isComponent, ref value))
					{
						return true;
					}
				}
				if ((authOptions & AuthOptions.SearchUp) == AuthOptions.SearchUp && AuthorShared.AttributeKeyValueList.SearchGameObjectUp(instance.gameObject, attribute, args, isComponent, ref value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000054DC File Offset: 0x000036DC
		private static bool SearchGameObject(GameObject self, AuthorShared.AttributeKeyValueList.AuthField attribute, ArrayList options, bool isComponent, ref object value)
		{
			foreach (object obj in options)
			{
				if (obj is Object)
				{
					Object @object = (Object)obj;
					if (@object)
					{
						if ((attribute.options & (AuthOptions)4) == (AuthOptions)0 || !(@object.name != attribute.nameMask))
						{
							if (isComponent)
							{
								Component component;
								if (@object is GameObject)
								{
									GameObject gameObject = (GameObject)@object;
									component = gameObject.GetComponent(attribute.field.FieldType);
								}
								else if (attribute.field.FieldType.IsAssignableFrom(@object.GetType()))
								{
									component = (Component)@object;
								}
								else
								{
									if (!(@object is Component))
									{
										continue;
									}
									component = ((Component)@object).GetComponent(attribute.field.FieldType);
								}
								if (component)
								{
									value = component;
									return true;
								}
							}
							else if (@object is GameObject)
							{
								value = (GameObject)@object;
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000564C File Offset: 0x0000384C
		private static IEnumerable<Component> GetComponentDown(GameObject go, Type type, Transform childSkip)
		{
			if (go && typeof(Component).IsAssignableFrom(type))
			{
				foreach (object child in go.transform)
				{
					if (child as Transform && (Transform)child != childSkip)
					{
						foreach (Component component in ((Transform)child).gameObject.GetComponentsInChildren(type, true))
						{
							yield return component;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005694 File Offset: 0x00003894
		private static IEnumerable<Component> GetComponentDown(GameObject go, Type type)
		{
			if (go && typeof(Component).IsAssignableFrom(type))
			{
				foreach (object child in go.transform)
				{
					if (child as Transform)
					{
						foreach (Component component in ((Transform)child).gameObject.GetComponentsInChildren(type, true))
						{
							yield return component;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000056CC File Offset: 0x000038CC
		private static IEnumerable<Component> GetComponentUp(GameObject go, Type type, bool andThenDown)
		{
			if (go && typeof(Component).IsAssignableFrom(type))
			{
				int upCount = 0;
				Transform parent = go.transform.parent;
				if (parent)
				{
					do
					{
						upCount++;
						foreach (Component component in parent.GetComponents(type))
						{
							yield return component;
						}
						parent = parent.parent;
					}
					while (parent);
					if (!andThenDown)
					{
						yield break;
					}
					while (upCount > 0)
					{
						parent = go.transform.parent;
						Transform skip = go.transform;
						upCount--;
						for (int i = 0; i < upCount; i++)
						{
							parent = parent.parent;
							skip = skip.parent;
						}
						foreach (Component child in AuthorShared.AttributeKeyValueList.GetComponentDown(parent.gameObject, type, skip))
						{
							yield return child;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005714 File Offset: 0x00003914
		private static bool SearchGameObjectDown(GameObject self, AuthorShared.AttributeKeyValueList.AuthField attribute, ArrayList options, bool isComponent, ref object value)
		{
			Type type = (!isComponent) ? typeof(Transform) : attribute.field.FieldType;
			foreach (object obj in options)
			{
				if (obj is Object)
				{
					Object @object = (Object)obj;
					if (@object)
					{
						GameObject go;
						if (@object is GameObject)
						{
							go = (GameObject)@object;
						}
						else
						{
							if (!(@object is Component))
							{
								continue;
							}
							go = ((Component)@object).gameObject;
						}
						foreach (Component component in AuthorShared.AttributeKeyValueList.GetComponentDown(go, type))
						{
							if ((attribute.options & (AuthOptions)4) == (AuthOptions)0 || !(component.name != attribute.nameMask))
							{
								if (isComponent)
								{
									value = component;
									return true;
								}
								GameObject gameObject = component.gameObject;
								if (gameObject)
								{
									value = gameObject;
									return true;
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000058AC File Offset: 0x00003AAC
		private static bool SearchGameObjectUp(GameObject self, AuthorShared.AttributeKeyValueList.AuthField attribute, ArrayList options, bool isComponent, ref object value)
		{
			Type type = (!isComponent) ? typeof(Transform) : attribute.field.FieldType;
			foreach (object obj in options)
			{
				if (obj is Object)
				{
					Object @object = (Object)obj;
					if (@object)
					{
						GameObject go;
						if (@object is GameObject)
						{
							go = (GameObject)@object;
						}
						else
						{
							if (!(@object is Component))
							{
								continue;
							}
							go = ((Component)@object).gameObject;
						}
						foreach (Component component in AuthorShared.AttributeKeyValueList.GetComponentUp(go, type, false))
						{
							if ((attribute.options & (AuthOptions)4) == (AuthOptions)0 || !(component.name != attribute.nameMask))
							{
								if (isComponent)
								{
									value = component;
									return true;
								}
								GameObject gameObject = component.gameObject;
								if (gameObject)
								{
									value = gameObject;
									return true;
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005A48 File Offset: 0x00003C48
		public void Run(MonoBehaviour script)
		{
			if (this.dict.Count > 0)
			{
				AuthorShared.AttributeKeyValueList.TypeRunner.Exec(script, this);
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005A64 File Offset: 0x00003C64
		public void Run(GameObject go)
		{
			if (this.dict.Count > 0 && go)
			{
				foreach (MonoBehaviour monoBehaviour in go.GetComponentsInChildren<MonoBehaviour>(true))
				{
					AuthorShared.AttributeKeyValueList.TypeRunner.Exec(monoBehaviour, this);
				}
			}
		}

		// Token: 0x0400008A RID: 138
		private Dictionary<AuthTarg, ArrayList> dict;

		// Token: 0x0200001F RID: 31
		private class AuthField
		{
			// Token: 0x0400008B RID: 139
			public FieldInfo field;

			// Token: 0x0400008C RID: 140
			public AuthOptions options;

			// Token: 0x0400008D RID: 141
			public string nameMask;
		}

		// Token: 0x02000020 RID: 32
		private class TypeRunnerPlatform
		{
			// Token: 0x06000161 RID: 353 RVA: 0x00005AC4 File Offset: 0x00003CC4
			public void Exec(object instance, AuthorShared.AttributeKeyValueList kv)
			{
				if (this.hasBase)
				{
					this.@base.Exec(instance, kv);
				}
				if (this.hasDelegate)
				{
					this.exec(instance, kv);
				}
			}

			// Token: 0x0400008E RID: 142
			public AuthorShared.AttributeKeyValueList.TypeRunnerExec exec;

			// Token: 0x0400008F RID: 143
			public AuthorShared.AttributeKeyValueList.TypeRunnerPlatform @base;

			// Token: 0x04000090 RID: 144
			public bool tested;

			// Token: 0x04000091 RID: 145
			public bool hasDelegate;

			// Token: 0x04000092 RID: 146
			public bool hasBase;
		}

		// Token: 0x02000021 RID: 33
		private static class TypeRunner
		{
			// Token: 0x06000163 RID: 355 RVA: 0x00005B10 File Offset: 0x00003D10
			private static void GeneratePlatform(Type type, out AuthorShared.AttributeKeyValueList.TypeRunnerPlatform platform)
			{
				if (type.BaseType == typeof(MonoBehaviour))
				{
					platform = null;
				}
				else if (!AuthorShared.AttributeKeyValueList.TypeRunner.platforms.TryGetValue(type.BaseType, out platform))
				{
					AuthorShared.AttributeKeyValueList.TypeRunner.GeneratePlatform(type.BaseType, out platform);
				}
				AuthorShared.AttributeKeyValueList.TypeRunnerExec typeRunnerExec = (AuthorShared.AttributeKeyValueList.TypeRunnerExec)typeof(AuthorShared.AttributeKeyValueList.TypeRunner<>).MakeGenericType(new Type[]
				{
					type
				}).GetField("exec", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).GetValue(null);
				if (typeRunnerExec != null)
				{
					if (platform != null && platform.exec != null)
					{
						typeRunnerExec = (AuthorShared.AttributeKeyValueList.TypeRunnerExec)Delegate.Combine(typeRunnerExec, platform.exec);
					}
				}
				else if (platform != null)
				{
					typeRunnerExec = platform.exec;
				}
				Dictionary<Type, AuthorShared.AttributeKeyValueList.TypeRunnerPlatform> dictionary = AuthorShared.AttributeKeyValueList.TypeRunner.platforms;
				AuthorShared.AttributeKeyValueList.TypeRunnerPlatform value;
				platform = (value = new AuthorShared.AttributeKeyValueList.TypeRunnerPlatform
				{
					@base = platform,
					exec = typeRunnerExec,
					hasBase = (platform != null),
					hasDelegate = (typeRunnerExec != null),
					tested = true
				});
				dictionary[type] = value;
			}

			// Token: 0x06000164 RID: 356 RVA: 0x00005C18 File Offset: 0x00003E18
			public static void Exec(MonoBehaviour monoBehaviour, AuthorShared.AttributeKeyValueList kv)
			{
				if (monoBehaviour)
				{
					Type type = monoBehaviour.GetType();
					if (type != typeof(MonoBehaviour))
					{
						AuthorShared.AttributeKeyValueList.TypeRunnerPlatform typeRunnerPlatform;
						if (!AuthorShared.AttributeKeyValueList.TypeRunner.platforms.TryGetValue(type, out typeRunnerPlatform))
						{
							AuthorShared.AttributeKeyValueList.TypeRunner.GeneratePlatform(type, out typeRunnerPlatform);
						}
						typeRunnerPlatform.Exec(monoBehaviour, kv);
					}
				}
			}

			// Token: 0x06000165 RID: 357 RVA: 0x00005C6C File Offset: 0x00003E6C
			public static bool TestAttribute<T>(FieldInfo field, out T[] attribs) where T : Attribute
			{
				if (Attribute.IsDefined(field, typeof(T)))
				{
					Attribute[] customAttributes = Attribute.GetCustomAttributes(field, typeof(T), false);
					if (customAttributes.Length > 0)
					{
						attribs = new T[customAttributes.Length];
						for (int i = 0; i < customAttributes.Length; i++)
						{
							attribs[i] = (T)((object)customAttributes[i]);
						}
						return true;
					}
				}
				attribs = null;
				return false;
			}

			// Token: 0x04000093 RID: 147
			private static readonly Dictionary<Type, AuthorShared.AttributeKeyValueList.TypeRunnerPlatform> platforms = new Dictionary<Type, AuthorShared.AttributeKeyValueList.TypeRunnerPlatform>();
		}

		// Token: 0x02000022 RID: 34
		private static class TypeRunner<T> where T : MonoBehaviour
		{
			// Token: 0x06000166 RID: 358 RVA: 0x00005CE0 File Offset: 0x00003EE0
			static TypeRunner()
			{
				FieldInfo[] array = typeof(T).GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				int num = array.Length;
				for (int i = 0; i < num; i++)
				{
					PostAuthAttribute[] array2;
					if (AuthorShared.AttributeKeyValueList.TypeRunner.TestAttribute<PostAuthAttribute>(array[i], out array2))
					{
						List<KeyValuePair<AuthTarg, AuthorShared.AttributeKeyValueList.AuthField>> list = new List<KeyValuePair<AuthTarg, AuthorShared.AttributeKeyValueList.AuthField>>();
						bool flag;
						do
						{
							flag = false;
							int num2 = 0;
							int num3 = array2.Length;
							do
							{
								list.Add(new KeyValuePair<AuthTarg, AuthorShared.AttributeKeyValueList.AuthField>(array2[num2].target, new AuthorShared.AttributeKeyValueList.AuthField
								{
									field = array[i],
									options = array2[num2].options,
									nameMask = array2[num2].nameMask
								}));
							}
							while (++num2 < num3);
							while (++i < num)
							{
								if (flag = AuthorShared.AttributeKeyValueList.TypeRunner.TestAttribute<PostAuthAttribute>(array[i], out array2))
								{
									break;
								}
							}
						}
						while (flag);
						AuthorShared.AttributeKeyValueList.TypeRunner<T>.fields = list.ToArray();
						AuthorShared.AttributeKeyValueList.TypeRunner<T>.fieldCount = AuthorShared.AttributeKeyValueList.TypeRunner<T>.fields.Length;
						AuthorShared.AttributeKeyValueList.TypeRunner<T>.exec = new AuthorShared.AttributeKeyValueList.TypeRunnerExec(AuthorShared.AttributeKeyValueList.TypeRunner<T>.Exec);
						return;
					}
				}
				AuthorShared.AttributeKeyValueList.TypeRunner<T>.exec = null;
				AuthorShared.AttributeKeyValueList.TypeRunner<T>.fieldCount = 0;
				AuthorShared.AttributeKeyValueList.TypeRunner<T>.fields = null;
			}

			// Token: 0x06000167 RID: 359 RVA: 0x00005DF8 File Offset: 0x00003FF8
			private static void Exec(object instance, AuthorShared.AttributeKeyValueList list)
			{
				MonoBehaviour instance2 = (MonoBehaviour)instance;
				for (int i = 0; i < AuthorShared.AttributeKeyValueList.TypeRunner<T>.fieldCount; i++)
				{
					ArrayList args;
					if (list.dict.TryGetValue(AuthorShared.AttributeKeyValueList.TypeRunner<T>.fields[i].Key, out args))
					{
						AuthorShared.AttributeKeyValueList.RunInstance(instance2, AuthorShared.AttributeKeyValueList.TypeRunner<T>.fields[i].Value, args);
					}
				}
			}

			// Token: 0x04000094 RID: 148
			private static readonly KeyValuePair<AuthTarg, AuthorShared.AttributeKeyValueList.AuthField>[] fields;

			// Token: 0x04000095 RID: 149
			private static readonly int fieldCount;

			// Token: 0x04000096 RID: 150
			private static readonly AuthorShared.AttributeKeyValueList.TypeRunnerExec exec;
		}

		// Token: 0x0200085A RID: 2138
		// (Invoke) Token: 0x06004B40 RID: 19264
		private delegate void TypeRunnerExec(object instance, AuthorShared.AttributeKeyValueList kv);
	}

	// Token: 0x0200085B RID: 2139
	// (Invoke) Token: 0x06004B44 RID: 19268
	public delegate void CustomMenuProc(object userData, string[] options, int selected);

	// Token: 0x0200085C RID: 2140
	// (Invoke) Token: 0x06004B48 RID: 19272
	private delegate bool GenerateOptions(object args, ref int selected, out GUIContent[] options, out Array values);

	// Token: 0x0200085D RID: 2141
	// (Invoke) Token: 0x06004B4C RID: 19276
	public delegate bool ArrayFieldFunctor<T>(ref T value);
}
