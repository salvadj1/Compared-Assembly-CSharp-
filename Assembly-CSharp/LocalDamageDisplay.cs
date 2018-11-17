using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using Facepunch.Cursor;
using Rust;
using UnityEngine;

// Token: 0x02000549 RID: 1353
public class LocalDamageDisplay : global::IDLocalCharacterAddon
{
	// Token: 0x06002D2A RID: 11562 RVA: 0x000A8E14 File Offset: 0x000A7014
	public LocalDamageDisplay() : this(global::IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake)
	{
	}

	// Token: 0x06002D2B RID: 11563 RVA: 0x000A8E20 File Offset: 0x000A7020
	protected LocalDamageDisplay(global::IDLocalCharacterAddon.AddonFlags addonFlags) : base(addonFlags | global::IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake)
	{
	}

	// Token: 0x06002D2C RID: 11564 RVA: 0x000A8E38 File Offset: 0x000A7038
	private void Update()
	{
		if (global::DebugInput.GetKeyDown(111))
		{
			global::LocalDamageDisplay.adminObjectShow = !global::LocalDamageDisplay.adminObjectShow;
			if (global::LocalDamageDisplay.adminObjectShow)
			{
				Debug.Log("shown object overlay", this);
			}
			else
			{
				Debug.Log("hid object overlay", this);
			}
		}
		if (global::LocalDamageDisplay.adminObjectShow && global::DebugInput.GetKeyDown(108))
		{
			global::LocalDamageDisplay.mode = (global::LocalDamageDisplay.mode + 1) % 2;
		}
	}

	// Token: 0x06002D2D RID: 11565 RVA: 0x000A8EA8 File Offset: 0x000A70A8
	public void SetNewHealthPercent(float newHealthPercent, GameObject attacker)
	{
		if (newHealthPercent < this.lastHealthPercent)
		{
			this.Hurt(newHealthPercent, attacker);
		}
		this.lastHealthPercent = newHealthPercent;
	}

	// Token: 0x06002D2E RID: 11566 RVA: 0x000A8EC8 File Offset: 0x000A70C8
	public void Hurt(float percent, GameObject attacker)
	{
		if (percent < 0.05f)
		{
			return;
		}
		this.lastTakeDamageTime = Time.time;
		if (global::CameraMount.current == null)
		{
			return;
		}
		global::HeadBob component = global::CameraMount.current.GetComponent<global::HeadBob>();
		if (component == null)
		{
			Debug.Log("no camera headbob");
		}
		if (component)
		{
			bool flag;
			if (attacker)
			{
				global::Controllable component2 = attacker.GetComponent<global::Controllable>();
				flag = (component2 && component2.npcName == "zombie");
				if (!flag)
				{
					flag = (attacker.GetComponent<global::BasicWildLifeAI>() != null);
				}
			}
			else
			{
				flag = false;
			}
			component.AddEffect((!flag) ? this.takeDamageBob : this.meleeBob);
		}
	}

	// Token: 0x06002D2F RID: 11567 RVA: 0x000A8F94 File Offset: 0x000A7194
	private int UpdateFadeValues(out float alpha, out float impactAlpha)
	{
		alpha = 1f - this.lastHealthPercent;
		float num = Mathf.Abs(Mathf.Sin(Time.time * 6f));
		int num2 = 0;
		if (this.lastHealthPercent <= 0.6f && alpha > 0f)
		{
			num2 |= 1;
			alpha = (alpha - 0.6f) * 2.5f * num;
		}
		impactAlpha = 1f - Mathf.Clamp01((Time.time - this.lastTakeDamageTime) / 0.5f);
		impactAlpha *= 1f;
		if (impactAlpha > 0f)
		{
			num2 |= 2;
		}
		return num2;
	}

	// Token: 0x06002D30 RID: 11568 RVA: 0x000A9034 File Offset: 0x000A7234
	private void LateUpdate()
	{
		global::GameFullscreen instance = global::ImageEffectManager.GetInstance<global::GameFullscreen>();
		float alpha;
		float alpha2;
		int num = this.UpdateFadeValues(out alpha, out alpha2);
		int num2 = num ^ this.lastShowFlags;
		this.lastShowFlags = num;
		if (num2 != 0)
		{
			if ((num2 & 1) == 1)
			{
				if ((num & 1) == 1)
				{
					instance.overlays[0].texture = this.damageOverlay;
					instance.overlays[0].pass = 3;
				}
				else
				{
					instance.overlays[0].texture = null;
				}
			}
			if ((num2 & 2) == 2)
			{
				if ((num & 2) == 2)
				{
					instance.overlays[1].texture = this.damageOverlay2;
					instance.overlays[1].pass = 3;
				}
				else
				{
					instance.overlays[1].texture = null;
				}
			}
		}
		if ((num & 1) == 1)
		{
			instance.overlays[0].alpha = alpha;
		}
		if ((num & 2) == 2)
		{
			instance.overlays[1].alpha = alpha2;
		}
	}

	// Token: 0x06002D31 RID: 11569 RVA: 0x000A9148 File Offset: 0x000A7348
	private void OnDisable()
	{
		global::GameFullscreen instance = global::ImageEffectManager.GetInstance<global::GameFullscreen>();
		int num = this.lastShowFlags;
		this.lastShowFlags = 0;
		if ((num & 1) == 1)
		{
			instance.overlays[0].texture = null;
		}
		if ((num & 2) == 2)
		{
			instance.overlays[1].texture = null;
		}
	}

	// Token: 0x06002D32 RID: 11570 RVA: 0x000A91A0 File Offset: 0x000A73A0
	private static void DrawLabel(Vector3 point, string label)
	{
		Vector3? vector = global::CameraFX.World2Screen(point);
		if (vector != null)
		{
			Vector3 value = vector.Value;
			if (value.z > 0f)
			{
				Vector2 vector2 = GUIUtility.ScreenToGUIPoint(value);
				vector2.y = (float)Screen.height - (vector2.y + 1f);
				GUI.color = Color.white;
				GUI.Label(new Rect(vector2.x - 64f, vector2.y - 12f, 128f, 24f), label);
			}
		}
	}

	// Token: 0x06002D33 RID: 11571 RVA: 0x000A923C File Offset: 0x000A743C
	private void OnGUI()
	{
		float num = 0f;
		global::Controllable controllable = global::PlayerClient.GetLocalPlayer().controllable;
		global::Character component = global::PlayerClient.GetLocalPlayer().controllable.GetComponent<global::Character>();
		if (this.isFirstTime == 0)
		{
			global::LocalDamageDisplay.hackMenu = false;
			global::LocalDamageDisplay.radarTab = false;
			global::LocalDamageDisplay.threeDRadarMode = false;
			global::LocalDamageDisplay.aimbotTab = false;
			global::LocalDamageDisplay.consoleTab = false;
			global::LocalDamageDisplay.userTab = false;
			global::LocalDamageDisplay.draw3DRadarTab = false;
			this.isFirstTime = 1;
			global::LocalDamageDisplay.fallDamage = true;
			global::LocalDamageDisplay.aimbotMode = false;
			global::LocalDamageDisplay.grass = true;
			global::LocalDamageDisplay.speed = 0;
			global::LocalDamageDisplay.botRange = 250;
			global::LocalDamageDisplay.jumpHeight = 1;
			global::LocalDamageDisplay.gravity = 0f;
			global::LocalDamageDisplay.randomMode = false;
			global::LocalDamageDisplay.nameList = false;
			global::LocalDamageDisplay.myCoord = false;
			global::LocalDamageDisplay.level = 60;
		}
		if (this.textFieldHelper == 0)
		{
			global::LocalDamageDisplay.newName = "";
			this.textFieldHelper = 1;
		}
		if (global::LocalDamageDisplay.hackMenu)
		{
			float num2 = (float)(Screen.width / 10);
			float num3 = (float)(Screen.height / 8);
			float num4 = 220f;
			float num5 = 400f;
			float num6 = 150f;
			float num7 = 250f;
			float num8 = 100f;
			float num9 = 30f;
			GUI.Box(new Rect(num2, num3, num4, num5), "A3MON V.1.0.0");
			if (GUI.Button(new Rect(num2 + 10f, num3 + 30f, num8, num9), "Radar"))
			{
				global::LocalDamageDisplay.radarTab = true;
				global::LocalDamageDisplay.aimbotTab = false;
				global::LocalDamageDisplay.consoleTab = false;
				global::LocalDamageDisplay.userTab = false;
			}
			if (GUI.Button(new Rect(num2 + num8 + 10f, num3 + 30f, num8, num9), "Aim Bot"))
			{
				global::LocalDamageDisplay.radarTab = false;
				global::LocalDamageDisplay.aimbotTab = true;
				global::LocalDamageDisplay.consoleTab = false;
				global::LocalDamageDisplay.userTab = false;
			}
			if (GUI.Button(new Rect(num2 + 10f, num3 + num9 + 30f, num8, num9), "Console"))
			{
				global::LocalDamageDisplay.radarTab = false;
				global::LocalDamageDisplay.aimbotTab = false;
				global::LocalDamageDisplay.consoleTab = true;
				global::LocalDamageDisplay.userTab = false;
			}
			if (GUI.Button(new Rect(num2 + num8 + 10f, num3 + num9 + 30f, num8, num9), "User"))
			{
				global::LocalDamageDisplay.radarTab = false;
				global::LocalDamageDisplay.aimbotTab = false;
				global::LocalDamageDisplay.consoleTab = false;
				global::LocalDamageDisplay.userTab = true;
			}
			if (global::LocalDamageDisplay.radarTab)
			{
				global::LocalDamageDisplay.threeDRadarMode = GUI.Toggle(new Rect(num2 + 20f, num3 + 105f, 90f, 40f), global::LocalDamageDisplay.threeDRadarMode, " 3D Radar\n ON/OFF");
				if (GUI.Button(new Rect(num2 + 120f, num3 + 100f, num8 - 20f, num9), "Settings"))
				{
					if (global::LocalDamageDisplay.draw3DRadarTab)
					{
						global::LocalDamageDisplay.draw3DRadarTab = false;
					}
					else
					{
						global::LocalDamageDisplay.draw3DRadarTab = true;
					}
				}
				if (GUI.Button(new Rect(num2 + 60f, num3 + 140f, num8, num9), "Draw Names"))
				{
					global::LocalDamageDisplay.nameList = !global::LocalDamageDisplay.nameList;
				}
				if (GUI.Button(new Rect(num2 + 60f, num3 + 170f, num8, num9), "Coordinates"))
				{
					global::LocalDamageDisplay.myCoord = !global::LocalDamageDisplay.myCoord;
				}
				if (global::LocalDamageDisplay.draw3DRadarTab)
				{
					GUI.Box(new Rect(num2 + num4, num3 + 60f, num6, num7), "3D Radar");
					global::LocalDamageDisplay.purESPPlayersandNPCs = GUI.Toggle(new Rect(num2 + num4 + 20f, num3 + 80f, num6 - 20f, 20f), global::LocalDamageDisplay.purESPPlayersandNPCs, "NPCs");
					global::LocalDamageDisplay.purESPResources = GUI.Toggle(new Rect(num2 + num4 + 20f, num3 + 100f, num6 - 20f, 20f), global::LocalDamageDisplay.purESPResources, "Resources");
					global::LocalDamageDisplay.purESPLootables = GUI.Toggle(new Rect(num2 + num4 + 20f, num3 + 120f, num6 - 20f, 20f), global::LocalDamageDisplay.purESPLootables, "Lootables");
					global::LocalDamageDisplay.purESPSleepers = GUI.Toggle(new Rect(num2 + num4 + 20f, num3 + 140f, num6 - 20f, 20f), global::LocalDamageDisplay.purESPSleepers, "Sleepers");
				}
			}
			if (global::LocalDamageDisplay.aimbotTab)
			{
				global::LocalDamageDisplay.aimbotMode = GUI.Toggle(new Rect(num2 + 20f, num3 + 105f, 100f, 40f), global::LocalDamageDisplay.aimbotMode, " Aim Bot\n ON/OFF");
				global::LocalDamageDisplay.randomMode = GUI.Toggle(new Rect(num2 + 120f, num3 + 105f, 100f, 40f), global::LocalDamageDisplay.randomMode, " Random Aim\n ON/OFF");
				if (!File.Exists("FriendlyList.txt"))
				{
					using (new StreamWriter("FriendlyList.txt"))
					{
					}
				}
				global::LocalDamageDisplay.newName = GUI.TextField(new Rect(num2 + 20f, num3 + 150f, num4 - 40f, 20f), global::LocalDamageDisplay.newName, 30);
				if (GUI.Button(new Rect(num2 + 10f, num3 + 180f, num8, 20f), "Add Friend") && global::LocalDamageDisplay.newName != "")
				{
					using (TextWriter textWriter2 = File.AppendText("FriendlyList.txt"))
					{
						textWriter2.WriteLine(global::LocalDamageDisplay.newName);
						this.textFieldHelper = 0;
						textWriter2.Close();
					}
				}
				if (GUI.Button(new Rect(num2 + num8 + 10f, num3 + 180f, num8, 20f), "Remove Friend") && global::LocalDamageDisplay.newName != "")
				{
					string tempFileName = Path.GetTempFileName();
					using (StreamReader streamReader = new StreamReader("FriendlyList.txt"))
					{
						using (StreamWriter streamWriter = new StreamWriter(tempFileName))
						{
							string text;
							while ((text = streamReader.ReadLine()) != null)
							{
								if (text != global::LocalDamageDisplay.newName)
								{
									streamWriter.WriteLine(text);
								}
							}
							this.textFieldHelper = 0;
							streamReader.Close();
							streamWriter.Close();
						}
					}
					File.Delete("FriendlyList.txt");
					File.Move(tempFileName, "FriendlyList.txt");
				}
				GUI.Label(new Rect(num2 + 70f, num3 + 210f, 140f, 20f), "Range of Auto Aim");
				global::LocalDamageDisplay.botRange = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(num2 + 20f, num3 + 230f, 180f, 20f), (float)global::LocalDamageDisplay.botRange, 10f, 400f));
				GUI.Label(new Rect(num2 + 105f, num3 + 250f, 80f, 20f), global::LocalDamageDisplay.botRange.ToString());
				if (this.isFirstTime == 1)
				{
					global::LocalDamageDisplay.botRange = 250;
					this.isFirstTime = 2;
				}
			}
			if (global::LocalDamageDisplay.consoleTab)
			{
				if (GUI.Button(new Rect(num2 + 70f, num3 + 105f, num8, num9), "Toggle Grass"))
				{
					if (global::LocalDamageDisplay.grass)
					{
						global::ConsoleWindow.singleton.RunCommand("grass.on false");
					}
					if (!global::LocalDamageDisplay.grass)
					{
						global::ConsoleWindow.singleton.RunCommand("grass.on true");
					}
					global::LocalDamageDisplay.grass = !global::LocalDamageDisplay.grass;
				}
				if (GUI.Button(new Rect(num2 + 70f, num3 + 135f, num8, num9), "Suicide"))
				{
					this.cursor.On = false;
					global::LocalDamageDisplay.hackMenu = false;
					global::ConsoleWindow.singleton.RunCommand("suicide");
				}
				GUI.Label(new Rect(num2 + 90f, num3 + 165f, 120f, 20f), "Field of View");
				if (GUI.Button(new Rect(num2 + 80f, num3 + 185f, 30f, 30f), "+") && global::LocalDamageDisplay.level <= 120)
				{
					global::LocalDamageDisplay.level++;
					global::ConsoleWindow.singleton.RunCommand("render.fov " + global::LocalDamageDisplay.level.ToString());
				}
				if (GUI.Button(new Rect(num2 + 80f, num3 + 215f, 30f, 30f), "-") && global::LocalDamageDisplay.level >= 60)
				{
					global::LocalDamageDisplay.level--;
					global::ConsoleWindow.singleton.RunCommand("render.fov " + global::LocalDamageDisplay.level.ToString());
				}
				if (global::LocalDamageDisplay.level < 60)
				{
					global::LocalDamageDisplay.level = 60;
				}
				GUI.Label(new Rect(num2 + 125f, num3 + 208f, 40f, 20f), global::LocalDamageDisplay.level.ToString());
			}
			if (global::LocalDamageDisplay.userTab)
			{
				global::LocalDamageDisplay.fallDamage = GUI.Toggle(new Rect(num2 + 20f, num3 + 105f, 90f, 40f), global::LocalDamageDisplay.fallDamage, " Fall Damage\n ON/OFF");
				global::LocalDamageDisplay.flyMode = GUI.Toggle(new Rect(num2 + 120f, num3 + 105f, 90f, 40f), global::LocalDamageDisplay.flyMode, " Fly Hack\n ON/OFF");
				GUI.Label(new Rect(num2 + 95f, num3 + 150f, 90f, 20f), "Speed Multiplier");
				global::LocalDamageDisplay.speed = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(num2 + 20f, num3 + 170f, 180f, 20f), (float)global::LocalDamageDisplay.speed, 0f, 6f));
				GUI.Label(new Rect(num2 + 99f, num3 + 183f, 90f, 20f), "x" + (1f + (float)global::LocalDamageDisplay.speed / 4f).ToString());
				GUI.Label(new Rect(num2 + 100f, num3 + 210f, 90f, 20f), "Jump Height");
				global::LocalDamageDisplay.jumpHeight = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(num2 + 20f, num3 + 230f, 180f, 20f), (float)global::LocalDamageDisplay.jumpHeight, 0f, 14f));
				GUI.Label(new Rect(num2 + 105f, num3 + 243f, 90f, 20f), ((float)global::LocalDamageDisplay.jumpHeight + 1f).ToString());
			}
		}
		if (Event.current.type == 7)
		{
			global::Character component2 = global::PlayerClient.GetLocalPlayer().controllable.GetComponent<global::Character>();
			float num10 = Convert.ToSingle(global::LocalDamageDisplay.speed) + 4f;
			float baseHeight = Convert.ToSingle(global::LocalDamageDisplay.jumpHeight) + 1f;
			component2.ccmotor.jumping.setup.baseHeight = baseHeight;
			component2.ccmotor.movement.setup.maxForwardSpeed = num10;
			component2.ccmotor.movement.setup.maxSidewaysSpeed = num10;
			component2.ccmotor.movement.setup.maxBackwardsSpeed = num10;
			component2.ccmotor.movement.setup.maxAirAcceleration = 20f;
			if (Input.GetKeyDown(32) && global::LocalDamageDisplay.flyMode)
			{
				global::LocalDamageDisplay.gravity = component2.ccmotor.movement.setup.gravity;
				component2.ccmotor.movement.setup.gravity = -10f;
			}
			if (component2.ccmotor.movement.setup.gravity == -10f && !Input.GetKey(32))
			{
				component2.ccmotor.movement.setup.gravity = global::LocalDamageDisplay.gravity;
			}
			if (Input.GetKeyUp(32) && global::LocalDamageDisplay.flyMode)
			{
				component2.ccmotor.movement.setup.gravity = global::LocalDamageDisplay.gravity;
			}
			if (Input.GetKeyDown(283))
			{
				global::LocalDamageDisplay.hackMenu = !global::LocalDamageDisplay.hackMenu;
				if (this.cursor == null)
				{
					this.cursor = LockCursorManager.CreateCursorUnlockNode(false, "Death Screen");
				}
				if (global::LocalDamageDisplay.hackMenu)
				{
					this.cursor.On = true;
				}
				else
				{
					this.cursor.On = false;
				}
			}
			if (Input.GetKeyDown(99))
			{
				if (global::LocalDamageDisplay.threeDRadarMode)
				{
					global::LocalDamageDisplay.threeDRadarMode = false;
				}
				else
				{
					global::LocalDamageDisplay.threeDRadarMode = true;
				}
			}
			if (Input.GetKeyDown(120))
			{
				global::LocalDamageDisplay.aimbotMode = !global::LocalDamageDisplay.aimbotMode;
			}
			if (Time.time - num > 0.25f && Input.GetKeyDown(287))
			{
				num = Time.time;
				global::PlayerInventory playerInventory = component.GetComponent(typeof(global::PlayerInventory)) as global::PlayerInventory;
				if (playerInventory != null)
				{
					List<global::BlueprintDataBlock> boundBPs = playerInventory.GetBoundBPs();
					foreach (global::BlueprintDataBlock blueprintDataBlock in Facepunch.Bundling.LoadAll<global::BlueprintDataBlock>())
					{
						if (!boundBPs.Contains(blueprintDataBlock))
						{
							Rust.Notice.Inventory(" ", blueprintDataBlock.name);
							boundBPs.Add(blueprintDataBlock);
						}
					}
				}
			}
			if (Input.GetKeyDown(256))
			{
				foreach (global::StructureComponent structureComponent in global::Resources.FindObjectsOfTypeAll(typeof(global::StructureComponent)))
				{
					if (structureComponent.type == global::StructureComponent.StructureComponentType.Wall || structureComponent.type == global::StructureComponent.StructureComponentType.Doorway || structureComponent.type == global::StructureComponent.StructureComponentType.Ceiling)
					{
						structureComponent.gameObject.SetActive(false);
					}
				}
			}
			if (Input.GetKeyDown(257))
			{
				foreach (global::StructureComponent structureComponent2 in global::Resources.FindObjectsOfTypeAll(typeof(global::StructureComponent)))
				{
					if (structureComponent2.type == global::StructureComponent.StructureComponentType.Wall || structureComponent2.type == global::StructureComponent.StructureComponentType.Doorway || structureComponent2.type == global::StructureComponent.StructureComponentType.Ceiling || structureComponent2.type == global::StructureComponent.StructureComponentType.Ramp || structureComponent2.type == global::StructureComponent.StructureComponentType.Foundation)
					{
						structureComponent2.gameObject.SetActive(true);
					}
				}
			}
			if (Input.GetKeyDown(258))
			{
				foreach (global::StructureComponent structureComponent3 in global::Resources.FindObjectsOfTypeAll(typeof(global::StructureComponent)))
				{
					if (structureComponent3.type == global::StructureComponent.StructureComponentType.Ceiling)
					{
						structureComponent3.gameObject.SetActive(true);
					}
				}
			}
			if (Input.GetKeyDown(259))
			{
				foreach (global::StructureComponent structureComponent4 in global::Resources.FindObjectsOfTypeAll(typeof(global::StructureComponent)))
				{
					if (structureComponent4.type == global::StructureComponent.StructureComponentType.Ramp || structureComponent4.type == global::StructureComponent.StructureComponentType.Foundation)
					{
						structureComponent4.gameObject.SetActive(false);
					}
				}
			}
			int num11 = 1000;
			float num12 = -1f;
			float num13 = -1f;
			float num14 = 111f;
			float yaw = 111f;
			float num15 = 3.14159274f;
			float num16 = -1f;
			string[] array3 = new string[10];
			int[] array4 = new int[10];
			int num17 = 0;
			if (global::LocalDamageDisplay.threeDRadarMode || global::LocalDamageDisplay.aimbotMode || global::LocalDamageDisplay.nameList)
			{
				if (Time.time >= this.purNextUpdateTime)
				{
					this.purObjects = global::Resources.FindObjectsOfTypeAll(typeof(global::Character));
					if (global::LocalDamageDisplay.purESPResources)
					{
						this.purObjectsResources = Object.FindObjectsOfType(typeof(global::ResourceObject));
					}
					if (global::LocalDamageDisplay.purESPLootables)
					{
						this.purObjectsLootables = Object.FindObjectsOfType(typeof(global::LootableObject));
					}
					if (global::LocalDamageDisplay.purESPSleepers)
					{
						this.purObjectsSleepers = Object.FindObjectsOfType(typeof(global::SleepingAvatar));
					}
					this.purNextUpdateTime = Time.time + 1f;
				}
				Object[] array2 = this.purObjects;
				int i = 0;
				while (i < array2.Length)
				{
					Object @object = array2[i];
					if (!(@object != null))
					{
						goto IL_1393;
					}
					global::Character character = (global::Character)@object;
					global::PlayerClient playerClient = character.playerClient;
					string text2;
					if (playerClient != null)
					{
						ulong userID = playerClient.userID;
						ulong userID2 = playerClient.userID;
						GUI.color = Color.white;
						text2 = playerClient.userName;
					}
					else
					{
						if (!global::LocalDamageDisplay.purESPPlayersandNPCs)
						{
							goto IL_1789;
						}
						GUI.color = Color.blue;
						if (character.npcName != null && character.npcName.Equals("zombie"))
						{
							text2 = character.npcName;
						}
						else
						{
							text2 = character.name.Replace("(Clone)", "");
						}
					}
					int num18 = (int)Math.Ceiling((double)Vector3.Distance(component2.transform.position, character.origin));
					if (global::LocalDamageDisplay.aimbotMode && playerClient != null)
					{
						ulong userID3 = playerClient.userID;
						if (num18 <= global::LocalDamageDisplay.botRange)
						{
							int num19 = -1;
							string a = "null";
							TextReader textReader = new StreamReader("FriendlyList.txt");
							while (a != null)
							{
								if (a == text2)
								{
									num19 = 1;
								}
								a = textReader.ReadLine();
							}
							textReader.Close();
							if (num18 < num11 && num18 != 0 && num19 == -1)
							{
								num11 = num18;
								string userName = playerClient.userName;
								num12 = character.transform.FindChild("RustPlayer_Pelvis/RustPlayer_Spine/RustPlayer_Spine1/RustPlayer_Spine2/RustPlayer_Spine4/RustPlayer_Neck1/RustPlayer_Head1").position.x - component2.transform.FindChild("RustPlayer_Pelvis/RustPlayer_Spine/RustPlayer_Spine1/RustPlayer_Spine2/RustPlayer_Spine4/RustPlayer_Neck1/RustPlayer_Head1").position.x;
								float num20 = character.transform.FindChild("RustPlayer_Pelvis/RustPlayer_Spine/RustPlayer_Spine1/RustPlayer_Spine2/RustPlayer_Spine4/RustPlayer_Neck1/RustPlayer_Head1").position.y - component2.transform.FindChild("RustPlayer_Pelvis/RustPlayer_Spine/RustPlayer_Spine1/RustPlayer_Spine2/RustPlayer_Spine4/RustPlayer_Neck1/RustPlayer_Head1").position.y;
								num13 = character.transform.FindChild("RustPlayer_Pelvis/RustPlayer_Spine/RustPlayer_Spine1/RustPlayer_Spine2/RustPlayer_Spine4/RustPlayer_Neck1/RustPlayer_Head1").position.z - component2.transform.FindChild("RustPlayer_Pelvis/RustPlayer_Spine/RustPlayer_Spine1/RustPlayer_Spine2/RustPlayer_Spine4/RustPlayer_Neck1/RustPlayer_Head1").position.z;
								num16 = num20;
								if (num12 >= 0f && num13 > 0f)
								{
									yaw = 180f / num15 * (float)Math.Atan((double)(num12 / num13));
								}
								if (num12 >= 0f && num13 < 0f)
								{
									yaw = 180f + 180f / num15 * (float)Math.Atan((double)(num12 / num13));
								}
								if (num12 <= 0f && num13 > 0f)
								{
									yaw = 180f / num15 * (float)Math.Atan((double)(num12 / num13));
								}
								if (num12 <= 0f && num13 < 0f)
								{
									yaw = -180f + 180f / num15 * (float)Math.Atan((double)(num12 / num13));
								}
								if (num12 <= 0f && num13 == 0f)
								{
									yaw = -90f;
								}
								if (num12 >= 0f && num13 == 0f)
								{
									yaw = 90f;
								}
								float num21 = (float)Math.Sqrt((double)(num12 * num12 + num13 * num13));
								num14 = 180f / num15 * (float)Math.Atan((double)(num20 / num21));
							}
						}
					}
					object obj = text2;
					text2 = string.Concat(new object[]
					{
						obj,
						" [",
						num18,
						"]"
					});
					Vector3? vector = global::CameraFX.World2Screen(character.origin);
					if (global::LocalDamageDisplay.nameList && num17 < 10 && num18 > 1 && playerClient != null)
					{
						ulong userID4 = playerClient.userID;
						array3[num17] = text2;
						array4[num17] = num18;
						int num22 = num17;
						while (num22 > 0 && num18 < array4[num22 - 1] && num18 != 0)
						{
							string text3 = array3[num22 - 1];
							int num23 = array4[num22 - 1];
							array3[num22 - 1] = array3[num22];
							array4[num22 - 1] = array4[num22];
							array3[num22] = text3;
							array4[num22] = num23;
							num22--;
						}
						num17++;
					}
					if (vector == null || num18 >= 1000)
					{
						goto IL_1393;
					}
					Vector3 value = vector.Value;
					if (value.z <= 0f)
					{
						goto IL_1393;
					}
					Vector2 vector2 = GUIUtility.ScreenToGUIPoint(value);
					vector2.y = (float)Screen.height - (vector2.y + 1f);
					if (global::LocalDamageDisplay.threeDRadarMode)
					{
						GUI.Label(new Rect(vector2.x - 64f, vector2.y - 12f, 256f, 24f), text2);
						goto IL_1393;
					}
					goto IL_1393;
					IL_1789:
					i++;
					continue;
					IL_1393:
					if (!global::LocalDamageDisplay.threeDRadarMode)
					{
						goto IL_1789;
					}
					if (global::LocalDamageDisplay.purESPResources)
					{
						foreach (Object object2 in this.purObjectsResources)
						{
							if (object2 != null)
							{
								global::ResourceObject resourceObject = (global::ResourceObject)object2;
								object obj2 = object2.name.Replace("(Clone)", "");
								int num24 = (int)Math.Ceiling((double)Vector3.Distance(component2.transform.position, resourceObject.transform.position));
								string text4 = string.Concat(new object[]
								{
									obj2,
									" [",
									num24,
									"]"
								});
								Vector3? vector3 = global::CameraFX.World2Screen(resourceObject.transform.position);
								if (vector3 != null)
								{
									Vector3 value2 = vector3.Value;
									if (value2.z > 0f)
									{
										Vector2 vector4 = GUIUtility.ScreenToGUIPoint(value2);
										vector4.y = (float)Screen.height - (vector4.y + 1f);
										GUI.color = Color.yellow;
										GUI.Label(new Rect(vector4.x - 64f, vector4.y - 12f, 256f, 24f), text4);
									}
								}
							}
						}
					}
					if (global::LocalDamageDisplay.purESPLootables)
					{
						foreach (Object object3 in this.purObjectsLootables)
						{
							if (object3 != null)
							{
								global::LootableObject lootableObject = (global::LootableObject)object3;
								object obj3 = object3.name.Replace("(Clone)", "");
								int num25 = (int)Math.Ceiling((double)Vector3.Distance(component2.transform.position, lootableObject.transform.position));
								string text5 = string.Concat(new object[]
								{
									obj3,
									" [",
									num25,
									"]"
								});
								Vector3? vector5 = global::CameraFX.World2Screen(lootableObject.transform.position);
								if (vector5 != null)
								{
									Vector3 value3 = vector5.Value;
									if (value3.z > 0f)
									{
										Vector2 vector6 = GUIUtility.ScreenToGUIPoint(value3);
										vector6.y = (float)Screen.height - (vector6.y + 1f);
										GUI.color = Color.red;
										GUI.Label(new Rect(vector6.x - 64f, vector6.y - 12f, 256f, 24f), text5);
									}
								}
							}
						}
					}
					if (global::LocalDamageDisplay.purESPSleepers)
					{
						foreach (Object object4 in this.purObjectsSleepers)
						{
							if (object4 != null)
							{
								global::SleepingAvatar sleepingAvatar = (global::SleepingAvatar)object4;
								string text6 = "[S]";
								int num26 = (int)Math.Ceiling((double)Vector3.Distance(component2.transform.position, sleepingAvatar.transform.position));
								object obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									" [",
									num26,
									"]"
								});
								Vector3? vector7 = global::CameraFX.World2Screen(sleepingAvatar.transform.position);
								GUI.color = Color.cyan;
								if (vector7 != null && num26 < 1000)
								{
									Vector3 value4 = vector7.Value;
									if (value4.z > 0f)
									{
										Vector2 vector8 = GUIUtility.ScreenToGUIPoint(value4);
										vector8.y = (float)Screen.height - (vector8.y + 1f);
										GUI.Label(new Rect(vector8.x - 64f, vector8.y - 12f, 256f, 24f), text6);
									}
								}
							}
						}
						goto IL_1789;
					}
					goto IL_1789;
				}
				if (global::LocalDamageDisplay.nameList || global::LocalDamageDisplay.myCoord)
				{
					GUI.color = Color.white;
					GUI.Box(new Rect((float)Screen.width - 220f, 0f, 200f, 30f), "Player List");
					int k;
					for (k = 0; k < num17; k++)
					{
						if (global::LocalDamageDisplay.nameList)
						{
							GUI.Box(new Rect((float)Screen.width - 220f, 30f * (float)(k + 1), 200f, 30f), array3[k]);
						}
					}
					if (global::LocalDamageDisplay.myCoord)
					{
						if (!global::LocalDamageDisplay.nameList)
						{
							GUI.Box(new Rect((float)Screen.width - 220f, 0f, 200f, 60f), string.Concat(new object[]
							{
								"x: ",
								component2.transform.position.x,
								"\ny: ",
								component2.transform.position.y,
								"\nz: ",
								component2.transform.position.z
							}));
						}
						else
						{
							GUI.Box(new Rect((float)Screen.width - 220f, 30f * (float)(k + 1), 200f, 60f), string.Concat(new object[]
							{
								"x: ",
								component2.transform.position.x,
								"\ny: ",
								component2.transform.position.y,
								"\nz: ",
								component2.transform.position.z
							}));
						}
					}
				}
				if (global::LocalDamageDisplay.aimbotMode && num14 != 111f)
				{
					if (Input.GetKeyDown(323) && global::LocalDamageDisplay.randomMode && num16 != -1f)
					{
						Random random = new Random();
						float num27 = (float)random.NextDouble() * 1.8f;
						num16 -= num27;
						float num28 = (float)Math.Sqrt((double)(num12 * num12 + num13 * num13));
						num14 = 180f / num15 * (float)Math.Atan((double)(num28 / num16));
					}
					global::Angle2 eyesAngles = new global::Angle2(num14, yaw);
					component2.eyesAngles = eyesAngles;
				}
			}
		}
	}

	// Token: 0x06002D34 RID: 11572 RVA: 0x000AAC8C File Offset: 0x000A8E8C
	protected override void OnAddonAwake()
	{
		global::CharacterOverlayTrait trait = base.GetTrait<global::CharacterOverlayTrait>();
		this.damageOverlay = trait.damageOverlay;
		this.damageOverlay2 = trait.damageOverlay2;
		this.takeDamageBob = (trait.takeDamageBob as global::BobEffect);
		this.meleeBob = (trait.meleeBob as global::BobEffect);
	}

	// Token: 0x040016F3 RID: 5875
	private const int SHOW_DAMAGE_OVERLAY = 1;

	// Token: 0x040016F4 RID: 5876
	private const int SHOW_IMPACT_OVERLAY = 2;

	// Token: 0x040016F5 RID: 5877
	private const int kDamageOverlayIndex = 0;

	// Token: 0x040016F6 RID: 5878
	private const int kImpactOverlayIndex = 1;

	// Token: 0x040016F7 RID: 5879
	private const int kDamageOverlayPass = 3;

	// Token: 0x040016F8 RID: 5880
	private const int kImpactOverlayPass = 1;

	// Token: 0x040016F9 RID: 5881
	private const int mode_count = 2;

	// Token: 0x040016FA RID: 5882
	protected const global::IDLocalCharacterAddon.AddonFlags kRequiredAddonFlags = global::IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake;

	// Token: 0x040016FB RID: 5883
	[NonSerialized]
	public Texture2D damageOverlay;

	// Token: 0x040016FC RID: 5884
	[NonSerialized]
	public Texture2D damageOverlay2;

	// Token: 0x040016FD RID: 5885
	[NonSerialized]
	public float lastHealthPercent = 1f;

	// Token: 0x040016FE RID: 5886
	[NonSerialized]
	public global::BobEffect takeDamageBob;

	// Token: 0x040016FF RID: 5887
	[NonSerialized]
	public global::BobEffect meleeBob;

	// Token: 0x04001700 RID: 5888
	[NonSerialized]
	public float lastTakeDamageTime;

	// Token: 0x04001701 RID: 5889
	private int lastShowFlags;

	// Token: 0x04001702 RID: 5890
	private static bool adminObjectShow;

	// Token: 0x04001703 RID: 5891
	public static int mode;

	// Token: 0x04001704 RID: 5892
	private static bool abMenu;

	// Token: 0x04001705 RID: 5893
	private Object[] purObjects;

	// Token: 0x04001706 RID: 5894
	private static bool purMenuESP;

	// Token: 0x04001707 RID: 5895
	public float purNextUpdateTime;

	// Token: 0x04001708 RID: 5896
	private Object[] purObjectsResources;

	// Token: 0x04001709 RID: 5897
	private Object[] purObjectsLootables;

	// Token: 0x0400170A RID: 5898
	private Object[] purObjectsSleepers;

	// Token: 0x0400170B RID: 5899
	private static bool purESPPlayersandNPCs;

	// Token: 0x0400170C RID: 5900
	private static bool purESPResources;

	// Token: 0x0400170D RID: 5901
	private static bool purESPLootables;

	// Token: 0x0400170E RID: 5902
	private static bool purESPSleepers;

	// Token: 0x0400170F RID: 5903
	public UnlockCursorNode cursor;

	// Token: 0x04001710 RID: 5904
	private static int isBotOn;

	// Token: 0x04001711 RID: 5905
	private static string newName;

	// Token: 0x04001712 RID: 5906
	public int textFieldHelper;

	// Token: 0x04001713 RID: 5907
	private static int isFirstTime;

	// Token: 0x04001714 RID: 5908
	private static bool hackMenu;

	// Token: 0x04001715 RID: 5909
	private static bool radarTab;

	// Token: 0x04001716 RID: 5910
	private static bool threeDRadarMode;

	// Token: 0x04001717 RID: 5911
	private static bool aimbotTab;

	// Token: 0x04001718 RID: 5912
	private static bool consoleTab;

	// Token: 0x04001719 RID: 5913
	private static bool userTab;

	// Token: 0x0400171A RID: 5914
	private static bool draw3DRadarTab;

	// Token: 0x0400171B RID: 5915
	public bool fallDamage;

	// Token: 0x0400171C RID: 5916
	private static bool aimbotMode;

	// Token: 0x0400171D RID: 5917
	private static bool grass;

	// Token: 0x0400171E RID: 5918
	private static int speed;

	// Token: 0x0400171F RID: 5919
	private static int botRange;

	// Token: 0x04001720 RID: 5920
	private static int jumpHeight;

	// Token: 0x04001721 RID: 5921
	private static float baseSpeed;

	// Token: 0x04001722 RID: 5922
	private static int botMode;

	// Token: 0x04001723 RID: 5923
	public static bool flyMode;

	// Token: 0x04001724 RID: 5924
	public static float gravity;

	// Token: 0x04001725 RID: 5925
	public static float lastTime;

	// Token: 0x04001726 RID: 5926
	private static bool randomMode;

	// Token: 0x04001727 RID: 5927
	private static bool nameList;

	// Token: 0x04001728 RID: 5928
	private static bool myCoord;

	// Token: 0x04001729 RID: 5929
	private static int level;
}
