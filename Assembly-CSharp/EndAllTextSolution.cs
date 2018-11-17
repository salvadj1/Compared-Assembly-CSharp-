using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000740 RID: 1856
public class EndAllTextSolution : MonoBehaviour
{
	// Token: 0x17000BB2 RID: 2994
	// (get) Token: 0x06003DC0 RID: 15808 RVA: 0x000DE1F0 File Offset: 0x000DC3F0
	private static GUISkin skin
	{
		get
		{
			return GUI.skin;
		}
	}

	// Token: 0x17000BB3 RID: 2995
	// (get) Token: 0x06003DC1 RID: 15809 RVA: 0x000DE1F8 File Offset: 0x000DC3F8
	// (set) Token: 0x06003DC2 RID: 15810 RVA: 0x000DE200 File Offset: 0x000DC400
	private static bool changed
	{
		get
		{
			return GUI.changed;
		}
		set
		{
			GUI.changed = value;
		}
	}

	// Token: 0x06003DC3 RID: 15811 RVA: 0x000DE208 File Offset: 0x000DC408
	private static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style)
	{
		if (maxLength >= 0 && content.text.Length > maxLength)
		{
			content.text = content.text.Substring(0, maxLength);
		}
		global::EndAllTextSolution.GUI2.CheckOnGUI();
		TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), id);
		textEditor.content.text = content.text;
		textEditor.SaveBackup();
		textEditor.position = position;
		textEditor.style = style;
		textEditor.multiline = multiline;
		textEditor.controlID = id;
		textEditor.ClampPos();
		Event current = Event.current;
		bool flag = false;
		switch (current.type)
		{
		case 0:
			if (position.Contains(current.mousePosition))
			{
				GUIUtility.hotControl = id;
				GUIUtility.keyboardControl = id;
				textEditor.MoveCursorToPosition(Event.current.mousePosition);
				if (Event.current.clickCount == 2 && global::EndAllTextSolution.skin.settings.doubleClickSelectsWord)
				{
					textEditor.SelectCurrentWord();
					textEditor.DblClickSnap(0);
					textEditor.MouseDragSelectsWholeWords(true);
				}
				if (Event.current.clickCount == 3 && global::EndAllTextSolution.skin.settings.tripleClickSelectsLine)
				{
					textEditor.SelectCurrentParagraph();
					textEditor.MouseDragSelectsWholeWords(true);
					textEditor.DblClickSnap(1);
				}
				current.Use();
			}
			break;
		case 1:
			if (GUIUtility.hotControl == id)
			{
				textEditor.MouseDragSelectsWholeWords(false);
				GUIUtility.hotControl = 0;
				current.Use();
			}
			break;
		case 3:
			if (GUIUtility.hotControl == id)
			{
				if (!current.shift)
				{
					textEditor.SelectToPosition(Event.current.mousePosition);
				}
				else
				{
					textEditor.MoveCursorToPosition(Event.current.mousePosition);
				}
				current.Use();
			}
			break;
		case 4:
			if (GUIUtility.keyboardControl != id)
			{
				return;
			}
			if (textEditor.HandleKeyEvent(current))
			{
				current.Use();
				flag = true;
				content.text = textEditor.content.text;
			}
			else
			{
				if (current.keyCode == 9 || current.character == '\t')
				{
					return;
				}
				char character = current.character;
				if (character == '\n' && !multiline && !current.alt)
				{
					return;
				}
				Font font = style.font;
				if (font == null)
				{
					font = global::EndAllTextSolution.skin.font;
				}
				if (font.HasCharacter(character) || character == '\n')
				{
					textEditor.Insert(character);
					flag = true;
				}
				else if (character == '\0')
				{
					if (Input.compositionString.Length > 0)
					{
						textEditor.ReplaceSelection(string.Empty);
						flag = true;
					}
					current.Use();
				}
			}
			break;
		case 7:
			if (GUIUtility.keyboardControl == id)
			{
				textEditor.DrawCursor(content.text);
			}
			else
			{
				style.Draw(position, content, id, false);
			}
			break;
		}
		if (GUIUtility.keyboardControl == id)
		{
			global::EndAllTextSolution.GUI2.textFieldInput = true;
		}
		if (flag)
		{
			global::EndAllTextSolution.changed = true;
			content.text = textEditor.content.text;
			if (maxLength >= 0 && content.text.Length > maxLength)
			{
				content.text = content.text.Substring(0, maxLength);
			}
			current.Use();
		}
	}

	// Token: 0x06003DC4 RID: 15812 RVA: 0x000DE568 File Offset: 0x000DC768
	private void OnGUI()
	{
		int controlID = GUIUtility.GetControlID(1);
		global::EndAllTextSolution.DoTextField(new Rect(0f, 0f, (float)Screen.width, 30f), controlID, this.content, this.multiLine, this.maxLength, this.styleName);
	}

	// Token: 0x04001FB1 RID: 8113
	public GUIContent content = new GUIContent();

	// Token: 0x04001FB2 RID: 8114
	[SerializeField]
	private string styleName = "textfield";

	// Token: 0x04001FB3 RID: 8115
	[SerializeField]
	private bool multiLine;

	// Token: 0x04001FB4 RID: 8116
	[SerializeField]
	private int maxLength;

	// Token: 0x02000741 RID: 1857
	private static class GUI2
	{
		// Token: 0x06003DC5 RID: 15813 RVA: 0x000DE5BC File Offset: 0x000DC7BC
		static GUI2()
		{
			MethodInfo method = typeof(GUIUtility).GetMethod("CheckOnGUI", BindingFlags.Static | BindingFlags.NonPublic);
			global::EndAllTextSolution.GUI2.CheckOnGUI = (global::EndAllTextSolution.VoidCall)Delegate.CreateDelegate(typeof(global::EndAllTextSolution.VoidCall), method);
			global::EndAllTextSolution.GUI2.textFieldInputProperty = typeof(GUIUtility).GetProperty("textFieldInput", BindingFlags.Static | BindingFlags.NonPublic);
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06003DC6 RID: 15814 RVA: 0x000DE62C File Offset: 0x000DC82C
		// (set) Token: 0x06003DC7 RID: 15815 RVA: 0x000DE640 File Offset: 0x000DC840
		public static bool textFieldInput
		{
			get
			{
				return (bool)global::EndAllTextSolution.GUI2.textFieldInputProperty.GetValue(null, null);
			}
			set
			{
				global::EndAllTextSolution.GUI2.textFieldInputProperty.SetValue(null, (!value) ? global::EndAllTextSolution.GUI2.boxed_false : global::EndAllTextSolution.GUI2.boxed_true, null);
			}
		}

		// Token: 0x04001FB5 RID: 8117
		public static readonly global::EndAllTextSolution.VoidCall CheckOnGUI;

		// Token: 0x04001FB6 RID: 8118
		private static readonly PropertyInfo textFieldInputProperty;

		// Token: 0x04001FB7 RID: 8119
		private static readonly object boxed_true = true;

		// Token: 0x04001FB8 RID: 8120
		private static readonly object boxed_false = false;
	}

	// Token: 0x02000742 RID: 1858
	// (Invoke) Token: 0x06003DC9 RID: 15817
	private delegate void VoidCall();
}
