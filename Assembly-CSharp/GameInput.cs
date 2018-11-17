using System;
using UnityEngine;

// Token: 0x020000A0 RID: 160
public class GameInput : MonoBehaviour
{
	// Token: 0x06000360 RID: 864 RVA: 0x00010CBC File Offset: 0x0000EEBC
	public static GameInput.GameButton GetButton(string strName)
	{
		foreach (GameInput.GameButton gameButton in GameInput.Buttons)
		{
			if (gameButton.Name == strName)
			{
				return gameButton;
			}
		}
		return null;
	}

	// Token: 0x17000082 RID: 130
	// (get) Token: 0x06000361 RID: 865 RVA: 0x00010CFC File Offset: 0x0000EEFC
	public static float mouseSensitivity
	{
		get
		{
			return input.mousespeed;
		}
	}

	// Token: 0x17000083 RID: 131
	// (get) Token: 0x06000362 RID: 866 RVA: 0x00010D04 File Offset: 0x0000EF04
	public static float mouseDeltaX
	{
		get
		{
			return Input.GetAxis("Mouse X") * GameInput.mouseSensitivity;
		}
	}

	// Token: 0x17000084 RID: 132
	// (get) Token: 0x06000363 RID: 867 RVA: 0x00010D18 File Offset: 0x0000EF18
	public static float mouseDeltaY
	{
		get
		{
			return Input.GetAxis("Mouse Y") * GameInput.mouseSensitivity;
		}
	}

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x06000364 RID: 868 RVA: 0x00010D2C File Offset: 0x0000EF2C
	public static Vector2 mouseDelta
	{
		get
		{
			Vector2 result;
			result.x = GameInput.mouseDeltaX;
			result.y = GameInput.mouseDeltaY;
			return result;
		}
	}

	// Token: 0x06000365 RID: 869 RVA: 0x00010D54 File Offset: 0x0000EF54
	public static string GetConfig()
	{
		string text = string.Empty;
		foreach (GameInput.GameButton gameButton in GameInput.Buttons)
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

	// Token: 0x040002C7 RID: 711
	public static GameInput.GameButton[] Buttons = new GameInput.GameButton[]
	{
		new GameInput.GameButton("Left"),
		new GameInput.GameButton("Right"),
		new GameInput.GameButton("Up"),
		new GameInput.GameButton("Down"),
		new GameInput.GameButton("Jump"),
		new GameInput.GameButton("Duck"),
		new GameInput.GameButton("Sprint"),
		new GameInput.GameButton("Fire"),
		new GameInput.GameButton("AltFire"),
		new GameInput.GameButton("Reload"),
		new GameInput.GameButton("Use"),
		new GameInput.GameButton("Inventory"),
		new GameInput.GameButton("Flashlight"),
		new GameInput.GameButton("Laser"),
		new GameInput.GameButton("Voice"),
		new GameInput.GameButton("Chat")
	};

	// Token: 0x020000A1 RID: 161
	public class GameButton
	{
		// Token: 0x06000366 RID: 870 RVA: 0x00010DEC File Offset: 0x0000EFEC
		internal GameButton(string NiceName)
		{
			this.Name = NiceName;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00010DFC File Offset: 0x0000EFFC
		private static bool IsKeyHeld(KeyCode key)
		{
			return key != null && Input.GetKey(key);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00010E10 File Offset: 0x0000F010
		private static bool WasKeyPressed(KeyCode key)
		{
			return key != null && Input.GetKeyDown(key);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00010E24 File Offset: 0x0000F024
		private static bool WasKeyReleased(KeyCode key)
		{
			return key != null && Input.GetKeyUp(key);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00010E38 File Offset: 0x0000F038
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

		// Token: 0x0600036B RID: 875 RVA: 0x00010EA4 File Offset: 0x0000F0A4
		private static void SetKeyCode(ref KeyCode value, string name)
		{
			KeyCode? keyCode = GameInput.GameButton.ParseKeyCode(name);
			value = ((keyCode == null) ? value : keyCode.Value);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00010ED4 File Offset: 0x0000F0D4
		public void Bind(string A, string B)
		{
			GameInput.GameButton.SetKeyCode(ref this.bindingOne, A);
			GameInput.GameButton.SetKeyCode(ref this.bindingTwo, B);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00010EF0 File Offset: 0x0000F0F0
		public bool IsDown()
		{
			return GameInput.GameButton.IsKeyHeld(this.bindingOne) || (this.bindingOne != this.bindingTwo && GameInput.GameButton.IsKeyHeld(this.bindingTwo));
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00010F30 File Offset: 0x0000F130
		public bool IsPressed()
		{
			if (GameInput.GameButton.WasKeyPressed(this.bindingOne))
			{
				return this.bindingTwo == this.bindingOne || GameInput.GameButton.WasKeyPressed(this.bindingTwo) || !GameInput.GameButton.IsKeyHeld(this.bindingTwo);
			}
			return this.bindingTwo != this.bindingOne && GameInput.GameButton.WasKeyPressed(this.bindingTwo) && !GameInput.GameButton.IsKeyHeld(this.bindingOne);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00010FB4 File Offset: 0x0000F1B4
		public bool IsReleased()
		{
			if (GameInput.GameButton.WasKeyReleased(this.bindingOne))
			{
				return this.bindingTwo == this.bindingOne || GameInput.GameButton.WasKeyReleased(this.bindingTwo) || !GameInput.GameButton.IsKeyHeld(this.bindingTwo);
			}
			return this.bindingTwo != this.bindingOne && GameInput.GameButton.WasKeyReleased(this.bindingTwo) && !GameInput.GameButton.IsKeyHeld(this.bindingOne);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00011038 File Offset: 0x0000F238
		public override string ToString()
		{
			return this.Name ?? string.Empty;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001104C File Offset: 0x0000F24C
		public static bool operator true(GameInput.GameButton gameButton)
		{
			return !object.ReferenceEquals(gameButton, null) && gameButton.IsDown();
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00011064 File Offset: 0x0000F264
		public static bool operator false(GameInput.GameButton gameButton)
		{
			return !object.ReferenceEquals(gameButton, null) && !gameButton.IsDown();
		}

		// Token: 0x040002C8 RID: 712
		public readonly string Name;

		// Token: 0x040002C9 RID: 713
		public KeyCode bindingOne;

		// Token: 0x040002CA RID: 714
		public KeyCode bindingTwo;
	}
}
