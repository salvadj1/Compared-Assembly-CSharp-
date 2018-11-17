using System;

// Token: 0x020000BD RID: 189
public class actor : global::ConsoleSystem
{
	// Token: 0x06000426 RID: 1062 RVA: 0x00014C84 File Offset: 0x00012E84
	private static bool GetCharacterStuff(ref global::ConsoleSystem.Arg args, out global::Character character, out global::CameraMount camera, out global::ItemRepresentation itemRep, out global::ArmorModelRenderer armor)
	{
		character = null;
		itemRep = null;
		armor = null;
		camera = global::CameraMount.current;
		if (!camera)
		{
			args.ReplyWith("Theres no active camera mount.");
			return false;
		}
		character = (IDBase.GetMain(camera) as global::Character);
		if (!character)
		{
			args.ReplyWith("theres no character for the current mounted camera");
			return false;
		}
		armor = character.GetLocal<global::ArmorModelRenderer>();
		global::InventoryHolder local = character.GetLocal<global::InventoryHolder>();
		if (local)
		{
			itemRep = local.itemRepresentation;
		}
		return true;
	}

	// Token: 0x040003A2 RID: 930
	private static float last3rdPersonDistance = 2f;

	// Token: 0x040003A3 RID: 931
	private static float last3rdPersonYaw;

	// Token: 0x040003A4 RID: 932
	private static float last3rdPersonHeight = -0.5f;

	// Token: 0x040003A5 RID: 933
	private static float last3rdPersonOffset;

	// Token: 0x040003A6 RID: 934
	public static bool forceThirdPerson;
}
