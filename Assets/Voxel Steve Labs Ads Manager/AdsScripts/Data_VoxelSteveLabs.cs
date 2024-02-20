using UnityEngine;
public class Data_VoxelSteveLabs : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public static void RewardedAdWatched()
    {
        // Reward User Here
        Debug.Log("Rewarded-Ad Watched");
        GD.Controller.Instance.ActionVideo(true);
    }
}
