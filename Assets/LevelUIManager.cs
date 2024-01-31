using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelUIManager : MonoBehaviour
{

    public static LevelUIManager Instance;
    [SerializeField] private List<GameObject> screens;
    
    [SerializeField] public Mask_Making_Level_Methods MMP;
    public Button next;
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        NextScreen(MMP); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScreen(Mask_Making_Level_Methods screenProcess)
    {
      
      


        MMP = screenProcess;

        foreach (var item in screens)
        {
            item.SetActive(false);
        }

      

        switch (screenProcess)
        {
            case Mask_Making_Level_Methods.Chopping:
                ProcessChopping();
                break;
            case Mask_Making_Level_Methods.MoveToCrushing:
                break;
            case Mask_Making_Level_Methods.Crushing:
                ProcessMashing();
                break;
            case Mask_Making_Level_Methods.Pouring:
                ProcessPouring();
                break;
            case Mask_Making_Level_Methods.Mixing:
                ProcessMashing();
                break;
            case Mask_Making_Level_Methods.Injecting:
                ProcessTablet();
                break;
            case Mask_Making_Level_Methods.Mask_Making:
                ProcessMaking();
                break;
            case Mask_Making_Level_Methods.Mask_Applying:
                ProcessApplying();
                break;
            default:
                break;
        }



    }
    
    private void ProcessChopping()
    {
        EnableScreen(0);
    }

    private void ProcessMashing()
    {
        EnableScreen(1);
    }
    private void ProcessPouring()
    {
        EnableScreen(2);
    }
    private void ProcessMixing()
    {
        EnableScreen(2);
    }
    private void ProcessTablet()
    {
        EnableScreen(3);
    }
    private void ProcessMaking()
    {
        EnableScreen(4);
    }
    private void ProcessApplying()
    {
        EnableScreen(5);
    }
    private void ProcessComplete()
    {
        EnableScreen(6);
    }

    private void EnableScreen(int num)
    {
        screens[num].SetActive(true);
    }






}
