using System;
using System.Collections.Generic;

// Token: 0x0200012D RID: 301
public class CharacterPrefab : global::NetMainPrefab
{
	// Token: 0x060007C8 RID: 1992 RVA: 0x00022234 File Offset: 0x00020434
	public CharacterPrefab() : this(typeof(global::Character), false, null, false)
	{
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x0002224C File Offset: 0x0002044C
	protected CharacterPrefab(Type characterType) : this(characterType, true, null, false)
	{
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x00022258 File Offset: 0x00020458
	protected CharacterPrefab(Type characterType, params Type[] requiredIDLocalComponents) : this(characterType, true, requiredIDLocalComponents, requiredIDLocalComponents != null && requiredIDLocalComponents.Length > 0)
	{
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x00022274 File Offset: 0x00020474
	private CharacterPrefab(Type characterType, bool typeCheck, Type[] requiredIDLocalComponents, bool anyRequiredIDLocalComponents) : base(characterType)
	{
		if (typeCheck && !typeof(global::Character).IsAssignableFrom(characterType))
		{
			throw new ArgumentOutOfRangeException("type", "type must be assignable to Character");
		}
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x000222B4 File Offset: 0x000204B4
	protected static Type[] TypeArrayAppend(Type[] mustInclude, Type[] given)
	{
		if (mustInclude == null || mustInclude.Length == 0)
		{
			return given;
		}
		if (given == null || given.Length == 0)
		{
			return mustInclude;
		}
		List<Type> list = new List<Type>(given);
		for (int i = 0; i < mustInclude.Length; i++)
		{
			bool flag = false;
			for (int j = 0; j < given.Length; j++)
			{
				if (mustInclude[i].IsAssignableFrom(given[j]))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				list.Add(mustInclude[i]);
			}
		}
		return list.ToArray();
	}
}
