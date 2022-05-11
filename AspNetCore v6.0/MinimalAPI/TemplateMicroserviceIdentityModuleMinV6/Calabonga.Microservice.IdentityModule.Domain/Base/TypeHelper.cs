namespace $safeprojectname$.Base;

/// <summary>
/// Some helpful methods for Type manipulations
/// </summary>
public static class TypeHelper
{
    /// <summary>
    /// Check type before converting
    /// </summary>
    /// <param name="value"></param>
    /// <param name="conversionType"></param>
    /// <returns></returns>
    public static bool CanChangeType(object value, Type conversionType)
    {
        if (conversionType == null)
        {
            return false;
        }
        return value is IConvertible;
    }
}