using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> screens;
   
    [SerializeField] public MaskMakingProcesses MMP;
    

    // Start is called before the first frame update
    void Start()
    {
        NextScreen(MMP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScreen(MaskMakingProcesses screenProcess)
    {
        if (screenProcess == MMP)
            return;


        MMP = screenProcess;

        foreach (var item in screens)
        {
            item.SetActive(false);
        }

        switch (MMP)
        {
            case MaskMakingProcesses.Chopping:
                ProcessChopping();
                break;
            case MaskMakingProcesses.Mashing:
                ProcessMashing();
                break;
            case MaskMakingProcesses.Mixing:
                ProcessMixing();
                break;
            case MaskMakingProcesses.Tablet:
                ProcessTablet();
                break;
            case MaskMakingProcesses.Making:
                ProcessMaking();
                break;
            case MaskMakingProcesses.Applying:
                ProcessApplying();
                break;
            case MaskMakingProcesses.Complete:
                ProcessComplete();
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

    }
    private void ProcessMixing()
    {
        EnableScreen(1);
    }
    private void ProcessTablet()
    {

    }
    private void ProcessMaking()
    {
        EnableScreen(2);
    }
    private void ProcessApplying()
    {
        EnableScreen(3);
    }
    private void ProcessComplete()
    {

    }

    private void EnableScreen(int num)
    {
        screens[num].SetActive(true);
    }






}
public enum MaskMakingProcesses
{
    Chopping,
    Mashing,
    Mixing,
    Tablet,
    Making,
    Applying,
    Complete
    
}