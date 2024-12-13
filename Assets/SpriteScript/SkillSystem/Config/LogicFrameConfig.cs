/// <summary>
/// 逻辑帧的配置
/// </summary>
public class LogicFrameConfig
{

    /// <summary>
    /// 逻辑帧ID(自增)
    /// </summary>
    public static long LogicFrameId;

    /// <summary>
    /// 实际逻辑帧间隔(默认逻辑帧为1秒15帧(1/15 = 0.0666667))
    /// </summary>
    public const float LogicFrameInterval = 0.066f;

    /// <summary>
    /// 毫秒级的逻辑帧间隔 用来计算当前逻辑帧的累加时间
    /// </summary>
    public const int LogicFrameIntervalMS = (int)(LogicFrameInterval * 1000);

}
