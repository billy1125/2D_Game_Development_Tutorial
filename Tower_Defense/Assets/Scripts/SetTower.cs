using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

[System.Serializable]
public class Towers 
{
    public GameObject TowerPrefab; //砲塔預置物件
    public int Cost; //這個砲塔的耗費是多少？
}

public class SetTower : MonoBehaviour
{
    public Towers[] TowerPrefabList; //砲塔類型
    public GameManager gameManager;
    public GameObject eventSystem;

    string SelectedTower;
    private List<Vector3> TowerLocations = new List<Vector3>(); // 使用List<T>，儲存目前已經有砲塔的位置座標

    // Update is called once per frame
    void Update()
    {
        // 透過EventSystem這個物件得知目前選擇了什麼物件
        if (eventSystem.GetComponent<EventSystem>().currentSelectedGameObject != null)
        {
            SelectedTower = eventSystem.GetComponent<EventSystem>().currentSelectedGameObject.name;
        }        
       
        if (Input.GetMouseButtonDown(0)) //滑鼠點選事件
        {
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition); //取得滑鼠點選遊戲場景中的位置點
            //再將滑鼠點選遊戲場景中的位置點，轉換成為格線系統上的格子位置
            GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
            Vector3Int cellPosition = gridLayout.WorldToCell(pz);
            //格線系統上的格子位置取得之後，因為只是格子的原點位置，所以再把x、y軸再加0.5f，就能獲得格子的中心點位置
            Vector3 towerPosition = gridLayout.CellToWorld(cellPosition) + new Vector3(0.5f, 0.5f, 0);
            //獲得格子的中心點位置後，再確認這個格子上有沒有砲塔設置點的圖樣，有的話再把砲塔放上去
            //if (GetComponent<Tilemap>().GetTile(cellPosition) != null)
            if (GetComponent<Tilemap>().HasTile(cellPosition) && TowerLocations.Contains(towerPosition) == false)
            {                
                setTower(towerPosition);            
            }
        }
    }

    // 設定砲塔的函式
    void setTower(Vector3 _towerPosition)
    {
        switch (SelectedTower)
        {
            case "OneGun": // 如果是選擇單管砲塔，設置單管砲塔
                if (gameManager.Money >= TowerPrefabList[0].Cost)
                    Instantiate(TowerPrefabList[0].TowerPrefab, _towerPosition, this.transform.rotation);
                    gameManager.UpdateMoney(-TowerPrefabList[0].Cost);
                    TowerLocations.Add(_towerPosition);
                break;
            case "TwoGun":  // 如果是選擇雙管砲塔，設置雙管砲塔
                if (gameManager.Money >= TowerPrefabList[1].Cost)
                    Instantiate(TowerPrefabList[1].TowerPrefab, _towerPosition, this.transform.rotation);
                    gameManager.UpdateMoney(-TowerPrefabList[1].Cost);
                    TowerLocations.Add(_towerPosition);
                break;
            case "MissileTower":  // 如果是選擇飛彈砲塔，設置飛彈砲塔
                if (gameManager.Money >= TowerPrefabList[2].Cost)
                    Instantiate(TowerPrefabList[2].TowerPrefab, _towerPosition, this.transform.rotation);
                    gameManager.UpdateMoney(-TowerPrefabList[2].Cost);
                    TowerLocations.Add(_towerPosition);
                break;
            default:
                break;
        }

        SelectedTower = ""; // 重新設定目前的選擇
    }
    
    // 砲塔被摧毀時，用來回報自己位置
    public void TowerIsDestroy(Vector3 _location)
    {
        // 搜尋目前砲塔已儲存的位置中，被摧毀砲塔位置的資料索引值
        int removeIndex = TowerLocations.FindIndex(towers => towers.Equals(_location)); 
        TowerLocations.RemoveAt(removeIndex); //刪除被摧毀砲塔的位置資料
    }

    //void getAllTiles()
    //{
    //    Tilemap tilemap = GetComponent<Tilemap>();

    //    foreach (var pos in tilemap.cellBounds.allPositionsWithin)
    //    {
    //        Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
    //        Vector3 place = tilemap.CellToWorld(localPlace);
    //        if (tilemap.HasTile(localPlace))
    //        {
    //            tileWorldLocations.Add(place);
    //        }
    //    }
    //}
}
