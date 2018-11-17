using System;
using UnityEngine;

// Token: 0x020000B3 RID: 179
public class GameInput : MonoBehaviour
{
	// Token: 0x060003D8 RID: 984 RVA: 0x000124AC File Offset: 0x000106AC
	public static global::GameInput.GameButton GetButton(string strName)
	{
		foreach (global::GameInput.GameButton gameButton in global::GameInput.Buttons)
		{
			if (gameButton.Name == strName)
			{
				return gameButton;
			}
		}
		return null;
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x060003D9 RID: 985 RVA: 0x000124EC File Offset: 0x000106EC
	public static float mouseSensitivity
	{
		get
		{
			return global::input.mousespeed;
		}
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x060003DA RID: 986 RVA: 0x000124F4 File Offset: 0x000106F4
	public static float mouseDeltaX
	{
		get
		{
			return Input.GetAxis("Mouse X") * global::GameInput.mouseSensitivity;
		}
	}

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x060003DB RID: 987 RVA: 0x00012508 File Offset: 0x00010708
	public static float mouseDeltaY
	{
		get
		{
			return Input.GetAxis("Mouse Y") * global::GameInput.mouseSensitivity;
		}
	}

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x060003DC RID: 988 RVA: 0x0001251C File Offset: 0x0001071C
	public static Vector2 mouseDelta
	{
		get
		{
			Vector2 result;
			result.x = global::GameInput.mouseDeltaX;
			result.y = global::GameInput.mouseDeltaY;
			return result;
		}
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00012544 File Offset: 0x00010744
	public static string GetConfig()
	{
		string text = string.Empty;
		foreach (global::GameInput.GameButton gameButton in global::GameInput.Buttons)
		{
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"input.bind ",
				gameButton.Name,
				" ",
				gameButton.bindingOne.ToString(),
				" ",
				gameButton.bindingTwo.ToString(),
				"\n"
			});
		}
		return text;
	}

	// Token: 0x04000332 RID: 818
	public static global::GameInput.GameButton[] Buttons = new global::GameInput.GameButton[]
	{
		new global::GameInput.GameButton("Left"),
		new global::GameInput.GameButton("Right"),
		new global::GameInput.GameButton("Up"),
		new global::GameInput.GameButton("Down"),
		new global::GameInput.GameButton("Jump"),
		new global::GameInput.GameButton("Duck"),
		new global::GameInput.GameButton("Sprint"),
		new global::GameInput.GameButton("Fire"),
		new global::GameInput.GameButton("AltFire"),
		new global::GameInput.GameButton("Reload"),
		new global::GameInput.GameButton("Use"),
		new global::GameInput.GameButton("Inventory"),
		new global::GameInput.GameButton("Flashlight"),
		new global::GameInput.GameButton("Laser"),
		new global::GameInput.GameButton("Voice"),
		new global::GameInput.GameButton("Chat")
	};

	// Token: 0x020000B4 RID: 180
	public class GameButton
	{
		// Token: 0x060003DE RID: 990 RVA: 0x000125DC File Offset: 0x000107DC
		internal GameButton(string NiceName)
		{
			this.Name = NiceName;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000125EC File Offset: 0x000107EC
		private static bool IsKeyHeld(KeyCode key)
		{
			return key != null && Input.GetKey(key);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00012600 File Offset: 0x00010800
		private static bool WasKeyPressed(KeyCode key)
		{
			return key != null && Input.GetKeyDown(key);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00012614 File Offset: 0x00010814
		private static bool WasKeyReleased(KeyCode key)
		{
			return key != null && Input.GetKeyUp(key);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00012628 File Offset: 0x00010828
		private static KeyCode? ParseKeyCode(string name)
		{
			KeyCode? result;
			try
			{
				result = new KeyCode?((int)Enum.Parse(typeof(KeyCode), name, true));
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				result = null;
			}
			return result;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00012694 File Offset: 0x00010894
		private static void SetKeyCode(ref KeyCode value, string name)
		{
			KeyCode? keyCode = global::GameInput.GameButton.ParseKeyCode(name);
			value = ((keyCode == null) ? value : keyCode.Value);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000126C4 File Offset: 0x000108C4
		public void Bind(string A, string B)
		{
			global::GameInput.GameButton.SetKeyCode(ref this.bindingOne, A);
			global::GameInput.GameButton.SetKeyCode(ref this.bindingTwo, B);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000126E0 File Offset: 0x000108E0
		public bool IsDown()
		{
			return global::GameInput.GameButton.IsKeyHeld(this.bindingOne) || (this.bindingOne != this.bindingTwo && global::GameInput.GameButton.IsKeyHeld(this.bindingTwo));
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00012720 File Offset: 0x00010920
		public bool IsPressed()
		{
			if (global::GameInput.GameButton.WasKeyPressed(this.bindingOne))
			{
				return this.bindingTwo == this.bindingOne || global::GameInput.GameButton.WasKeyPressed(this.bindingTwo) || !global::GameInput.GameButton.IsKeyHeld(this.bindingTwo);
			}
			return this.bindingTwo != this.bindingOne && global::GameInput.GameButton.WasKeyPressed(this.bindingTwo) && !global::GameInput.GameButton.IsKeyHeld(this.bindingOne);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000127A4 File Offset: 0x000109A4
		public bool IsReleased()
		{
			if (global::GameInput.GameButton.WasKeyReleased(this.bindingOne))
			{
				return this.bindingTwo == this.bindingOne || global::GameInput.GameButton.WasKeyReleased(this.bindingTwo) || !global::GameInput.GameButton.IsKeyHeld(this.bindingTwo);
			}
			return this.bindingTwo != this.bindingOne && global::GameInput.GameButton.WasKeyReleased(this.bindingTwo) && !global::GameInput.GameButton.IsKeyHeld(this.bindingOne);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00012828 File Offset: 0x00010A28
		public override string ToString()
		{
			return this.Name ?? string.Empty;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001283C File Offset: 0x00010A3C
		public static bool operator true(global::GameInput.GameButton gameButton)
		{
			return !object.ReferenceEquals(gameButton, null) && gameButton.IsDown();
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00012854 File Offset: 0x00010A54
		public static bool operator false(global::GameInput.GameButton gameButton)
		{
			return !object.ReferenceEquals(gameButton, null) && !gameButton.IsDown();
		}

		// Token: 0x04000333 RID: 819
		public readonly string Name;

		// Token: 0x04000334 RID: 820
		public KeyCode bindingOne;

		// Token: 0x04000335 RID: 821
		public KeyCode bindingTwo;
	}
}
