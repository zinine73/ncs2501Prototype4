using UnityEditor.PackageManager;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public delegate void MineHandler();
    public static event MineHandler OnMineReady;
    private int catched = 0;
    public void AddCatch()
    {
        catched++;
        if (catched >= 2)
        {
            //GameObject.Find("Player").GetComponent<PlayerController>().MineIsReady();
            OnMineReady();
            Destroy(gameObject);
        }
    }
}
