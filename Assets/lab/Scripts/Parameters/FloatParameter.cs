namespace lab {
    /// <summary>
    /// Serializable float parameter. This weird solution of inheriting from generic class is to avoid boiler code.
    /// </summary>
    [System.Serializable]
    public class FloatParameter : ASerializableParameter<string, float> { }
}
