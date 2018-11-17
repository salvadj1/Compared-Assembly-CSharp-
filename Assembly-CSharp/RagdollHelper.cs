using System;
using Facepunch.Actor;
using UnityEngine;

// Token: 0x020001C7 RID: 455
public class RagdollHelper : MonoBehaviour
{
	// Token: 0x06000CDE RID: 3294 RVA: 0x000322D4 File Offset: 0x000304D4
	private void Start()
	{
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x000322D8 File Offset: 0x000304D8
	private void Update()
	{
	}

	// Token: 0x06000CE0 RID: 3296 RVA: 0x000322DC File Offset: 0x000304DC
	private static void _RecursiveLinkTransformsByName(Transform ragdoll, Transform body)
	{
		for (int i = 0; i < ragdoll.childCount; i++)
		{
			Transform childAtIndex = FindChildHelper.GetChildAtIndex(ragdoll, i);
			Transform transform = FindChildHelper.FindChildByName(childAtIndex.name, body);
			if (transform)
			{
				childAtIndex.position = transform.position;
				childAtIndex.rotation = transform.rotation;
			}
			RagdollHelper._RecursiveLinkTransformsByName(childAtIndex, body);
		}
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x00032340 File Offset: 0x00030540
	private static void _RecursiveLinkTransformsByName(Transform ragdoll, Transform body, Transform bodyMatchTransform, ref Transform ragdollMatchTransform, ref bool foundMatch)
	{
		ragdollMatchTransform = null;
		for (int i = 0; i < ragdoll.childCount; i++)
		{
			Transform childAtIndex = FindChildHelper.GetChildAtIndex(ragdoll, i);
			Transform transform = FindChildHelper.FindChildByName(childAtIndex.name, body);
			if (transform)
			{
				childAtIndex.position = transform.position;
				childAtIndex.rotation = transform.rotation;
				if (!foundMatch && transform == bodyMatchTransform)
				{
					foundMatch = true;
					ragdollMatchTransform = childAtIndex;
				}
				if (foundMatch)
				{
					RagdollHelper._RecursiveLinkTransformsByName(childAtIndex, transform);
				}
				else
				{
					RagdollHelper._RecursiveLinkTransformsByName(childAtIndex, transform, bodyMatchTransform, ref ragdollMatchTransform, ref foundMatch);
				}
			}
		}
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x000323DC File Offset: 0x000305DC
	public static void RecursiveLinkTransformsByName(Transform ragdoll, Transform body)
	{
		BoneStructure component = body.GetComponent<BoneStructure>();
		if (component)
		{
			BoneStructure component2 = ragdoll.GetComponent<BoneStructure>();
			if (component2)
			{
				using (BoneStructure.ParentDownOrdered.Enumerator enumerator = component.parentDown.GetEnumerator())
				{
					using (BoneStructure.ParentDownOrdered.Enumerator enumerator2 = component2.parentDown.GetEnumerator())
					{
						while (enumerator.MoveNext() && enumerator2.MoveNext())
						{
							Transform transform = enumerator.Current;
							Transform transform2 = enumerator2.Current;
							transform2.position = transform.position;
							transform2.rotation = transform.rotation;
						}
					}
				}
				return;
			}
		}
		RagdollHelper._RecursiveLinkTransformsByName(ragdoll, body);
	}

	// Token: 0x06000CE3 RID: 3299 RVA: 0x000324D8 File Offset: 0x000306D8
	public static bool RecursiveLinkTransformsByName(Transform ragdoll, Transform body, Transform bodyMatchTransform, out Transform ragdollMatchTransform)
	{
		if (!bodyMatchTransform)
		{
			ragdollMatchTransform = null;
			RagdollHelper.RecursiveLinkTransformsByName(ragdoll, body);
			return false;
		}
		if (body == bodyMatchTransform)
		{
			ragdollMatchTransform = ragdoll;
			RagdollHelper.RecursiveLinkTransformsByName(ragdoll, body);
			return true;
		}
		BoneStructure component = body.GetComponent<BoneStructure>();
		if (component)
		{
			BoneStructure component2 = ragdoll.GetComponent<BoneStructure>();
			if (component2)
			{
				using (BoneStructure.ParentDownOrdered.Enumerator enumerator = component.parentDown.GetEnumerator())
				{
					using (BoneStructure.ParentDownOrdered.Enumerator enumerator2 = component2.parentDown.GetEnumerator())
					{
						while (enumerator.MoveNext() && enumerator2.MoveNext())
						{
							Transform transform = enumerator.Current;
							Transform transform2 = enumerator2.Current;
							transform2.position = transform.position;
							transform2.rotation = transform.rotation;
							if (transform == bodyMatchTransform)
							{
								ragdollMatchTransform = transform2;
								while (enumerator.MoveNext() && enumerator2.MoveNext())
								{
									transform = enumerator.Current;
									transform2 = enumerator2.Current;
									transform2.position = transform.position;
									transform2.rotation = transform.rotation;
								}
								return true;
							}
						}
					}
				}
				ragdollMatchTransform = null;
				return false;
			}
		}
		bool result = false;
		ragdollMatchTransform = null;
		RagdollHelper._RecursiveLinkTransformsByName(ragdoll, body, bodyMatchTransform, ref ragdollMatchTransform, ref result);
		return result;
	}
}
