using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200069D RID: 1693
[AddComponentMenu("Daikon Forge/User Interface/Animation Clip")]
[Serializable]
public class dfAnimationClip : MonoBehaviour
{
	// Token: 0x17000B4D RID: 2893
	// (get) Token: 0x06003A85 RID: 14981 RVA: 0x000DB654 File Offset: 0x000D9854
	// (set) Token: 0x06003A86 RID: 14982 RVA: 0x000DB65C File Offset: 0x000D985C
	public dfAtlas Atlas
	{
		get
		{
			return this.atlas;
		}
		set
		{
			this.atlas = value;
		}
	}

	// Token: 0x17000B4E RID: 2894
	// (get) Token: 0x06003A87 RID: 14983 RVA: 0x000DB668 File Offset: 0x000D9868
	public List<string> Sprites
	{
		get
		{
			return this.sprites;
		}
	}

	// Token: 0x04001EF2 RID: 7922
	[SerializeField]
	private dfAtlas atlas;

	// Token: 0x04001EF3 RID: 7923
	[SerializeField]
	private List<string> sprites = new List<string>();
}
