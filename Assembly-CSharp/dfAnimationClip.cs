using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000763 RID: 1891
[AddComponentMenu("Daikon Forge/User Interface/Animation Clip")]
[Serializable]
public class dfAnimationClip : MonoBehaviour
{
	// Token: 0x17000BD1 RID: 3025
	// (get) Token: 0x06003E83 RID: 16003 RVA: 0x000E40E4 File Offset: 0x000E22E4
	// (set) Token: 0x06003E84 RID: 16004 RVA: 0x000E40EC File Offset: 0x000E22EC
	public global::dfAtlas Atlas
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

	// Token: 0x17000BD2 RID: 3026
	// (get) Token: 0x06003E85 RID: 16005 RVA: 0x000E40F8 File Offset: 0x000E22F8
	public List<string> Sprites
	{
		get
		{
			return this.sprites;
		}
	}

	// Token: 0x040020EE RID: 8430
	[SerializeField]
	private global::dfAtlas atlas;

	// Token: 0x040020EF RID: 8431
	[SerializeField]
	private List<string> sprites = new List<string>();
}
