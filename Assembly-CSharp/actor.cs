using System;

// Token: 0x020000AA RID: 170
public class actor : ConsoleSystem
{
	// Token: 0x060003AE RID: 942 RVA: 0x00013494 File Offset: 0x00011694
	private static bool GetCharacterStuff(ref ConsoleSystem.Arg args, out Character character, out CameraMount camera, out ItemRepresentation itemRep, out ArmorModelRenderer armor)
	{
		character = null;
		itemRep = null;
		armor = null;
		camera = CameraMount.current;
		if (!camera)
		{
			args.ReplyWith("Theres no active camera mount.");
			return false;
		}
		character = (IDBase.GetMain(camera) as Character);
		if (!character)
		{
			args.ReplyWith("theres no character for the current mounted camera");
			return false;
		}
		armor = character.GetLocal<ArmorModelRenderer>();
		InventoryHolder local = character.GetLocal<InventoryHolder>();
		if (local)
		{
			itemRep = local.itemRepresentation;
		}
		return true;
	}

	// Token: 0x04000337 RID: 823
	private static float last3rdPersonDistance = 2f;

	// Token: 0x04000338 RID: 824
	private static float last3rdPersonYaw;

	// Token: 0x04000339 RID: 825
	private static float last3rdPersonHeight = -0.5f;

	// Token: 0x0400033A RID: 826
	private static float last3rdPersonOffset;

	// Token: 0x0400033B RID: 827
	public static bool forceThirdPerson;
}
