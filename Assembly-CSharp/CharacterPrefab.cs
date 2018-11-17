using System;
using System.Collections.Generic;

// Token: 0x0200010E RID: 270
public class CharacterPrefab : NetMainPrefab
{
	// Token: 0x060006F6 RID: 1782 RVA: 0x0001F660 File Offset: 0x0001D860
	public CharacterPrefab() : this(typeof(Character), false, null, false)
	{
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x0001F678 File Offset: 0x0001D878
	protected CharacterPrefab(Type characterType) : this(characterType, true, null, false)
	{
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x0001F684 File Offset: 0x0001D884
	protected CharacterPrefab(Type characterType, params Type[] requiredIDLocalComponents) : this(characterType, true, requiredIDLocalComponents, requiredIDLocalComponents != null && requiredIDLocalComponents.Length > 0)
	{
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x0001F6A0 File Offset: 0x0001D8A0
	private CharacterPrefab(Type characterType, bool typeCheck, Type[] requiredIDLocalComponents, bool anyRequiredIDLocalComponents) : base(characterType)
	{
		if (typeCheck && !typeof(Character).IsAssignableFrom(characterType))
		{
			throw new ArgumentOutOfRangeException("type", "type must be assignable to Character");
		}
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x0001F6E0 File Offset: 0x0001D8E0
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
