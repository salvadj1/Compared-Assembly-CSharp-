using System;
using System.Reflection;
using UnityEngine;

// Token: 0x0200067C RID: 1660
public class EndAllTextSolution : MonoBehaviour
{
	// Token: 0x17000B30 RID: 2864
	// (get) Token: 0x060039CC RID: 14796 RVA: 0x000D5810 File Offset: 0x000D3A10
	private static GUISkin skin
	{
		get
		{
			return GUI.skin;
		}
	}

	// Token: 0x17000B31 RID: 2865
	// (get) Token: 0x060039CD RID: 14797 RVA: 0x000D5818 File Offset: 0x000D3A18
	// (set) Token: 0x060039CE RID: 14798 RVA: 0x000D5820 File Offset: 0x000D3A20
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

	// Token: 0x060039CF RID: 14799 RVA: 0x000D5828 File Offset: 0x000D3A28
	private static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style)
	{
		if (maxLength >= 0 && content.text.Length > maxLength)
		{
			content.text = content.text.Substring(0, maxLength);
		}
		EndAllTextSolution.GUI2.CheckOnGUI();
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
				if (Event.current.clickCount == 2 && EndAllTextSolution.skin.settings.doubleClickSelectsWord)
				{
					textEditor.SelectCurrentWord();
					textEditor.DblClickSnap(0);
					textEditor.MouseDragSelectsWholeWords(true);
				}
				if (Event.current.clickCount == 3 && EndAllTextSolution.skin.settings.tripleClickSelectsLine)
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
					font = EndAllTextSolution.skin.font;
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
			EndAllTextSolution.GUI2.textFieldInput = true;
		}
		if (flag)
		{
			EndAllTextSolution.changed = true;
			content.text = textEditor.content.text;
			if (maxLength >= 0 && content.text.Length > maxLength)
			{
				content.text = content.text.Substring(0, maxLength);
			}
			current.Use();
		}
	}

	// Token: 0x060039D0 RID: 14800 RVA: 0x000D5B88 File Offset: 0x000D3D88
	private void OnGUI()
	{
		int controlID = GUIUtility.GetControlID(1);
		EndAllTextSolution.DoTextField(new Rect(0f, 0f, (float)Screen.width, 30f), controlID, this.content, this.multiLine, this.maxLength, this.styleName);
	}

	// Token: 0x04001DB9 RID: 7609
	public GUIContent content = new GUIContent();

	// Token: 0x04001DBA RID: 7610
	[SerializeField]
	private string styleName = "textfield";

	// Token: 0x04001DBB RID: 7611
	[SerializeField]
	private bool multiLine;

	// Token: 0x04001DBC RID: 7612
	[SerializeField]
	private int maxLength;

	// Token: 0x0200067D RID: 1661
	private static class GUI2
	{
		// Token: 0x060039D1 RID: 14801 RVA: 0x000D5BDC File Offset: 0x000D3DDC
		static GUI2()
		{
			MethodInfo method = typeof(GUIUtility).GetMethod("CheckOnGUI", BindingFlags.Static | BindingFlags.NonPublic);
			EndAllTextSolution.GUI2.CheckOnGUI = (EndAllTextSolution.VoidCall)Delegate.CreateDelegate(typeof(EndAllTextSolution.VoidCall), method);
			EndAllTextSolution.GUI2.textFieldInputProperty = typeof(GUIUtility).GetProperty("textFieldInput", BindingFlags.Static | BindingFlags.NonPublic);
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x060039D2 RID: 14802 RVA: 0x000D5C4C File Offset: 0x000D3E4C
		// (set) Token: 0x060039D3 RID: 14803 RVA: 0x000D5C60 File Offset: 0x000D3E60
		public static bool textFieldInput
		{
			get
			{
				return (bool)EndAllTextSolution.GUI2.textFieldInputProperty.GetValue(null, null);
			}
			set
			{
				EndAllTextSolution.GUI2.textFieldInputProperty.SetValue(null, (!value) ? EndAllTextSolution.GUI2.boxed_false : EndAllTextSolution.GUI2.boxed_true, null);
			}
		}

		// Token: 0x04001DBD RID: 7613
		public static readonly EndAllTextSolution.VoidCall CheckOnGUI;

		// Token: 0x04001DBE RID: 7614
		private static readonly PropertyInfo textFieldInputProperty;

		// Token: 0x04001DBF RID: 7615
		private static readonly object boxed_true = true;

		// Token: 0x04001DC0 RID: 7616
		private static readonly object boxed_false = false;
	}

	// Token: 0x020008DB RID: 2267
	// (Invoke) Token: 0x06004D44 RID: 19780
	private delegate void VoidCall();
}
