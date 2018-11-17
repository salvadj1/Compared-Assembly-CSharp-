using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000047 RID: 71
public class global : ConsoleSystem
{
	// Token: 0x06000275 RID: 629 RVA: 0x0000DA90 File Offset: 0x0000BC90
	[ConsoleSystem.Help("Creates an error", "")]
	[ConsoleSystem.Client]
	public static void create_error(ref ConsoleSystem.Arg arg)
	{
		Debug.LogError("this is an error");
	}

	// Token: 0x06000276 RID: 630 RVA: 0x0000DA9C File Offset: 0x0000BC9C
	[ConsoleSystem.Admin]
	[ConsoleSystem.User]
	[ConsoleSystem.Client]
	[ConsoleSystem.Help("Search for a command", "string Name")]
	public static void find(ref ConsoleSystem.Arg arg)
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
				if (types[j].IsSubclassOf(typeof(ConsoleSystem)))
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
										global.BuildMethodString(ref methods[k]),
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
										global.BuildFieldsString(ref fields[l]),
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
									global.BuildPropertyString(ref properties[m]),
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

	// Token: 0x06000277 RID: 631 RVA: 0x0000DDB4 File Offset: 0x0000BFB4
	public static string BuildMethodString(ref MethodInfo method)
	{
		string text = string.Empty;
		string text2 = "no help";
		object[] customAttributes = method.GetCustomAttributes(true);
		foreach (object obj in customAttributes)
		{
			if (obj is ConsoleSystem.Help)
			{
				text = (obj as ConsoleSystem.Help).argsDescription;
				text2 = (obj as ConsoleSystem.Help).helpDescription;
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

	// Token: 0x06000278 RID: 632 RVA: 0x0000DE60 File Offset: 0x0000C060
	public static string BuildFieldsString(ref FieldInfo field)
	{
		string str = "no help";
		object[] customAttributes = field.GetCustomAttributes(true);
		foreach (object obj in customAttributes)
		{
			if (obj is ConsoleSystem.Help)
			{
				str = (obj as ConsoleSystem.Help).helpDescription;
			}
		}
		return field.Name + " : " + str;
	}

	// Token: 0x06000279 RID: 633 RVA: 0x0000DEC4 File Offset: 0x0000C0C4
	public static string BuildPropertyString(ref PropertyInfo field)
	{
		string str = "no help";
		object[] customAttributes = field.GetCustomAttributes(true);
		foreach (object obj in customAttributes)
		{
			if (obj is ConsoleSystem.Help)
			{
				str = (obj as ConsoleSystem.Help).helpDescription;
			}
		}
		return field.Name + " : " + str;
	}

	// Token: 0x0600027A RID: 634 RVA: 0x0000DF28 File Offset: 0x0000C128
	[ConsoleSystem.User]
	[ConsoleSystem.Help("Prints something to the debug output", "string output")]
	[ConsoleSystem.Client]
	[ConsoleSystem.Admin]
	public static void echo(ref ConsoleSystem.Arg arg)
	{
		arg.ReplyWith(arg.ArgsStr);
	}

	// Token: 0x0600027B RID: 635 RVA: 0x0000DF38 File Offset: 0x0000C138
	[ConsoleSystem.Admin]
	[ConsoleSystem.Client]
	[ConsoleSystem.Help("Quits the game", "")]
	public static void quit(ref ConsoleSystem.Arg arg)
	{
		Application.Quit();
	}

	// Token: 0x040001A0 RID: 416
	[ConsoleSystem.Client]
	[ConsoleSystem.Help("When set to True, all console printing will go through Debug.Log", "")]
	[ConsoleSystem.User]
	public static bool logprint;

	// Token: 0x040001A1 RID: 417
	[ConsoleSystem.Help("Prints fps at said interval", "interval (seconds)")]
	[ConsoleSystem.Admin]
	[ConsoleSystem.User]
	[ConsoleSystem.Client]
	public static float fpslog = -1f;
}
