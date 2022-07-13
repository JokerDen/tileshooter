using System.Collections.Generic;
using UnityEngine;

public class CellRequirement
{
    public enum RequirementType
    {
        Undefined,
        NotBlocked,
        Road,
        Blocks
    }
    
    public RequirementType type = RequirementType.Undefined;
    public Vector3Int pos;

    public CellRequirement(Vector3Int pos)
    {
        this.pos = pos;
    }

    public bool ParsePixel(Color pixel)
    {
        if (pixel.g > pixel.b)
            type = RequirementType.NotBlocked;
        else if (pixel.b > pixel.g)
            type = RequirementType.Road;
        else if (pixel.grayscale == 1f)
            type = RequirementType.Blocks;
        else
            return false;

        return true;
    }

    public bool Fit(CellContent content)
    {
        if (type == RequirementType.Blocks)
            return content is CellBlocks;

        if (type == RequirementType.Road)
            return content is CellRoad;

        if (type == RequirementType.NotBlocked)
            return !(content is CellBlocks) && !(content is CellBuilding) ;

        return true;
    }

    public bool CellRequired()
    {
        return type == RequirementType.Road || type == RequirementType.Blocks;
    }
}

public class RequirementFormat
{
    public List<CellRequirement> reqs = new List<CellRequirement>();
    public Vector3Int buildingCenter;
}

public class BuildingProject : MonoBehaviour
{
    public Texture2D plan;

    public GameBuilding buildingPrefab;
    public Vector3Int buildingOffset;

    // private List<List<CellRequirement>> requirementsRotates = new List<List<CellRequirement>>();
    private RequirementFormat[] reqFormats;

    private void Start()
    {
        var w = plan.width;
        var h = plan.height;

        // buildingOffset.x = w / 2;
        // buildingOffset.z = h / 2;

        reqFormats = new RequirementFormat[4];

        for (int i = 0; i < 4; i++)
        {
            reqFormats[i] = new RequirementFormat();
            reqFormats[i].buildingCenter = Vector3Int.RoundToInt(Quaternion.AngleAxis(i * 90, Vector3.up) * buildingOffset);
            if (i >= 1)
                reqFormats[i].buildingCenter.z += (h - 1);
            if (i >= 2)
                reqFormats[i].buildingCenter.x += (w - 1);
            if (i >= 3)
                reqFormats[i].buildingCenter.z -= (h - 1);
            
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    var pixel = plan.GetPixel(x, y);

                    var req = new CellRequirement(new Vector3Int(x, 0, y));
                    if (req.ParsePixel(pixel))
                        reqFormats[i].reqs.Add(req);
                }
            }

            plan = rotateTexture(plan); 
        }
    }
    
    public Texture2D rotateTexture(Texture2D image )
    {
        Texture2D target = new Texture2D(image.height, image.width, image.format, false);    //flip image width<>height, as we rotated the image, it might be a rect. not a square image
         
        Color32[] pixels = image.GetPixels32(0);
        pixels = rotateTextureGrid(pixels, image.width, image.height);
        target.SetPixels32(pixels);
        target.Apply();
 
        //flip image width<>height, as we rotated the image, it might be a rect. not a square image
 
        return target;
    }
 
    public Color32[] rotateTextureGrid(Color32[] tex, int wid, int hi)
    {
        Color32[] ret = new Color32[wid * hi];      //reminder we are flipping these in the target
 
        for (int y = 0; y < hi; y++)
        {
            for (int x = 0; x < wid; x++)
            {
                // ret[(hi-1)-y + x * hi] = tex[x + y * wid];         //juggle the pixels around
                ret[y + (wid-1-x) * hi] = tex[x + y * wid];         //juggle the pixels around
                 
            }
        }
 
        return ret;
    }

    public GameBuilding TryBuild(GameCell gameCell)
    {
        for (int i = 0; i < reqFormats.Length; i++)
        {
            var reqs = reqFormats[i].reqs;
            foreach (var req in reqs)
            {
                var diff = gameCell.Pos - req.pos;
                if (req.Fit(gameCell.content))
                {
                    if (CheckPlan(reqs, diff, gameCell.Parent))
                    {
                        var pos = diff + reqFormats[i].buildingCenter;
                        
                        var buildingPosCell = gameCell.Parent.GetCell(pos);
                        var rot = Quaternion.Euler(0f, 90f * i, 0f);
                        var building = Instantiate(buildingPrefab, buildingPosCell.transform.position, rot, buildingPosCell.Parent.transform);
                        building.format = reqFormats[i];
                        building.offset = diff;
                        building.gameObject.SetActive(true);

                        foreach (var reqB in reqs)
                            if (reqB.type == CellRequirement.RequirementType.Blocks)
                                gameCell.Parent.GetCell(reqB.pos + diff).SetBuilding(building);
                        
                        return building;
                    }
                }
            }
        }
        
        return null;
    }

    private bool CheckPlan(List<CellRequirement> reqs, Vector3Int diff, GameField field)
    {
        foreach (var req in reqs)
        {
            bool hasCell = false;
            foreach (var cell in field.cells)
                if (cell.Pos.Equals(req.pos + diff))
                {
                    hasCell = true;
                    if (!req.Fit(cell.content))
                        return false;
                }

            if (!hasCell && req.CellRequired())
                return false;
        }
        
        return true;
    }
}
