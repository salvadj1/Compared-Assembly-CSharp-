using System;
using UnityEngine;

// Token: 0x020004DB RID: 1243
public class RPOS_Craft_IngredientPlaque : MonoBehaviour
{
	// Token: 0x06002AFA RID: 11002 RVA: 0x0009FB10 File Offset: 0x0009DD10
	public void Bind(global::BlueprintDataBlock.IngredientEntry ingredient, int needAmount, int haveAmount)
	{
		global::ItemDataBlock ingredient2 = ingredient.Ingredient;
		Color color;
		if (needAmount <= haveAmount)
		{
			this.checkIcon.enabled = true;
			this.xIcon.enabled = false;
			color = Color.green;
		}
		else
		{
			this.checkIcon.enabled = false;
			this.xIcon.enabled = true;
			color = Color.red;
		}
		global::UIWidget uiwidget = this.need;
		Color color2 = color;
		this.have.color = color2;
		uiwidget.color = color2;
		this.itemName.text = ingredient2.name;
		this.need.text = needAmount.ToString("N0");
		this.have.text = haveAmount.ToString("N0");
	}

	// Token: 0x040014EB RID: 5355
	[PrefetchChildComponent(NameMask = "ItemName")]
	public global::UILabel itemName;

	// Token: 0x040014EC RID: 5356
	[PrefetchChildComponent(NameMask = "NeedLabel")]
	public global::UILabel need;

	// Token: 0x040014ED RID: 5357
	[PrefetchChildComponent(NameMask = "HaveLabel")]
	public global::UILabel have;

	// Token: 0x040014EE RID: 5358
	[PrefetchChildComponent(NameMask = "checkmark")]
	public global::UISprite checkIcon;

	// Token: 0x040014EF RID: 5359
	[PrefetchChildComponent(NameMask = "xmark")]
	public global::UISprite xIcon;
}
