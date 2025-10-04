using System.Collections.Generic;
using UnityEngine;

public class CardZone : MonoBehaviour
{
    [Tooltip("Distance between each card in world units.")]
    public float spacing = 1.65f;
    [SerializeField]
    bool forcesItemPosition = true;

    // Global registry so cards can find zones to drop into
    public static readonly List<CardZone> All = new List<CardZone>();

    [Header("Assign your 2D collider here")]
    [SerializeField] private Collider2D zoneCollider2D;

    public List<CardObject> items = new List<CardObject>();

    void OnEnable()
    {
        if (!All.Contains(this)) All.Add(this);
    }

    void OnDisable()
    {
        All.Remove(this);
    }

    public bool ContainsPoint(Vector2 worldPos)
    {
        return zoneCollider2D != null && zoneCollider2D.OverlapPoint(worldPos);
    }

    public void AddItem(CardObject item)
    {
        if (item == null) return;

        item.transform.SetParent(transform, worldPositionStays: forcesItemPosition ? false : true);
        if (!items.Contains(item)) items.Add(item);
        item.CurrentZone = this;
        Reorganize();
    }

    public void RemoveItem(CardObject item)
    {
        if (item == null) return;

        if (items.Remove(item))
        {
            item.CurrentZone = null;
            Reorganize();
        }
    }

    public void Reorganize()
    {
        if (!forcesItemPosition) return;
        int count = items.Count;
        if (count == 0) return;

        float startX = -0.5f * (count - 1) * spacing;

        for (int i = 0; i < count; i++)
        {
            float x = startX + i * spacing;
            items[i].transform.localPosition = new Vector3(x, 0f, 0f);
            items[i].transform.localRotation = Quaternion.identity;
        }
    }
}
