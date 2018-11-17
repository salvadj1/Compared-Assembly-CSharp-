using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000059 RID: 89
public class global : global::ConsoleSystem
{
	// Token: 0x060002E7 RID: 743 RVA: 0x0000F038 File Offset: 0x0000D238
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Help("Creates an error", "")]
	public static void create_error(ref global::ConsoleSystem.Arg arg)
	{
		Debug.LogError("this is an error");
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x0000F044 File Offset: 0x0000D244
	[global::ConsoleSystem.User]
	[global::ConsoleSystem.Help("Search for a command", "string Name")]
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Admin]
	public static void find(ref global::ConsoleSystem.Arg arg)
	{
		if (!arg.HasArgs(1))
		{
			return;
		}
		string text = arg.Args[0];
		string text2 = string.Empty;
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		for (int i = 0; i < assemblies.Length; i++)
		{
			Type[] types = assemblies[i].GetTypes();
			for (int j = 0; j < types.Length; j++)
			{
				if (types[j].IsSubclassOf(typeof(global::ConsoleSystem)))
				{
					MethodInfo[] methods = types[j].GetMethods();
					for (int k = 0; k < methods.Length; k++)
					{
						if (methods[k].IsStatic)
						{
							if (!(text != "*") || types[j].Name.Contains(text) || methods[k].Name.Contains(text))
							{
								if (arg.CheckPermissions(methods[k].GetCustomAttributes(true)))
								{
									string text3 = text2;
									text2 = string.Concat(new string[]
									{
										text3,
										types[j].Name,
										".",
										global::global.BuildMethodString(ref methods[k]),
										"\n"
									});
								}
							}
						}
					}
					FieldInfo[] fields = types[j].GetFields();
					for (int l = 0; l < fields.Length; l++)
					{
						if (fields[l].IsStatic)
						{
							if (!(text != "*") || types[j].Name.Contains(text) || fields[l].Name.Contains(text))
							{
								if (arg.CheckPermissions(fields[l].GetCustomAttributes(true)))
								{
									string text3 = text2;
									text2 = string.Concat(new string[]
									{
										text3,
										types[j].Name,
										".",
										global::global.BuildFieldsString(ref fields[l]),
										"\n"
									});
								}
							}
						}
					}
					PropertyInfo[] properties = types[j].GetProperties();
					for (int m = 0; m < properties.Length; m++)
					{
						if (!(text != "*") || types[j].Name.Contains(text) || properties[m].Name.Contains(text))
						{
							if (arg.CheckPermissions(properties[m].GetCustomAttributes(true)))
							{
								string text3 = text2;
								text2 = string.Concat(new string[]
								{
									text3,
									types[j].Name,
									".",
									global::global.BuildPropertyString(ref properties[m]),
									"\n"
								});
							}
						}
					}
				}
			}
		}
		arg.ReplyWith("Finding " + text + ":\n" + text2);
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x0000F35C File Offset: 0x0000D55C
	public static string BuildMethodString(ref MethodInfo method)
	{
		string text = string.Empty;
		string text2 = "no help";
		object[] customAttributes = method.GetCustomAttributes(true);
		foreach (object obj in customAttributes)
		{
			if (obj is global::ConsoleSystem.Help)
			{
				text = (obj as global::ConsoleSystem.Help).argsDescription;
				text2 = (obj as global::ConsoleSystem.Help).helpDescription;
				text = " " + text.Trim() + " ";
			}
		}
		return string.Concat(new string[]
		{
			method.Name,
			"(",
			text,
			") : ",
			text2
		});
	}

	// Token: 0x060002EA RID: 746 RVA: 0x0000F408 File Offset: 0x0000D608
	public static string BuildFieldsString(ref FieldInfo field)
	{
		string str = "no help";
		object[] customAttributes = field.GetCustomAttributes(true);
		foreach (object obj in customAttributes)
		{
			if (obj is global::ConsoleSystem.Help)
			{
				str = (obj as global::ConsoleSystem.Help).helpDescription;
			}
		}
		return field.Name + " : " + str;
	}

	// Token: 0x060002EB RID: 747 RVA: 0x0000F46C File Offset: 0x0000D66C
	public static string BuildPropertyString(ref PropertyInfo field)
	{
		string str = "no help";
		object[] customAttributes = field.GetCustomAttributes(true);
		foreach (object obj in customAttributes)
		{
			if (obj is global::ConsoleSystem.Help)
			{
				str = (obj as global::ConsoleSystem.Help).helpDescription;
			}
		}
		return field.Name + " : " + str;
	}

	// Token: 0x060002EC RID: 748 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Admin]
	[global::ConsoleSystem.Help("Prints something to the debug output", "string output")]
	[global::ConsoleSystem.User]
	public static void echo(ref global::ConsoleSystem.Arg arg)
	{
		arg.ReplyWith(arg.ArgsStr);
	}

	// Token: 0x060002ED RID: 749 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
	[global::ConsoleSystem.Help("Quits the game", "")]
	[global::ConsoleSystem.Admin]
	[global::ConsoleSystem.Client]
	public static void quit(ref global::ConsoleSystem.Arg arg)
	{
		Application.Quit();
	}

	// Token: 0x04000202 RID: 514
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.User]
	[global::ConsoleSystem.Help("When set to True, all console printing will go through Debug.Log", "")]
	public static bool logprint;

	// Token: 0x04000203 RID: 515
	[global::ConsoleSystem.Help("Prints fps at said interval", "interval (seconds)")]
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Admin]
	[global::ConsoleSystem.User]
	public static float fpslog = -1f;
}
