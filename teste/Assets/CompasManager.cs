using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompasManager : MonoBehaviour
{
    public GameObject pointIcon;
    List<PointMarker> pointMarkers = new List<PointMarker>();
    public List<GameObject> balises = new List<GameObject>();
    public Transform player;
    public RawImage compas;

    public float maxDistance = 200f;

    public float yIcon = 36f;
    
    private float compasUnit;
    

    private void Start()
    {
        compasUnit = compas.rectTransform.rect.width / 360f;
        
        balises.AddRange(GameObject.FindGameObjectsWithTag("Balise"));
        foreach (GameObject balise in balises)
        {
            AddPointMarker(balise.GetComponent<PointMarker>());
        }
        
        
    }


    void Update()
    {
        float uvX = player.localEulerAngles.y / 360f;
        compas.uvRect = new Rect(uvX,0,1,1);

        foreach (PointMarker marker in pointMarkers)
        {
            marker.image.rectTransform.anchoredPosition = GetPosOnCompas(marker);

            float dist = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), marker.position);
            float scale = 0f;

            if (dist < maxDistance)
            {
                scale = 1f - (dist / maxDistance);
            }
            
            marker.image.rectTransform.localScale = Vector3.one * scale;

            if (marker.GetComponent<Balise_Fonctionnement>().isCapture)
            {
                marker.image.enabled = false;
                pointMarkers.Remove(marker);
            }
        }
        
        
        
    }

    public void AddPointMarker(PointMarker marker)
    {
        GameObject newMarker = Instantiate(pointIcon, compas.transform);
        marker.image = newMarker.GetComponent<Image>();
        marker.image.sprite = marker.icon;

        pointMarkers.Add(marker);
    }

    Vector2 GetPosOnCompas(PointMarker marker)
    {
        Vector2 playerPos = new Vector2(player.position.x, player.position.z);
        Vector2 playerFwd = new Vector2(player.forward.x, player.forward.z);
        float angle = Vector2.SignedAngle(marker.position - playerPos, playerFwd);
        
        return new Vector2(compasUnit * angle, yIcon);
    }
}
