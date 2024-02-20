using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitch_VoxelSteveLabs : MonoBehaviour
{
    public int TimeDelay;
    public string SceneName;
    void Start()
    {
        Invoke("Switch", TimeDelay);
    }
    void Switch()
    {
        SceneManager.LoadScene(SceneName);
    }
}
