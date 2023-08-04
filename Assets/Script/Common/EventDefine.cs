public enum EventDefine
{
    // load scene
    LoadBattleField,
    LoadMap,

    // Summon function
    Summon,
    SummonFinished,
    SummonCopy,

    // Resource system
    ResourceNum,  //set different types of resource Num

    // Panels
    TownPanel,
    TaskPanel,
    onShowDecButtonClicked,
    UpgradePanelOpen,
    CloseUpgradePanel,

    // EnemeyManager
    moraleChange,

    //Wizard in battle field
    WizardHurt,

    // For UI Test

    //Hat movement in the Task Goal Panel
    HatInPanelMove,
    HatInPanelMoveFinished,
    //reach different event point
    ReachEndPoint,
    ReachBattlePoint, 
    //startFromNewTown
    StartFromNewTown,
    //TaskManager
    CheckTaskIsFinish,
    FinishTask,

    // Scene Load issues
    ReStartScene,

    // Hat on map
    HatStartMove,
    HatContinueMove,
    HatFinishMove,

    // Level Manager
    ThreatUp,
    ClockDown,
    Win,
    BossLevel,

    //EventPanel
    EventPanel,
    //generate smallSmile
    GenerateSmallSmile,

}
