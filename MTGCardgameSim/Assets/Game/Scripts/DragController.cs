using UnityEngine;

public class DragController2D : MonoBehaviour
{
    [SerializeField] private LayerMask cardsLayer;

    private Camera cam;
    private CardObject selected;
    private CardZone lastZone;
    private Vector3 lastLocalPos;
    private Vector3 grabOffsetWorld;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) TryBeginDrag();
        if (Input.GetMouseButton(0)) Drag();
        if (Input.GetMouseButtonUp(0)) EndDrag();
    }

    void TryBeginDrag()
    {
        if (cam == null) return;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 p = mouseWorld;

        // pick ONLY cards
        Collider2D hit = Physics2D.OverlapPoint(p, cardsLayer);
        if (hit == null) return;

        selected = hit.GetComponentInParent<CardObject>();
        if (selected == null) return;

        // remember origin to snap back if needed
        selected.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
        lastZone = selected.CurrentZone;
        lastLocalPos = selected.transform.localPosition;

        // detach from zone immediately
        if (lastZone != null) lastZone.RemoveItem(selected);

        // keep offset so grabbing isn’t jumpy
        mouseWorld.z = selected.transform.position.z;
        grabOffsetWorld = selected.transform.position - mouseWorld;
    }

    void Drag()
    {
        if (selected == null || cam == null) return;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = selected.transform.position.z;
        selected.transform.position = mouseWorld + grabOffsetWorld;
    }

    void EndDrag()
    {
        if (selected == null) return;

        // find first zone under card
        Vector2 p = selected.transform.position;
        selected.transform.localScale = Vector3.one;
        CardZone target = null;
        foreach (var zone in CardZone.All)
        {
            if (zone != null && zone.ContainsPoint(p)) { target = zone; break; }
        }

        if (target != null)
        {
            target.AddItem(selected);
        }
        else if (lastZone != null)
        {
            lastZone.AddItem(selected);
            selected.transform.localPosition = lastLocalPos;
            lastZone.Reorganize();
        }
        // else: leave where dropped (no zone)

        selected = null;
    }
}
