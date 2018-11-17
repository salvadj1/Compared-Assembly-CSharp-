using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Facepunch.Util;
using Facepunch.Utility;
using UnityEngine;

// Token: 0x020001A9 RID: 425
public class ConsoleSystem
{
	// Token: 0x06000CB6 RID: 3254 RVA: 0x0003100C File Offset: 0x0002F20C
	public static void RegisterLogCallback(Application.LogCallback Callback, bool CallbackWritesToConsole = false)
	{
		if (global::ConsoleSystem.RegisteredLogCallback)
		{
			if (Callback != global::ConsoleSystem.LogCallback)
			{
				if (object.ReferenceEquals(Callback, null))
				{
					Application.RegisterLogCallback(null);
					global::ConsoleSystem.LogCallbackWritesToConsole = (global::ConsoleSystem.RegisteredLogCallback = false);
					global::ConsoleSystem.LogCallback = null;
				}
				else
				{
					Application.RegisterLogCallback(Callback);
					global::ConsoleSystem.LogCallback = Callback;
					global::ConsoleSystem.LogCallbackWritesToConsole = CallbackWritesToConsole;
				}
			}
			else
			{
				global::ConsoleSystem.LogCallbackWritesToConsole = CallbackWritesToConsole;
			}
		}
		else if (!object.ReferenceEquals(Callback, null))
		{
			Application.RegisterLogCallback(Callback);
			global::ConsoleSystem.RegisteredLogCallback = true;
			global::ConsoleSystem.LogCallbackWritesToConsole = CallbackWritesToConsole;
			global::ConsoleSystem.LogCallback = Callback;
		}
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x000310A4 File Offset: 0x0002F2A4
	public static bool UnregisterLogCallback(Application.LogCallback Callback)
	{
		if (global::ConsoleSystem.RegisteredLogCallback && Callback == global::ConsoleSystem.LogCallback)
		{
			global::ConsoleSystem.RegisterLogCallback(null, false);
			return true;
		}
		return false;
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x000310D8 File Offset: 0x0002F2D8
	public static void Print(object message, bool toLogFile = false)
	{
		global::ConsoleSystem.PrintLogType(3, message, toLogFile);
	}

	// Token: 0x06000CB9 RID: 3257 RVA: 0x000310E4 File Offset: 0x0002F2E4
	public static void PrintWarning(object message, bool toLogFile = false)
	{
		global::ConsoleSystem.PrintLogType(2, message, toLogFile);
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x000310F0 File Offset: 0x0002F2F0
	public static void PrintError(object message, bool toLogFile = false)
	{
		global::ConsoleSystem.PrintLogType(0, message, toLogFile);
	}

	// Token: 0x06000CBB RID: 3259 RVA: 0x000310FC File Offset: 0x0002F2FC
	public static void Log(object message)
	{
		Debug.Log(message);
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x00031104 File Offset: 0x0002F304
	public static void Log(object message, Object context)
	{
		Debug.Log(message, context);
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x00031110 File Offset: 0x0002F310
	public static void LogWarning(object message)
	{
		Debug.LogWarning(message);
	}

	// Token: 0x06000CBE RID: 3262 RVA: 0x00031118 File Offset: 0x0002F318
	public static void LogWarning(object message, Object context)
	{
		Debug.LogWarning(message, context);
	}

	// Token: 0x06000CBF RID: 3263 RVA: 0x00031124 File Offset: 0x0002F324
	public static void LogError(object message)
	{
		Debug.LogError(message);
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x0003112C File Offset: 0x0002F32C
	public static void LogError(object message, Object context)
	{
		Debug.LogError(message, context);
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x00031138 File Offset: 0x0002F338
	public static void LogException(Exception exception)
	{
		Debug.LogException(exception);
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x00031140 File Offset: 0x0002F340
	public static void LogException(Exception exception, Object context)
	{
		Debug.LogException(exception, context);
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x0003114C File Offset: 0x0002F34C
	private static void PrintLogType(LogType logType, string message, bool log = false)
	{
		if (global::global.logprint)
		{
			switch (logType)
			{
			case 0:
				global::ConsoleSystem.LogError(message);
				return;
			case 2:
				global::ConsoleSystem.LogWarning(message);
				return;
			case 3:
				global::ConsoleSystem.Log(message);
				return;
			}
		}
		if (log && !global::ConsoleSystem.LogCallbackWritesToConsole)
		{
			try
			{
				((logType != 3) ? Console.Error : Console.Out).WriteLine("Print{0}:{1}", logType, message);
			}
			catch (Exception arg)
			{
				Console.Error.WriteLine("PrintLogType Log Exception\n:{0}", arg);
			}
		}
		if (global::ConsoleSystem.RegisteredLogCallback)
		{
			try
			{
				global::ConsoleSystem.LogCallback.Invoke(message, string.Empty, logType);
			}
			catch (Exception arg2)
			{
				Console.Error.WriteLine("PrintLogType Exception\n:{0}", arg2);
			}
		}
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x00031254 File Offset: 0x0002F454
	private static void PrintLogType(LogType logType, object obj, bool log = false)
	{
		global::ConsoleSystem.PrintLogType(logType, string.Concat(obj ?? "Null"), log);
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x00031270 File Offset: 0x0002F470
	public static string CollectSavedFields(Type type)
	{
		string text = string.Empty;
		FieldInfo[] fields = type.GetFields();
		for (int i = 0; i < fields.Length; i++)
		{
			if (fields[i].IsStatic)
			{
				if (Facepunch.Util.Reflection.HasAttribute(fields[i], typeof(global::ConsoleSystem.Saved)))
				{
					string text2 = type.Name + ".";
					if (text2 == "global.")
					{
						text2 = string.Empty;
					}
					string text3 = text;
					text = string.Concat(new string[]
					{
						text3,
						text2,
						fields[i].Name,
						" ",
						fields[i].GetValue(null).ToString(),
						"\n"
					});
				}
			}
		}
		return text;
	}

	// Token: 0x06000CC6 RID: 3270 RVA: 0x00031338 File Offset: 0x0002F538
	public static string CollectSavedProperties(Type type)
	{
		string text = string.Empty;
		PropertyInfo[] properties = type.GetProperties();
		for (int i = 0; i < properties.Length; i++)
		{
			if (properties[i].GetGetMethod().IsStatic)
			{
				if (Facepunch.Util.Reflection.HasAttribute(properties[i], typeof(global::ConsoleSystem.Saved)))
				{
					string text2 = type.Name + ".";
					if (text2 == "global.")
					{
						text2 = string.Empty;
					}
					string text3 = text;
					text = string.Concat(new string[]
					{
						text3,
						text2,
						properties[i].Name,
						" ",
						properties[i].GetValue(null, null).ToString(),
						"\n"
					});
				}
			}
		}
		return text;
	}

	// Token: 0x06000CC7 RID: 3271 RVA: 0x00031408 File Offset: 0x0002F608
	public static string CollectSavedFunctions(Type type)
	{
		string text = string.Empty;
		MethodInfo[] methods = type.GetMethods();
		for (int i = 0; i < methods.Length; i++)
		{
			if (methods[i].IsStatic)
			{
				if (Facepunch.Util.Reflection.HasAttribute(methods[i], typeof(global::ConsoleSystem.Saved)))
				{
					if (methods[i].ReturnType == typeof(string))
					{
						text += methods[i].Invoke(null, null);
					}
				}
			}
		}
		return text;
	}

	// Token: 0x06000CC8 RID: 3272 RVA: 0x00031494 File Offset: 0x0002F694
	public static string SaveToConfigString()
	{
		string text = string.Empty;
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		for (int i = 0; i < assemblies.Length; i++)
		{
			Type[] types = assemblies[i].GetTypes();
			for (int j = 0; j < types.Length; j++)
			{
				if (types[j].IsSubclassOf(typeof(global::ConsoleSystem)))
				{
					text += global::ConsoleSystem.CollectSavedFields(types[j]);
					text += global::ConsoleSystem.CollectSavedProperties(types[j]);
					text += global::ConsoleSystem.CollectSavedFunctions(types[j]);
				}
			}
		}
		return text;
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x00031538 File Offset: 0x0002F738
	public static void RunFile(string strFile)
	{
		string[] array = strFile.Split(new char[]
		{
			'\n'
		}, StringSplitOptions.RemoveEmptyEntries);
		foreach (string text in array)
		{
			if (text[0] != '#')
			{
				global::ConsoleSystem.Run(text, false);
			}
		}
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x00031590 File Offset: 0x0002F790
	public static bool Run(string strCommand, bool bWantsFeedback = false)
	{
		string empty = string.Empty;
		bool result = global::ConsoleSystem.RunCommand_Clientside(strCommand, out empty, bWantsFeedback);
		if (empty.Length > 0)
		{
			Debug.Log(empty);
		}
		return result;
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x000315C0 File Offset: 0x0002F7C0
	public static bool RunCommand_Clientside(string strCommand, out string StrOutput, bool bWantsFeedback = false)
	{
		StrOutput = string.Empty;
		global::ConsoleSystem.Arg arg = new global::ConsoleSystem.Arg(strCommand);
		if (arg.Invalid)
		{
			return false;
		}
		if (!global::ConsoleSystem.RunCommand(ref arg, bWantsFeedback))
		{
			return false;
		}
		if (arg.Reply != null && arg.Reply.Length > 0)
		{
			StrOutput = arg.Reply;
		}
		return true;
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x0003161C File Offset: 0x0002F81C
	public static bool RunCommand(ref global::ConsoleSystem.Arg arg, bool bWantReply = true)
	{
		Type[] array = global::ConsoleSystem.FindTypes(arg.Class);
		if (array.Length == 0)
		{
			if (bWantReply)
			{
				arg.ReplyWith("Console class not found: " + arg.Class);
			}
			return false;
		}
		if (bWantReply)
		{
			arg.ReplyWith(string.Concat(new string[]
			{
				"command ",
				arg.Class,
				".",
				arg.Function,
				" was executed"
			}));
		}
		Type[] array2 = array;
		int i = 0;
		while (i < array2.Length)
		{
			Type type = array2[i];
			MethodInfo method = type.GetMethod(arg.Function);
			if (method != null && method.IsStatic)
			{
				if (!arg.CheckPermissions(method.GetCustomAttributes(true)))
				{
					if (bWantReply)
					{
						arg.ReplyWith("No permission: " + arg.Class + "." + arg.Function);
					}
					return false;
				}
				object[] array3 = new global::ConsoleSystem.Arg[]
				{
					arg
				};
				try
				{
					method.Invoke(null, array3);
				}
				catch (Exception ex)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"Error: ",
						arg.Class,
						".",
						arg.Function,
						" - ",
						ex.Message
					}));
					arg.ReplyWith(string.Concat(new string[]
					{
						"Error: ",
						arg.Class,
						".",
						arg.Function,
						" - ",
						ex.Message
					}));
					return false;
				}
				arg = (array3[0] as global::ConsoleSystem.Arg);
				return true;
			}
			else
			{
				FieldInfo field = type.GetField(arg.Function);
				if (field != null && field.IsStatic)
				{
					if (!arg.CheckPermissions(field.GetCustomAttributes(true)))
					{
						if (bWantReply)
						{
							arg.ReplyWith("No permission: " + arg.Class + "." + arg.Function);
						}
						return false;
					}
					Type fieldType = field.FieldType;
					if (arg.HasArgs(1))
					{
						try
						{
							string str = field.GetValue(null).ToString();
							if (fieldType == typeof(float))
							{
								field.SetValue(null, float.Parse(arg.Args[0]));
							}
							if (fieldType == typeof(int))
							{
								field.SetValue(null, int.Parse(arg.Args[0]));
							}
							if (fieldType == typeof(string))
							{
								field.SetValue(null, arg.Args[0]);
							}
							if (fieldType == typeof(bool))
							{
								field.SetValue(null, bool.Parse(arg.Args[0]));
							}
							if (bWantReply)
							{
								arg.ReplyWith(string.Concat(new string[]
								{
									arg.Class,
									".",
									arg.Function,
									": changed ",
									Facepunch.Utility.String.QuoteSafe(str),
									" to ",
									Facepunch.Utility.String.QuoteSafe(field.GetValue(null).ToString()),
									" (",
									fieldType.Name,
									")"
								}));
							}
						}
						catch (Exception)
						{
							if (bWantReply)
							{
								arg.ReplyWith("error setting value: " + arg.Class + "." + arg.Function);
							}
						}
					}
					else if (bWantReply)
					{
						arg.ReplyWith(string.Concat(new string[]
						{
							arg.Class,
							".",
							arg.Function,
							": ",
							Facepunch.Utility.String.QuoteSafe(field.GetValue(null).ToString()),
							" (",
							fieldType.Name,
							")"
						}));
					}
					return true;
				}
				else
				{
					PropertyInfo property = type.GetProperty(arg.Function);
					if (property != null && property.GetGetMethod().IsStatic && property.GetSetMethod().IsStatic)
					{
						if (!arg.CheckPermissions(property.GetCustomAttributes(true)))
						{
							if (bWantReply)
							{
								arg.ReplyWith("No permission: " + arg.Class + "." + arg.Function);
							}
							return false;
						}
						Type propertyType = property.PropertyType;
						if (arg.HasArgs(1))
						{
							try
							{
								string str2 = property.GetValue(null, null).ToString();
								if (propertyType == typeof(float))
								{
									property.SetValue(null, float.Parse(arg.Args[0]), null);
								}
								if (propertyType == typeof(int))
								{
									property.SetValue(null, int.Parse(arg.Args[0]), null);
								}
								if (propertyType == typeof(string))
								{
									property.SetValue(null, arg.Args[0], null);
								}
								if (propertyType == typeof(bool))
								{
									property.SetValue(null, bool.Parse(arg.Args[0]), null);
								}
								if (bWantReply)
								{
									arg.ReplyWith(string.Concat(new string[]
									{
										arg.Class,
										".",
										arg.Function,
										": changed ",
										Facepunch.Utility.String.QuoteSafe(str2),
										" to ",
										Facepunch.Utility.String.QuoteSafe(property.GetValue(null, null).ToString()),
										" (",
										propertyType.Name,
										")"
									}));
								}
							}
							catch (Exception)
							{
								if (bWantReply)
								{
									arg.ReplyWith("error setting value: " + arg.Class + "." + arg.Function);
								}
							}
						}
						else if (bWantReply)
						{
							arg.ReplyWith(string.Concat(new string[]
							{
								arg.Class,
								".",
								arg.Function,
								": ",
								Facepunch.Utility.String.QuoteSafe(property.GetValue(null, null).ToString()),
								" (",
								propertyType.Name,
								")"
							}));
						}
						return true;
					}
					else
					{
						i++;
					}
				}
			}
		}
		if (bWantReply)
		{
			arg.ReplyWith("Command not found: " + arg.Class + "." + arg.Function);
		}
		return false;
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x00031D24 File Offset: 0x0002FF24
	public static Type[] FindTypes(string className)
	{
		List<Type> list = new List<Type>();
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		for (int i = 0; i < assemblies.Length; i++)
		{
			Type type = assemblies[i].GetType(className);
			if (type != null)
			{
				if (type.IsSubclassOf(typeof(global::ConsoleSystem)))
				{
					list.Add(type);
				}
			}
		}
		return list.ToArray();
	}

	// Token: 0x04000841 RID: 2113
	private static bool RegisteredLogCallback;

	// Token: 0x04000842 RID: 2114
	private static bool LogCallbackWritesToConsole;

	// Token: 0x04000843 RID: 2115
	private static Application.LogCallback LogCallback;

	// Token: 0x020001AA RID: 426
	public class Arg
	{
		// Token: 0x06000CCE RID: 3278 RVA: 0x00031D94 File Offset: 0x0002FF94
		public Arg(string rconCommand)
		{
			rconCommand = global::ConsoleSystem.Arg.RemoveInvalidCharacters(rconCommand);
			if (rconCommand.IndexOf('.') <= 0 || rconCommand.IndexOf(' ', 0, rconCommand.IndexOf('.')) != -1)
			{
				rconCommand = "global." + rconCommand;
			}
			if (rconCommand.IndexOf('.') <= 0)
			{
				return;
			}
			this.Class = rconCommand.Substring(0, rconCommand.IndexOf('.'));
			if (this.Class.Length <= 1)
			{
				return;
			}
			this.Class = this.Class.ToLower();
			this.Function = rconCommand.Substring(this.Class.Length + 1);
			if (this.Function.Length <= 1)
			{
				return;
			}
			this.Invalid = false;
			if (this.Function.IndexOf(' ') <= 0)
			{
				return;
			}
			this.ArgsStr = this.Function.Substring(this.Function.IndexOf(' '));
			this.ArgsStr = this.ArgsStr.Trim();
			this.Args = Facepunch.Utility.String.SplitQuotesStrings(this.ArgsStr);
			this.Function = this.Function.Substring(0, this.Function.IndexOf(' '));
			this.Function.ToLower();
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00031F0C File Offset: 0x0003010C
		private static string RemoveInvalidCharacters(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in str)
			{
				if (char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSeparator(c) || char.IsSymbol(c))
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00031F80 File Offset: 0x00030180
		public bool CheckPermissions(object[] attributes)
		{
			foreach (object obj in attributes)
			{
				if (obj is global::ConsoleSystem.Client)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00031FB8 File Offset: 0x000301B8
		public void ReplyWith(string strValue)
		{
			this.Reply = strValue;
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00031FC4 File Offset: 0x000301C4
		public bool HasArgs(int iMinimum = 1)
		{
			return this.Args != null && this.Args.Length >= iMinimum;
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00031FE4 File Offset: 0x000301E4
		public string GetString(int iArg, string def = "")
		{
			if (this.HasArgs(iArg + 1))
			{
				return global::ConsoleSystem.Parse.DefaultString(this.Args[iArg], def);
			}
			return def;
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00032004 File Offset: 0x00030204
		public int GetInt(int iArg, int def = 0)
		{
			return global::ConsoleSystem.Parse.DefaultInt(this.GetString(iArg, null), def);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00032014 File Offset: 0x00030214
		public ulong GetUInt64(int iArg, ulong def = 0UL)
		{
			return global::ConsoleSystem.Parse.DefaultUInt64(this.GetString(iArg, null), def);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00032024 File Offset: 0x00030224
		public float GetFloat(int iArg, float def = 0f)
		{
			return global::ConsoleSystem.Parse.DefaultFloat(this.GetString(iArg, null), def);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00032034 File Offset: 0x00030234
		public bool GetBool(int iArg, bool def = false)
		{
			return global::ConsoleSystem.Parse.DefaultBool(this.GetString(iArg, null), def);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00032044 File Offset: 0x00030244
		public Enum GetEnum(Type enumType, int iArg, Enum def)
		{
			return global::ConsoleSystem.Parse.DefaultEnum(enumType, this.GetString(iArg, null), def);
		}

		// Token: 0x04000844 RID: 2116
		public string Class = string.Empty;

		// Token: 0x04000845 RID: 2117
		public string Function = string.Empty;

		// Token: 0x04000846 RID: 2118
		public string ArgsStr = string.Empty;

		// Token: 0x04000847 RID: 2119
		public string[] Args;

		// Token: 0x04000848 RID: 2120
		public bool Invalid = true;

		// Token: 0x04000849 RID: 2121
		public string Reply = string.Empty;
	}

	// Token: 0x020001AB RID: 427
	public static class Parse
	{
		// Token: 0x06000CD9 RID: 3289 RVA: 0x00032058 File Offset: 0x00030258
		public static float Float(string text)
		{
			return float.Parse(text);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00032060 File Offset: 0x00030260
		public static bool AttemptFloat(string text, out float value)
		{
			return float.TryParse(text, out value);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0003206C File Offset: 0x0003026C
		public static float DefaultFloat(string text, float @default)
		{
			float result;
			if (object.ReferenceEquals(text, null) || !global::ConsoleSystem.Parse.AttemptFloat(text, out result))
			{
				result = @default;
			}
			return result;
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00032098 File Offset: 0x00030298
		public static float DefaultFloat(string text)
		{
			return global::ConsoleSystem.Parse.DefaultFloat(text, 0f);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x000320A8 File Offset: 0x000302A8
		public static int Int(string text)
		{
			return int.Parse(text);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x000320B0 File Offset: 0x000302B0
		public static bool AttemptInt(string text, out int value)
		{
			return int.TryParse(text, out value);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x000320BC File Offset: 0x000302BC
		public static int DefaultInt(string text, int @default)
		{
			int result;
			if (object.ReferenceEquals(text, null) || !global::ConsoleSystem.Parse.AttemptInt(text, out result))
			{
				result = @default;
			}
			return result;
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x000320E8 File Offset: 0x000302E8
		public static ulong DefaultUInt64(string text, ulong @default)
		{
			if (text == null)
			{
				return @default;
			}
			ulong result = @default;
			ulong.TryParse(text, out result);
			return result;
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0003210C File Offset: 0x0003030C
		public static int DefaultInt(string text)
		{
			return global::ConsoleSystem.Parse.DefaultInt(text, 0);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00032118 File Offset: 0x00030318
		public static bool AttemptBool(string text, out bool value)
		{
			if (bool.TryParse(text, out value))
			{
				return true;
			}
			if (text.Length != 0)
			{
				decimal d2;
				if (char.IsLetter(text[0]))
				{
					decimal d;
					if (text.Length == 4)
					{
						if (string.Equals(text, "true", StringComparison.InvariantCultureIgnoreCase))
						{
							value = true;
							return true;
						}
					}
					else if (text.Length == 5)
					{
						if (string.Equals(text, "false", StringComparison.InvariantCultureIgnoreCase))
						{
							value = false;
							return true;
						}
					}
					else if (decimal.TryParse(text, out d))
					{
						value = (d != 0m);
						return true;
					}
				}
				else if (decimal.TryParse(text, out d2))
				{
					value = (d2 != 0m);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x000321E0 File Offset: 0x000303E0
		public static bool Bool(string text)
		{
			bool result;
			if (!global::ConsoleSystem.Parse.AttemptBool(text, out result))
			{
				throw new FormatException("not in the correct format.");
			}
			return result;
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00032208 File Offset: 0x00030408
		public static bool DefaultBool(string text, bool @default)
		{
			bool result;
			if (object.ReferenceEquals(text, null) || !global::ConsoleSystem.Parse.AttemptBool(text, out result))
			{
				result = @default;
			}
			return result;
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00032234 File Offset: 0x00030434
		public static bool DefaultBool(string text)
		{
			return global::ConsoleSystem.Parse.DefaultBool(text, false);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00032240 File Offset: 0x00030440
		public static TEnum Enum<TEnum>(string text) where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			return global::ConsoleSystem.Parse.VerifyEnum<TEnum>.Parse(text);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00032248 File Offset: 0x00030448
		public static bool AttemptEnum<TEnum>(string text, out TEnum value) where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			return global::ConsoleSystem.Parse.VerifyEnum<TEnum>.TryParse(text, out value);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00032254 File Offset: 0x00030454
		public static TEnum DefaultEnum<TEnum>(string text, TEnum @default) where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			TEnum result;
			if (object.ReferenceEquals(text, null) || !global::ConsoleSystem.Parse.AttemptEnum<TEnum>(text, out result))
			{
				result = @default;
			}
			return result;
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00032280 File Offset: 0x00030480
		public static TEnum DefaultEnum<TEnum>(string text) where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			return global::ConsoleSystem.Parse.DefaultEnum<TEnum>(text, default(TEnum));
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0003229C File Offset: 0x0003049C
		public static Enum Enum(Type enumType, string text)
		{
			Enum result;
			try
			{
				result = (Enum)System.Enum.Parse(enumType, text, true);
			}
			catch (Exception ex)
			{
				try
				{
					result = (Enum)System.Enum.ToObject(enumType, long.Parse(text));
				}
				catch
				{
					throw ex;
				}
			}
			return result;
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00032320 File Offset: 0x00030520
		public static bool AttemptEnum(Type enumType, string text, out Enum value)
		{
			bool result;
			try
			{
				value = (Enum)System.Enum.Parse(enumType, text, true);
				result = true;
			}
			catch
			{
				try
				{
					value = (Enum)System.Enum.ToObject(enumType, long.Parse(text));
					result = true;
				}
				catch
				{
					value = null;
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x000323B4 File Offset: 0x000305B4
		public static Enum DefaultEnum(Type enumType, string text, Enum @default)
		{
			Enum result;
			if (object.ReferenceEquals(text, null) || !global::ConsoleSystem.Parse.AttemptEnum(enumType, text, out result))
			{
				result = @default;
			}
			return result;
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x000323E0 File Offset: 0x000305E0
		public static Enum DefaultEnum(Type enumType, string text)
		{
			return global::ConsoleSystem.Parse.DefaultEnum(enumType, text, null);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x000323EC File Offset: 0x000305EC
		public static string String(string text)
		{
			if (object.ReferenceEquals(text, null))
			{
				throw new ArgumentNullException("text");
			}
			if (text.Length == 1)
			{
				throw new FormatException("Cannot use empty strings.");
			}
			return text;
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00032420 File Offset: 0x00030620
		public static bool AttemptString(string text, out string value)
		{
			if (string.IsNullOrEmpty(text))
			{
				value = string.Empty;
				return false;
			}
			value = text;
			return true;
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0003243C File Offset: 0x0003063C
		public static string DefaultString(string text, string @default)
		{
			string result;
			if (!global::ConsoleSystem.Parse.AttemptString(text, out result))
			{
				result = @default;
			}
			return result;
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0003245C File Offset: 0x0003065C
		public static string DefaultString(string text)
		{
			return global::ConsoleSystem.Parse.DefaultString(text, string.Empty);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0003246C File Offset: 0x0003066C
		public static bool IsSupported(Type type)
		{
			if (object.ReferenceEquals(type, null))
			{
				return false;
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				return typeof(bool) == type;
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
				return type.IsEnum;
			case TypeCode.Int32:
				return typeof(int) == type || type.IsEnum;
			case TypeCode.Single:
				return typeof(float) == type;
			case TypeCode.String:
				return typeof(string) == type;
			}
			return false;
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00032528 File Offset: 0x00030728
		public static bool IsSupported<T>()
		{
			return global::ConsoleSystem.Parse.PrecachedSupport<T>.IsSupported;
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00032530 File Offset: 0x00030730
		public static bool AttemptObject(Type type, string value, out object boxed)
		{
			try
			{
				switch (Type.GetTypeCode(type))
				{
				case TypeCode.Boolean:
					if (typeof(bool) == type)
					{
						boxed = global::ConsoleSystem.Parse.Bool(value);
						return true;
					}
					break;
				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
					if (type.IsEnum)
					{
						boxed = global::ConsoleSystem.Parse.Enum(type, value);
						return true;
					}
					break;
				case TypeCode.Int32:
					if (type == typeof(int))
					{
						boxed = global::ConsoleSystem.Parse.Int(value);
					}
					else
					{
						if (!type.IsEnum)
						{
							break;
						}
						boxed = global::ConsoleSystem.Parse.Enum(type, value);
					}
					return true;
				case TypeCode.Single:
					if (typeof(float) == type)
					{
						boxed = global::ConsoleSystem.Parse.Float(value);
						return true;
					}
					break;
				case TypeCode.String:
					if (typeof(string) == type)
					{
						boxed = global::ConsoleSystem.Parse.String(value);
						return true;
					}
					break;
				}
			}
			catch (Exception ex)
			{
				boxed = ex;
				return false;
			}
			boxed = null;
			return false;
		}

		// Token: 0x0400084A RID: 2122
		private const bool kEnumCaseInsensitive = true;

		// Token: 0x020001AC RID: 428
		private static class VerifyEnum<TEnum> where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			// Token: 0x06000CF5 RID: 3317 RVA: 0x000326AC File Offset: 0x000308AC
			static VerifyEnum()
			{
				if (!typeof(TEnum).IsEnum)
				{
					throw new ArgumentException("TEnum", "Is not a enum type");
				}
			}

			// Token: 0x06000CF6 RID: 3318 RVA: 0x000326E0 File Offset: 0x000308E0
			public static bool TryParse(string text, out TEnum value)
			{
				bool result;
				try
				{
					value = (TEnum)((object)System.Enum.Parse(typeof(TEnum), text, true));
					result = true;
				}
				catch
				{
					try
					{
						value = (TEnum)((object)System.Enum.ToObject(typeof(TEnum), long.Parse(text)));
						result = true;
					}
					catch
					{
						value = default(TEnum);
						result = false;
					}
				}
				return result;
			}

			// Token: 0x06000CF7 RID: 3319 RVA: 0x00032798 File Offset: 0x00030998
			public static TEnum Parse(string text)
			{
				TEnum result;
				try
				{
					result = (TEnum)((object)System.Enum.Parse(typeof(TEnum), text, true));
				}
				catch (Exception ex)
				{
					try
					{
						result = (TEnum)((object)System.Enum.ToObject(typeof(TEnum), long.Parse(text)));
					}
					catch
					{
						throw ex;
					}
				}
				return result;
			}
		}

		// Token: 0x020001AD RID: 429
		private static class PrecachedSupport<T>
		{
			// Token: 0x0400084B RID: 2123
			public static readonly bool IsSupported = global::ConsoleSystem.Parse.IsSupported(typeof(T));
		}
	}

	// Token: 0x020001AE RID: 430
	[AttributeUsage(AttributeTargets.All)]
	public sealed class Admin : Attribute
	{
	}

	// Token: 0x020001AF RID: 431
	[AttributeUsage(AttributeTargets.All)]
	public sealed class User : Attribute
	{
	}

	// Token: 0x020001B0 RID: 432
	[AttributeUsage(AttributeTargets.All)]
	public sealed class Client : Attribute
	{
	}

	// Token: 0x020001B1 RID: 433
	[AttributeUsage(AttributeTargets.All)]
	public sealed class Saved : Attribute
	{
	}

	// Token: 0x020001B2 RID: 434
	[AttributeUsage(AttributeTargets.All)]
	public sealed class Help : Attribute
	{
		// Token: 0x06000CFD RID: 3325 RVA: 0x00032868 File Offset: 0x00030A68
		public Help(string strHelp, string strArgs = "")
		{
			this.helpDescription = strHelp;
			this.argsDescription = strArgs;
		}

		// Token: 0x0400084C RID: 2124
		public string helpDescription;

		// Token: 0x0400084D RID: 2125
		public string argsDescription;
	}
}
